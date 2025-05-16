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
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            if (e.IsSuccess)
            {
                loggingService.LogInfo($"Navigation completed: {webView.Source}");
                UpdateStatus($"Loaded: {webView.Source}");
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
                        
                        // Open the template editor
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
                MessageBox.Show($"Error executing step: {ex.Message}", "Step Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            UpdateStatus("Ready");
        }
    }
}
