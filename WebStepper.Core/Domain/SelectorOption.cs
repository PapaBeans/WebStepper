using System;

namespace WebStepper.Core.Domain
{
    /// <summary>
    /// Represents a selector option for an element on the webpage
    /// </summary>
    public class SelectorOption
    {
        /// <summary>
        /// Type of selector (CSS, XPath, etc.)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Value of the selector
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Specificity score for the selector (higher is more specific)
        /// </summary>
        public int SpecificityScore { get; set; }

        /// <summary>
        /// Number of elements matched by this selector
        /// </summary>
        public int MatchCount { get; set; }

        /// <summary>
        /// Preview text or information about the elements matched
        /// </summary>
        public string MatchPreview { get; set; }
    }
}
