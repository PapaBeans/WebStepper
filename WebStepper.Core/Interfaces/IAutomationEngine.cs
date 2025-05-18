using System;
using System.Threading.Tasks;
using WebStepper.Core.Domain;

namespace WebStepper.Core.Interfaces
{
    /// <summary>
    /// Interface for the automation engine that executes templates
    /// </summary>
    public interface IAutomationEngine
    {
        /// <summary>
        /// Event triggered when step execution starts
        /// </summary>
        event EventHandler<StepExecutionEventArgs> StepExecutionStarted;

        /// <summary>
        /// Event triggered when step execution completes
        /// </summary>
        event EventHandler<StepExecutionEventArgs> StepExecutionCompleted;

        /// <summary>
        /// Event triggered when the current page changes
        /// </summary>
        event EventHandler<PageChangeEventArgs> PageChanged;

        /// <summary>
        /// Event triggered when automation status changes
        /// </summary>
        event EventHandler<AutomationStatusEventArgs> StatusChanged;

        /// <summary>
        /// Starts template automation
        /// </summary>
        /// <param name="template">Template to execute</param>
        /// <param name="startingStep">Optional starting step (if null, starts from beginning)</param>
        Task StartAutomation(Template template, Step startingStep = null);

        /// <summary>
        /// Pauses the currently running automation
        /// </summary>
        void PauseAutomation();

        /// <summary>
        /// Resumes a paused automation
        /// </summary>
        Task ResumeAutomation();

        /// <summary>
        /// Stops the currently running automation
        /// </summary>
        void StopAutomation();

        /// <summary>
        /// Executes a single step and advances to the next
        /// </summary>
        Task ExecuteStep();
    }

    /// <summary>
    /// Event arguments for step execution events
    /// </summary>
    public class StepExecutionEventArgs : EventArgs
    {
        /// <summary>
        /// The page containing the executing step
        /// </summary>
        public Page Page { get; set; }

        /// <summary>
        /// The step being executed
        /// </summary>
        public Step Step { get; set; }

        /// <summary>
        /// Whether the step executed successfully
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Error message if the step failed
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// Event arguments for page change events
    /// </summary>
    public class PageChangeEventArgs : EventArgs
    {
        /// <summary>
        /// The new current page
        /// </summary>
        public Page NewPage { get; set; }
    }

    /// <summary>
    /// Event arguments for automation status change events
    /// </summary>
    public class AutomationStatusEventArgs : EventArgs
    {
        /// <summary>
        /// Whether automation is running
        /// </summary>
        public bool IsRunning { get; set; }

        /// <summary>
        /// Whether automation is paused
        /// </summary>
        public bool IsPaused { get; set; }
    }
}
