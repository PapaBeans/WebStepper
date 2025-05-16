namespace InsuranceAutomation.UI.Dialogs
{
    partial class TemplateEditorForm
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
            this.jsonTextBox = new System.Windows.Forms.TextBox();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.saveButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.validateButton = new System.Windows.Forms.Button();
            this.formatButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // jsonTextBox
            // 
            this.jsonTextBox.AcceptsReturn = true;
            this.jsonTextBox.AcceptsTab = true;
            this.jsonTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jsonTextBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.jsonTextBox.Location = new System.Drawing.Point(3, 3);
            this.jsonTextBox.Multiline = true;
            this.jsonTextBox.Name = "jsonTextBox";
            this.jsonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.jsonTextBox.Size = new System.Drawing.Size(794, 534);
            this.jsonTextBox.TabIndex = 0;
            this.jsonTextBox.WordWrap = false;
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 1;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Controls.Add(this.jsonTextBox, 0, 0);
            this.mainPanel.Controls.Add(this.buttonsPanel, 0, 1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 2;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.mainPanel.Size = new System.Drawing.Size(800, 600);
            this.mainPanel.TabIndex = 0;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.closeButton);
            this.buttonsPanel.Controls.Add(this.formatButton);
            this.buttonsPanel.Controls.Add(this.validateButton);
            this.buttonsPanel.Controls.Add(this.saveAsButton);
            this.buttonsPanel.Controls.Add(this.saveButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(3, 543);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(794, 54);
            this.buttonsPanel.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(709, 5);
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
            this.saveAsButton.Location = new System.Drawing.Point(619, 5);
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
            this.validateButton.Location = new System.Drawing.Point(529, 5);
            this.validateButton.Margin = new System.Windows.Forms.Padding(5);
            this.validateButton.Name = "validateButton";
            this.validateButton.Size = new System.Drawing.Size(80, 30);
            this.validateButton.TabIndex = 2;
            this.validateButton.Text = "Validate";
            this.validateButton.UseVisualStyleBackColor = true;
            this.validateButton.Click += new System.EventHandler(this.ValidateButton_Click);
            // 
            // formatButton
            // 
            this.formatButton.Location = new System.Drawing.Point(439, 5);
            this.formatButton.Margin = new System.Windows.Forms.Padding(5);
            this.formatButton.Name = "formatButton";
            this.formatButton.Size = new System.Drawing.Size(80, 30);
            this.formatButton.TabIndex = 3;
            this.formatButton.Text = "Format";
            this.formatButton.UseVisualStyleBackColor = true;
            this.formatButton.Click += new System.EventHandler(this.FormatButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(349, 5);
            this.closeButton.Margin = new System.Windows.Forms.Padding(5);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(80, 30);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // TemplateEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.mainPanel);
            this.Name = "TemplateEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Template Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TemplateEditorForm_FormClosing);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox jsonTextBox;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.Button validateButton;
        private System.Windows.Forms.Button formatButton;
        private System.Windows.Forms.Button closeButton;
    }
}
