using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using InsuranceAutomation.Models;

namespace InsuranceAutomation.Services.Interfaces
{
    /// <summary>
    /// Defines the interface for automation execution service.
    /// </summary>
    public interface IAutomationService
    {
        /// <summary>
        /// Event raised when a step is about to be executed.
        /// </summary>
        event EventHandler<AutomationStepEventArgs> StepStarted;

        /// <summary>
        /// Event raised when a step has been completed.
        /// </summary>
        event EventHandler<AutomationStepEventArgs> StepCompleted;

        /// <summary>
        /// Event raised when an error occurs during automation.
        /// </summary>
        event EventHandler<AutomationErrorEventArgs> AutomationError;

        /// <summary>
        /// Event raised when log messages are generated.
        /// </summary>
        event EventHandler<LogEventArgs> LogMessageGenerated;

        /// <summary>
        /// Gets a value indicating whether automation is currently running.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets a value indicating whether automation is currently paused.
        /// </summary>
        bool IsPaused { get; }

        /// <summary>
        /// Gets the current step index (0-based).
        /// </summary>
        int CurrentStepIndex { get; }

        /// <summary>
        /// Sets the collection of steps to be executed.
        /// </summary>
        /// <param name="steps">The steps to execute.</param>
        void SetSteps(IEnumerable<AutomationStep> steps);

        /// <summary>
        /// Navigates to a URL.
        /// </summary>
        /// <param name="url">The URL to navigate to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task NavigateAsync(string url);

        /// <summary>
        /// Starts the automation process.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the automation process.</returns>
        Task RunAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Pauses the automation process.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes a paused automation process.
        /// </summary>
        void Resume();

        /// <summary>
        /// Executes a single step.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing the step execution.</returns>
        Task StepAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Resets the automation state.
        /// </summary>
        void Reset();
    }

    /// <summary>
    /// Event arguments for automation step events.
    /// </summary>
    public class AutomationStepEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the step index.
        /// </summary>
        public int StepIndex { get; }

        /// <summary>
        /// Gets the automation step.
        /// </summary>
        public AutomationStep Step { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomationStepEventArgs"/> class.
        /// </summary>
        /// <param name="stepIndex">The step index.</param>
        /// <param name="step">The automation step.</param>
        public AutomationStepEventArgs(int stepIndex, AutomationStep step)
        {
            StepIndex = stepIndex;
            Step = step;
        }
    }

    /// <summary>
    /// Event arguments for automation error events.
    /// </summary>
    public class AutomationErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the step index where the error occurred.
        /// </summary>
        public int StepIndex { get; }

        /// <summary>
        /// Gets the automation step where the error occurred.
        /// </summary>
        public AutomationStep Step { get; }

        /// <summary>
        /// Gets the exception that occurred.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutomationErrorEventArgs"/> class.
        /// </summary>
        /// <param name="stepIndex">The step index.</param>
        /// <param name="step">The automation step.</param>
        /// <param name="exception">The exception that occurred.</param>
        public AutomationErrorEventArgs(int stepIndex, AutomationStep step, Exception exception)
        {
            StepIndex = stepIndex;
            Step = step;
            Exception = exception;
        }
    }

    /// <summary>
    /// Event arguments for log message events.
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the log message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the timestamp when the log message was generated.
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEventArgs"/> class.
        /// </summary>
        /// <param name="message">The log message.</param>
        public LogEventArgs(string message)
        {
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
