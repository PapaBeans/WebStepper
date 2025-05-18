using System;
using System.Linq;
using System.Threading.Tasks;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;

namespace WebStepper.Core.Application
{
    /// <summary>
    /// Implementation of the template service
    /// </summary>
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ILogService _logService;

        /// <summary>
        /// Event triggered when a template is loaded
        /// </summary>
        public event EventHandler<TemplateEventArgs> TemplateLoaded;

        /// <summary>
        /// Event triggered when a template is saved
        /// </summary>
        public event EventHandler<TemplateEventArgs> TemplateSaved;

        /// <summary>
        /// Creates a new template service
        /// </summary>
        /// <param name="templateRepository">Template repository</param>
        /// <param name="logService">Log service</param>
        public TemplateService(ITemplateRepository templateRepository, ILogService logService)
        {
            _templateRepository = templateRepository ?? throw new ArgumentNullException(nameof(templateRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        /// <inheritdoc/>
        public async Task<Template> LoadTemplateAsync(string path)
        {
            _logService.LogInfo($"Loading template from: {path}");

            try
            {
                var template = await _templateRepository.LoadTemplateAsync(path);

                if (template != null)
                {
                    _logService.LogInfo($"Template loaded: {template.Name}");
                    TemplateLoaded?.Invoke(this, new TemplateEventArgs { Template = template, TemplatePath = path });
                    return template;
                }
                else
                {
                    _logService.LogError("Failed to load template: Template is null");
                    throw new Exception("Failed to load template: Template is null");
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error loading template: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task SaveTemplateAsync(string path, Template template)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            _logService.LogInfo($"Saving template: {template.Name}");

            try
            {
                // Validate the template before saving
                if (!ValidateTemplate(template))
                {
                    _logService.LogError("Template validation failed");
                    throw new Exception("Template validation failed");
                }

                await _templateRepository.SaveTemplateAsync(path, template);
                _logService.LogInfo($"Template saved to: {path}");
                TemplateSaved?.Invoke(this, new TemplateEventArgs { Template = template, TemplatePath = path });
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error saving template: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public Template CreateNewTemplate()
        {
            _logService.LogInfo("Creating new template");

            var template = new Template
            {
                Id = Guid.NewGuid().ToString(),
                Name = "New Template",
                Description = "Template Description",
                Category = "Uncategorized",
                Version = "1.0.0",
                Pages = new System.Collections.Generic.List<Page>
                {
                    new Page
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "First Page",
                        PageIdentifierSelector = "",
                        Steps = new System.Collections.Generic.List<Step>()
                    }
                },
                Variables = new System.Collections.Generic.Dictionary<string, string>()
            };

            return template;
        }

        /// <inheritdoc/>
        public async Task<string[]> GetCategoriesAsync()
        {
            _logService.LogInfo("Getting template categories");

            try
            {
                return await _templateRepository.GetCategoriesAsync();
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error getting template categories: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<string[]> GetTemplatesInCategoryAsync(string category)
        {
            _logService.LogInfo($"Getting templates in category: {category}");

            try
            {
                return await _templateRepository.GetTemplateNamesInCategoryAsync(category);
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error getting templates in category: {ex.Message}");
                throw;
            }
        }

        /// <inheritdoc/>
        public bool ValidateTemplate(Template template)
        {
            if (template == null)
            {
                _logService.LogError("Template is null");
                return false;
            }

            if (string.IsNullOrWhiteSpace(template.Id))
            {
                _logService.LogError("Template ID is missing");
                return false;
            }

            if (string.IsNullOrWhiteSpace(template.Name))
            {
                _logService.LogError("Template name is missing");
                return false;
            }

            if (template.Pages == null || !template.Pages.Any())
            {
                _logService.LogError("Template has no pages");
                return false;
            }

            // Validate each page
            foreach (var page in template.Pages)
            {
                if (string.IsNullOrWhiteSpace(page.Id))
                {
                    _logService.LogError("Page ID is missing");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(page.Name))
                {
                    _logService.LogError("Page name is missing");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(page.PageIdentifierSelector))
                {
                    _logService.LogWarning($"Page identifier selector is missing for page: {page.Name}");
                    // This is a warning, not an error
                }

                // Validate each step
                foreach (var step in page.Steps)
                {
                    if (string.IsNullOrWhiteSpace(step.Id))
                    {
                        _logService.LogError($"Step ID is missing in page: {page.Name}");
                        return false;
                    }

                    if (string.IsNullOrWhiteSpace(step.Name))
                    {
                        _logService.LogError($"Step name is missing in page: {page.Name}");
                        return false;
                    }

                    // Validate step-specific requirements
                    switch (step.Type)
                    {
                        case StepType.WaitForElement:
                        case StepType.ClickButton:
                            if (string.IsNullOrWhiteSpace(step.Selector))
                            {
                                _logService.LogError($"Selector is missing for step: {step.Name}");
                                return false;
                            }
                            break;
                        case StepType.FillForm:
                            if (string.IsNullOrWhiteSpace(step.Selector))
                            {
                                _logService.LogError($"Selector is missing for step: {step.Name}");
                                return false;
                            }
                            // Value can be empty for clearing fields
                            break;
                        case StepType.ExecuteScript:
                            if (string.IsNullOrWhiteSpace(step.Value))
                            {
                                _logService.LogError($"Script value is missing for step: {step.Name}");
                                return false;
                            }
                            break;
                    }
                }
            }

            return true;
        }
    }
}
