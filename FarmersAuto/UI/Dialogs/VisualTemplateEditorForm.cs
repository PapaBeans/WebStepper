using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using InsuranceAutomation.Models;
using InsuranceAutomation.Services;
using InsuranceAutomation.Services.Interfaces;
using InsuranceAutomation.UI.Controls;
using InsuranceAutomation.UI.Dialogs;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InsuranceAutomation.UI.Dialogs
{
    /// <summary>
    /// Visual editor for automation templates.
    /// </summary>
    public partial class VisualTemplateEditorForm : Form
    {
        private readonly ITemplateService templateService;
        private readonly string filePath;
        private readonly bool readOnly;
        private AutomationTemplate template;
        private StepControl selectedStep;
        private bool hasUnsavedChanges = false;
        private bool updatingJson = false;
        private Dictionary<string, List<StepControl>> groupedSteps = new Dictionary<string, List<StepControl>>();
        private WebView2 previewWebView;
        private ElementPickerService elementPickerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualTemplateEditorForm"/> class.
        /// </summary>
        /// <param name="templateService">The template service.</param>
        /// <param name="filePath">The file path of the template to edit.</param>
        /// <param name="readOnly">Whether the template should be opened in read-only mode.</param>
        public VisualTemplateEditorForm(ITemplateService templateService, string filePath, bool readOnly = false)
        {
            this.templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
            this.filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            this.readOnly = readOnly;
            
            InitializeComponent();
            
            // Update form title
            this.Text = readOnly ? 
                $"Visual Template Viewer - {Path.GetFileName(filePath)} (Read Only)" : 
                $"Visual Template Editor - {Path.GetFileName(filePath)}";
            
            // Set read-only mode
            if (readOnly)
            {
                SetReadOnlyMode();
            }
            
            stepsFlowLayoutPanel.AllowDrop = true;
            
            // Initialize element picker service
            elementPickerService = ElementPickerService.Instance;
        }

        private void SetReadOnlyMode()
        {
            // Disable editing controls
            addStepDropDownButton.Enabled = false;
            groupStepsButton.Enabled = false;
            ungroupStepsButton.Enabled = false;
            templatePropertiesButton.Enabled = false;
            saveButton.Enabled = false;
            saveAsButton.Enabled = false;
            validateButton.Enabled = false;
            jsonTextBox.ReadOnly = true;
            propertyGrid.Enabled = false;
        }

        private void VisualTemplateEditorForm_Load(object sender, EventArgs e)
        {
            LoadTemplateFile();
            
            // Try to find a WebView2 control in the parent form
            FindWebView();
        }
        
        private void FindWebView()
        {
            // Try to find the WebView2 control in the owner form
            if (this.Owner is Form ownerForm)
            {
                foreach (Control control in ownerForm.Controls)
                {
                    if (control is WebView2 webView)
                    {
                        previewWebView = webView;
                        InitializeElementPicker();
                        break;
                    }
                    
                    // Search recursively in container controls
                    FindWebViewRecursive(control);
                    
                    if (previewWebView != null)
                        break;
                }
            }
        }
        
        private void FindWebViewRecursive(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                if (child is WebView2 webView)
                {
                    previewWebView = webView;
                    InitializeElementPicker();
                    return;
                }
                
                if (child.Controls.Count > 0)
                {
                    FindWebViewRecursive(child);
                    
                    if (previewWebView != null)
                        return;
                }
            }
        }
        
        private async void InitializeElementPicker()
        {
            if (previewWebView != null && previewWebView.CoreWebView2 != null)
            {
                try
                {
                    await elementPickerService.InitializeAsync(previewWebView);
                    
                    // Subscribe to element picker events
                    elementPickerService.ElementSelected += ElementPickerService_ElementSelected;
                    elementPickerService.PickingCanceled += ElementPickerService_PickingCanceled;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error initializing element picker: {ex.Message}", 
                        "Element Picker Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadTemplateFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    template = templateService.LoadTemplate(filePath);
                    
                    // Initialize template properties UI
                    templateNameTextBox.Text = Path.GetFileNameWithoutExtension(template.FileName);
                    templateUrlTextBox.Text = template.TargetUrl;
                    templateDescriptionTextBox.Text = template.Description;
                    
                    // Load steps into visual editor
                    LoadStepsIntoEditor();
                    
                    // Update JSON preview
                    UpdateJsonPreview();
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

        private void LoadStepsIntoEditor()
        {
            stepsFlowLayoutPanel.SuspendLayout();
            stepsFlowLayoutPanel.Controls.Clear();
            groupedSteps.Clear();
            
            for (int i = 0; i < template.Steps.Count; i++)
            {
                var stepControl = new StepControl(template.Steps[i], i);
                stepControl.Width = stepsFlowLayoutPanel.Width - 25; // Leave space for scrollbar
                stepControl.StepSelected += StepControl_StepSelected;
                stepControl.StepChanged += StepControl_StepChanged;
                stepControl.DragStarted += StepControl_DragStarted;
                stepControl.ContextMenuStrip = stepContextMenuStrip;
                
                stepsFlowLayoutPanel.Controls.Add(stepControl);
            }
            
            stepsFlowLayoutPanel.ResumeLayout();
        }

        private void UpdateJsonPreview()
        {
            if (updatingJson) return;
            
            updatingJson = true;
            try
            {
                string json = JsonConvert.SerializeObject(template, Formatting.Indented);
                jsonTextBox.Text = json;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating JSON preview: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                updatingJson = false;
            }
        }

        private void StepControl_StepSelected(object sender, StepControlEventArgs e)
        {
            // Deselect previously selected step
            if (selectedStep != null && selectedStep != e.StepControl)
            {
                selectedStep.IsSelected = false;
            }
            
            // Select new step
            selectedStep = e.StepControl;
            selectedStep.IsSelected = true;
            
            // Update property grid
            propertyGrid.SelectedObject = selectedStep.Step;
            
            // Add custom menu item for picking elements if applicable
            if (selectedStep.Step.Type == "wait_for_element" || 
                selectedStep.Step.Type == "click_button" || 
                selectedStep.Step.Type == "fill_form")
            {
                // Add context menu option for element picking
                AddElementPickerContextMenu();
            }
        }
        
        private void AddElementPickerContextMenu()
        {
            // Check if the menu item already exists
            bool hasPickerMenuItem = false;
            foreach (ToolStripItem item in stepContextMenuStrip.Items)
            {
                if (item.Name == "pickElementToolStripMenuItem")
                {
                    hasPickerMenuItem = true;
                    break;
                }
            }
            
            if (!hasPickerMenuItem && previewWebView != null)
            {
                // Add a separator if there are other items
                if (stepContextMenuStrip.Items.Count > 0)
                {
                    stepContextMenuStrip.Items.Add(new ToolStripSeparator());
                }
                
                // Add element picker menu item
                var pickElementMenuItem = new ToolStripMenuItem("Pick Element from Web Page");
                pickElementMenuItem.Name = "pickElementToolStripMenuItem";
                pickElementMenuItem.Click += PickElementMenuItem_Click;
                stepContextMenuStrip.Items.Add(pickElementMenuItem);
            }
        }
        
        private async void PickElementMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedStep == null || previewWebView == null)
                return;
                
            ShowSelectorPicker(selectedStep.Step);
        }
        
        private void ShowSelectorPicker(AutomationStep step)
        {
            if (previewWebView == null || previewWebView.CoreWebView2 == null)
            {
                MessageBox.Show("Element picker requires a web page to be loaded.",
                    "Element Picker", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            using (var pickerDialog = new SelectorPickerDialog(previewWebView, step.Selector))
            {
                if (pickerDialog.ShowDialog() == DialogResult.OK)
                {
                    // Update the step with the selected selector
                    step.Selector = pickerDialog.SelectedSelector;
                    
                    // Update the UI
                    if (selectedStep != null)
                    {
                        selectedStep.UpdateUI();
                        
                        // Update JSON preview
                        SynchronizeTemplateWithControls();
                        UpdateJsonPreview();
                        
                        hasUnsavedChanges = true;
                    }
                }
            }
        }
        
        private void ElementPickerService_ElementSelected(object sender, ElementSelectedEventArgs e)
        {
            if (selectedStep != null && e.SelectorInfo != null)
            {
                // Update the selected step with the new selector
                selectedStep.Step.Selector = e.SelectorInfo.Optimal;
                
                // Update the UI
                selectedStep.UpdateUI();
                
                // Update the template and JSON preview
                SynchronizeTemplateWithControls();
                UpdateJsonPreview();
                
                hasUnsavedChanges = true;
            }
        }
        
        private void ElementPickerService_PickingCanceled(object sender, EventArgs e)
        {
            // Handle cancellation if needed
        }

        private void StepControl_StepChanged(object sender, StepControlEventArgs e)
        {
            // Update the template with the new step values
            SynchronizeTemplateWithControls();
            
            // Update the JSON preview
            UpdateJsonPreview();
            
            hasUnsavedChanges = true;
        }

        private void StepControl_DragStarted(object sender, StepControlEventArgs e)
        {
            if (selectedStep != e.StepControl)
            {
                StepControl_StepSelected(sender, e);
            }
        }

        private void SynchronizeTemplateWithControls()
        {
            // Update template steps from controls
            template.Steps.Clear();
            foreach (Control control in stepsFlowLayoutPanel.Controls)
            {
                if (control is StepControl stepControl && !stepControl.IsGroupHeader)
                {
                    template.Steps.Add(stepControl.Step);
                }
            }
            
            // Update template properties
            template.Description = templateDescriptionTextBox.Text;
            template.TargetUrl = templateUrlTextBox.Text;
        }

        private void StepsFlowLayoutPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(StepControl)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void StepsFlowLayoutPanel_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(StepControl)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            
            e.Effect = DragDropEffects.Move;
            
            // Get the client coordinates of the mouse position
            Point clientPoint = stepsFlowLayoutPanel.PointToClient(new Point(e.X, e.Y));
            
            // Get the control at the mouse position
            Control controlAtPoint = GetChildControlAtPoint(stepsFlowLayoutPanel, clientPoint);
            
            if (controlAtPoint is StepControl targetControl)
            {
                // Highlight the insertion point
                // This could be done by adding a temporary visual indicator
                // For simplicity, we'll just change the border color of the target control
                foreach (Control control in stepsFlowLayoutPanel.Controls)
                {
                    if (control is StepControl stepControl)
                    {
                        stepControl.BackColor = SystemColors.Control;
                    }
                }
                
                targetControl.BackColor = Color.LightBlue;
            }
        }

        private void StepsFlowLayoutPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(StepControl)))
            {
                return;
            }
            
            // Get the dragged step control
            var draggedControl = (StepControl)e.Data.GetData(typeof(StepControl));
            
            // Get the client coordinates of the mouse position
            Point clientPoint = stepsFlowLayoutPanel.PointToClient(new Point(e.X, e.Y));
            
            // Get the control at the mouse position
            Control controlAtPoint = GetChildControlAtPoint(stepsFlowLayoutPanel, clientPoint);
            
            // Get the index of the control at the drop point
            int targetIndex = controlAtPoint != null 
                ? stepsFlowLayoutPanel.Controls.GetChildIndex(controlAtPoint) 
                : stepsFlowLayoutPanel.Controls.Count - 1;
            
            // Get the current index of the dragged control
            int sourceIndex = stepsFlowLayoutPanel.Controls.GetChildIndex(draggedControl);
            
            // Adjust the target index if dragging from a higher index to a lower index
            if (sourceIndex < targetIndex)
            {
                targetIndex--;
            }
            
            // Reorder controls
            stepsFlowLayoutPanel.Controls.SetChildIndex(draggedControl, targetIndex);
            
            // Reset the highlight
            foreach (Control control in stepsFlowLayoutPanel.Controls)
            {
                if (control is StepControl stepControl)
                {
                    stepControl.BackColor = SystemColors.Control;
                }
            }
            
            // Update step indices
            UpdateStepIndices();
            
            // Update the template
            SynchronizeTemplateWithControls();
            
            // Update the JSON preview
            UpdateJsonPreview();
            
            hasUnsavedChanges = true;
        }

        private Control GetChildControlAtPoint(Control container, Point point)
        {
            foreach (Control child in container.Controls)
            {
                if (child.Bounds.Contains(point))
                {
                    return child;
                }
            }
            return null;
        }

        private void UpdateStepIndices()
        {
            for (int i = 0; i < stepsFlowLayoutPanel.Controls.Count; i++)
            {
                if (stepsFlowLayoutPanel.Controls[i] is StepControl stepControl)
                {
                    stepControl.StepIndex = i;
                }
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
                // First synchronize the template with the controls
                SynchronizeTemplateWithControls();
                
                // Validate the template
                var validationResult = ValidateTemplate();
                if (!validationResult.IsValid)
                {
                    MessageBox.Show($"Invalid template: {validationResult.ErrorMessage}",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.None;
                    return;
                }

                templateService.SaveTemplate(template, savePath);
                
                // Update form title if saving to a different file
                if (savePath != filePath)
                {
                    this.Text = $"Visual Template Editor - {Path.GetFileName(savePath)}";
                }
                
                MessageBox.Show("Template saved successfully.", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                hasUnsavedChanges = false;
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
            var result = ValidateTemplate();
            if (result.IsValid)
            {
                MessageBox.Show("Template validation successful!", 
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Template validation failed: {result.ErrorMessage}", 
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private (bool IsValid, string ErrorMessage) ValidateTemplate()
        {
            if (template == null)
            {
                return (false, "Template is null");
            }
            
            // Check for empty steps
            if (template.Steps == null || template.Steps.Count == 0)
            {
                return (false, "Template must have at least one step");
            }
            
            // Validate each step
            for (int i = 0; i < template.Steps.Count; i++)
            {
                var step = template.Steps[i];
                switch (step.Type)
                {
                    case "fill_form":
                        if (string.IsNullOrEmpty(step.Selector) || string.IsNullOrEmpty(step.Value))
                        {
                            return (false, $"Step {i + 1}: Fill Form step must have selector and value");
                        }
                        break;
                    case "click_button":
                    case "wait_for_element":
                        if (string.IsNullOrEmpty(step.Selector))
                        {
                            return (false, $"Step {i + 1}: {step.Type} step must have a selector");
                        }
                        break;
                    case "execute_script":
                        if (string.IsNullOrEmpty(step.Script))
                        {
                            return (false, $"Step {i + 1}: Execute Script step must have a script");
                        }
                        break;
                    default:
                        return (false, $"Step {i + 1}: Unknown step type '{step.Type}'");
                }
            }
            
            return (true, null);
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (selectedStep != null)
            {
                // Update the step control UI
                selectedStep.UpdateUI();
                
                // Update the template and JSON preview
                SynchronizeTemplateWithControls();
                UpdateJsonPreview();
                
                hasUnsavedChanges = true;
                
                // If the property is a selector, check if we want to show the picker button
                if (e.PropertyDescriptor.Name == "Selector")
                {
                    // Show selector picker dialog
                    ShowSelectorPicker(selectedStep.Step);
                }
            }
        }

        private void WaitForElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewStep("wait_for_element");
        }

        private void FillFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewStep("fill_form");
        }

        private void ClickButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewStep("click_button");
        }

        private void ExecuteScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewStep("execute_script");
        }

        private void AddNewStep(string stepType)
        {
            AutomationStep newStep = new AutomationStep
            {
                Type = stepType,
                Description = $"New {stepType} step"
            };
            
            switch (stepType)
            {
                case "fill_form":
                    newStep.Selector = "#example";
                    newStep.Value = "example value";
                    break;
                case "click_button":
                case "wait_for_element":
                    newStep.Selector = "#example";
                    break;
                case "execute_script":
                    newStep.Script = "console.log('Hello, world!');";
                    break;
            }
            
            // Add to template
            template.Steps.Add(newStep);
            
            // Add to editor
            var stepControl = new StepControl(newStep, template.Steps.Count - 1);
            stepControl.Width = stepsFlowLayoutPanel.Width - 25;
            stepControl.StepSelected += StepControl_StepSelected;
            stepControl.StepChanged += StepControl_StepChanged;
            stepControl.DragStarted += StepControl_DragStarted;
            stepControl.ContextMenuStrip = stepContextMenuStrip;
            
            stepsFlowLayoutPanel.Controls.Add(stepControl);
            
            // Select the new step
            StepControl_StepSelected(stepControl, new StepControlEventArgs(stepControl));
            
            // Update JSON preview
            UpdateJsonPreview();
            
            hasUnsavedChanges = true;
        }

        private void EditStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedStep != null)
            {
                // The step is already selected, so the property grid is already showing it
                // We could show a custom editor if needed
            }
        }

        private void DuplicateStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedStep != null)
            {
                // Create a deep copy of the step
                string json = JsonConvert.SerializeObject(selectedStep.Step);
                var duplicatedStep = JsonConvert.DeserializeObject<AutomationStep>(json);
                
                // Add to template
                template.Steps.Add(duplicatedStep);
                
                // Add to editor
                var stepControl = new StepControl(duplicatedStep, template.Steps.Count - 1);
                stepControl.Width = stepsFlowLayoutPanel.Width - 25;
                stepControl.StepSelected += StepControl_StepSelected;
                stepControl.StepChanged += StepControl_StepChanged;
                stepControl.DragStarted += StepControl_DragStarted;
                stepControl.ContextMenuStrip = stepContextMenuStrip;
                
                stepsFlowLayoutPanel.Controls.Add(stepControl);
                
                // Select the new step
                StepControl_StepSelected(stepControl, new StepControlEventArgs(stepControl));
                
                // Update JSON preview
                UpdateJsonPreview();
                
                hasUnsavedChanges = true;
            }
        }

        private void DeleteStepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedStep != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this step?", 
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Remove from template
                    template.Steps.RemoveAt(selectedStep.StepIndex);
                    
                    // Remove from editor
                    stepsFlowLayoutPanel.Controls.Remove(selectedStep);
                    
                    // Update indices
                    UpdateStepIndices();
                    
                    // Clear selection
                    selectedStep = null;
                    propertyGrid.SelectedObject = null;
                    
                    // Update JSON preview
                    UpdateJsonPreview();
                    
                    hasUnsavedChanges = true;
                }
            }
        }

        private void GroupStepsButton_Click(object sender, EventArgs e)
        {
            // Get selected steps
            List<StepControl> selectedSteps = new List<StepControl>();
            foreach (Control control in stepsFlowLayoutPanel.Controls)
            {
                if (control is StepControl stepControl && stepControl.IsSelected)
                {
                    selectedSteps.Add(stepControl);
                }
            }
            
            if (selectedSteps.Count < 2)
            {
                MessageBox.Show("Please select at least two steps to group", 
                    "Group Steps", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Prompt for group name
            using (var inputForm = new TextInputForm("Group Steps", "Enter group name:"))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string groupName = inputForm.InputText;
                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        groupName = "Group " + (groupedSteps.Count + 1);
                    }
                    
                    // Create a header for the group
                    var groupHeader = new StepControl();
                    groupHeader.IsGroupHeader = true;
                    groupHeader.GroupName = groupName;
                    groupHeader.Width = stepsFlowLayoutPanel.Width - 25;
                    groupHeader.StepSelected += StepControl_StepSelected;
                    groupHeader.DragStarted += StepControl_DragStarted;
                    groupHeader.ContextMenuStrip = stepContextMenuStrip;
                    
                    // Add header at the position of the first selected step
                    int insertPosition = stepsFlowLayoutPanel.Controls.GetChildIndex(selectedSteps[0]);
                    stepsFlowLayoutPanel.Controls.Add(groupHeader);
                    stepsFlowLayoutPanel.Controls.SetChildIndex(groupHeader, insertPosition);
                    
                    // Add selected steps to the group
                    groupedSteps[groupName] = selectedSteps;
                    
                    // Update indices
                    UpdateStepIndices();
                    
                    hasUnsavedChanges = true;
                }
            }
        }

        private void UngroupStepsButton_Click(object sender, EventArgs e)
        {
            if (selectedStep != null && selectedStep.IsGroupHeader)
            {
                string groupName = selectedStep.GroupName;
                
                if (groupedSteps.ContainsKey(groupName))
                {
                    // Remove group header
                    stepsFlowLayoutPanel.Controls.Remove(selectedStep);
                    
                    // Remove group from dictionary
                    groupedSteps.Remove(groupName);
                    
                    // Update indices
                    UpdateStepIndices();
                    
                    // Clear selection
                    selectedStep = null;
                    propertyGrid.SelectedObject = null;
                    
                    hasUnsavedChanges = true;
                }
            }
            else
            {
                MessageBox.Show("Please select a group header to ungroup steps", 
                    "Ungroup Steps", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TemplatePropertiesButton_Click(object sender, EventArgs e)
        {
            // Toggle template properties panel visibility
            templatePropertiesPanel.Visible = !templatePropertiesPanel.Visible;
            
            // Adjust layout
            if (templatePropertiesPanel.Visible)
            {
                templatePropertiesPanel.Height = 80;
            }
            else
            {
                templatePropertiesPanel.Height = 1;
            }
        }

        private void TemplatePropertyChanged(object sender, EventArgs e)
        {
            // Update template properties
            template.Description = templateDescriptionTextBox.Text;
            template.TargetUrl = templateUrlTextBox.Text;
            
            // Update JSON preview
            UpdateJsonPreview();
            
            hasUnsavedChanges = true;
        }

        private void VisualTemplateEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            // Unsubscribe from element picker events
            if (elementPickerService != null)
            {
                elementPickerService.ElementSelected -= ElementPickerService_ElementSelected;
                elementPickerService.PickingCanceled -= ElementPickerService_PickingCanceled;
                
                // Stop element picking if active
                if (elementPickerService.IsPickingActive)
                {
                    elementPickerService.StopPickingAsync().ConfigureAwait(false);
                }
            }
            
            if (readOnly)
                return;

            if (hasUnsavedChanges && e.CloseReason == CloseReason.UserClosing &&
                this.DialogResult != DialogResult.OK && this.DialogResult != DialogResult.Cancel)
            {
                DialogResult result = MessageBox.Show(
                    "Do you want to save changes before closing?",
                    "Save Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SaveButton_Click(this, e);
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
}