using System;
using System.Threading.Tasks;
using WebStepper.Core.Domain;

namespace WebStepper.Core.Interfaces
{
    /// <summary>
    /// Interface for accessing and storing templates
    /// </summary>
    public interface ITemplateRepository
    {
        /// <summary>
        /// Gets a list of all available template names
        /// </summary>
        /// <returns>Array of template names</returns>
        Task<string[]> GetTemplateNamesAsync();

        /// <summary>
        /// Gets a list of template names in a specific category
        /// </summary>
        /// <param name="category">Category to filter by</param>
        /// <returns>Array of template names</returns>
        Task<string[]> GetTemplateNamesInCategoryAsync(string category);

        /// <summary>
        /// Gets all available categories
        /// </summary>
        /// <returns>Array of category names</returns>
        Task<string[]> GetCategoriesAsync();

        /// <summary>
        /// Loads a template from storage
        /// </summary>
        /// <param name="path">Path to the template file</param>
        /// <returns>The loaded template</returns>
        Task<Template> LoadTemplateAsync(string path);

        /// <summary>
        /// Saves a template to storage
        /// </summary>
        /// <param name="path">Path to save the template to</param>
        /// <param name="template">Template to save</param>
        Task SaveTemplateAsync(string path, Template template);
    }
}
