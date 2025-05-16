using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using InsuranceAutomation.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InsuranceAutomation.UI.Dialogs
{
    /// <summary>
    /// Form for editing automation templates.
    /// </summary>
    public partial class TemplateEditorForm : Form
    {
        private readonly ITemplateService templateService;
        private readonly string filePath;
        private readonly bool readOnly;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateEditorForm"/> class.
        /// </summary>
        /// <param name="templateService">The template service.</param>
        /// <param name="filePath">The file path of the template to edit.</param>
        /// <param name="readOnly">Whether the template should be opened in read-only mode.</param>
        public TemplateEditorForm(ITemplateService templateService, string filePath, bool readOnly = false)
        {
            this.templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this.readOnly = readOnly;
            
            InitializeComponent();
            
            // Update form title
            this.Text = readOnly ? 
                $"Template Viewer - {Path.GetFileName(filePath)} (Read Only)" : 
                $"Template Editor - {Path.GetFileName(filePath)}";
            
            // Set read-only mode
            jsonTextBox.ReadOnly = readOnly;
            
            // Hide buttons in read-only mode
            if (readOnly)
            {
                saveButton.Visible = false;
                saveAsButton.Visible = false;
                validateButton.Visible = false;
                formatButton.Visible = false;
            }
            
            LoadTemplateFile();
        }

        private void LoadTemplateFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    jsonTextBox.Text = templateService.FormatTemplateContent(json);
                }
                else
                {
                    MessageBox.Show($"Template file not found: {filePath}", 
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading template file: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveTemplate(filePath);
        }

        private void SaveAsButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
                saveFileDialog.Title = "Save Template As";
                saveFileDialog.DefaultExt = "json";
                saveFileDialog.InitialDirectory = templateService.GetTemplatesDirectory();
                saveFileDialog.FileName = Path.GetFileName(filePath);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveTemplate(saveFileDialog.FileName);
                }
            }
        }

        private void SaveTemplate(string savePath)
        {
            try
            {
                // First validate the JSON
                var validationResult = templateService.ValidateTemplateContent(jsonTextBox.Text);
                if (!validationResult.IsValid)
                {
                    MessageBox.Show($"Invalid template: {validationResult.ErrorMessage}",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                templateService.SaveTemplateContent(jsonTextBox.Text, savePath);
                
                // Update form title if saving to a different file
                if (savePath != filePath)
                {
                    this.Text = $"Template Editor - {Path.GetFileName(savePath)}";
                }
                
                MessageBox.Show("Template saved successfully.", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        private void ValidateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var validationResult = templateService.ValidateTemplateContent(jsonTextBox.Text);
                if (validationResult.IsValid)
                {
                    MessageBox.Show("Template validation successful!", 
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Template validation failed: {validationResult.ErrorMessage}", 
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating template: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatButton_Click(object sender, EventArgs e)
        {
            try
            {
                string formattedJson = templateService.FormatTemplateContent(jsonTextBox.Text);
                jsonTextBox.Text = formattedJson;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Invalid JSON: {ex.Message}", 
                    "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error formatting JSON: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TemplateEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (readOnly)
                return;

            if (e.CloseReason == CloseReason.UserClosing && 
                this.DialogResult != DialogResult.OK && 
                this.DialogResult != DialogResult.Cancel)
            {
                // Only prompt if changes might be lost
                if (HasUnsavedChanges())
                {
                    DialogResult result = MessageBox.Show(
                        "Do you want to save changes before closing?",
                        "Save Changes",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        SaveButton_Click(sender, e);
                        // If saving failed, cancel closing
                        if (this.DialogResult != DialogResult.OK)
                        {
                            e.Cancel = true;
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private bool HasUnsavedChanges()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string currentContent = File.ReadAllText(filePath);
                    
                    // Compare after formatting both to ignore whitespace differences
                    JToken currentJson = JToken.Parse(currentContent);
                    JToken editorJson = JToken.Parse(jsonTextBox.Text);
                    
                    return !JToken.DeepEquals(currentJson, editorJson);
                }
                return !string.IsNullOrWhiteSpace(jsonTextBox.Text);
            }
            catch
            {
                // If parsing fails, assume there are changes
                return true;
            }
        }
    }
}
