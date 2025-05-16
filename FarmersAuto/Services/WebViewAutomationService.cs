using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InsuranceAutomation.Models;
using InsuranceAutomation.Services.Interfaces;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace InsuranceAutomation.Services
{
    /// <summary>
    /// Service for executing automation steps using WebView2.
    /// </summary>
    public class WebViewAutomationService : IAutomationService
    {
        private readonly WebView2 webView;
        private List<AutomationStep> steps;
        private int currentStepIndex = -1;
        private bool isPaused;
        private bool isRunning;
        private CancellationTokenSource cancellationTokenSource;

        /// <inheritdoc/>
        public event EventHandler<AutomationStepEventArgs> StepStarted;
        
        /// <inheritdoc/>
        public event EventHandler<AutomationStepEventArgs> StepCompleted;
        
        /// <inheritdoc/>
        public event EventHandler<AutomationErrorEventArgs> AutomationError;
        
        /// <inheritdoc/>
        public event EventHandler<LogEventArgs> LogMessageGenerated;

        /// <inheritdoc/>
        public bool IsRunning => isRunning;

        /// <inheritdoc/>
        public bool IsPaused => isPaused;

        /// <inheritdoc/>
        public int CurrentStepIndex => currentStepIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebViewAutomationService"/> class.
        /// </summary>
        /// <param name="webView">The WebView2 control to use for automation.</param>
        public WebViewAutomationService(WebView2 webView)
        {
            this.webView = webView ?? throw new ArgumentNullException(nameof(webView));
            this.steps = new List<AutomationStep>();
        }

        /// <inheritdoc/>
        public void SetSteps(IEnumerable<AutomationStep> steps)
        {
            this.steps = steps?.ToList() ?? throw new ArgumentNullException(nameof(steps));
            Log($"Loaded {this.steps.Count} automation steps.");
        }

        /// <inheritdoc/>
        public async Task NavigateAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL cannot be empty.", nameof(url));
            }

            try
            {
                // Ensure WebView2 is initialized
                if (webView.CoreWebView2 == null)
                {
                    await webView.EnsureCoreWebView2Async(null);
                }

                Log($"Navigating to: {url}");
                webView.CoreWebView2.Navigate(url);
            }
            catch (Exception ex)
            {
                Log($"Navigation error: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            if (steps == null || !steps.Any())
            {
                throw new InvalidOperationException("No automation steps defined. Please load a template first.");
            }

            if (isRunning)
            {
                throw new InvalidOperationException("Automation is already running.");
            }

            try
            {
                isRunning = true;
                isPaused = false;
                currentStepIndex = -1;
                cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

                Log("Starting automation sequence...");

                for (int i = 0; i < steps.Count; i++)
                {
                    while (isPaused && !cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        await Task.Delay(100, cancellationTokenSource.Token);
                    }

                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    
                    await ExecuteStepInternalAsync(i, cancellationTokenSource.Token);
                }
                
                Log("Automation completed successfully.");
            }
            catch (OperationCanceledException)
            {
                Log("Automation was cancelled.");
                throw;
            }
            catch (Exception ex)
            {
                Log($"Error during automation: {ex.Message}");
                throw;
            }
            finally
            {
                isRunning = false;
                isPaused = false;
            }
        }

        /// <inheritdoc/>
        public void Pause()
        {
            if (isRunning && !isPaused)
            {
                isPaused = true;
                Log("Automation paused.");
            }
        }

        /// <inheritdoc/>
        public void Resume()
        {
            if (isRunning && isPaused)
            {
                isPaused = false;
                Log("Automation resumed.");
            }
        }

        /// <inheritdoc/>
        public async Task StepAsync(CancellationToken cancellationToken)
        {
            if (steps == null || !steps.Any())
            {
                throw new InvalidOperationException("No automation steps defined. Please load a template first.");
            }

            int nextStepIndex = currentStepIndex + 1;
            if (nextStepIndex >= steps.Count)
            {
                Log("Reached the end of automation steps.");
                return;
            }

            try
            {
                await ExecuteStepInternalAsync(nextStepIndex, cancellationToken);
            }
            catch (Exception ex)
            {
                Log($"Error executing step: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public void Reset()
        {
            if (isRunning)
            {
                cancellationTokenSource?.Cancel();
            }

            isRunning = false;
            isPaused = false;
            currentStepIndex = -1;
            Log("Automation reset.");
        }

        private async Task ExecuteStepInternalAsync(int stepIndex, CancellationToken cancellationToken)
        {
            if (stepIndex < 0 || stepIndex >= steps.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(stepIndex));
            }

            currentStepIndex = stepIndex;
            AutomationStep step = steps[stepIndex];
            
            try
            {
                // Raise event before executing step
                StepStarted?.Invoke(this, new AutomationStepEventArgs(stepIndex, step));
                
                Log($"Executing step {stepIndex + 1}: {step.GetDisplayText()}");
                
                switch (step.Type)
                {
                    case "fill_form":
                        await FillFormAsync(step.Selector, step.Value, cancellationToken);
                        break;
                    case "click_button":
                        await ClickButtonAsync(step.Selector, cancellationToken);
                        break;
                    case "wait_for_element":
                        await WaitForElementAsync(step.Selector, cancellationToken);
                        break;
                    case "execute_script":
                        await ExecuteScriptAsync(step.Script, cancellationToken);
                        break;
                    default:
                        Log($"Unknown step type: {step.Type}");
                        break;
                }
                
                // Add a small delay between steps for stability
                await Task.Delay(500, cancellationToken);
                
                // Raise event after executing step
                StepCompleted?.Invoke(this, new AutomationStepEventArgs(stepIndex, step));
            }
            catch (Exception ex)
            {
                Log($"Error executing step {stepIndex + 1}: {ex.Message}");
                AutomationError?.Invoke(this, new AutomationErrorEventArgs(stepIndex, step, ex));
                throw;
            }
        }

        private async Task FillFormAsync(string selector, string value, CancellationToken cancellationToken)
        {
            string script = $@"
                (function() {{
                    const element = document.querySelector('{EscapeJsString(selector)}');
                    if (!element) {{
                        return {{ success: false, error: 'Element not found' }};
                    }}
                    
                    try {{
                        element.value = '{EscapeJsString(value)}';
                        
                        // Trigger input event to make sure that any listeners are notified
                        const event = new Event('input', {{ bubbles: true }});
                        element.dispatchEvent(event);
                        
                        // Trigger change event as well
                        const changeEvent = new Event('change', {{ bubbles: true }});
                        element.dispatchEvent(changeEvent);
                        
                        return {{ success: true }};
                    }} catch (error) {{
                        return {{ success: false, error: error.toString() }};
                    }}
                }})();
            ";

            var result = await webView.CoreWebView2.ExecuteScriptAsync(script);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response.success == false)
            {
                string error = response.error;
                Log($"Failed to fill form field '{selector}': {error}");
                throw new InvalidOperationException($"Failed to fill form field '{selector}': {error}");
            }

            Log($"Filled form field '{selector}' with value '{value}'.");
        }

        private async Task ClickButtonAsync(string selector, CancellationToken cancellationToken)
        {
            string script = $@"
                (function() {{
                    const element = document.querySelector('{EscapeJsString(selector)}');
                    if (!element) {{
                        return {{ success: false, error: 'Element not found' }};
                    }}
                    
                    try {{
                        element.click();
                        return {{ success: true }};
                    }} catch (error) {{
                        return {{ success: false, error: error.toString() }};
                    }}
                }})();
            ";

            var result = await webView.CoreWebView2.ExecuteScriptAsync(script);
            var response = JsonConvert.DeserializeObject<dynamic>(result);

            if (response.success == false)
            {
                string error = response.error;
                Log($"Failed to click button '{selector}': {error}");
                throw new InvalidOperationException($"Failed to click button '{selector}': {error}");
            }

            Log($"Clicked button '{selector}'.");
        }

        private async Task WaitForElementAsync(string selector, CancellationToken cancellationToken)
        {
            Log($"Waiting for element '{selector}' to appear...");
            
            string script = $@"
                (function() {{
                    const element = document.querySelector('{EscapeJsString(selector)}');
                    return !!element;
                }})();
            ";

            DateTime startTime = DateTime.Now;
            TimeSpan timeout = TimeSpan.FromSeconds(30); // 30 seconds timeout
            
            while (DateTime.Now - startTime < timeout)
            {
                cancellationToken.ThrowIfCancellationRequested();
                
                var result = await webView.CoreWebView2.ExecuteScriptAsync(script);
                if (result.Equals("true"))
                {
                    Log($"Element '{selector}' found.");
                    return;
                }
                
                await Task.Delay(500, cancellationToken); // Check every 500ms
            }
            
            string errorMessage = $"Timed out waiting for element '{selector}' after {timeout.TotalSeconds} seconds.";
            Log(errorMessage);
            throw new TimeoutException(errorMessage);
        }

        private async Task ExecuteScriptAsync(string script, CancellationToken cancellationToken)
        {
            try
            {
                var result = await webView.CoreWebView2.ExecuteScriptAsync(script);
                Log($"Script executed successfully. Result: {result}");
            }
            catch (Exception ex)
            {
                Log($"Error executing script: {ex.Message}");
                throw;
            }
        }

        private string EscapeJsString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
                
            return input
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
        }

        private void Log(string message)
        {
            LogMessageGenerated?.Invoke(this, new LogEventArgs(message));
        }
    }
}
