using System;
using System.Collections.Generic;
using InsuranceAutomation.Models;

namespace InsuranceAutomation.Services.Interfaces
{
    /// <summary>
    /// Defines the interface for template management services.
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Gets all available templates.
        /// </summary>
        /// <returns>A list of available templates.</returns>
        List<string> GetAvailableTemplates();

        /// <summary>
        /// Creates a new template with the specified name.
        /// </summary>
        /// <param name="templateName">The name of the template.</param>
        /// <returns>The file path to the new template.</returns>
        string CreateNewTemplate(string templateName);

        /// <summary>
        /// Loads a template from the specified file path.
        /// </summary>
        /// <param name="filePath">The file path of the template.</param>
        /// <returns>The loaded template.</returns>
        AutomationTemplate LoadTemplate(string filePath);

        /// <summary>
        /// Saves a template to the specified file path.
        /// </summary>
        /// <param name="template">The template to save.</param>
        /// <param name="filePath">The file path where to save the template.</param>
        void SaveTemplate(AutomationTemplate template, string filePath);

        /// <summary>
        /// Saves template content to the specified file path.
        /// </summary>
        /// <param name="content">The JSON content of the template.</param>
        /// <param name="filePath">The file path where to save the template.</param>
        void SaveTemplateContent(string content, string filePath);

        /// <summary>
        /// Gets the version history of a template.
        /// </summary>
        /// <param name="templateName">The name of the template.</param>
        /// <returns>A list of template versions.</returns>
        List<TemplateVersion> GetTemplateVersionHistory(string templateName);

        /// <summary>
        /// Restores a template version.
        /// </summary>
        /// <param name="versionPath">The file path of the version to restore.</param>
        /// <param name="templatePath">The file path of the template to restore to.</param>
        void RestoreTemplateVersion(string versionPath, string templatePath);

        /// <summary>
        /// Validates the JSON content of a template.
        /// </summary>
        /// <param name="jsonContent">The JSON content to validate.</param>
        /// <returns>A tuple containing a boolean indicating if the template is valid and error message if not.</returns>
        (bool IsValid, string ErrorMessage) ValidateTemplateContent(string jsonContent);

        /// <summary>
        /// Formats the JSON content of a template.
        /// </summary>
        /// <param name="jsonContent">The JSON content to format.</param>
        /// <returns>The formatted JSON content.</returns>
        string FormatTemplateContent(string jsonContent);

        /// <summary>
        /// Gets the templates directory path.
        /// </summary>
        /// <returns>The path to the templates directory.</returns>
        string GetTemplatesDirectory();

        /// <summary>
        /// Gets the template history directory path.
        /// </summary>
        /// <returns>The path to the template history directory.</returns>
        string GetTemplateHistoryDirectory();
    }
}
