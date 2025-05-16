using System;
using Newtonsoft.Json;

namespace InsuranceAutomation.Models
{
    /// <summary>
    /// Represents a single automation step with its properties.
    /// </summary>
    public class AutomationStep
    {
        /// <summary>
        /// Gets or sets the type of automation step (e.g., "fill_form", "click_button").
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the CSS selector for identifying elements on the page.
        /// </summary>
        [JsonProperty("selector")]
        public string Selector { get; set; }

        /// <summary>
        /// Gets or sets the value to be used (e.g., text for input fields).
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the JavaScript code to execute.
        /// </summary>
        [JsonProperty("script")]
        public string Script { get; set; }

        /// <summary>
        /// Gets or sets the description of this automation step.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets a user-friendly description of this step based on its type and properties.
        /// </summary>
        /// <returns>A string describing the step.</returns>
        public string GetDisplayText()
        {
            return Type switch
            {
                "fill_form" => !string.IsNullOrEmpty(Description) 
                    ? Description 
                    : $"Fill '{Selector}' with '{Value}'",
                
                "click_button" => !string.IsNullOrEmpty(Description) 
                    ? Description 
                    : $"Click '{Selector}'",
                
                "wait_for_element" => !string.IsNullOrEmpty(Description) 
                    ? Description 
                    : $"Wait for '{Selector}'",
                
                "execute_script" => !string.IsNullOrEmpty(Description) 
                    ? Description 
                    : $"Execute script: {(Script?.Length > 30 ? Script.Substring(0, 30) + "..." : Script)}",
                
                _ => !string.IsNullOrEmpty(Description) 
                    ? Description 
                    : $"Unknown step type: {Type}"
            };
        }
    }
}
