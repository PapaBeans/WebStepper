using System;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace InsuranceAutomation.Utilities
{
    /// <summary>
    /// Utility methods for common operations.
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Initializes a WebView2 control.
        /// </summary>
        /// <param name="webView">The WebView2 control to initialize.</param>
        /// <returns>A task representing the initialization process.</returns>
        public static async Task InitializeWebView2Async(WebView2 webView)
        {
            if (webView == null)
            {
                throw new ArgumentNullException(nameof(webView));
            }

            try
            {
                await webView.EnsureCoreWebView2Async(null);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to initialize WebView2: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Escapes a string for use in JavaScript.
        /// </summary>
        /// <param name="input">The input string to escape.</param>
        /// <returns>The escaped string.</returns>
        public static string EscapeJsString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;
                
            return input
                .Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\"", "\\\"")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n")
                .Replace("\t", "\\t");
        }

        /// <summary>
        /// Creates a timestamp string for file names.
        /// </summary>
        /// <returns>A timestamp string in the format yyyyMMdd_HHmmss.</returns>
        public static string GetTimestampForFileName()
        {
            return DateTime.Now.ToString("yyyyMMdd_HHmmss");
        }

        /// <summary>
        /// Creates a directory if it doesn't exist.
        /// </summary>
        /// <param name="directoryPath">The directory path to create.</param>
        /// <returns>True if the directory was created, false if it already existed.</returns>
        public static bool EnsureDirectoryExists(string directoryPath)
        {
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Formats an exception message with inner exception details.
        /// </summary>
        /// <param name="ex">The exception to format.</param>
        /// <returns>A formatted error message.</returns>
        public static string FormatExceptionMessage(Exception ex)
        {
            if (ex == null)
                return string.Empty;

            string message = ex.Message;
            
            if (ex.InnerException != null)
            {
                message += $" Inner exception: {ex.InnerException.Message}";
            }
            
            return message;
        }
    }
}
