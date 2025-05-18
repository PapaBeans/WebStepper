using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;

namespace WebStepper.Infrastructure
{
    // helper DTO
    class SelectorValidationResult
    {
        public bool valid { get; set; }
        public int count { get; set; }
        public string error { get; set; }
    }

    public class ClickResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }
    /// <summary>
    /// Implementation of the WebView2 bridge for browser integration
    /// </summary>
    public class WebView2Bridge : IWebView2Bridge
    {
        private readonly WebView2 _webView;
        private readonly ILogService _logService;

        /// <inheritdoc/>
        public event EventHandler<WebMessageEventArgs> WebMessageReceived;

        /// <summary>
        /// Creates a new WebView2 bridge
        /// </summary>
        /// <param name="webView">WebView2 control</param>
        /// <param name="logService">Log service</param>
        public WebView2Bridge(WebView2 webView, ILogService logService)
        {
            _webView = webView ?? throw new ArgumentNullException(nameof(webView));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));

            // Register for WebMessage events
            _webView.WebMessageReceived += OnWebMessageReceived;
        }

        /// <inheritdoc/>
        public async Task<bool> WaitForElement(string selector, int timeoutMs = 5000)
        {
            var start = DateTime.UtcNow;
            TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMs);

            while (DateTime.UtcNow - start < timeout)
            {
                var (isValid, count, error) = await ValidateSelectorAsync(selector);
                if (!isValid)
                {
                    _logService.LogError($"Selector validation failed: {error}");
                    return false;
                }
                if (count > 0)
                {
                    _logService.LogInfo($"Element '{selector}' appeared.");
                    return true;
                }
                await Task.Delay(200);
            }

            _logService.LogError($"Timed out waiting for '{selector}'.");
            return false;
        }


        private async Task<(bool IsValid, int MatchCount, string Error)> ValidateSelectorAsync(string selector)
        {
            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Selector cannot be null or empty", nameof(selector));

            // Decide CSS vs. XPath
            bool isXPath = selector.TrimStart().StartsWith("/");

            // Escape it for JS
            string jsSel = EscapeJsString(selector);

            // Build the right JS snippet
            string script;
            if (isXPath)
            {
                script = $@"
            (function() {{
                try {{
                    // Evaluate XPath
                    var snap = document.evaluate(
                        '{jsSel}',
                        document,
                        null,
                        XPathResult.ORDERED_NODE_SNAPSHOT_TYPE,
                        null
                    );
                    return {{ valid: true, count: snap.snapshotLength, error: null }};
                }} catch(err) {{
                    return {{ valid: false, count: 0, error: err.toString() }};
                }}
            }})();
        ";
            }
            else
            {
                script = $@"
            (function() {{
                try {{
                    var list = document.querySelectorAll('{jsSel}');
                    return {{ valid: true, count: list.length, error: null }};
                }} catch(err) {{
                    return {{ valid: false, count: 0, error: err.toString() }};
                }}
            }})();
        ";
            }

            // Execute and parse
            var raw = await _webView.CoreWebView2.ExecuteScriptAsync(script);
            var result = JsonConvert.DeserializeObject<SelectorValidationResult>(raw);

            return (result.valid, result.count, result.error);
        }



        private async Task<(bool IsValid, int MatchCount, string Error)> ValidateSelectorAsync_old(string selector)
        {
            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Selector cannot be null or empty", nameof(selector));

            // turn your C# string into a safe JS literal:
            var jsonSel = JsonConvert.SerializeObject(selector);

            // returns an object: { valid:bool, count:int, error:string|null }
            var script = $@"
        (function(sel) {{
            try {{
                const list = document.querySelectorAll(sel);
                return {{ valid: true, count: list.length, error: null }};
            }} catch(err) {{
                return {{ valid: false, count: 0, error: err.message }};
            }}
        }})({jsonSel});
    ";

            var raw = await _webView.CoreWebView2.ExecuteScriptAsync(script);
            // raw is something like: {"valid":false,"count":0,"error":"Failed to execute 'querySelectorAll'..."}
            // so parse it:
            var result = JsonConvert.DeserializeObject<SelectorValidationResult>(raw);
            return (result.valid, result.count, result.error);
        }


        /// <inheritdoc/>
        public async Task FillFormElement(string selector, string value)
        {
            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Selector cannot be null or empty", nameof(selector));

            // Escape both selector & value for JS
            string jsSel = EscapeJsString(selector);
            string jsValue = EscapeJsString(value ?? "");

            string script = $@"
        (function() {{
            function getElement(sel) {{
                if (sel.trim().startsWith('/')) {{
                    var r = document.evaluate(
                      sel, document, null,
                      XPathResult.FIRST_ORDERED_NODE_TYPE, null
                    );
                    return r.singleNodeValue;
                }} else {{
                    return document.querySelector(sel);
                }}
            }}
            var element = getElement('{jsSel}');
            if (!element) {{
                return {{ success: false, error: 'Element not found' }};
            }}
            try {{
                element.value = '{jsValue}';
                element.dispatchEvent(new Event('input', {{ bubbles: true }}));
                element.dispatchEvent(new Event('change', {{ bubbles: true }}));
                return {{ success: true }};
            }} catch (e) {{
                return {{ success: false, error: e.toString() }};
            }}
        }})();";

            var raw = await _webView.CoreWebView2.ExecuteScriptAsync(script);
            dynamic resp = JsonConvert.DeserializeObject<dynamic>(raw);
            if (resp.success == false)
            {
                string err = resp.error.ToString();
                _logService.LogError($"Failed to fill '{selector}': {err}");
                throw new InvalidOperationException($"Fill failed: {err}");
            }

            _logService.LogInfo($"Filled '{selector}' with '{value}'.");
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

        public async Task ClickElement(string selector, int timeoutMs = 5000)
        {
            if (string.IsNullOrWhiteSpace(selector))
                throw new ArgumentException("Selector cannot be null or empty", nameof(selector));

            // 1) Wait for element (supports both CSS & XPath)
            bool exists = await WaitForElement(selector, timeoutMs);
            if (!exists)
                throw new InvalidOperationException($"Element '{selector}' not found within {timeoutMs}ms.");

            // 2) Build JS that picks the element via CSS or XPath, then .click()
            string jsSel = EscapeJsString(selector);
            string script = $@"
        (function() {{
            function getElement(sel) {{
                if (sel.trim().startsWith('/')) {{
                    // XPath
                    var res = document.evaluate(
                      sel, document, null,
                      XPathResult.FIRST_ORDERED_NODE_TYPE, null
                    );
                    return res.singleNodeValue;
                }} else {{
                    // CSS
                    return document.querySelector(sel);
                }}
            }}
            var el = getElement('{jsSel}');
            if (!el) {{
                return {{ success: false, error: 'Element not found' }};
            }}
            try {{
                el.click();
                return {{ success: true }};
            }} catch (err) {{
                return {{ success: false, error: err.toString() }};
            }}
        }})();";

            var raw = await _webView.CoreWebView2.ExecuteScriptAsync(script);
            var resp = JsonConvert.DeserializeObject<ClickResponse>(raw);
            if (!resp.Success)
            {
                _logService.LogError($"Failed to click '{selector}': {resp.Error}");
                throw new InvalidOperationException($"Click failed: {resp.Error}");
            }

            _logService.LogInfo($"Clicked element '{selector}'.");
        }


        /// <inheritdoc/>
        public async Task<string> ExecuteScript(string script)
        {
            if (string.IsNullOrWhiteSpace(script))
            {
                throw new ArgumentException("Script cannot be null or empty", nameof(script));
            }

            try
            {
                return await _webView.CoreWebView2.ExecuteScriptAsync(script);
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error executing script: {ex.Message}");
                throw;
            }
        }

        private void OnWebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // Forward the event to our listeners
            WebMessageReceived?.Invoke(this, new WebMessageEventArgs { WebMessageAsJson = e.WebMessageAsJson });
        }
    }
}
