using WebStepper.Core.Application;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;
using WebStepper.Infrastructure;
using WebStepper.UI.Controls;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Windows.Forms;

namespace WebStepper.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly ITemplateService _templateService;
        private readonly IAutomationEngine _automationEngine;
        private readonly IElementPickerService _elementPickerService;
        private readonly ILogService _logService;
        private readonly IPageTrackerService _pageTrackerService;

        // WebView2 bridge
        private IWebView2Bridge _webView2Bridge;

        // State
        private Template _currentTemplate;
        private bool _isAutomationRunning;
        private bool _isAutomationPaused;

        public MainForm(
            ITemplateService templateService,
            IAutomationEngine automationEngine,
            IElementPickerService elementPickerService,
            ILogService logService,
            IPageTrackerService pageTrackerService)
        {
            _templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
            _automationEngine = automationEngine ?? throw new ArgumentNullException(nameof(automationEngine));
            _elementPickerService = elementPickerService ?? throw new ArgumentNullException(nameof(elementPickerService));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _pageTrackerService = pageTrackerService ?? throw new ArgumentNullException(nameof(pageTrackerService));

            InitializeComponent();

            // Subscribe to events
            Load += MainForm_Load;
            FormClosing += MainForm_FormClosing;
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize WebView2
                await webView.EnsureCoreWebView2Async();

                // Create WebView2 bridge
                _webView2Bridge = new WebView2Bridge(webView, _logService);

                // Initialize services with the WebView2Bridge
                ((ElementPickerService)_elementPickerService).Initialize(_webView2Bridge);
                ((PageTrackerService)_pageTrackerService).Initialize(_webView2Bridge);
                ((AutomationEngine)_automationEngine).Initialize(_webView2Bridge);

                // Register event handlers
                RegisterEventHandlers();

                // Navigate to a default page
                webView.CoreWebView2.Navigate("https://www.google.com");

                _logService.LogInfo("Application started successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing application: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error initializing application: {ex.Message}");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop any running automation
            if (_isAutomationRunning)
            {
                _automationEngine.StopAutomation();
            }

            _logService.LogInfo("Application shutting down");
        }

        private void RegisterEventHandlers()
        {
            // Toolbar events
            toolbarControl.OpenTemplateClicked += OnOpenTemplateClicked;
            toolbarControl.SaveTemplateClicked += OnSaveTemplateClicked;
            toolbarControl.NewTemplateClicked += OnNewTemplateClicked;
            toolbarControl.StartAutomationClicked += OnStartAutomationClicked;
            toolbarControl.PauseAutomationClicked += OnPauseAutomationClicked;
            toolbarControl.ResumeAutomationClicked += OnResumeAutomationClicked;
            toolbarControl.StepAutomationClicked += OnStepAutomationClicked;
            toolbarControl.ResetAutomationClicked += OnResetAutomationClicked;
            toolbarControl.PickElementClicked += OnPickElementClicked;

            // Template editor events
            templateEditorControl.TemplateChanged += OnTemplateChanged;

            // Step tree events
            stepTreeControl.StepSelected += OnStepSelected;
            stepTreeControl.TemplateChanged += OnTemplateChanged;

            // Selector panel events
            selectorPanelControl.SelectorInserted += OnSelectorInserted;
            selectorPanelControl.StepAdded += OnStepAdded;

            // Automation engine events
            _automationEngine.StepExecutionStarted += OnStepExecutionStarted;
            _automationEngine.StepExecutionCompleted += OnStepExecutionCompleted;
            _automationEngine.PageChanged += OnPageChanged;
            _automationEngine.StatusChanged += OnAutomationStatusChanged;

            // Element picker events
            _elementPickerService.SelectorOptionsGenerated += OnSelectorOptionsGenerated;

            // Log service events
            _logService.LogEntryAdded += OnLogEntryAdded;

            // Page tracker events
            _pageTrackerService.PageDetected += OnPageDetected;
        }

        #region Event Handlers

        private async void OnOpenTemplateClicked(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json",
                    Title = "Open Template"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _currentTemplate = await _templateService.LoadTemplateAsync(dialog.FileName);
                    templateEditorControl.SetTemplate(_currentTemplate);
                    stepTreeControl.SetTemplate(_currentTemplate);
                    _logService.LogInfo($"Template loaded: {dialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading template: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error loading template: {ex.Message}");
            }
        }

        private async void OnSaveTemplateClicked(object sender, EventArgs e)
        {
            if (_currentTemplate == null)
            {
                MessageBox.Show("No template to save", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logService.LogWarning("No template to save");
                return;
            }

            try
            {
                SaveFileDialog dialog = new SaveFileDialog
                {
                    Filter = "JSON Files (*.json)|*.json",
                    Title = "Save Template",
                    FileName = _currentTemplate.Name + ".json"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    await _templateService.SaveTemplateAsync(dialog.FileName, _currentTemplate);
                    _logService.LogInfo($"Template saved: {dialog.FileName}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error saving template: {ex.Message}");
            }
        }

        private void OnNewTemplateClicked(object sender, EventArgs e)
        {
            try
            {
                _currentTemplate = _templateService.CreateNewTemplate();
                templateEditorControl.SetTemplate(_currentTemplate);
                stepTreeControl.SetTemplate(_currentTemplate);
                _logService.LogInfo("New template created");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating new template: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error creating new template: {ex.Message}");
            }
        }

        private async void OnStartAutomationClicked(object sender, EventArgs e)
        {
            if (_currentTemplate == null)
            {
                MessageBox.Show("No template loaded", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _logService.LogWarning("No template loaded");
                return;
            }

            try
            {
                await _automationEngine.StartAutomation(_currentTemplate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting automation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error starting automation: {ex.Message}");
            }
        }

        private void OnPauseAutomationClicked(object sender, EventArgs e)
        {
            try
            {
                _automationEngine.PauseAutomation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error pausing automation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error pausing automation: {ex.Message}");
            }
        }

        private async void OnResumeAutomationClicked(object sender, EventArgs e)
        {
            try
            {
                await _automationEngine.ResumeAutomation();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resuming automation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error resuming automation: {ex.Message}");
            }
        }

        private async void OnStepAutomationClicked(object sender, EventArgs e)
        {
            try
            {
                await _automationEngine.ExecuteStep();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error executing step: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error executing step: {ex.Message}");
            }
        }

        private void OnResetAutomationClicked(object sender, EventArgs e)
        {
            try
            {
                _automationEngine.StopAutomation();
                stepTrackerControl.Reset();
                _logService.LogInfo("Automation reset");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting automation: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error resetting automation: {ex.Message}");
            }
        }

        private async void OnPickElementClicked(object sender, EventArgs e)
        {
            try
            {
                // Make the selector panel visible
                bottomTabControl.SelectedTab = tabSelectorPanel;

                await _elementPickerService.ActivateElementPicker();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error activating element picker: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error activating element picker: {ex.Message}");
            }
        }

        private void OnStepAdded(object sender, Controls.StepAddedEventArgs e)
        {
            try
            {
                if (_currentTemplate == null)
                {
                    _logService.LogWarning("No template loaded, cannot add step");
                    return;
                }

                bool added = stepTreeControl.AddStep(e.Step);
                if (added)
                {
                    _logService.LogInfo($"Step added: {e.Step.Name}");
                }
                else
                {
                    _logService.LogWarning($"Failed to add step: {e.Step.Name}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding step: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error adding step: {ex.Message}");
            }
        }

        private void OnTemplateChanged(object sender, EventArgs e)
        {
            if (e is TemplateEventArgs templateEvent)
            {
                _currentTemplate = templateEvent.Template;
                stepTreeControl.SetTemplate(_currentTemplate);
            }
            else if (e is Controls.TemplateChangedEventArgs treeEvent)
            {
                _currentTemplate = treeEvent.Template;
                templateEditorControl.SetTemplate(_currentTemplate);
            }
        }

        private void OnStepSelected(object sender, StepEventArgs e)
        {
            stepTrackerControl.SetCurrentStep(e.Page, e.Step);
        }

        private void OnSelectorInserted(object sender, SelectorEventArgs e)
        {
            try
            {
                templateEditorControl.InsertSelector(e.SelectorValue);
                _logService.LogInfo($"Selector inserted: {e.SelectorValue}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting selector: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logService.LogError($"Error inserting selector: {ex.Message}");
            }
        }

        private void OnStepExecutionStarted(object sender, StepExecutionEventArgs e)
        {
            // Update UI on the UI thread
            Invoke((MethodInvoker)delegate
            {
                stepTrackerControl.SetCurrentStep(e.Page, e.Step);
                stepTreeControl.HighlightStep(e.Step.Id);
            });
        }

        private void OnStepExecutionCompleted(object sender, StepExecutionEventArgs e)
        {
            // Update UI on the UI thread
            Invoke((MethodInvoker)delegate
            {
                if (!e.Success)
                {
                    MessageBox.Show($"Step execution failed: {e.ErrorMessage}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void OnPageChanged(object sender, PageChangeEventArgs e)
        {
            // Update UI on the UI thread
            Invoke((MethodInvoker)delegate
            {
                stepTrackerControl.SetCurrentPage(e.NewPage);
                stepTreeControl.ExpandPage(e.NewPage.Id);
            });
        }

        private void OnAutomationStatusChanged(object sender, AutomationStatusEventArgs e)
        {
            // Update UI on the UI thread
            Invoke((MethodInvoker)delegate
            {
                _isAutomationRunning = e.IsRunning;
                _isAutomationPaused = e.IsPaused;

                toolbarControl.UpdateButtonStates(_isAutomationRunning, _isAutomationPaused);
            });
        }

        private void OnSelectorOptionsGenerated(object sender, SelectorOptionsEventArgs e)
        {
            // Update UI on the UI thread
            Invoke((MethodInvoker)delegate
            {
                selectorPanelControl.DisplaySelectorOptions(e.SelectorOptions, e.ElementInfo);
            });
        }

        private void OnLogEntryAdded(object sender, LogEntryEventArgs e)
        {
            // Update UI on the UI thread
            Invoke((MethodInvoker)delegate
            {
                logViewerControl.AddLogEntry(e.LogEntry);
            });
        }

        private void OnPageDetected(object sender, PageDetectedEventArgs e)
        {
            // Update UI on the UI thread
            Invoke((MethodInvoker)delegate
            {
                stepTrackerControl.SetCurrentPage(e.DetectedPage);
                stepTreeControl.ExpandPage(e.DetectedPage.Id);
            });
        }

        #endregion
    }
}
