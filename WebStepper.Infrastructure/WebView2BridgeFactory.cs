// Create this new file in Infrastructure project
using WebStepper.Core.Interfaces;
using Microsoft.Web.WebView2.WinForms;

namespace WebStepper.Infrastructure
{
    public class WebView2BridgeFactory
    {
        private readonly ILogService _logService;
        private IWebView2Bridge _webView2Bridge;

        public WebView2BridgeFactory(ILogService logService)
        {
            _logService = logService;
        }

        public void Initialize(WebView2 webView)
        {
            _webView2Bridge = new WebView2Bridge(webView, _logService);
        }

        public IWebView2Bridge GetBridge()
        {
            return _webView2Bridge;
        }
    }
}
