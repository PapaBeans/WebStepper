using System;
using System.Collections.Generic;

namespace WebStepper.Core.Domain
{
    /// <summary>
    /// Represents a page within a template with its own set of automation steps
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Unique identifier for the page
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the page
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// CSS selector used to identify when the browser is on this page
        /// </summary>
        public string PageIdentifierSelector { get; set; }

        /// <summary>
        /// Collection of automation steps for this page
        /// </summary>
        public List<Step> Steps { get; set; } = new List<Step>();
    }
}
