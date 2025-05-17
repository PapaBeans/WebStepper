using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using InsuranceAutomation.Models;
using InsuranceAutomation.Services;
using InsuranceAutomation.Services.Interfaces;
using InsuranceAutomation.UI.Dialogs;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Text;
using System.Diagnostics.Eventing.Reader;

namespace InsuranceAutomation.UI
{
    /// <summary>
    /// The main form of the application.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ITemplateService templateService;
        private readonly ILoggingService loggingService;
        private IAutomationService automationService;
        private ElementPickerService elementPickerService;
    
        private AutomationTemplate currentTemplate;
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            templateService = new TemplateService();
            loggingService = new LoggingService();
            
            InitializeServices().ContinueWith(t => 
            {
                if (t.IsFaulted)
                {
                    loggingService.LogError("Error initializing services", t.Exception);
                    MessageBox.Show($"Failed to initialize application: {t.Exception.Message}\n\nMake sure WebView2 Runtime is installed.", 
                        "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                // Only set up event handlers after services are initialized
                SetupEventHandlers();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task InitializeServices()
        {
            try
            {
                // Initialize WebView2
                await Utilities.Utilities.InitializeWebView2Async(webView);
                webView.CoreWebView2.NavigationCompleted += WebView_NavigationCompleted;
                loggingService.LogInfo("WebView2 initialized successfully.");

                // Set up automation service
                automationService = new WebViewAutomationService(webView);
                
                // Initialize element picker service
                elementPickerService = ElementPickerService.Instance;
                await elementPickerService.InitializeAsync(webView);
                elementPickerService.ElementSelected += ElementPickerService_ElementSelected;
                elementPickerService.PickingCanceled += ElementPickerService_PickingCanceled;
                loggingService.LogInfo("Element picker service initialized successfully.");
                
                UpdateStatus("Ready");
            }
            catch (Exception ex)
            {
                loggingService.LogError("Error initializing services", ex);
                throw; // Rethrow to be caught by the continuation in the constructor
            }
        }

        private void SetupEventHandlers()
        {
            // Ensure we're on the UI thread
            if (InvokeRequired)
            {
                Invoke(new Action(SetupEventHandlers));
                return;
            }

            // Automation service events
            if (automationService != null)
            {
                automationService.StepStarted += AutomationService_StepStarted;
                automationService.StepCompleted += AutomationService_StepCompleted;
                automationService.AutomationError += AutomationService_AutomationError;
                automationService.LogMessageGenerated += AutomationService_LogMessageGenerated;
            }
            else
            {
                loggingService.LogWarning("SetupEventHandlers called before automationService was initialized");
            }

            // Logging service events
            loggingService.LogEntryAdded += LoggingService_LogEntryAdded;
        
            // Element picker events
            if (elementPickerService != null)
            {
                elementPickerService.ElementSelected += ElementPickerService_ElementSelected;
                elementPickerService.PickingCanceled += ElementPickerService_PickingCanceled;
            }

            // Form events
            this.FormClosing += MainForm_FormClosing;
        
            loggingService.LogInfo("Event handlers set up successfully");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (automationService != null && automationService.IsRunning)
            {
                var result = MessageBox.Show("Automation is currently running. Are you sure you want to exit?", 
                    "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
        
                // Cancel any running automation
                cancellationTokenSource?.Cancel();
            }
    
            // Clean up element picker
            if (elementPickerService != null)
            {
                elementPickerService.ElementSelected -= ElementPickerService_ElementSelected;
                elementPickerService.PickingCanceled -= ElementPickerService_PickingCanceled;
        
                if (elementPickerService.IsPickingActive)
                {
                    elementPickerService.StopPickingAsync().ConfigureAwait(false);
                }
            }
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                loggingService.LogInfo($"Navigation completed: {webView.Source}");
                UpdateStatus($"Loaded: {webView.Source}");
            
                // Re-initialize element picker after navigation completes
                if (elementPickerService != null)
                {
                    elementPickerService.InitializeAsync(webView).ConfigureAwait(false);
                }
            }
            else
            {
                loggingService.LogError($"Navigation failed with error code: {e.WebErrorStatus}");
                UpdateStatus("Navigation failed");
            }
        }

        private void AutomationService_StepStarted(object sender, AutomationStepEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, AutomationStepEventArgs>(AutomationService_StepStarted), sender, e);
                return;
            }

            UpdateStepListSelection(e.StepIndex);
            UpdateStatus($"Executing step {e.StepIndex + 1} of {currentTemplate?.Steps.Count ?? 0}");
            
            // Add context menu for step troubleshooting during automation
            if (automationService.IsRunning && automationService.IsPaused)
            {
                AddStepTroubleshootingMenu(e.Step);
            }
        }

        private void AutomationService_StepCompleted(object sender, AutomationStepEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, AutomationStepEventArgs>(AutomationService_StepCompleted), sender, e);
                return;
            }
        }

        private void AutomationService_AutomationError(object sender, AutomationErrorEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, AutomationErrorEventArgs>(AutomationService_AutomationError), sender, e);
                return;
            }

            loggingService.LogError($"Automation error at step {e.StepIndex + 1}: {e.Exception.Message}", e.Exception);
        }

        private void AutomationService_LogMessageGenerated(object sender, LogEventArgs e)
        {
            loggingService.LogInfo(e.Message);
        }

        private void LoggingService_LogEntryAdded(object sender, LogEntryEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, LogEntryEventArgs>(LoggingService_LogEntryAdded), sender, e);
                return;
            }

            logTextBox.AppendText(e.LogEntry.GetFormattedText() + Environment.NewLine);
            
            // Scroll to the bottom
            logTextBox.SelectionStart = logTextBox.Text.Length;
            logTextBox.ScrollToCaret();
        }

        private void UpdateStepListSelection(int stepIndex)
        {
            if (stepIndex >= 0 && stepIndex < stepsListBox.Items.Count)
            {
                stepsListBox.SelectedIndex = stepIndex;
            }
        }
    
        private void AddStepTroubleshootingMenu(AutomationStep step)
        {
            if (step == null) return;
        
            // Only add troubleshooting for steps that use selectors
            if (step.Type == "wait_for_element" || step.Type == "click_button" || step.Type == "fill_form")
            {
                // Create context menu if it doesn't exist
                if (stepsListBox.ContextMenuStrip == null)
                {
                    stepsListBox.ContextMenuStrip = new ContextMenuStrip();
                }
                else
                {
                    stepsListBox.ContextMenuStrip.Items.Clear();
                }
            
                // Add pick element menu item
                var pickElementMenuItem = new ToolStripMenuItem("Pick Element for This Step");
                pickElementMenuItem.Click += StepPickElementMenuItem_Click;
                stepsListBox.ContextMenuStrip.Items.Add(pickElementMenuItem);
            
                // Add test selector menu item
                var testSelectorMenuItem = new ToolStripMenuItem("Test Current Selector");
                testSelectorMenuItem.Click += TestSelectorMenuItem_Click;
                stepsListBox.ContextMenuStrip.Items.Add(testSelectorMenuItem);
            }
        }
    
        private async void StepPickElementMenuItem_Click(object sender, EventArgs e)
        {
            if (stepsListBox.SelectedIndex < 0 || currentTemplate == null)
                return;
            
            var selectedStep = currentTemplate.Steps[stepsListBox.SelectedIndex];
            await ShowElementPicker(selectedStep);
        }
    
        private async void TestSelectorMenuItem_Click(object sender, EventArgs e)
        {
            if (stepsListBox.SelectedIndex < 0 || currentTemplate == null)
                return;
            
            var selectedStep = currentTemplate.Steps[stepsListBox.SelectedIndex];
            if (string.IsNullOrEmpty(selectedStep.Selector))
            {
                MessageBox.Show("No selector defined for this step.", "Test Selector", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        
            try
            {
                var result = await elementPickerService.TestSelectorAsync(selectedStep.Selector);
                if (result.IsValid)
                {
                    string message = result.Count == 1 
                        ? "Selector uniquely identifies one element!"
                        : $"Selector matches {result.Count} elements. Consider refining it for better precision.";
                    
                    MessageBox.Show(message, "Selector Test Result", MessageBoxButtons.OK, 
                        result.Count == 1 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                
                    // Highlight the matching elements
                    await elementPickerService.HighlightMatchingElementsAsync(selectedStep.Selector);
                }
                else
                {
                    MessageBox.Show($"Invalid selector: {result.Message}", "Selector Test Failed", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                loggingService.LogError($"Error testing selector: {ex.Message}", ex);
                MessageBox.Show($"Error testing selector: {ex.Message}", "Test Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private async Task ShowElementPicker(AutomationStep step)
        {
            if (webView == null || webView.CoreWebView2 == null)
            {
                MessageBox.Show("WebView is not initialized.", "Element Picker", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Can't use 'using' with Show() since it returns immediately
                var pickerDialog = new SelectorPickerDialog(webView, step.Selector);
                
                // Need to set up an event handler to get the selector when chosen
                pickerDialog.FormClosed += (sender, e) => 
                {
                    // Check if the user selected something (we'd need to add this property)
                    if (pickerDialog.DialogResult == DialogResult.OK)
                    {
                        if (stepsListBox.SelectedIndex >= 0 && currentTemplate != null)
                        {
                            var step = currentTemplate.Steps[stepsListBox.SelectedIndex];

                            // Update the step with the selected selector
                            step.Selector = pickerDialog.SelectedSelector;

                            //Save the template
                            templateService.SaveTemplate(currentTemplate, currentTemplate.FilePath);
                            
                            loggingService.LogInfo($"MainForm_ShowElementPicker: Updated selector for step {stepsListBox.SelectedIndex + 1} to: {step.Selector}");
                        }
                        else
                        {
                            loggingService.LogInfo($"MainForm_ShowElementPicker: Failed to update selector for step {stepsListBox.SelectedIndex + 1} to: {step.Selector}");
                        }
                    }

                    // Dispose the form manually since we're not using 'using'
                    pickerDialog.Dispose();
                };
                
                pickerDialog.Show();
            }
            catch (Exception ex)
            {
                loggingService.LogError($"Error using element picker: {ex.Message}", ex);
                MessageBox.Show($"Error using element picker: {ex.Message}", 
                    "Element Picker Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
        private void ElementPickerService_ElementSelected(object sender, ElementSelectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, ElementSelectedEventArgs>(ElementPickerService_ElementSelected), sender, e);
                return;
            }
        
            if (stepsListBox.SelectedIndex >= 0 && currentTemplate != null && e.SelectorInfo != null)
            {
                var step = currentTemplate.Steps[stepsListBox.SelectedIndex];
            
                // Update the step with the optimal selector
                step.Selector = e.SelectorInfo.Optimal;
            
                // Refresh the steps list
                stepsListBox.Items[stepsListBox.SelectedIndex] = step.GetDisplayText();
            
                loggingService.LogInfo($"ElementPickerService_ElementSelected: Updated selector for step #{stepsListBox.SelectedIndex + 1} to: {step.Selector}");
            }
        }
    
        private void ElementPickerService_PickingCanceled(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object, EventArgs>(ElementPickerService_PickingCanceled), sender, e);
                return;
            }
        
            loggingService.LogInfo("Element picking was cancelled.");
        }

        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), message);
                return;
            }

            statusLabel.Text = message;
        }

        private void NavigateButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(urlTextBox.Text))
            {
                try
                {
                    webView.CoreWebView2.Navigate(urlTextBox.Text);
                    loggingService.LogInfo($"Navigating to: {urlTextBox.Text}");
                    
                    // If element picking is active, stop it when navigating
                    if (elementPickerService.IsPickingActive)
                    {
                        elementPickerService.StopPickingAsync().ConfigureAwait(false);
                    }
                }
                catch (Exception ex)
                {
                    loggingService.LogError($"Navigation error: {ex.Message}", ex);
                    MessageBox.Show($"Failed to navigate: {ex.Message}", "Navigation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadTemplateButton_Click(object sender, EventArgs e)
        {
            using (var selectionForm = new TemplateSelectionForm(templateService))
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    string templatePath = selectionForm.SelectedTemplatePath;
                    if (!string.IsNullOrEmpty(templatePath) && File.Exists(templatePath))
                    {
                        try
                        {
                            LoadTemplateFromPath(templatePath);
                        }
                        catch (Exception ex)
                        {
                            loggingService.LogError($"Error loading template: {ex.Message}", ex);
                            MessageBox.Show($"Failed to load template: {ex.Message}", 
                                "Template Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void NewTemplateButton_Click(object sender, EventArgs e)
        {
            using (var inputForm = new TextInputForm("New Template", "Enter template name:"))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string templateName = inputForm.InputText;
                    if (string.IsNullOrWhiteSpace(templateName))
                    {
                        MessageBox.Show("Template name cannot be empty.", 
                            "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    try
                    {
                        string templatePath = templateService.CreateNewTemplate(templateName);
                        
                        // Ask user which editor they want to use
                        DialogResult editorChoice = MessageBox.Show(
                            "Would you like to use the visual template editor?\n\n" +
                            "Yes = Visual Editor (Recommended)\n" +
                            "No = Text Editor (JSON)",
                            "Choose Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        
                        if (editorChoice == DialogResult.Cancel)
                        {
                            return;
                        }
                        
                        if (editorChoice == DialogResult.Yes)
                        {
                            // Open visual editor
                            using (var visualEditorForm = new Dialogs.VisualTemplateEditorForm(templateService, templatePath))
                            {
                                if (visualEditorForm.ShowDialog() == DialogResult.OK)
                                {
                                    // Load the new template
                                    LoadTemplateFromPath(templatePath);
                                    loggingService.LogInfo($"New template created and loaded with visual editor: {Path.GetFileName(templatePath)}");
                                }
                            }
                        }
                        else
                        {
                            // Open text-based editor
                            using (var editorForm = new TemplateEditorForm(templateService, templatePath))
                            {
                                if (editorForm.ShowDialog() == DialogResult.OK)
                                {
                                    // Load the new template
                                    LoadTemplateFromPath(templatePath);
                                    loggingService.LogInfo($"New template created and loaded: {Path.GetFileName(templatePath)}");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        loggingService.LogError($"Error creating template: {ex.Message}", ex);
                        MessageBox.Show($"Failed to create template: {ex.Message}", 
                            "Template Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void EditTemplateButton_Click(object sender, EventArgs e)
        {
            if (currentTemplate == null || string.IsNullOrEmpty(currentTemplate.FilePath))
            {
                MessageBox.Show("No template is currently loaded.", "Edit Template", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Ask user which editor they want to use
            DialogResult editorChoice = MessageBox.Show(
                "Would you like to use the visual template editor?\n\n" +
                "Yes = Visual Editor (Recommended)\n" +
                "No = Text Editor (JSON)",
                "Choose Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            
            if (editorChoice == DialogResult.Cancel)
            {
                return;
            }
            
            if (editorChoice == DialogResult.Yes)
            {
                // Open visual editor
                OpenVisualEditor();
            }
            else
            {
                // Open text-based editor
                using (var editorForm = new TemplateEditorForm(templateService, currentTemplate.FilePath))
                {
                    if (editorForm.ShowDialog() == DialogResult.OK)
                    {
                        // Reload the template if it was modified
                        try
                        {
                            LoadTemplateFromPath(currentTemplate.FilePath);
                            loggingService.LogInfo("Template reloaded after editing.");
                        }
                        catch (Exception ex)
                        {
                            loggingService.LogError($"Error reloading template: {ex.Message}", ex);
                        }
                    }
                }
            }
        }
        
        private void OpenVisualEditor()
        {
            try
            {
                using (var visualEditorForm = new Dialogs.VisualTemplateEditorForm(templateService, currentTemplate.FilePath))
                {
                    if (visualEditorForm.ShowDialog() == DialogResult.OK)
                    {
                        // Reload the template if it was modified
                        try
                        {
                            LoadTemplateFromPath(currentTemplate.FilePath);
                            loggingService.LogInfo("Template reloaded after visual editing.");
                        }
                        catch (Exception ex)
                        {
                            loggingService.LogError($"Error reloading template after visual editing: {ex.Message}", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loggingService.LogError($"Error opening visual editor: {ex.Message}", ex);
                MessageBox.Show($"Error opening visual editor: {ex.Message}", 
                    "Visual Editor Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VersionHistoryButton_Click(object sender, EventArgs e)
        {
            if (currentTemplate == null || string.IsNullOrEmpty(currentTemplate.FilePath))
            {
                MessageBox.Show("No template is currently loaded.", "Version History", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            using (var historyForm = new VersionHistoryForm(templateService, currentTemplate.FileName))
            {
                if (historyForm.ShowDialog() == DialogResult.OK)
                {
                    // User selected a version to restore
                    string versionPath = historyForm.SelectedVersionPath;
                    if (!string.IsNullOrEmpty(versionPath) && File.Exists(versionPath))
                    {
                        try
                        {
                            // Ask for confirmation before restoring
                            if (MessageBox.Show("Are you sure you want to restore this version? This will overwrite the current template.",
                                "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                // Restore the selected version
                                templateService.RestoreTemplateVersion(versionPath, currentTemplate.FilePath);
                                
                                // Reload the template
                                LoadTemplateFromPath(currentTemplate.FilePath);
                                loggingService.LogInfo($"Template restored from version: {Path.GetFileName(versionPath)}");
                            }
                        }
                        catch (Exception ex)
                        {
                            loggingService.LogError($"Error restoring template version: {ex.Message}", ex);
                            MessageBox.Show($"Failed to restore template version: {ex.Message}", 
                                "Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            if (currentTemplate == null || currentTemplate.Steps.Count == 0)
            {
                MessageBox.Show("Please load a template first.", "No Template", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Set UI state
            startButton.Enabled = false;
            loadTemplateButton.Enabled = false;
            newTemplateButton.Enabled = false;
            editTemplateButton.Enabled = false;
            versionHistoryButton.Enabled = false;
            pauseButton.Enabled = true;
            resumeButton.Enabled = false;
            stepButton.Enabled = false;
            resetButton.Enabled = true;
            
            cancellationTokenSource = new CancellationTokenSource();
            
            try
            {
                // Set the steps in the automation service
                automationService.SetSteps(currentTemplate.Steps);
                
                // If a target URL is specified and different from current URL, navigate to it
                if (!string.IsNullOrEmpty(currentTemplate.TargetUrl) && 
                    webView.Source?.ToString() != currentTemplate.TargetUrl)
                {
                    urlTextBox.Text = currentTemplate.TargetUrl;
                    await automationService.NavigateAsync(currentTemplate.TargetUrl);
                    
                    // Wait a bit for the page to load
                    await Task.Delay(1000);
                }
                
                // Start the automation
                await automationService.RunAsync(cancellationTokenSource.Token);
                
                loggingService.LogInfo("Automation completed successfully.");
                UpdateStatus("Automation completed");
                MessageBox.Show("Automation completed successfully.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OperationCanceledException)
            {
                loggingService.LogInfo("Automation was cancelled.");
                UpdateStatus("Automation cancelled");
            }
            catch (Exception ex)
            {
                loggingService.LogError($"Error during automation: {ex.Message}", ex);
                MessageBox.Show($"Automation error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("Automation failed");
            }
            finally
            {
                // Reset UI state
                ResetUiState();
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            automationService.Pause();
            pauseButton.Enabled = false;
            resumeButton.Enabled = true;
            stepButton.Enabled = true;
            
            // When paused, allow the user to troubleshoot the current step
            if (stepsListBox.SelectedIndex >= 0 && currentTemplate != null && 
                stepsListBox.SelectedIndex < currentTemplate.Steps.Count)
            {
                AddStepTroubleshootingMenu(currentTemplate.Steps[stepsListBox.SelectedIndex]);
            }
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            automationService.Resume();
            pauseButton.Enabled = true;
            resumeButton.Enabled = false;
            stepButton.Enabled = false;
        }

        private async void StepButton_Click(object sender, EventArgs e)
        {
            try
            {
                await automationService.StepAsync(cancellationTokenSource.Token);
            }
            catch (Exception ex)
            {
                loggingService.LogError($"Error executing step: {ex.Message}", ex);
        
                var result = MessageBox.Show(
                    $"Error executing step: {ex.Message}\n\nWould you like to pick a new element for this step?", 
                    "Step Error", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Error);
            
                if (result == DialogResult.Yes && stepsListBox.SelectedIndex >= 0)
                {
                    ShowElementPicker(currentTemplate.Steps[stepsListBox.SelectedIndex]);
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            automationService.Reset();
            cancellationTokenSource?.Cancel();
            ResetUiState();
        }

        private void ExportLogsButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Log files (*.log)|*.log|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialog.Title = "Export Logs";
                saveFileDialog.DefaultExt = "log";
                saveFileDialog.FileName = $"automation_logs_{DateTime.Now:yyyyMMdd_HHmmss}.log";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        loggingService.ExportLogs(saveFileDialog.FileName);
                        loggingService.LogInfo($"Logs exported to: {saveFileDialog.FileName}");
                        MessageBox.Show($"Logs exported successfully to:\n{saveFileDialog.FileName}", 
                            "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        loggingService.LogError($"Error exporting logs: {ex.Message}", ex);
                        MessageBox.Show($"Failed to export logs: {ex.Message}", 
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void LoadTemplateFromPath(string templatePath)
        {
            currentTemplate = templateService.LoadTemplate(templatePath);
        
            // Update list of steps
            stepsListBox.Items.Clear();
            foreach (var step in currentTemplate.Steps)
            {
                stepsListBox.Items.Add(step.GetDisplayText());
            }
        
            // Enable template-related buttons
            editTemplateButton.Enabled = true;
            versionHistoryButton.Enabled = true;
        
            // Update URL text box if target URL is specified
            if (!string.IsNullOrEmpty(currentTemplate.TargetUrl))
            {
                urlTextBox.Text = currentTemplate.TargetUrl;
            }
        
            // Add context menu to steps list box for element picking
            if (stepsListBox.ContextMenuStrip == null)
            {
                stepsListBox.ContextMenuStrip = new ContextMenuStrip();
                var pickElementMenuItem = new ToolStripMenuItem("Pick Element for Selected Step");
                pickElementMenuItem.Click += StepPickElementMenuItem_Click;
                stepsListBox.ContextMenuStrip.Items.Add(pickElementMenuItem);
            }
        
            loggingService.LogInfo($"Loaded template with {currentTemplate.Steps.Count} steps from {Path.GetFileName(templatePath)}");
            UpdateStatus($"Template loaded: {Path.GetFileName(templatePath)}");
        }

        private void ResetUiState()
        {
            startButton.Enabled = true;
            loadTemplateButton.Enabled = true;
            newTemplateButton.Enabled = true;
            editTemplateButton.Enabled = currentTemplate != null;
            versionHistoryButton.Enabled = currentTemplate != null;
            pauseButton.Enabled = false;
            resumeButton.Enabled = false;
            stepButton.Enabled = false;
            
            // Clear selection in the list box
            if (stepsListBox.Items.Count > 0)
            {
                stepsListBox.SelectedIndex = -1;
            }
            
            // If element picking is active, stop it
            if (elementPickerService != null && elementPickerService.IsPickingActive)
            {
                elementPickerService.StopPickingAsync().ConfigureAwait(false);
            }
            
            UpdateStatus("Ready");
        }
    }
}
