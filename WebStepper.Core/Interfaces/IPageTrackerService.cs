using System;
using System.Threading.Tasks;
using WebStepper.Core.Domain;

namespace WebStepper.Core.Interfaces
{
    /// <summary>
    /// Interface for page tracking service
    /// </summary>
    public interface IPageTrackerService
    {
        /// <summary>
        /// Event triggered when the current page is detected
        /// </summary>
        event EventHandler<PageDetectedEventArgs> PageDetected;

        /// <summary>
        /// Validates if the browser is currently on the specified page
        /// </summary>
        /// <param name="page">Page to validate</param>
        /// <returns>True if on the specified page, false otherwise</returns>
        Task<bool> ValidatePage(Page page);

        /// <summary>
        /// Attempts to detect the current page from the template
        /// </summary>
        /// <param name="template">Template to use for detection</param>
        /// <returns>The detected page, or null if none detected</returns>
        Task<Page> DetectCurrentPage(Template template);
    }

    /// <summary>
    /// Event arguments for page detected events
    /// </summary>
    public class PageDetectedEventArgs : EventArgs
    {
        /// <summary>
        /// The detected page
        /// </summary>
        public Page DetectedPage { get; set; }
    }
}
