using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InsuranceAutomation.Services
{
    /// <summary>
    /// Service for picking elements from web pages in WebView2.
    /// </summary>
    public class ElementPickerService
    {
        private static ElementPickerService instance;
        private WebView2 webView;
        private bool isPickingActive = false;
        private bool isInitialized = false;

        /// <summary>
        /// Event raised when an element is selected.
        /// </summary>
        public event EventHandler<ElementSelectedEventArgs> ElementSelected;

        /// <summary>
        /// Event raised when element picking is canceled.
        /// </summary>
        public event EventHandler PickingCanceled;

        /// <summary>
        /// Gets the singleton instance of the ElementPickerService.
        /// </summary>
        public static ElementPickerService Instance => instance ?? (instance = new ElementPickerService());

        /// <summary>
        /// Gets a value indicating whether element picking is currently active.
        /// </summary>
        public bool IsPickingActive => isPickingActive;

        /// <summary>
        /// Private constructor to enforce singleton pattern.
        /// </summary>
        private ElementPickerService()
        {
        }

        /// <summary>
        /// Initializes the element picker with a WebView2 instance.
        /// </summary>
        /// <param name="webView">The WebView2 control to use for element picking.</param>
        public async Task InitializeAsync(WebView2 webView)
        {
            this.webView = webView ?? throw new ArgumentNullException(nameof(webView));

            if (webView.CoreWebView2 == null)
            {
                throw new InvalidOperationException("WebView2 must be initialized before using ElementPickerService");
            }

            // Inject the JavaScript code if not already injected
            if (!isInitialized)
            {
                // Add the JavaScript callback function
                await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(@"
                    function onElementSelected(selectorInfo) {
                        window.chrome.webview.postMessage(selectorInfo);
                    }
                ");

                // Load and inject the ElementPicker.js script
                string scriptContent = GetElementPickerScript();
                await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(scriptContent);

                // Set up message handler for responses from JavaScript
                webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

                isInitialized = true;
            }
        }

        /// <summary>
        /// Starts the element picking process.
        /// </summary>
        public async Task StartPickingAsync()
        {
            if (webView == null || webView.CoreWebView2 == null)
            {
                throw new InvalidOperationException("WebView2 is not initialized. Call InitializeAsync first.");
            }

            if (isPickingActive)
            {
                return; // Already picking
            }

            isPickingActive = true;

            // First, initialize the element picker
            await webView.CoreWebView2.ExecuteScriptAsync("ElementPicker.initialize('onElementSelected')");
            
            // Then start the picker
            await webView.CoreWebView2.ExecuteScriptAsync("ElementPicker.start()");
        }

        /// <summary>
        /// Stops the element picking process.
        /// </summary>
        public async Task StopPickingAsync()
        {
            if (webView == null || webView.CoreWebView2 == null || !isPickingActive)
            {
                return;
            }

            isPickingActive = false;
            await webView.CoreWebView2.ExecuteScriptAsync("ElementPicker.stop()");
        }

        /// <summary>
        /// Tests a CSS selector and returns information about matching elements.
        /// </summary>
        /// <param name="selector">The CSS selector to test.</param>
        /// <returns>Information about the selector test results.</returns>
        public async Task<SelectorTestResult> TestSelectorAsync(string selector)
        {
            if (webView == null || webView.CoreWebView2 == null)
            {
                throw new InvalidOperationException("WebView2 is not initialized. Call InitializeAsync first.");
            }

            if (string.IsNullOrWhiteSpace(selector))
            {
                return new SelectorTestResult
                {
                    IsValid = false,
                    Count = 0,
                    Message = "Selector cannot be empty."
                };
            }

            // Escape the selector string for JavaScript
            string escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n");
            
            // Execute the test
            string result = await webView.CoreWebView2.ExecuteScriptAsync($"JSON.stringify(ElementPicker.testSelector('{escapedSelector}'))");
            
            // Parse the result
            try
            {
                return JsonConvert.DeserializeObject<SelectorTestResult>(result);
            }
            catch (Exception ex)
            {
                return new SelectorTestResult
                {
                    IsValid = false,
                    Count = 0,
                    Message = $"Error parsing test result: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Highlights all elements that match a given selector.
        /// </summary>
        /// <param name="selector">The CSS selector to highlight.</param>
        /// <returns>Information about the highlighted elements.</returns>
        public async Task<HighlightResult> HighlightMatchingElementsAsync(string selector)
        {
            if (webView == null || webView.CoreWebView2 == null)
            {
                throw new InvalidOperationException("WebView2 is not initialized. Call InitializeAsync first.");
            }

            if (string.IsNullOrWhiteSpace(selector))
            {
                return new HighlightResult
                {
                    Count = 0,
                    Message = "Selector cannot be empty."
                };
            }

            // Escape the selector string for JavaScript
            string escapedSelector = selector.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\r", "\\r").Replace("\n", "\\n");
            
            // Execute the highlight
            string result = await webView.CoreWebView2.ExecuteScriptAsync($"JSON.stringify(ElementPicker.highlightMatchingElements('{escapedSelector}'))");
            
            // Parse the result
            try
            {
                return JsonConvert.DeserializeObject<HighlightResult>(result);
            }
            catch (Exception ex)
            {
                return new HighlightResult
                {
                    Count = 0,
                    Message = $"Error highlighting elements: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Handles messages received from the JavaScript code.
        /// </summary>
        private void CoreWebView2_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            string message = e.TryGetWebMessageAsString();
            
            try
            {
                // First check if the message contains a cancellation string directly
                if (message.Contains("\"canceled\":true"))
                {
                    isPickingActive = false;
                    PickingCanceled?.Invoke(this, EventArgs.Empty);
                    return;
                }
                
                try
                { 
                    // Try to parse the message as JSON
                    JObject jsonObject = JObject.Parse(message);
                    
                    // Check again for cancellation but with proper JSON parsing
                    if (jsonObject["canceled"] != null && jsonObject["canceled"].Value<bool>())
                    {
                        isPickingActive = false;
                        PickingCanceled?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                    
                    // Process the selected element info
                    var selectorInfo = jsonObject.ToObject<ElementSelectorInfo>();
                    
                    if (selectorInfo != null && !string.IsNullOrEmpty(selectorInfo.Optimal))
                    {
                        // Stop the picker if it's still active
                        isPickingActive = false;
                        StopPickingAsync().ConfigureAwait(false);
                        
                        // Raise the event
                        ElementSelected?.Invoke(this, new ElementSelectedEventArgs(selectorInfo));
                    }
                    else
                    {
                        throw new InvalidOperationException($"Received invalid selector data: {message}");
                    }
                }
                catch (JsonReaderException)
                {
                    // If JSON parsing fails, try to deserialize directly
                    var selectorInfo = JsonConvert.DeserializeObject<ElementSelectorInfo>(message);
                    
                    if (selectorInfo != null && !string.IsNullOrEmpty(selectorInfo.Optimal))
                    {
                        isPickingActive = false;
                        StopPickingAsync().ConfigureAwait(false);
                        ElementSelected?.Invoke(this, new ElementSelectedEventArgs(selectorInfo));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing element selection: {ex.Message}\n\nMessage: {message}", 
                    "Element Picker Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gets the JavaScript code for the element picker.
        /// </summary>
        /// <returns>The JavaScript code.</returns>
        private string GetElementPickerScript()
        {
            string resourcePath = "InsuranceAutomation.Resources.ElementPicker.js";
            
            try
            {
                // Try to load from embedded resource
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
                {
                    if (stream != null)
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                
                // Fallback: Load from file in the resources directory
                string filePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "Resources", 
                    "ElementPicker.js");
                
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                
                throw new FileNotFoundException($"Could not find ElementPicker.js in resources: {resourcePath}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error loading element picker script: {ex.Message}", ex);
            }
        }
    }

    /// <summary>
    /// Information about a CSS selector test.
    /// </summary>
    public class SelectorTestResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the selector is valid.
        /// </summary>
        public bool IsValid { get; set; }
        
        /// <summary>
        /// Gets or sets the number of elements that match the selector.
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Gets or sets a message about the test result.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Information about a highlight operation.
    /// </summary>
    public class HighlightResult
    {
        /// <summary>
        /// Gets or sets the number of elements highlighted.
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// Gets or sets a message about the highlight result.
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Information about selectors for an element.
    /// </summary>
    public class ElementSelectorInfo
    {
        /// <summary>
        /// Gets or sets the optimal selector for the element.
        /// </summary>
        public string Optimal { get; set; }
        
        /// <summary>
        /// Gets or sets the ID-based selector.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the class-based selector.
        /// </summary>
        public string Class { get; set; }
        
        /// <summary>
        /// Gets or sets the attribute-based selectors.
        /// </summary>
        public string[] Attributes { get; set; }
        
        /// <summary>
        /// Gets or sets the position-based selector.
        /// </summary>
        public string Position { get; set; }
        
        /// <summary>
        /// Gets or sets alternative selectors.
        /// </summary>
        public string[] Alternatives { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this message is a cancellation.
        /// </summary>
        [JsonProperty("canceled")]
        public bool Canceled { get; set; }
    }

    /// <summary>
    /// Event arguments for when an element is selected.
    /// </summary>
    public class ElementSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the selector information for the selected element.
        /// </summary>
        public ElementSelectorInfo SelectorInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementSelectedEventArgs"/> class.
        /// </summary>
        /// <param name="selectorInfo">The selector information.</param>
        public ElementSelectedEventArgs(ElementSelectorInfo selectorInfo)
        {
            SelectorInfo = selectorInfo;
        }
    }
}