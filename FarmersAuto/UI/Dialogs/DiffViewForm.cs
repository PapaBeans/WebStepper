using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using InsuranceAutomation.Services.Interfaces;

namespace InsuranceAutomation.UI.Dialogs
{
    /// <summary>
    /// A simple form for viewing differences between template versions.
    /// </summary>
    public partial class DiffViewForm : Form
    {
        private readonly ITemplateService templateService;
        private readonly string currentFilePath;
        private readonly string versionFilePath;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DiffViewForm"/> class.
        /// </summary>
        /// <param name="templateService">The template service.</param>
        /// <param name="currentFilePath">The current template file path.</param>
        /// <param name="versionFilePath">The version template file path.</param>
        public DiffViewForm(ITemplateService templateService, string currentFilePath, string versionFilePath)
        {
            this.templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
            this.currentFilePath = currentFilePath ?? throw new ArgumentNullException(nameof(currentFilePath));
            this.versionFilePath = versionFilePath ?? throw new ArgumentNullException(nameof(versionFilePath));
            
            InitializeComponent();
            
            // Update form title
            string currentFileName = Path.GetFileName(currentFilePath);
            string versionFileName = Path.GetFileName(versionFilePath);
            this.Text = $"Template Comparison - {currentFileName} vs {versionFileName}";
            
            LoadTemplates();
        }
        
        private void LoadTemplates()
        {
            try
            {
                // Load current template
                string currentContent = File.ReadAllText(currentFilePath);
                string formattedCurrentContent = templateService.FormatTemplateContent(currentContent);
                currentTextBox.Text = formattedCurrentContent;
                
                // Load version template
                string versionContent = File.ReadAllText(versionFilePath);
                string formattedVersionContent = templateService.FormatTemplateContent(versionContent);
                versionTextBox.Text = formattedVersionContent;
                
                // Highlight differences (basic implementation - could be enhanced)
                HighlightDifferences();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading templates for comparison: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void HighlightDifferences()
        {
            // This is a simple placeholder implementation.
            // A real implementation would use a proper diff algorithm
            // and highlight specific differences with colors.
            
            // For now, we'll just check if the contents are different
            if (currentTextBox.Text != versionTextBox.Text)
            {
                differenceLabel.Text = "The templates are different. For detailed differences, please review manually.";
                differenceLabel.ForeColor = Color.Red;
            }
            else
            {
                differenceLabel.Text = "The templates are identical.";
                differenceLabel.ForeColor = Color.Green;
            }
        }
    }
}
