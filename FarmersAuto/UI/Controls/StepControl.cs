using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using InsuranceAutomation.Models;

namespace InsuranceAutomation.UI.Controls
{
    /// <summary>
    /// A user control that represents an automation step in the visual editor.
    /// </summary>
    public partial class StepControl : UserControl
    {
        private AutomationStep step;
        private bool isSelected = false;
        private bool isCollapsed = false;
        private bool isGroupHeader = false;
        private bool isDragging = false;
        private string groupName = string.Empty;
        private int stepIndex;

        /// <summary>
        /// Event raised when the control is selected.
        /// </summary>
        public event EventHandler<StepControlEventArgs> StepSelected;

        /// <summary>
        /// Event raised when the step properties are changed.
        /// </summary>
        public event EventHandler<StepControlEventArgs> StepChanged;

        /// <summary>
        /// Event raised when a drag operation is started on this control.
        /// </summary>
        public event EventHandler<StepControlEventArgs> DragStarted;

        /// <summary>
        /// Gets the automation step associated with this control.
        /// </summary>
        [Browsable(false)]
        public AutomationStep Step => step;

        /// <summary>
        /// Gets or sets the index of this step in the template.
        /// </summary>
        [Browsable(false)]
        public int StepIndex
        {
            get => stepIndex;
            set
            {
                stepIndex = value;
                lblStepNumber.Text = $"Step {stepIndex + 1}";
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this step is selected.
        /// </summary>
        [Browsable(false)]
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    SetSelectionAppearance();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this step is collapsed.
        /// </summary>
        [Browsable(false)]
        public bool IsCollapsed
        {
            get => isCollapsed;
            set
            {
                isCollapsed = value;
                btnToggleCollapse.Text = isCollapsed ? "+" : "-";
                // Notify parent to update layout
                OnStepChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this step is a group header.
        /// </summary>
        [Browsable(false)]
        public bool IsGroupHeader
        {
            get => isGroupHeader;
            set
            {
                isGroupHeader = value;
                btnToggleCollapse.Visible = isGroupHeader;
                SetAppearanceForType();
            }
        }

        /// <summary>
        /// Gets or sets the name of the group this step belongs to.
        /// </summary>
        [Browsable(false)]
        public string GroupName
        {
            get => groupName;
            set 
            { 
                groupName = value;
                if (IsGroupHeader)
                {
                    lblDescription.Text = $"Group: {groupName}";
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StepControl"/> class.
        /// </summary>
        public StepControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.AllowDrop = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StepControl"/> class with the specified step.
        /// </summary>
        /// <param name="step">The automation step to associate with this control.</param>
        /// <param name="stepIndex">The index of this step in the template.</param>
        public StepControl(AutomationStep step, int stepIndex = 0) : this()
        {
            this.step = step;
            this.stepIndex = stepIndex;
            InitializeStepUI();
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnToggleCollapse = new System.Windows.Forms.Button();
            this.lblStepNumber = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.btnToggleCollapse);
            this.pnlMain.Controls.Add(this.lblStepNumber);
            this.pnlMain.Controls.Add(this.lblType);
            this.pnlMain.Controls.Add(this.lblDescription);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(350, 60);
            this.pnlMain.TabIndex = 0;
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StepControl_MouseDown);
            this.pnlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StepControl_MouseMove);
            this.pnlMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StepControl_MouseUp);
            // 
            // btnToggleCollapse
            // 
            this.btnToggleCollapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleCollapse.Location = new System.Drawing.Point(323, 3);
            this.btnToggleCollapse.Name = "btnToggleCollapse";
            this.btnToggleCollapse.Size = new System.Drawing.Size(22, 22);
            this.btnToggleCollapse.TabIndex = 3;
            this.btnToggleCollapse.Text = "-";
            this.btnToggleCollapse.UseVisualStyleBackColor = true;
            this.btnToggleCollapse.Visible = false;
            this.btnToggleCollapse.Click += new System.EventHandler(this.BtnToggleCollapse_Click);
            // 
            // lblStepNumber
            // 
            this.lblStepNumber.AutoSize = true;
            this.lblStepNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStepNumber.Location = new System.Drawing.Point(3, 5);
            this.lblStepNumber.Name = "lblStepNumber";
            this.lblStepNumber.Size = new System.Drawing.Size(45, 13);
            this.lblStepNumber.TabIndex = 0;
            this.lblStepNumber.Text = "Step 1";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(60, 5);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(82, 13);
            this.lblType.TabIndex = 1;
            this.lblType.Text = "wait_for_element";
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescription.Location = new System.Drawing.Point(3, 25);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(342, 30);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Description";
            // 
            // StepControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Name = "StepControl";
            this.Size = new System.Drawing.Size(350, 60);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.StepControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.StepControl_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.StepControl_DragOver);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        private void InitializeStepUI()
        {
            if (step != null)
            {
                lblStepNumber.Text = $"Step {stepIndex + 1}";
                lblType.Text = step.Type;
                lblDescription.Text = step.GetDisplayText();
                SetAppearanceForType();
            }
        }

        private void SetAppearanceForType()
        {
            if (step == null) return;

            // Different background colors for different step types
            if (isGroupHeader)
            {
                pnlMain.BackColor = Color.LightBlue;
                return;
            }

            switch (step.Type)
            {
                case "wait_for_element":
                    pnlMain.BackColor = Color.White;
                    break;
                case "click_button":
                    pnlMain.BackColor = Color.LightCyan;
                    break;
                case "fill_form":
                    pnlMain.BackColor = Color.LightYellow;
                    break;
                case "execute_script":
                    pnlMain.BackColor = Color.LightGreen;
                    break;
                default:
                    pnlMain.BackColor = Color.White;
                    break;
            }
        }

        private void SetSelectionAppearance()
        {
            if (isSelected)
            {
                pnlMain.BorderStyle = BorderStyle.FixedSingle;
                pnlMain.BackColor = ColorTranslator.FromHtml(isGroupHeader ? "#ADD8E6" : "#F0F0F0");
                pnlMain.ForeColor = Color.Black;
            }
            else
            {
                pnlMain.BorderStyle = BorderStyle.FixedSingle;
                SetAppearanceForType();
            }
        }

        private void StepControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Select this step
                OnStepSelected();

                // Start potential drag operation
                if (e.Clicks == 1)
                {
                    isDragging = true;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Select this step and show context menu
                OnStepSelected();
            }
        }

        private void StepControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left)
            {
                // Initiate drag and drop operation
                DoDragDrop(this, DragDropEffects.Move);
                isDragging = false;
                OnDragStarted();
            }
        }

        private void StepControl_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void StepControl_DragEnter(object sender, DragEventArgs e)
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

        private void StepControl_DragOver(object sender, DragEventArgs e)
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

        private void StepControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(StepControl)))
            {
                // Handle in parent container
            }
        }

        private void BtnToggleCollapse_Click(object sender, EventArgs e)
        {
            IsCollapsed = !IsCollapsed;
        }

        /// <summary>
        /// Updates the UI to reflect changes in the underlying step.
        /// </summary>
        public void UpdateUI()
        {
            if (step != null)
            {
                lblDescription.Text = isGroupHeader ? $"Group: {groupName}" : step.GetDisplayText();
                lblType.Text = step.Type;
                SetAppearanceForType();
            }
        }

        /// <summary>
        /// Sets the automation step associated with this control.
        /// </summary>
        /// <param name="newStep">The new automation step.</param>
        public void SetStep(AutomationStep newStep)
        {
            step = newStep;
            InitializeStepUI();
            OnStepChanged();
        }

        protected virtual void OnStepSelected()
        {
            StepSelected?.Invoke(this, new StepControlEventArgs(this));
        }

        protected virtual void OnStepChanged()
        {
            StepChanged?.Invoke(this, new StepControlEventArgs(this));
        }

        protected virtual void OnDragStarted()
        {
            DragStarted?.Invoke(this, new StepControlEventArgs(this));
        }

        private Panel pnlMain;
        private Label lblStepNumber;
        private Label lblType;
        private Label lblDescription;
        private Button btnToggleCollapse;
    }

    /// <summary>
    /// Event arguments for step control events.
    /// </summary>
    public class StepControlEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the step control that raised the event.
        /// </summary>
        public StepControl StepControl { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StepControlEventArgs"/> class.
        /// </summary>
        /// <param name="stepControl">The step control that raised the event.</param>
        public StepControlEventArgs(StepControl stepControl)
        {
            StepControl = stepControl;
        }
    }
}