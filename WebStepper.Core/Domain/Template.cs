using System;
using System.Collections.Generic;

namespace WebStepper.Core.Domain
{
    /// <summary>
    /// Represents an automation template for an insurance quoting website
    /// </summary>
    public class Template
    {
        /// <summary>
        /// Unique identifier for the template
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the template
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the template
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Category path for template organization (e.g., "Auto/FL/Progressive")
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Version number for the template
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Collection of pages in the template
        /// </summary>
        public List<Page> Pages { get; set; } = new List<Page>();

        /// <summary>
        /// Collection of variables that can be used in template steps
        /// </summary>
        public Dictionary<string, string> Variables { get; set; } = new Dictionary<string, string>();
    }
}
