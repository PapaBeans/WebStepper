namespace InsuranceAutomation.UI.Dialogs
{
    partial class VisualTemplateEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.editorSplitContainer = new System.Windows.Forms.SplitContainer();
            this.stepsPanel = new System.Windows.Forms.Panel();
            this.stepsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.propertiesPanel = new System.Windows.Forms.Panel();
            this.propertiesGroupBox = new System.Windows.Forms.GroupBox();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.jsonTextBox = new System.Windows.Forms.TextBox();
            this.jsonGroupBox = new System.Windows.Forms.GroupBox();
            this.editorToolStrip = new System.Windows.Forms.ToolStrip();
            this.addStepDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.waitForElementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fillFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clickButtonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupStepsButton = new System.Windows.Forms.ToolStripButton();
            this.ungroupStepsButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.templatePropertiesButton = new System.Windows.Forms.ToolStripButton();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.validateButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.stepContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templateNameLabel = new System.Windows.Forms.Label();
            this.templateNameTextBox = new System.Windows.Forms.TextBox();
            this.templateUrlLabel = new System.Windows.Forms.Label();
            this.templateUrlTextBox = new System.Windows.Forms.TextBox();
            this.templateDescriptionLabel = new System.Windows.Forms.Label();
            this.templateDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.templatePropertiesPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).BeginInit();
            this.editorSplitContainer.Panel1.SuspendLayout();
            this.editorSplitContainer.Panel2.SuspendLayout();
            this.editorSplitContainer.SuspendLayout();
            this.stepsPanel.SuspendLayout();
            this.propertiesPanel.SuspendLayout();
            this.propertiesGroupBox.SuspendLayout();
            this.jsonGroupBox.SuspendLayout();
            this.editorToolStrip.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.stepContextMenuStrip.SuspendLayout();
            this.templatePropertiesPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(3, 43);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.editorSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.jsonGroupBox);
            this.mainSplitContainer.Size = new System.Drawing.Size(994, 548);
            this.mainSplitContainer.SplitterDistance = 650;
            this.mainSplitContainer.TabIndex = 0;
            // 
            // editorSplitContainer
            // 
            this.editorSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.editorSplitContainer.Name = "editorSplitContainer";
            // 
            // editorSplitContainer.Panel1
            // 
            this.editorSplitContainer.Panel1.Controls.Add(this.stepsPanel);
            // 
            // editorSplitContainer.Panel2
            // 
            this.editorSplitContainer.Panel2.Controls.Add(this.propertiesPanel);
            this.editorSplitContainer.Size = new System.Drawing.Size(650, 548);
            this.editorSplitContainer.SplitterDistance = 400;
            this.editorSplitContainer.TabIndex = 0;
            // 
            // stepsPanel
            // 
            this.stepsPanel.AutoScroll = true;
            this.stepsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stepsPanel.Controls.Add(this.stepsFlowLayoutPanel);
            this.stepsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepsPanel.Location = new System.Drawing.Point(0, 0);
            this.stepsPanel.Name = "stepsPanel";
            this.stepsPanel.Size = new System.Drawing.Size(400, 548);
            this.stepsPanel.TabIndex = 0;
            // 
            // stepsFlowLayoutPanel
            // 
            this.stepsFlowLayoutPanel.AllowDrop = true;
            this.stepsFlowLayoutPanel.AutoScroll = true;
            this.stepsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.stepsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.stepsFlowLayoutPanel.Name = "stepsFlowLayoutPanel";
            this.stepsFlowLayoutPanel.Size = new System.Drawing.Size(398, 546);
            this.stepsFlowLayoutPanel.TabIndex = 0;
            this.stepsFlowLayoutPanel.WrapContents = false;
            this.stepsFlowLayoutPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.StepsFlowLayoutPanel_DragDrop);
            this.stepsFlowLayoutPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.StepsFlowLayoutPanel_DragEnter);
            this.stepsFlowLayoutPanel.DragOver += new System.Windows.Forms.DragEventHandler(this.StepsFlowLayoutPanel_DragOver);
            // 
            // propertiesPanel
            // 
            this.propertiesPanel.Controls.Add(this.propertiesGroupBox);
            this.propertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesPanel.Location = new System.Drawing.Point(0, 0);
            this.propertiesPanel.Name = "propertiesPanel";
            this.propertiesPanel.Size = new System.Drawing.Size(246, 548);
            this.propertiesPanel.TabIndex = 0;
            // 
            // propertiesGroupBox
            // 
            this.propertiesGroupBox.Controls.Add(this.propertyGrid);
            this.propertiesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertiesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.propertiesGroupBox.Name = "propertiesGroupBox";
            this.propertiesGroupBox.Size = new System.Drawing.Size(246, 548);
            this.propertiesGroupBox.TabIndex = 0;
            this.propertiesGroupBox.TabStop = false;
            this.propertiesGroupBox.Text = "Step Properties";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(3, 16);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.propertyGrid.Size = new System.Drawing.Size(240, 529);
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGrid_PropertyValueChanged);
            // 
            // jsonTextBox
            // 
            this.jsonTextBox.AcceptsReturn = true;
            this.jsonTextBox.AcceptsTab = true;
            this.jsonTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jsonTextBox.Location = new System.Drawing.Point(3, 16);
            this.jsonTextBox.Multiline = true;
            this.jsonTextBox.Name = "jsonTextBox";
            this.jsonTextBox.ReadOnly = true;
            this.jsonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.jsonTextBox.Size = new System.Drawing.Size(334, 529);
            this.jsonTextBox.TabIndex = 0;
            this.jsonTextBox.WordWrap = false;
            // 
            // jsonGroupBox
            // 
            this.jsonGroupBox.Controls.Add(this.jsonTextBox);
            this.jsonGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonGroupBox.Location = new System.Drawing.Point(0, 0);
            this.jsonGroupBox.Name = "jsonGroupBox";
            this.jsonGroupBox.Size = new System.Drawing.Size(340, 548);
            this.jsonGroupBox.TabIndex = 0;
            this.jsonGroupBox.TabStop = false;
            this.jsonGroupBox.Text = "JSON Preview";
            // 
            // editorToolStrip
            // 
            this.editorToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addStepDropDownButton,
            this.toolStripSeparator1,
            this.groupStepsButton,
            this.ungroupStepsButton,
            this.toolStripSeparator2,
            this.templatePropertiesButton});
            this.editorToolStrip.Location = new System.Drawing.Point(3, 3);
            this.editorToolStrip.Name = "editorToolStrip";
            this.editorToolStrip.Size = new System.Drawing.Size(994, 25);
            this.editorToolStrip.TabIndex = 0;
            this.editorToolStrip.Text = "toolStrip1";
            // 
            // addStepDropDownButton
            // 
            this.addStepDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.addStepDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.waitForElementToolStripMenuItem,
            this.fillFormToolStripMenuItem,
            this.clickButtonToolStripMenuItem,
            this.executeScriptToolStripMenuItem});
            this.addStepDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addStepDropDownButton.Name = "addStepDropDownButton";
            this.addStepDropDownButton.Size = new System.Drawing.Size(72, 22);
            this.addStepDropDownButton.Text = "Add Step";
            // 
            // waitForElementToolStripMenuItem
            // 
            this.waitForElementToolStripMenuItem.Name = "waitForElementToolStripMenuItem";
            this.waitForElementToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.waitForElementToolStripMenuItem.Text = "Wait for Element";
            this.waitForElementToolStripMenuItem.Click += new System.EventHandler(this.WaitForElementToolStripMenuItem_Click);
            // 
            // fillFormToolStripMenuItem
            // 
            this.fillFormToolStripMenuItem.Name = "fillFormToolStripMenuItem";
            this.fillFormToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.fillFormToolStripMenuItem.Text = "Fill Form";
            this.fillFormToolStripMenuItem.Click += new System.EventHandler(this.FillFormToolStripMenuItem_Click);
            // 
            // clickButtonToolStripMenuItem
            // 
            this.clickButtonToolStripMenuItem.Name = "clickButtonToolStripMenuItem";
            this.clickButtonToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.clickButtonToolStripMenuItem.Text = "Click Button";
            this.clickButtonToolStripMenuItem.Click += new System.EventHandler(this.ClickButtonToolStripMenuItem_Click);
            // 
            // executeScriptToolStripMenuItem
            // 
            this.executeScriptToolStripMenuItem.Name = "executeScriptToolStripMenuItem";
            this.executeScriptToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.executeScriptToolStripMenuItem.Text = "Execute Script";
            this.executeScriptToolStripMenuItem.Click += new System.EventHandler(this.ExecuteScriptToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // groupStepsButton
            // 
            this.groupStepsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.groupStepsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.groupStepsButton.Name = "groupStepsButton";
            this.groupStepsButton.Size = new System.Drawing.Size(78, 22);
            this.groupStepsButton.Text = "Group Steps";
            this.groupStepsButton.Click += new System.EventHandler(this.GroupStepsButton_Click);
            // 
            // ungroupStepsButton
            // 
            this.ungroupStepsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ungroupStepsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ungroupStepsButton.Name = "ungroupStepsButton";
            this.ungroupStepsButton.Size = new System.Drawing.Size(93, 22);
            this.ungroupStepsButton.Text = "Ungroup Steps";
            this.ungroupStepsButton.Click += new System.EventHandler(this.UngroupStepsButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // templatePropertiesButton
            // 
            this.templatePropertiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.templatePropertiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.templatePropertiesButton.Name = "templatePropertiesButton";
            this.templatePropertiesButton.Size = new System.Drawing.Size(114, 22);
            this.templatePropertiesButton.Text = "Template Properties";
            this.templatePropertiesButton.Click += new System.EventHandler(this.TemplatePropertiesButton_Click);
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.closeButton);
            this.buttonsPanel.Controls.Add(this.validateButton);
            this.buttonsPanel.Controls.Add(this.saveAsButton);
            this.buttonsPanel.Controls.Add(this.saveButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(3, 597);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(994, 40);
            this.buttonsPanel.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(746, 5);
            this.saveButton.Margin = new System.Windows.Forms.Padding(5);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(80, 30);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Location = new System.Drawing.Point(836, 5);
            this.saveAsButton.Margin = new System.Windows.Forms.Padding(5);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(80, 30);
            this.saveAsButton.TabIndex = 1;
            this.saveAsButton.Text = "Save As...";
            this.saveAsButton.UseVisualStyleBackColor = true;
            this.saveAsButton.Click += new System.EventHandler(this.SaveAsButton_Click);
            // 
            // validateButton
            // 
            this.validateButton.Location = new System.Drawing.Point(656, 5);
            this.validateButton.Margin = new System.Windows.Forms.Padding(5);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(80, 30);
            this.validateButton.TabIndex = 2;
            this.validateButton.Text = "Validate";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.ValidateButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(926, 5);
            this.closeButton.Margin = new System.Windows.Forms.Padding(5);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(80, 30);
            this.closeButton.TabIndex = 3;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // stepContextMenuStrip
            // 
            this.stepContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editStepToolStripMenuItem,
            this.duplicateStepToolStripMenuItem,
            this.deleteStepToolStripMenuItem});
            this.stepContextMenuStrip.Name = "stepContextMenuStrip";
            this.stepContextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // editStepToolStripMenuItem
            // 
            this.editStepToolStripMenuItem.Name = "editStepToolStripMenuItem";
            this.editStepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editStepToolStripMenuItem.Text = "Edit Step";
            this.editStepToolStripMenuItem.Click += new System.EventHandler(this.EditStepToolStripMenuItem_Click);
            // 
            // duplicateStepToolStripMenuItem
            // 
            this.duplicateStepToolStripMenuItem.Name = "duplicateStepToolStripMenuItem";
            this.duplicateStepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.duplicateStepToolStripMenuItem.Text = "Duplicate Step";
            this.duplicateStepToolStripMenuItem.Click += new System.EventHandler(this.DuplicateStepToolStripMenuItem_Click);
            // 
            // deleteStepToolStripMenuItem
            // 
            this.deleteStepToolStripMenuItem.Name = "deleteStepToolStripMenuItem";
            this.deleteStepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteStepToolStripMenuItem.Text = "Delete Step";
            this.deleteStepToolStripMenuItem.Click += new System.EventHandler(this.DeleteStepToolStripMenuItem_Click);
            // 
            // templateNameLabel
            // 
            this.templateNameLabel.AutoSize = true;
            this.templateNameLabel.Location = new System.Drawing.Point(8, 13);
            this.templateNameLabel.Name = "templateNameLabel";
            this.templateNameLabel.Size = new System.Drawing.Size(38, 13);
            this.templateNameLabel.TabIndex = 0;
            this.templateNameLabel.Text = "Name:";
            // 
            // templateNameTextBox
            // 
            this.templateNameTextBox.Location = new System.Drawing.Point(86, 10);
            this.templateNameTextBox.Name = "templateNameTextBox";
            this.templateNameTextBox.Size = new System.Drawing.Size(200, 20);
            this.templateNameTextBox.TabIndex = 1;
            this.templateNameTextBox.TextChanged += new System.EventHandler(this.TemplatePropertyChanged);
            // 
            // templateUrlLabel
            // 
            this.templateUrlLabel.AutoSize = true;
            this.templateUrlLabel.Location = new System.Drawing.Point(304, 13);
            this.templateUrlLabel.Name = "templateUrlLabel";
            this.templateUrlLabel.Size = new System.Drawing.Size(65, 13);
            this.templateUrlLabel.TabIndex = 2;
            this.templateUrlLabel.Text = "Target URL:";
            // 
            // templateUrlTextBox
            // 
            this.templateUrlTextBox.Location = new System.Drawing.Point(375, 10);
            this.templateUrlTextBox.Name = "templateUrlTextBox";
            this.templateUrlTextBox.Size = new System.Drawing.Size(300, 20);
            this.templateUrlTextBox.TabIndex = 3;
            this.templateUrlTextBox.TextChanged += new System.EventHandler(this.TemplatePropertyChanged);
            // 
            // templateDescriptionLabel
            // 
            this.templateDescriptionLabel.AutoSize = true;
            this.templateDescriptionLabel.Location = new System.Drawing.Point(8, 45);
            this.templateDescriptionLabel.Name = "templateDescriptionLabel";
            this.templateDescriptionLabel.Size = new System.Drawing.Size(63, 13);
            this.templateDescriptionLabel.TabIndex = 4;
            this.templateDescriptionLabel.Text = "Description:";
            // 
            // templateDescriptionTextBox
            // 
            this.templateDescriptionTextBox.Location = new System.Drawing.Point(86, 42);
            this.templateDescriptionTextBox.Name = "templateDescriptionTextBox";
            this.templateDescriptionTextBox.Size = new System.Drawing.Size(589, 20);
            this.templateDescriptionTextBox.TabIndex = 5;
            this.templateDescriptionTextBox.TextChanged += new System.EventHandler(this.TemplatePropertyChanged);
            // 
            // templatePropertiesPanel
            // 
            this.templatePropertiesPanel.Controls.Add(this.templateNameLabel);
            this.templatePropertiesPanel.Controls.Add(this.templateDescriptionTextBox);
            this.templatePropertiesPanel.Controls.Add(this.templateNameTextBox);
            this.templatePropertiesPanel.Controls.Add(this.templateDescriptionLabel);
            this.templatePropertiesPanel.Controls.Add(this.templateUrlLabel);
            this.templatePropertiesPanel.Controls.Add(this.templateUrlTextBox);
            this.templatePropertiesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templatePropertiesPanel.Location = new System.Drawing.Point(3, 28);
            this.templatePropertiesPanel.Name = "templatePropertiesPanel";
            this.templatePropertiesPanel.Size = new System.Drawing.Size(994, 15);
            this.templatePropertiesPanel.TabIndex = 4;
            this.templatePropertiesPanel.Visible = false;
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 1;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Controls.Add(this.editorToolStrip, 0, 0);
            this.mainPanel.Controls.Add(this.templatePropertiesPanel, 0, 1);
            this.mainPanel.Controls.Add(this.mainSplitContainer, 0, 2);
            this.mainPanel.Controls.Add(this.buttonsPanel, 0, 3);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 4;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.mainPanel.Size = new System.Drawing.Size(1000, 640);
            this.mainPanel.TabIndex = 1;
            // 
            // VisualTemplateEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(1000, 640);
            this.Controls.Add(this.mainPanel);
            this.MinimizeBox = false;
            this.Name = "VisualTemplateEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visual Template Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisualTemplateEditorForm_FormClosing);
            this.Load += new System.EventHandler(this.VisualTemplateEditorForm_Load);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.editorSplitContainer.Panel1.ResumeLayout(false);
            this.editorSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editorSplitContainer)).EndInit();
            this.editorSplitContainer.ResumeLayout(false);
            this.stepsPanel.ResumeLayout(false);
            this.propertiesPanel.ResumeLayout(false);
            this.propertiesGroupBox.ResumeLayout(false);
            this.jsonGroupBox.ResumeLayout(false);
            this.jsonGroupBox.PerformLayout();
            this.editorToolStrip.ResumeLayout(false);
            this.editorToolStrip.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.stepContextMenuStrip.ResumeLayout(false);
            this.templatePropertiesPanel.ResumeLayout(false);
            this.templatePropertiesPanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer editorSplitContainer;
        private System.Windows.Forms.Panel stepsPanel;
        private System.Windows.Forms.FlowLayoutPanel stepsFlowLayoutPanel;
        private System.Windows.Forms.Panel propertiesPanel;
        private System.Windows.Forms.GroupBox propertiesGroupBox;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.TextBox jsonTextBox;
        private System.Windows.Forms.GroupBox jsonGroupBox;
        private System.Windows.Forms.ToolStrip editorToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton addStepDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem waitForElementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fillFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clickButtonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton groupStepsButton;
        private System.Windows.Forms.ToolStripButton ungroupStepsButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton templatePropertiesButton;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.ContextMenuStrip stepContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteStepToolStripMenuItem;
        private System.Windows.Forms.Label templateNameLabel;
        private System.Windows.Forms.TextBox templateNameTextBox;
        private System.Windows.Forms.Label templateUrlLabel;
        private System.Windows.Forms.TextBox templateUrlTextBox;
        private System.Windows.Forms.Label templateDescriptionLabel;
        private System.Windows.Forms.TextBox templateDescriptionTextBox;
        private System.Windows.Forms.Panel templatePropertiesPanel;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
    }
}