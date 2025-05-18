using System;
using System.Windows.Forms;

namespace WebStepper.UI.Controls
{
    public partial class ToolbarControl : UserControl
    {
        // Events
        public event EventHandler NewTemplateClicked;
        public event EventHandler OpenTemplateClicked;
        public event EventHandler SaveTemplateClicked;
        public event EventHandler StartAutomationClicked;
        public event EventHandler PauseAutomationClicked;
        public event EventHandler ResumeAutomationClicked;
        public event EventHandler StepAutomationClicked;
        public event EventHandler ResetAutomationClicked;
        public event EventHandler PickElementClicked;

        public ToolbarControl()
        {
            InitializeComponent();

            // Set initial button states
            UpdateButtonStates(false, false);
        }

        public void UpdateButtonStates(bool isRunning, bool isPaused)
        {
            // Template buttons
            btnNew.Enabled = !isRunning || isPaused;
            btnOpen.Enabled = !isRunning || isPaused;
            btnSave.Enabled = true;

            // Automation buttons
            btnStart.Enabled = !isRunning || isPaused;
            btnPause.Enabled = isRunning && !isPaused;
            btnResume.Enabled = isRunning && isPaused;
            btnStep.Enabled = !isRunning || isPaused;
            btnReset.Enabled = isRunning;

            // Element picker button
            btnPickElement.Enabled = !isRunning || isPaused;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewTemplateClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenTemplateClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTemplateClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartAutomationClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseAutomationClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            ResumeAutomationClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            StepAutomationClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAutomationClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnPickElement_Click(object sender, EventArgs e)
        {
            PickElementClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
