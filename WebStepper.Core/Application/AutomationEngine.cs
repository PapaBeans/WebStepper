using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;

namespace WebStepper.Core.Application
{
    /// <summary>
    /// Automation engine that executes templates
    /// </summary>
    public class AutomationEngine : IAutomationEngine
    {
        private readonly ILogService _logService;
        private IWebView2Bridge _webView2Bridge;
        private readonly IPageTrackerService _pageTrackerService;

        private Template _currentTemplate;
        private Page _currentPage;
        private Step _currentStep;
        private bool _isPaused;
        private bool _isRunning;
        private bool _isStepping;
        private TaskCompletionSource<bool> _pauseCompletionSource;

        /// <inheritdoc/>
        public event EventHandler<StepExecutionEventArgs> StepExecutionStarted;

        /// <inheritdoc/>
        public event EventHandler<StepExecutionEventArgs> StepExecutionCompleted;

        /// <inheritdoc/>
        public event EventHandler<PageChangeEventArgs> PageChanged;

        /// <inheritdoc/>
        public event EventHandler<AutomationStatusEventArgs> StatusChanged;

        public AutomationEngine(ILogService logService, IPageTrackerService pageTrackerService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _pageTrackerService = pageTrackerService ?? throw new ArgumentNullException(nameof(pageTrackerService));
        }

        public void Initialize(IWebView2Bridge webView2Bridge)
        {
            _webView2Bridge = webView2Bridge ?? throw new ArgumentNullException(nameof(webView2Bridge));
        }

        /// <inheritdoc/>
        public async Task StartAutomation(Template template, Step startingStep = null)
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot activate Start Automation.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }

            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (_isRunning && !_isPaused)
            {
                _logService.LogWarning("Automation is already running");
                return;
            }

            _currentTemplate = template;
            _isRunning = true;
            _isPaused = false;
            _isStepping = false;

            // Create a new pause completion source
            _pauseCompletionSource = new TaskCompletionSource<bool>();

            StatusChanged?.Invoke(this, new AutomationStatusEventArgs { IsRunning = true, IsPaused = false });
            _logService.LogInfo($"Starting automation for template: {template.Name}");

            // Find starting page and step
            if (startingStep != null)
            {
                // Find the page containing the step
                foreach (var page in template.Pages)
                {
                    var step = page.Steps.FirstOrDefault(s => s.Id == startingStep.Id);
                    if (step != null)
                    {
                        _currentPage = page;
                        _currentStep = step;
                        break;
                    }
                }
            }
            else
            {
                // Start from the first page and step
                _currentPage = template.Pages.FirstOrDefault();
                _currentStep = _currentPage?.Steps.FirstOrDefault();
            }

            if (_currentPage == null || _currentStep == null)
            {
                _logService.LogError("Unable to find starting page or step");
                StopAutomation();
                return;
            }

            _logService.LogInfo($"Starting on page '{_currentPage.Name}' at step '{_currentStep.Name}'");

            await ExecuteAutomation();
        }

        /// <inheritdoc/>
        public void PauseAutomation()
        {
            if (_isRunning && !_isPaused)
            {
                _isPaused = true;
                StatusChanged?.Invoke(this, new AutomationStatusEventArgs { IsRunning = true, IsPaused = true });
                _logService.LogInfo("Automation paused");
            }
        }

        /// <inheritdoc/>
        public async Task ResumeAutomation()
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot activate Resume Automation.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }

            if (_isRunning && _isPaused)
            {
                _isPaused = false;
                StatusChanged?.Invoke(this, new AutomationStatusEventArgs { IsRunning = true, IsPaused = false });
                _logService.LogInfo("Automation resumed");

                // Complete the pause task to resume execution
                var oldCompletionSource = _pauseCompletionSource;
                _pauseCompletionSource = new TaskCompletionSource<bool>();
                oldCompletionSource.SetResult(true);

                if (!_isStepping)
                {
                    await ExecuteAutomation();
                }
            }
        }

        /// <inheritdoc/>
        public void StopAutomation()
        {
            if (_isRunning)
            {
                _isRunning = false;
                _isPaused = false;
                _isStepping = false;

                // Complete any pending pause tasks
                _pauseCompletionSource.TrySetResult(false);

                StatusChanged?.Invoke(this, new AutomationStatusEventArgs { IsRunning = false, IsPaused = false });
                _logService.LogInfo("Automation stopped");
            }
        }

        /// <inheritdoc/>
        public async Task ExecuteStep()
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot activate ExecuteStep.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }

            if (_currentStep == null)
            {
                _logService.LogWarning("No current step to execute");
                return;
            }

            _isStepping = true;

            if (!_isRunning)
            {
                _isRunning = true;
                _isPaused = false;
                StatusChanged?.Invoke(this, new AutomationStatusEventArgs { IsRunning = true, IsPaused = false });
            }

            // Execute the current step
            await ExecuteSingleStep(_currentPage, _currentStep);

            // After execution, move to the next step
            MoveToNextStep();

            // Pause after executing a single step
            _isPaused = true;
            _isStepping = false;
            StatusChanged?.Invoke(this, new AutomationStatusEventArgs { IsRunning = true, IsPaused = true });
            _logService.LogInfo("Step execution completed, automation paused");
        }

        private async Task ExecuteAutomation()
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot activate ExecuteAutomation.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }
            while (_isRunning)
            {
                // Check if paused
                if (_isPaused)
                {
                    _logService.LogInfo("Automation paused, waiting to resume");
                    await _pauseCompletionSource.Task;

                    // If we've been instructed to stop, exit the loop
                    if (!_isRunning)
                    {
                        break;
                    }

                    _logService.LogInfo("Automation resumed");
                }

                // Validate current page
                bool isOnCorrectPage = await _pageTrackerService.ValidatePage(_currentPage);
                if (!isOnCorrectPage)
                {
                    _logService.LogWarning($"Not on expected page: {_currentPage.Name}");

                    // Try to detect the current page
                    var detectedPage = await _pageTrackerService.DetectCurrentPage(_currentTemplate);
                    if (detectedPage != null)
                    {
                        _logService.LogInfo($"Detected page: {detectedPage.Name}");
                        _currentPage = detectedPage;
                        _currentStep = detectedPage.Steps.FirstOrDefault();
                        PageChanged?.Invoke(this, new PageChangeEventArgs { NewPage = detectedPage });
                    }
                    else
                    {
                        _logService.LogError("Unable to detect current page, pausing automation");
                        PauseAutomation();
                        continue;
                    }
                }

                // Execute the current step
                await ExecuteSingleStep(_currentPage, _currentStep);

                // If we've stopped or paused during step execution, exit the loop
                if (!_isRunning || _isPaused)
                {
                    break;
                }

                // Move to the next step
                if (!MoveToNextStep())
                {
                    // End of template reached
                    _logService.LogInfo("Template execution completed");
                    StopAutomation();
                    break;
                }
            }
        }

        private bool MoveToNextStep()
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot MoveToNextStep.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }

            if (_currentPage == null || _currentStep == null)
            {
                return false;
            }

            // Get the index of the current step
            int currentStepIndex = _currentPage.Steps.IndexOf(_currentStep);

            // Check if there are more steps in the current page
            if (currentStepIndex < _currentPage.Steps.Count - 1)
            {
                // Move to the next step in the current page
                _currentStep = _currentPage.Steps[currentStepIndex + 1];
                return true;
            }
            else
            {
                // Move to the next page
                int currentPageIndex = _currentTemplate.Pages.IndexOf(_currentPage);
                if (currentPageIndex < _currentTemplate.Pages.Count - 1)
                {
                    // Move to the first step of the next page
                    _currentPage = _currentTemplate.Pages[currentPageIndex + 1];
                    _currentStep = _currentPage.Steps.FirstOrDefault();

                    // Notify page change
                    PageChanged?.Invoke(this, new PageChangeEventArgs { NewPage = _currentPage });

                    return _currentStep != null; // Return true if the next page has steps
                }
                else
                {
                    // End of template reached
                    return false;
                }
            }
        }

        private async Task ExecuteSingleStep(Page page, Step step)
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot ExecuteSingleStep.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }

            if (page == null || step == null)
            {
                throw new ArgumentNullException(page == null ? nameof(page) : nameof(step));
            }

            try
            {
                _logService.LogInfo($"Executing step: {step.Name}");
                StepExecutionStarted?.Invoke(this, new StepExecutionEventArgs { Page = page, Step = step });

                // Wait before execution if specified
                if (step.WaitBeforeMs > 0)
                {
                    _logService.LogInfo($"Waiting {step.WaitBeforeMs}ms before execution");
                    await Task.Delay(step.WaitBeforeMs);
                }

                // Execute step based on type
                switch (step.Type)
                {
                    case StepType.WaitForElement:
                        await ExecuteWaitForElement(step);
                        break;
                    case StepType.FillForm:
                        await ExecuteFillForm(step);
                        break;
                    case StepType.ClickButton:
                        await ExecuteClickButton(step);
                        break;
                    case StepType.ExecuteScript:
                        await ExecuteScript(step);
                        break;
                    default:
                        _logService.LogError($"Unsupported step type: {step.Type}");
                        throw new NotSupportedException($"Unsupported step type: {step.Type}");
                }

                // Wait after execution if specified
                if (step.WaitAfterMs > 0)
                {
                    _logService.LogInfo($"Waiting {step.WaitAfterMs}ms after execution");
                    await Task.Delay(step.WaitAfterMs);
                }

                StepExecutionCompleted?.Invoke(this, new StepExecutionEventArgs { Page = page, Step = step, Success = true });
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error executing step: {step.Name} - {ex.Message}");
                StepExecutionCompleted?.Invoke(this, new StepExecutionEventArgs { Page = page, Step = step, Success = false, ErrorMessage = ex.Message });
                PauseAutomation();
            }
        }

        private async Task ExecuteWaitForElement(Step step)
        {
            int waitTime = step.MaxWaitMs > 0 ? step.MaxWaitMs : 5000; // Default to 5 seconds if not specified
            _logService.LogInfo($"Waiting for element: {step.Selector} (timeout: {waitTime}ms)");

            bool elementFound = await _webView2Bridge.WaitForElement(step.Selector, waitTime);

            if (!elementFound)
            {
                _logService.LogError($"Element not found: {step.Selector} after waiting {waitTime}ms");
                throw new Exception($"Element not found: {step.Selector} after waiting {waitTime}ms");
            }

            _logService.LogInfo($"Element found: {step.Selector}");
        }

        private async Task ExecuteFillForm(Step step)
        {
            // Process variables in the value
            string processedValue = ProcessVariables(step.Value);
            _logService.LogInfo($"Filling form element: {step.Selector} with value: {processedValue}");

            await _webView2Bridge.FillFormElement(step.Selector, processedValue);
            _logService.LogInfo($"Form element filled: {step.Selector}");
        }

        private async Task ExecuteClickButton(Step step)
        {
            _logService.LogInfo($"Clicking element: {step.Selector}");
            await _webView2Bridge.ClickElement(step.Selector);
            _logService.LogInfo($"Element clicked: {step.Selector}");
        }

        private async Task ExecuteScript(Step step)
        {
            // Process variables in the script
            string processedScript = ProcessVariables(step.Value);
            _logService.LogInfo("Executing script");

            string result = await _webView2Bridge.ExecuteScript(processedScript);
            _logService.LogInfo($"Script executed, result: {result}");
        }

        private string ProcessVariables(string value)
        {
            // Replace variables in format {VariableName} with their values
            if (string.IsNullOrEmpty(value) || _currentTemplate == null || _currentTemplate.Variables == null)
            {
                return value;
            }

            string processedValue = value;

            foreach (var variable in _currentTemplate.Variables)
            {
                processedValue = processedValue.Replace($"{{{variable.Key}}}", variable.Value);
            }

            return processedValue;
        }
    }
}
