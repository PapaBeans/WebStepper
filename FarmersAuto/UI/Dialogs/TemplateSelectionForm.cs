using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using InsuranceAutomation.Services.Interfaces;

namespace InsuranceAutomation.UI.Dialogs
{
    /// <summary>
    /// Dialog for selecting an existing template.
    /// </summary>
    public partial class TemplateSelectionForm : Form
    {
        private readonly ITemplateService templateService;
        
        /// <summary>
        /// Gets the path of the selected template.
        /// </summary>
        public string SelectedTemplatePath { get; private set; }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateSelectionForm"/> class.
        /// </summary>
        /// <param name="templateService">The template service.</param>
        public TemplateSelectionForm(ITemplateService templateService)
        {
            this.templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
            InitializeComponent();
            LoadTemplateList();
        }
        
        private void LoadTemplateList()
        {
            templatesListBox.Items.Clear();
            
            List<string> templates = templateService.GetAvailableTemplates();
            
            if (templates.Count == 0)
            {
                templatesListBox.Items.Add("(No templates available)");
                templatesListBox.Enabled = false;
            }
            else
            {
                templatesListBox.Enabled = true;
                foreach (string template in templates)
                {
                    templatesListBox.Items.Add(template);
                }
                
                if (templatesListBox.Items.Count > 0)
                {
                    templatesListBox.SelectedIndex = 0;
                }
            }
        }
        
        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (templatesListBox.SelectedItem == null || !templatesListBox.Enabled)
            {
                MessageBox.Show("Please select a template or create a new one.", 
                    "No Template Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            
            string templateName = templatesListBox.SelectedItem.ToString();
            if (templateName == "(No templates available)")
            {
                MessageBox.Show("There are no templates available. Please create a new template.", 
                    "No Templates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            
            SelectedTemplatePath = Path.Combine(templateService.GetTemplatesDirectory(), templateName);
            
            if (!File.Exists(SelectedTemplatePath))
            {
                MessageBox.Show($"Template file not found: {SelectedTemplatePath}", 
                    "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                return;
            }
        }
        
        private void TemplatesListBox_DoubleClick(object sender, EventArgs e)
        {
            ListBox templatesListBox = sender as ListBox;
            if (templatesListBox.SelectedItem != null && templatesListBox.Enabled &&
                templatesListBox.SelectedItem.ToString() != "(No templates available)")
            {
                SelectButton_Click(sender, e);
                if (this.DialogResult != DialogResult.None)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            LoadTemplateList();
        }
    }
}
