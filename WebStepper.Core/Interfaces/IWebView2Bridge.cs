using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebStepper.Core.Interfaces
{
    /// <summary>
    /// Interface for WebView2 browser integration
    /// </summary>
    public interface IWebView2Bridge
    {
        /// <summary>
        /// Event triggered when a web message is received from JavaScript
        /// </summary>
        event EventHandler<WebMessageEventArgs> WebMessageReceived;

        /// <summary>
        /// Waits for an element matching the selector to appear
        /// </summary>
        /// <param name="selector">CSS selector to wait for</param>
        /// <param name="timeoutMs">Timeout in milliseconds</param>
        /// <returns>True if the element appeared, false if timed out</returns>
        Task<bool> WaitForElement(string selector, int timeoutMs = 5000);

        /// <summary>
        /// Fills a form element with a value
        /// </summary>
        /// <param name="selector">CSS selector for the element</param>
        /// <param name="value">Value to fill</param>
        Task FillFormElement(string selector, string value);

        /// <summary>
        /// Clicks an element
        /// </summary>
        /// <param name="selector">CSS selector for the element</param>
        Task ClickElement(string selector, int timeoutMs = 5000);

        /// <summary>
        /// Executes JavaScript code
        /// </summary>
        /// <param name="script">JavaScript code to execute</param>
        /// <returns>Result of the script execution</returns>
        Task<string> ExecuteScript(string script);
    }

    /// <summary>
    /// Event arguments for web message events
    /// </summary>
    public class WebMessageEventArgs : EventArgs
    {
        /// <summary>
        /// The web message JSON
        /// </summary>
        public string WebMessageAsJson { get; set; }
    }
}
