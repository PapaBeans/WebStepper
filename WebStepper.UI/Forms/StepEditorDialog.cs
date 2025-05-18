using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WebStepper.Core.Domain;

namespace WebStepper.UI.Forms
{
    public partial class StepEditorDialog : Form
    {
        private Step _step;
        private readonly bool _isNewStep;

        public Step Step => _step;

        public StepEditorDialog(string selectedSelector = null, bool isNewStep = true)
        {
            InitializeComponent();
            _isNewStep = isNewStep;

            // Set up the combo box for step types
            cboStepType.Items.Clear();
            cboStepType.DataSource = Enum.GetValues(typeof(StepType));
            cboStepType.SelectedIndex = 0;

            // If we have a selector, automatically populate the field
            if (!string.IsNullOrEmpty(selectedSelector))
            {
                txtSelector.Text = selectedSelector;
            }

            // Initialize a new step
            _step = new Step
            {
                Id = Guid.NewGuid().ToString(),
                Name = "New Step",
                Type = StepType.WaitForElement,
                Selector = selectedSelector ?? string.Empty,
                Value = string.Empty,
                WaitBeforeMs = 0,
                WaitAfterMs = 0,
                MaxWaitMs = 5000,
                Description = string.Empty
            };

            // Set form title based on new or edit mode
            this.Text = isNewStep ? "Create New Step" : "Edit Step";

            // Connect event handlers
            cboStepType.SelectedIndexChanged += CboStepType_SelectedIndexChanged;
            Load += StepEditorDialog_Load;
        }

        public StepEditorDialog(Step step) : this(step.Selector, false)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));

            _step = new Step
            {
                Id = step.Id,
                Name = step.Name,
                Type = step.Type,
                Selector = step.Selector,
                Value = step.Value,
                WaitBeforeMs = step.WaitBeforeMs,
                WaitAfterMs = step.WaitAfterMs,
                MaxWaitMs = step.MaxWaitMs,
                Description = step.Description
            };
        }

        private void StepEditorDialog_Load(object sender, EventArgs e)
        {
            // Populate fields with step data
            txtStepName.Text = _step.Name;
            cboStepType.SelectedItem = _step.Type;
            txtSelector.Text = _step.Selector;
            txtValue.Text = _step.Value;
            numWaitBefore.Value = _step.WaitBeforeMs;
            numWaitAfter.Value = _step.WaitAfterMs;
            numMaxWait.Value = _step.MaxWaitMs;
            txtDescription.Text = _step.Description;

            // Update UI based on selected step type
            UpdateUIForStepType(_step.Type);
        }

        private void CboStepType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStepType.SelectedItem is StepType selectedType)
            {
                UpdateUIForStepType(selectedType);
            }
        }

        private void UpdateUIForStepType(StepType stepType)
        {
            // Reset all fields
            lblSelector.Visible = true;
            txtSelector.Visible = true;
            lblValue.Visible = true;
            txtValue.Visible = true;
            lblMaxWait.Visible = true;
            numMaxWait.Visible = true;

            // Update fields based on step type
            switch (stepType)
            {
                case StepType.WaitForElement:
                    lblValue.Visible = false;
                    txtValue.Visible = false;
                    lblValue.Text = "Value:";
                    break;

                case StepType.FillForm:
                    lblValue.Text = "Value to Fill:";
                    break;

                case StepType.ClickButton:
                    lblValue.Visible = false;
                    txtValue.Visible = false;
                    lblMaxWait.Visible = false;
                    numMaxWait.Visible = false;
                    lblValue.Text = "Value:";
                    break;

                case StepType.ExecuteScript:
                    lblSelector.Visible = false;
                    txtSelector.Visible = false;
                    lblMaxWait.Visible = false;
                    numMaxWait.Visible = false;
                    lblValue.Text = "JavaScript:";
                    break;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtStepName.Text))
            {
                MessageBox.Show("Step name is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStepName.Focus();
                return;
            }

            if (cboStepType.SelectedItem is StepType selectedType)
            {
                // Validate based on step type
                switch (selectedType)
                {
                    case StepType.WaitForElement:
                    case StepType.FillForm:
                    case StepType.ClickButton:
                        if (string.IsNullOrWhiteSpace(txtSelector.Text))
                        {
                            MessageBox.Show("Selector is required for this step type.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSelector.Focus();
                            return;
                        }
                        break;

                    case StepType.ExecuteScript:
                        if (string.IsNullOrWhiteSpace(txtValue.Text))
                        {
                            MessageBox.Show("Script is required for this step type.", "Validation Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtValue.Focus();
                            return;
                        }
                        break;
                }

                // Update step with form values
                _step.Name = txtStepName.Text;
                _step.Type = selectedType;
                _step.Selector = txtSelector.Text;
                _step.Value = txtValue.Text;
                _step.WaitBeforeMs = (int)numWaitBefore.Value;
                _step.WaitAfterMs = (int)numWaitAfter.Value;
                _step.MaxWaitMs = (int)numMaxWait.Value;
                _step.Description = txtDescription.Text;

                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select a valid step type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
