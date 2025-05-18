using System;

namespace WebStepper.Core.Domain
{
    /// <summary>
    /// Represents a single automation step within a page
    /// </summary>
    public class Step
    {
        /// <summary>
        /// Unique identifier for the step
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the step
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the step
        /// </summary>
        public StepType Type { get; set; }

        /// <summary>
        /// CSS or XPath selector for the element to interact with
        /// </summary>
        public string Selector { get; set; }

        /// <summary>
        /// Value to use for form fills or script execution
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Time to wait in milliseconds before executing the step
        /// </summary>
        public int WaitBeforeMs { get; set; }

        /// <summary>
        /// Time to wait in milliseconds after executing the step
        /// </summary>
        public int WaitAfterMs { get; set; }

        /// <summary>
        /// Maximum time to wait for an element to appear (for wait_for_element type)
        /// </summary>
        public int MaxWaitMs { get; set; }

        /// <summary>
        /// Description of the step
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Defines the types of automation steps available
    /// </summary>
    public enum StepType
    {
        /// <summary>
        /// Wait for an element to appear on the page
        /// </summary>
        WaitForElement,

        /// <summary>
        /// Fill a form field with a value
        /// </summary>
        FillForm,

        /// <summary>
        /// Click a button or link
        /// </summary>
        ClickButton,

        /// <summary>
        /// Execute custom JavaScript
        /// </summary>
        ExecuteScript
    }
}
