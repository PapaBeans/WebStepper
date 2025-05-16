using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InsuranceAutomation.Services.Interfaces;

namespace InsuranceAutomation.Services
{
    /// <summary>
    /// Service for logging application events and messages.
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly List<LogEntry> logEntries = new List<LogEntry>();
        private readonly object lockObject = new object();

        /// <inheritdoc/>
        public event EventHandler<LogEntryEventArgs> LogEntryAdded;

        /// <inheritdoc/>
        public IReadOnlyList<LogEntry> LogEntries => logEntries.AsReadOnly();

        /// <inheritdoc/>
        public void LogInfo(string message)
        {
            AddLogEntry(LogLevel.Info, message);
        }

        /// <inheritdoc/>
        public void LogWarning(string message)
        {
            AddLogEntry(LogLevel.Warning, message);
        }

        /// <inheritdoc/>
        public void LogError(string message, Exception exception = null)
        {
            AddLogEntry(LogLevel.Error, message, exception);
        }

        /// <inheritdoc/>
        public void ClearLogs()
        {
            lock (lockObject)
            {
                logEntries.Clear();
            }
        }

        /// <inheritdoc/>
        public void ExportLogs(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));
            }

            lock (lockObject)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine("--- Insurance Automation Logs ---");
                        writer.WriteLine($"Exported: {DateTime.Now}");
                        writer.WriteLine("--------------------------------");
                        writer.WriteLine();

                        foreach (var entry in logEntries)
                        {
                            writer.WriteLine(entry.GetFormattedText());
                            
                            if (entry.Exception != null)
                            {
                                writer.WriteLine($"Exception: {entry.Exception.Message}");
                                writer.WriteLine(entry.Exception.StackTrace);
                                writer.WriteLine();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogError($"Failed to export logs: {ex.Message}", ex);
                    throw;
                }
            }
        }

        private void AddLogEntry(LogLevel level, string message, Exception exception = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = exception?.Message ?? "No message";
            }

            var logEntry = new LogEntry(level, message, exception);
            
            lock (lockObject)
            {
                logEntries.Add(logEntry);
                LogEntryAdded?.Invoke(this, new LogEntryEventArgs(logEntry));
            }
        }
    }
}
