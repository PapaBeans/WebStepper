namespace InsuranceAutomation.UI.Dialogs
{
    partial class TemplateSelectionForm
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
            this.templatesListBox = new System.Windows.Forms.ListBox();
            this.headerLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // templatesListBox
            // 
            this.templatesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templatesListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.templatesListBox.FormattingEnabled = true;
            this.templatesListBox.ItemHeight = 15;
            this.templatesListBox.Location = new System.Drawing.Point(13, 43);
            this.templatesListBox.Name = "templatesListBox";
            this.templatesListBox.Size = new System.Drawing.Size(474, 314);
            this.templatesListBox.TabIndex = 0;
            this.templatesListBox.DoubleClick += new System.EventHandler(this.TemplatesListBox_DoubleClick);
            // 
            // headerLabel
            // 
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.Location = new System.Drawing.Point(13, 10);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(474, 30);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Select a template or create a new one:";
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 1;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Controls.Add(this.headerLabel, 0, 0);
            this.mainPanel.Controls.Add(this.templatesListBox, 0, 1);
            this.mainPanel.Controls.Add(this.buttonsPanel, 0, 2);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mainPanel.RowCount = 3;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainPanel.Size = new System.Drawing.Size(500, 400);
            this.mainPanel.TabIndex = 0;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.selectButton);
            this.buttonsPanel.Controls.Add(this.refreshButton);
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(13, 360);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(474, 40);
            this.buttonsPanel.TabIndex = 2;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(304, 5);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 30);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(389, 5);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(5);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(80, 30);
            this.refreshButton.TabIndex = 1;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectButton.Location = new System.Drawing.Point(214, 5);
            this.selectButton.Margin = new System.Windows.Forms.Padding(5);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(80, 30);
            this.selectButton.TabIndex = 0;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // TemplateSelectionForm
            // 
            this.AcceptButton = this.selectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TemplateSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Template Selection";
            this.mainPanel.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox templatesListBox;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
