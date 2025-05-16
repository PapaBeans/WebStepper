using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InsuranceAutomation.Models;
using InsuranceAutomation.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InsuranceAutomation.Services
{
    /// <summary>
    /// Service for managing automation templates including loading, saving and versioning.
    /// </summary>
    public class TemplateService : ITemplateService
    {
        private const string TemplateFolder = "Templates";
        private const string TemplateHistoryFolder = "TemplateHistory";

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateService"/> class.
        /// </summary>
        public TemplateService()
        {
            EnsureFoldersExist();
        }

        /// <inheritdoc/>
        public List<string> GetAvailableTemplates()
        {
            EnsureFoldersExist();
            List<string> templates = new List<string>();

            string[] files = Directory.GetFiles(GetTemplatesDirectory(), "*.json");
            foreach (string file in files)
            {
                templates.Add(Path.GetFileName(file));
            }

            return templates;
        }

        /// <inheritdoc/>
        public string CreateNewTemplate(string templateName)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentException("Template name cannot be empty.", nameof(templateName));
            }

            if (!templateName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                templateName += ".json";
            }

            string templatePath = Path.Combine(GetTemplatesDirectory(), templateName);

            // Create basic template structure
            var template = AutomationTemplate.CreateDefault();
            var json = JsonConvert.SerializeObject(template, Formatting.Indented);
            File.WriteAllText(templatePath, json);

            return templatePath;
        }

        /// <inheritdoc/>
        public AutomationTemplate LoadTemplate(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Template file not found: {filePath}", filePath);
            }

            string json = File.ReadAllText(filePath);
            var template = DeserializeTemplate(json);
            template.FilePath = filePath;

            return template;
        }

        /// <inheritdoc/>
        public void SaveTemplate(AutomationTemplate template, string filePath)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));
            }

            // Create a backup before saving
            if (File.Exists(filePath))
            {
                CreateTemplateVersionBackup(filePath);
            }

            string json = JsonConvert.SerializeObject(template, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        /// <inheritdoc/>
        public void SaveTemplateContent(string content, string filePath)
        {
            if (string.IsNullOrEmpty(content))
            {
                throw new ArgumentException("Content cannot be empty.", nameof(content));
            }

            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be empty.", nameof(filePath));
            }

            // First validate the JSON
            JObject.Parse(content);

            // Create a backup before saving
            if (File.Exists(filePath))
            {
                CreateTemplateVersionBackup(filePath);
            }

            // Save the new content
            File.WriteAllText(filePath, content);
        }

        /// <inheritdoc/>
        public List<TemplateVersion> GetTemplateVersionHistory(string templateName)
        {
            if (string.IsNullOrWhiteSpace(templateName))
            {
                throw new ArgumentException("Template name cannot be empty.", nameof(templateName));
            }

            if (templateName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                templateName = Path.GetFileNameWithoutExtension(templateName);
            }

            List<TemplateVersion> history = new List<TemplateVersion>();

            string[] files = Directory.GetFiles(GetTemplateHistoryDirectory(), $"{templateName}_*.json");
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                var version = TemplateVersion.FromFileName(fileName, file);
                history.Add(version);
            }

            // Sort by timestamp (most recent first)
            return history.OrderByDescending(v => v.Timestamp).ToList();
        }

        /// <inheritdoc/>
        public void RestoreTemplateVersion(string versionPath, string templatePath)
        {
            if (!File.Exists(versionPath))
            {
                throw new FileNotFoundException($"Version file not found: {versionPath}", versionPath);
            }

            // Create a backup of the current template before restoring
            if (File.Exists(templatePath))
            {
                CreateTemplateVersionBackup(templatePath);
            }

            // Copy the version file to the template file
            File.Copy(versionPath, templatePath, true);
        }

        /// <inheritdoc/>
        public (bool IsValid, string ErrorMessage) ValidateTemplateContent(string jsonContent)
        {
            try
            {
                JObject template = JObject.Parse(jsonContent);
                
                
                // Verify steps array exists
                if (template["steps"] == null)
                {
                    return (false, "Template is missing the required 'steps' array.");
                }

                // Verify each step has the required properties
                foreach (JObject step in template["steps"])
                {
                    string type = step["type"]?.ToString();
                    
                    if (string.IsNullOrEmpty(type))
                    {
                        return (false, "A step is missing the required 'type' property.");
                    }
                    
                    // Check for required properties based on step type
                    switch (type)
                    {
                        case "fill_form":
                            if (step["selector"] == null || step["value"] == null)
                            {
                                return (false, "A 'fill_form' step is missing 'selector' or 'value' properties.");
                            }
                            break;
                        case "click_button":
                        case "wait_for_element":
                            if (step["selector"] == null)
                            {
                                return (false, $"A '{type}' step is missing the 'selector' property.");
                            }
                            break;
                        case "execute_script":
                            if (step["script"] == null)
                            {
                                return (false, "An 'execute_script' step is missing the 'script' property.");
                            }
                            break;
                        default:
                            return (false, $"Unknown step type: {type}");
                    }
                }
                
                return (true, null);
            }
            catch (JsonException ex)
            {
                return (false, $"Invalid JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, $"Error validating template: {ex.Message}");
            }
        }

        /// <inheritdoc/>
        public string FormatTemplateContent(string jsonContent)
        {
            JToken parsedJson = JToken.Parse(jsonContent);
            return parsedJson.ToString(Formatting.Indented);
        }

        /// <inheritdoc/>
        public string GetTemplatesDirectory()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TemplateFolder);
        }

        /// <inheritdoc/>
        public string GetTemplateHistoryDirectory()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TemplateHistoryFolder);
        }

        private void EnsureFoldersExist()
        {
            Directory.CreateDirectory(GetTemplatesDirectory());
            Directory.CreateDirectory(GetTemplateHistoryDirectory());
        }

        private void CreateTemplateVersionBackup(string templatePath)
        {
            if (!File.Exists(templatePath))
            {
                return;
            }

            try
            {
                string fileName = Path.GetFileName(templatePath);
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string backupFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{timestamp}.json";
                string backupPath = Path.Combine(GetTemplateHistoryDirectory(), backupFileName);

                File.Copy(templatePath, backupPath);
            }
            catch (Exception ex)
            {
                // Log the error but continue with the save operation
                System.Diagnostics.Debug.WriteLine($"Error creating backup: {ex.Message}");
            }
        }

        private AutomationTemplate DeserializeTemplate(string json)
        {
            var jObject = JObject.Parse(json);

            string GetString(string key) =>
                jObject.GetValue(key, StringComparison.OrdinalIgnoreCase)?
                    .ToString();

            var template = new AutomationTemplate
            {
                Version     = GetString("version"),
                Description = GetString("description"),
                TargetUrl   = GetString("targeturl"),
                Steps       = new List<AutomationStep>()
            };

            if (jObject.GetValue("steps", StringComparison.OrdinalIgnoreCase) is JArray steps)
            {
                foreach (JObject stepObj in steps)
                {
                    string GetStep(string key) =>
                        stepObj.GetValue(key, StringComparison.OrdinalIgnoreCase)?
                            .ToString();

                    template.Steps.Add(new AutomationStep
                    {
                        Type        = GetStep("type"),
                        Selector    = GetStep("selector"),
                        Value       = GetStep("value"),
                        Script      = GetStep("script"),
                        Description = GetStep("description")
                    });
                }
            }

            return template;
        }

    }
}
