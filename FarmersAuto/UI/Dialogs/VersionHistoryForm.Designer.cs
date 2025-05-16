namespace InsuranceAutomation.UI.Dialogs
{
    partial class VersionHistoryForm
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
            this.versionsListBox = new System.Windows.Forms.ListBox();
            this.headerLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.restoreButton = new System.Windows.Forms.Button();
            this.viewButton = new System.Windows.Forms.Button();
            this.compareButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.mainPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // versionsListBox
            // 
            this.versionsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionsListBox.FormattingEnabled = true;
            this.versionsListBox.IntegralHeight = false;
            this.versionsListBox.ItemHeight = 15;
            this.versionsListBox.Location = new System.Drawing.Point(13, 43);
            this.versionsListBox.Name = "versionsListBox";
            this.versionsListBox.Size = new System.Drawing.Size(574, 364);
            this.versionsListBox.TabIndex = 0;
            this.versionsListBox.DoubleClick += new System.EventHandler(this.VersionsListBox_DoubleClick);
            // 
            // headerLabel
            // 
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.Location = new System.Drawing.Point(13, 10);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(574, 30);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Select a version to restore:";
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 1;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Controls.Add(this.headerLabel, 0, 0);
            this.mainPanel.Controls.Add(this.versionsListBox, 0, 1);
            this.mainPanel.Controls.Add(this.buttonsPanel, 0, 2);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Padding = new System.Windows.Forms.Padding(10);
            this.mainPanel.RowCount = 3;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainPanel.Size = new System.Drawing.Size(600, 450);
            this.mainPanel.TabIndex = 0;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Controls.Add(this.compareButton);
            this.buttonsPanel.Controls.Add(this.viewButton);
            this.buttonsPanel.Controls.Add(this.restoreButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(13, 410);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(574, 40);
            this.buttonsPanel.TabIndex = 1;
            // 
            // restoreButton
            // 
            this.restoreButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.restoreButton.Location = new System.Drawing.Point(289, 5);
            this.restoreButton.Margin = new System.Windows.Forms.Padding(5);
            this.restoreButton.Name = "restoreButton";
            this.restoreButton.Size = new System.Drawing.Size(100, 30);
            this.restoreButton.TabIndex = 0;
            this.restoreButton.Text = "Restore";
            this.restoreButton.UseVisualStyleBackColor = true;
            this.restoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // viewButton
            // 
            this.viewButton.Location = new System.Drawing.Point(394, 5);
            this.viewButton.Margin = new System.Windows.Forms.Padding(5);
            this.viewButton.Name = "viewButton";
            this.viewButton.Size = new System.Drawing.Size(80, 30);
            this.viewButton.TabIndex = 1;
            this.viewButton.Text = "View";
            this.viewButton.UseVisualStyleBackColor = true;
            this.viewButton.Click += new System.EventHandler(this.ViewButton_Click);
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(479, 5);
            this.compareButton.Margin = new System.Windows.Forms.Padding(5);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(90, 30);
            this.compareButton.TabIndex = 2;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.CompareButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(199, 5);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 30);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // VersionHistoryForm
            // 
            this.AcceptButton = this.restoreButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionHistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Version History";
            this.mainPanel.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox versionsListBox;
        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.FlowLayoutPanel buttonsPanel;
        private System.Windows.Forms.Button restoreButton;
        private System.Windows.Forms.Button viewButton;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
