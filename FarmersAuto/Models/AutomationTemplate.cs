using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InsuranceAutomation.Models
{
    /// <summary>
    /// Represents an automation template with version information and steps.
    /// </summary>
    public class AutomationTemplate
    {
        /// <summary>
        /// Gets or sets the version of the template.
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the description of the template.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the target URL for this automation template.
        /// </summary>
        [JsonProperty("targeturl")]
        public string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets the collection of automation steps.
        /// </summary>
        [JsonProperty("steps")]
        public List<AutomationStep> Steps { get; set; } = new List<AutomationStep>();

        /// <summary>
        /// Gets or sets the file path where this template is stored.
        /// </summary>
        [JsonProperty("filepath")]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets the filename of this template.
        /// </summary>
        [JsonProperty("filename")]
        public string FileName => !string.IsNullOrEmpty(FilePath) ? 
            System.IO.Path.GetFileName(FilePath) : null;

        /// <summary>
        /// Creates a new instance of an automation template with default values.
        /// </summary>
        /// <returns>A new automation template instance.</returns>
        public static AutomationTemplate CreateDefault()
        {
            return new AutomationTemplate
            {
                Version = "1.0",
                Description = "New automation template",
                TargetUrl = "https://example.com",
                Steps = new List<AutomationStep>
                {
                    new AutomationStep
                    {
                        Type = "wait_for_element",
                        Selector = "#example",
                        Description = "Wait for an element to load"
                    }
                }
            };
        }
    }
}
