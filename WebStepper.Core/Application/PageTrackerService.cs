using System;
using System.Threading.Tasks;
using System.Linq;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;

namespace WebStepper.Core.Application
{
    /// <summary>
    /// Service for tracking the current page in the browser
    /// </summary>
    public class PageTrackerService : IPageTrackerService
    {
        private IWebView2Bridge _webView2Bridge;
        private readonly ILogService _logService;

        public event EventHandler<PageDetectedEventArgs> PageDetected;

        public PageTrackerService(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public void Initialize(IWebView2Bridge webView2Bridge)
        {
            _webView2Bridge = webView2Bridge ?? throw new ArgumentNullException(nameof(webView2Bridge));
        }

        public async Task<bool> ValidatePage(Page page)
        {
            if (_webView2Bridge == null)
            {
                _logService.LogWarning("WebView2Bridge not initialized. Page validation will fail.");
                return false;
            }

            // Rest of the method remains the same
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            // If the page has no identifier selector, we can't validate it
            if (string.IsNullOrWhiteSpace(page.PageIdentifierSelector))
            {
                _logService.LogWarning($"Page '{page.Name}' has no identifier selector, assuming valid");
                return true;
            }

            _logService.LogInfo($"Validating page '{page.Name}' with selector: {page.PageIdentifierSelector}");

            try
            {
                // Check if the page identifier selector exists on the current page
                bool exists = await _webView2Bridge.WaitForElement(page.PageIdentifierSelector, 500);

                if (exists)
                {
                    _logService.LogInfo($"Page '{page.Name}' validated");
                }
                else
                {
                    _logService.LogWarning($"Page '{page.Name}' not validated - selector not found");
                }

                return exists;
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error validating page '{page.Name}': {ex.Message}");
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task<Page> DetectCurrentPage(Template template)
        {
            if (_webView2Bridge == null)
            {
                _logService.LogWarning("WebView2Bridge not initialized. Page detection will fail.");
                return null;
            }

            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            _logService.LogInfo("Attempting to detect current page");

            // Check all pages with identifier selectors
            foreach (var page in template.Pages.Where(p => !string.IsNullOrWhiteSpace(p.PageIdentifierSelector)))
            {
                try
                {
                    bool exists = await _webView2Bridge.WaitForElement(page.PageIdentifierSelector, 500);

                    if (exists)
                    {
                        _logService.LogInfo($"Detected page: {page.Name}");

                        // Notify listeners
                        PageDetected?.Invoke(this, new PageDetectedEventArgs { DetectedPage = page });

                        return page;
                    }
                }
                catch (Exception ex)
                {
                    _logService.LogError($"Error checking page '{page.Name}': {ex.Message}");
                }
            }

            _logService.LogWarning("No page could be detected");
            return null;
        }
    }
}
