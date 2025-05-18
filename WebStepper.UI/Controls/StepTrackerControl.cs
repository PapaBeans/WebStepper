using System;
using System.Drawing;
using System.Windows.Forms;
using WebStepper.Core.Domain;

namespace WebStepper.UI.Controls
{
    public partial class StepTrackerControl : UserControl
    {
        private Page _currentPage;
        private Step _currentStep;

        public StepTrackerControl()
        {
            InitializeComponent();
        }

        public void SetCurrentPage(Page page)
        {
            _currentPage = page;
            UpdateDisplay();
        }

        public void SetCurrentStep(Page page, Step step)
        {
            _currentPage = page;
            _currentStep = step;
            UpdateDisplay();
        }

        public void Reset()
        {
            _currentPage = null;
            _currentStep = null;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // Update page label
            lblPage.Text = _currentPage != null ? _currentPage.Name : "No page selected";

            // Update step label
            lblStep.Text = _currentStep != null ? _currentStep.Name : "No step selected";

            // Update page identifier
            if (_currentPage != null && !string.IsNullOrEmpty(_currentPage.PageIdentifierSelector))
            {
                lblPageIdentifier.Text = _currentPage.PageIdentifierSelector;
            }
            else
            {
                lblPageIdentifier.Text = "No page identifier";
            }

            // Update step details
            if (_currentStep != null)
            {
                lblStepType.Text = _currentStep.Type.ToString();
                lblStepSelector.Text = _currentStep.Selector;
                lblStepValue.Text = _currentStep.Value;

                string waitTimes = string.Empty;
                if (_currentStep.WaitBeforeMs > 0)
                {
                    waitTimes += $"Before: {_currentStep.WaitBeforeMs}ms, ";
                }
                if (_currentStep.WaitAfterMs > 0)
                {
                    waitTimes += $"After: {_currentStep.WaitAfterMs}ms";
                }
                if (_currentStep.MaxWaitMs > 0)
                {
                    waitTimes += $"Max: {_currentStep.MaxWaitMs}ms";
                }

                lblStepWait.Text = !string.IsNullOrEmpty(waitTimes) ? waitTimes : "No waiting";
                txtStepDescription.Text = _currentStep.Description;
            }
            else
            {
                lblStepType.Text = "N/A";
                lblStepSelector.Text = "N/A";
                lblStepValue.Text = "N/A";
                lblStepWait.Text = "N/A";
                txtStepDescription.Text = string.Empty;
            }
        }
    }
}
