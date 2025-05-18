using System;

namespace WebStepper.Core.Domain
{
    /// <summary>
    /// Contains information to identify a specific page in the application
    /// </summary>
    public class PageIdentifier
    {
        /// <summary>
        /// ID of the page to identify
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// CSS selector that uniquely identifies the page
        /// </summary>
        public string Selector { get; set; }
    }
}
