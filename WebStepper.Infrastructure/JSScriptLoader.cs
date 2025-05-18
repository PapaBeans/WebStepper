using System;
using System.IO;
using WebStepper.Core.Interfaces;

namespace WebStepper.Infrastructure
{
    /// <summary>
    /// Utility class for loading JavaScript files
    /// </summary>
    public class JSScriptLoader
    {
        private readonly ILogService _logService;

        /// <summary>
        /// Creates a new JavaScript script loader
        /// </summary>
        /// <param name="logService">Log service</param>
        public JSScriptLoader(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        /// <summary>
        /// Loads a JavaScript file from the embedded resources
        /// </summary>
        /// <param name="resourceName">Name of the resource</param>
        /// <returns>The JavaScript code</returns>
        public string LoadScript(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException("Resource name cannot be null or empty", nameof(resourceName));
            }

            try
            {
                // In a real application, this would load from embedded resources
                // For this example, we'll look for scripts in a 'Scripts' directory
                string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts", resourceName);

                if (!File.Exists(scriptPath))
                {
                    _logService.LogError($"Script file not found: {scriptPath}");
                    throw new FileNotFoundException($"Script file not found: {scriptPath}");
                }

                string script = File.ReadAllText(scriptPath);

                _logService.LogInfo($"Loaded script: {resourceName}");

                return script;
            }
            catch (Exception ex) when (!(ex is FileNotFoundException))
            {
                _logService.LogError($"Error loading script '{resourceName}': {ex.Message}");
                throw;
            }
        }
    }
}
