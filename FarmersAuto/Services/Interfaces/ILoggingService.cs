using System;
using System.Collections.Generic;
using System.IO;
using InsuranceAutomation.Services.Interfaces;

namespace InsuranceAutomation.Services.Interfaces
{
    /// <summary>
    /// Interface for logging service.
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Event raised when a log entry is added.
        /// </summary>
        event EventHandler<LogEntryEventArgs> LogEntryAdded;

        /// <summary>
        /// Gets the log entries collection.
        /// </summary>
        IReadOnlyList<LogEntry> LogEntries { get; }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        void LogInfo(string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="exception">The exception associated with the error, if any.</param>
        void LogError(string message, Exception exception = null);

        /// <summary>
        /// Clears all log entries.
        /// </summary>
        void ClearLogs();

        /// <summary>
        /// Exports logs to a file.
        /// </summary>
        /// <param name="filePath">The file path to save logs to.</param>
        void ExportLogs(string filePath);
    }

    /// <summary>
    /// Represents a log entry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Gets the timestamp of the log entry.
        /// </summary>
        public DateTime Timestamp { get; }
        
        /// <summary>
        /// Gets the log level.
        /// </summary>
        public LogLevel Level { get; }
        
        /// <summary>
        /// Gets the log message.
        /// </summary>
        public string Message { get; }
        
        /// <summary>
        /// Gets the exception associated with the log entry, if any.
        /// </summary>
        public Exception Exception { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">The log message.</param>
        /// <param name="exception">The exception, if any.</param>
        public LogEntry(LogLevel level, string message, Exception exception = null)
        {
            Timestamp = DateTime.Now;
            Level = level;
            Message = message ?? string.Empty;
            Exception = exception;
        }

        /// <summary>
        /// Gets a formatted string representation of the log entry.
        /// </summary>
        /// <returns>A formatted string.</returns>
        public string GetFormattedText()
        {
            string levelText = Level switch
            {
                LogLevel.Info => "INFO",
                LogLevel.Warning => "WARN",
                LogLevel.Error => "ERROR",
                _ => "UNKNOWN"
            };

            return $"{Timestamp:HH:mm:ss} - [{levelText}] {Message}";
        }
    }

    /// <summary>
    /// Event arguments for log entry events.
    /// </summary>
    public class LogEntryEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the log entry.
        /// </summary>
        public LogEntry LogEntry { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntryEventArgs"/> class.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        public LogEntryEventArgs(LogEntry logEntry)
        {
            LogEntry = logEntry;
        }
    }

    /// <summary>
    /// Defines logging levels.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Informational message.
        /// </summary>
        Info,
        
        /// <summary>
        /// Warning message.
        /// </summary>
        Warning,
        
        /// <summary>
        /// Error message.
        /// </summary>
        Error
    }
}
