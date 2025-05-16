using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using InsuranceAutomation.Models;
using InsuranceAutomation.Services.Interfaces;

namespace InsuranceAutomation.UI.Dialogs
{
    /// <summary>
    /// Form for managing template version history.
    /// </summary>
    public partial class VersionHistoryForm : Form
    {
        private readonly ITemplateService templateService;
        private readonly string templateName;
        private List<TemplateVersion> versions;
        
        /// <summary>
        /// Gets the path of the selected template version.
        /// </summary>
        public string SelectedVersionPath { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="VersionHistoryForm"/> class.
        /// </summary>
        /// <param name="templateService">The template service.</param>
        /// <param name="templateName">The name of the template.</param>
        public VersionHistoryForm(ITemplateService templateService, string templateName)
        {
            this.templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
            this.templateName = templateName ?? throw new ArgumentNullException(nameof(templateName));
            
            InitializeComponent();
            
            this.Text = $"Version History - {templateName}";
            
            LoadVersionHistory();
        }
        
        private void LoadVersionHistory()
        {
            versionsListBox.Items.Clear();
            
            versions = templateService.GetTemplateVersionHistory(templateName);
            
            if (versions.Count == 0)
            {
                versionsListBox.Items.Add("(No previous versions available)");
                versionsListBox.Enabled = false;
                restoreButton.Enabled = false;
                viewButton.Enabled = false;
                compareButton.Enabled = false;
            }
            else
            {
                versionsListBox.Enabled = true;
                restoreButton.Enabled = true;
                viewButton.Enabled = true;
                compareButton.Enabled = true;
                
                foreach (var version in versions)
                {
                    versionsListBox.Items.Add(version.DisplayText);
                }
                
                if (versionsListBox.Items.Count > 0)
                {
                    versionsListBox.SelectedIndex = 0;
                }
            }
        }
        
        private void RestoreButton_Click(object sender, EventArgs e)
        {
            if (versionsListBox.SelectedItem == null || !versionsListBox.Enabled)
            {
                MessageBox.Show("No version history available to restore.", 
                    "No History", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            
            int selectedIndex = versionsListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < versions.Count)
            {
                SelectedVersionPath = versions[selectedIndex].FilePath;
                
                if (!File.Exists(SelectedVersionPath))
                {
                    MessageBox.Show($"Version file not found: {SelectedVersionPath}", 
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a valid version to restore.", 
                    "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
            }
        }
        
        private void VersionsListBox_DoubleClick(object sender, EventArgs e)
        {
            if (versionsListBox.SelectedItem != null && versionsListBox.Enabled &&
                versionsListBox.SelectedItem.ToString() != "(No previous versions available)")
            {
                ViewButton_Click(sender, e);
            }
        }
        
        private void ViewButton_Click(object sender, EventArgs e)
        {
            if (versionsListBox.SelectedItem == null || !versionsListBox.Enabled)
            {
                MessageBox.Show("No version selected.", 
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int selectedIndex = versionsListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < versions.Count)
            {
                string versionPath = versions[selectedIndex].FilePath;
                
                if (!File.Exists(versionPath))
                {
                    MessageBox.Show($"Version file not found: {versionPath}", 
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Open in read-only mode
                using (var editorForm = new TemplateEditorForm(templateService, versionPath, true))
                {
                    editorForm.ShowDialog();
                }
            }
        }
        
        private void CompareButton_Click(object sender, EventArgs e)
        {
            if (versionsListBox.SelectedItem == null || !versionsListBox.Enabled)
            {
                MessageBox.Show("No version selected.", 
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int selectedIndex = versionsListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < versions.Count)
            {
                string versionPath = versions[selectedIndex].FilePath;
                
                if (!File.Exists(versionPath))
                {
                    MessageBox.Show($"Version file not found: {versionPath}", 
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Construct current template path to compare with
                string currentTemplatePath = Path.Combine(templateService.GetTemplatesDirectory(), templateName);
                
                if (!File.Exists(currentTemplatePath))
                {
                    MessageBox.Show($"Current template file not found: {currentTemplatePath}", 
                        "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                // Show a simple diff view
                using (var diffForm = new DiffViewForm(templateService, currentTemplatePath, versionPath))
                {
                    diffForm.ShowDialog();
                }
            }
        }
    }
}
