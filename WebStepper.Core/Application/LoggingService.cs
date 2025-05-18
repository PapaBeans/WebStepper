using System;
using System.Collections.Generic;
using WebStepper.Core.Interfaces;

namespace WebStepper.Core.Application
{
    /// <summary>
    /// Service for logging application events
    /// </summary>
    public class LoggingService : ILogService
    {
        private readonly List<LogEntry> _logs = new List<LogEntry>();
        private readonly object _lockObject = new object();

        /// <inheritdoc/>
        public event EventHandler<LogEntryEventArgs> LogEntryAdded;

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
        public void LogError(string message)
        {
            AddLogEntry(LogLevel.Error, message);
        }

        /// <inheritdoc/>
        public void ClearLogs()
        {
            lock (_lockObject)
            {
                _logs.Clear();
            }
        }

        /// <inheritdoc/>
        public LogEntry[] GetLogEntries()
        {
            lock (_lockObject)
            {
                return _logs.ToArray();
            }
        }

        private void AddLogEntry(LogLevel level, string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            var entry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Level = level,
                Message = message
            };

            lock (_lockObject)
            {
                _logs.Add(entry);
            }

            // Raise event
            LogEntryAdded?.Invoke(this, new LogEntryEventArgs { LogEntry = entry });

            // Also write to console for debugging
            Console.WriteLine($"[{entry.Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{entry.Level}] {entry.Message}");
        }
    }
}
