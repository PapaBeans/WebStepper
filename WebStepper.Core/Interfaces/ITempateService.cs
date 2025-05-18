using System;
using System.Threading.Tasks;
using WebStepper.Core.Domain;

namespace WebStepper.Core.Interfaces
{
    /// <summary>
    /// Interface for template service
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Event triggered when a template is loaded
        /// </summary>
        event EventHandler<TemplateEventArgs> TemplateLoaded;

        /// <summary>
        /// Event triggered when a template is saved
        /// </summary>
        event EventHandler<TemplateEventArgs> TemplateSaved;

        /// <summary>
        /// Loads a template from a file
        /// </summary>
        /// <param name="path">Path to the template file</param>
        /// <returns>The loaded template</returns>
        Task<Template> LoadTemplateAsync(string path);

        /// <summary>
        /// Saves a template to a file
        /// </summary>
        /// <param name="path">Path to save the template to</param>
        /// <param name="template">Template to save</param>
        Task SaveTemplateAsync(string path, Template template);

        /// <summary>
        /// Creates a new empty template
        /// </summary>
        /// <returns>A new template</returns>
        Template CreateNewTemplate();

        /// <summary>
        /// Gets all template categories
        /// </summary>
        /// <returns>Array of category names</returns>
        Task<string[]> GetCategoriesAsync();

        /// <summary>
        /// Gets templates in a specific category
        /// </summary>
        /// <param name="category">Category to filter by</param>
        /// <returns>Array of template names</returns>
        Task<string[]> GetTemplatesInCategoryAsync(string category);

        /// <summary>
        /// Validates a template
        /// </summary>
        /// <param name="template">Template to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        bool ValidateTemplate(Template template);
    }

    /// <summary>
    /// Event arguments for template events
    /// </summary>
    public class TemplateEventArgs : EventArgs
    {
        /// <summary>
        /// The template
        /// </summary>
        public Template Template { get; set; }

        /// <summary>
        /// Path to the template file
        /// </summary>
        public string TemplatePath { get; set; }
    }
}
