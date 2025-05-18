using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;
using Newtonsoft.Json;

namespace WebStepper.Infrastructure
{
    /// <summary>
    /// Implementation of the template repository using the file system
    /// </summary>
    public class FileTemplateRepository : ITemplateRepository
    {
        private readonly string _baseDirectory;
        private readonly ILogService _logService;

        /// <summary>
        /// Creates a new file template repository
        /// </summary>
        /// <param name="baseDirectory">Base directory for templates</param>
        /// <param name="logService">Log service</param>
        public FileTemplateRepository(string baseDirectory, ILogService logService)
        {
            _baseDirectory = baseDirectory ?? throw new ArgumentNullException(nameof(baseDirectory));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));

            // Ensure the base directory exists
            if (!Directory.Exists(_baseDirectory))
            {
                Directory.CreateDirectory(_baseDirectory);
                _logService.LogInfo($"Created templates directory: {_baseDirectory}");
            }
        }

        /// <inheritdoc/>
        public async Task<string[]> GetTemplateNamesAsync()
        {
            _logService.LogInfo("Getting all template names");

            try
            {
                // Find all JSON files in the base directory and its subdirectories
                var files = Directory.GetFiles(_baseDirectory, "*.json", SearchOption.AllDirectories);

                // Extract the template names (filename without extension)
                var templateNames = files.Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();

                return await Task.FromResult(templateNames);
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error getting template names: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<string[]> GetTemplateNamesInCategoryAsync(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Category cannot be null or empty", nameof(category));
            }

            _logService.LogInfo($"Getting template names in category: {category}");

            try
            {
                // Convert category path format to directory path
                string categoryPath = ConvertCategoryToPath(category);
                string categoryDir = Path.Combine(_baseDirectory, categoryPath);

                if (!Directory.Exists(categoryDir))
                {
                    _logService.LogWarning($"Category directory does not exist: {categoryDir}");
                    return await Task.FromResult(new string[0]);
                }

                // Find all JSON files in the category directory
                var files = Directory.GetFiles(categoryDir, "*.json", SearchOption.TopDirectoryOnly);

                // Extract the template names (filename without extension)
                var templateNames = files.Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();

                return await Task.FromResult(templateNames);
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error getting template names in category '{category}': {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<string[]> GetCategoriesAsync()
        {
            _logService.LogInfo("Getting all categories");

            try
            {
                var categories = new List<string>();

                // Recursively find all directories
                GetDirectoriesRecursive(_baseDirectory, string.Empty, categories);

                return await Task.FromResult(categories.ToArray());
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error getting categories: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Template> LoadTemplateAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            _logService.LogInfo($"Loading template from: {path}");

            try
            {
                // Ensure the file exists
                if (!File.Exists(path))
                {
                    _logService.LogError($"Template file does not exist: {path}");
                    throw new FileNotFoundException($"Template file does not exist: {path}");
                }

                // Read the file
                string json = File.ReadAllText(path);

                // Deserialize the JSON
                var template = JsonConvert.DeserializeObject<Template>(json);

                if (template == null)
                {
                    _logService.LogError($"Failed to deserialize template: {path}");
                    throw new InvalidOperationException($"Failed to deserialize template: {path}");
                }

                _logService.LogInfo($"Template loaded: {template.Name}");

                return template;
            }
            catch (JsonException ex)
            {
                _logService.LogError($"Error parsing template JSON: {ex.Message}");
                throw;
            }
            catch (Exception ex) when (!(ex is FileNotFoundException || ex is InvalidOperationException))
            {
                _logService.LogError($"Error loading template: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SaveTemplateAsync(string path, Template template)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            _logService.LogInfo($"Saving template '{template.Name}' to: {path}");

            try
            {
                // Ensure the directory exists
                string directory = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    _logService.LogInfo($"Created directory: {directory}");
                }

                // Serialize the template
                string json = JsonConvert.SerializeObject(template, Formatting.Indented);

                // Write to the file
                File.WriteAllText(path, json);

                _logService.LogInfo($"Template saved to: {path}");
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error saving template: {ex.Message}");
                throw;
            }
        }

        private string ConvertCategoryToPath(string category)
        {
            // Replace forward slashes with backslashes
            return category.Replace('/', Path.DirectorySeparatorChar);
        }

        private string ConvertPathToCategory(string path)
        {
            // Replace backslashes with forward slashes
            return path.Replace(Path.DirectorySeparatorChar, '/');
        }

        private void GetDirectoriesRecursive(string baseDir, string currentPath, List<string> categories)
        {
            // Get all subdirectories
            var subdirs = Directory.GetDirectories(baseDir);

            foreach (var dir in subdirs)
            {
                // Get the directory name without the full path
                string dirName = Path.GetFileName(dir);

                // Combine with the current path
                string categoryPath = string.IsNullOrEmpty(currentPath)
                    ? dirName
                    : Path.Combine(currentPath, dirName);

                // Add to categories
                categories.Add(ConvertPathToCategory(categoryPath));

                // Recursively process subdirectories
                GetDirectoriesRecursive(dir, categoryPath, categories);
            }
        }
    }
}
