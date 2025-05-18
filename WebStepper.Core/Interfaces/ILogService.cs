using System;

namespace WebStepper.Core.Interfaces
{
    /// <summary>
    /// Interface for logging service
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Event triggered when a new log entry is added
        /// </summary>
        event EventHandler<LogEntryEventArgs> LogEntryAdded;

        /// <summary>
        /// Logs an informational message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogError(string message);

        /// <summary>
        /// Clears all log entries
        /// </summary>
        void ClearLogs();

        /// <summary>
        /// Gets all log entries
        /// </summary>
        /// <returns>Array of log entries</returns>
        LogEntry[] GetLogEntries();
    }

    /// <summary>
    /// Represents a single log entry
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Timestamp of the log entry
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Log level
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Log message
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Log severity levels
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Informational message
        /// </summary>
        Info,

        /// <summary>
        /// Warning message
        /// </summary>
        Warning,

        /// <summary>
        /// Error message
        /// </summary>
        Error
    }

    /// <summary>
    /// Event arguments for log entry events
    /// </summary>
    public class LogEntryEventArgs : EventArgs
    {
        /// <summary>
        /// The new log entry
        /// </summary>
        public LogEntry LogEntry { get; set; }
    }
}
