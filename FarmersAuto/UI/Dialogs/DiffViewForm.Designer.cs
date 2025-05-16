namespace InsuranceAutomation.UI.Dialogs
{
    partial class DiffViewForm
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
            this.currentTextBox = new System.Windows.Forms.TextBox();
            this.versionTextBox = new System.Windows.Forms.TextBox();
            this.currentLabel = new System.Windows.Forms.Label();
            this.versionLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.closeButton = new System.Windows.Forms.Button();
            this.differenceLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentTextBox
            // 
            this.currentTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentTextBox.Location = new System.Drawing.Point(3, 33);
            this.currentTextBox.Multiline = true;
            this.currentTextBox.Name = "currentTextBox";
            this.currentTextBox.ReadOnly = true;
            this.currentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.currentTextBox.Size = new System.Drawing.Size(494, 630);
            this.currentTextBox.TabIndex = 0;
            this.currentTextBox.WordWrap = false;
            // 
            // versionTextBox
            // 
            this.versionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionTextBox.Location = new System.Drawing.Point(503, 33);
            this.versionTextBox.Multiline = true;
            this.versionTextBox.Name = "versionTextBox";
            this.versionTextBox.ReadOnly = true;
            this.versionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.versionTextBox.Size = new System.Drawing.Size(494, 630);
            this.versionTextBox.TabIndex = 1;
            this.versionTextBox.WordWrap = false;
            // 
            // currentLabel
            // 
            this.currentLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentLabel.Location = new System.Drawing.Point(3, 0);
            this.currentLabel.Name = "currentLabel";
            this.currentLabel.Size = new System.Drawing.Size(494, 30);
            this.currentLabel.TabIndex = 2;
            this.currentLabel.Text = "Current Template";
            this.currentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // versionLabel
            // 
            this.versionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(503, 0);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(494, 30);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "Selected Version";
            this.versionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 2;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainPanel.Controls.Add(this.currentLabel, 0, 0);
            this.mainPanel.Controls.Add(this.versionLabel, 1, 0);
            this.mainPanel.Controls.Add(this.currentTextBox, 0, 1);
            this.mainPanel.Controls.Add(this.versionTextBox, 1, 1);
            this.mainPanel.Controls.Add(this.differenceLabel, 0, 2);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 3;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.mainPanel.Size = new System.Drawing.Size(1000, 700);
            this.mainPanel.TabIndex = 0;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(890, 662);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(100, 30);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // differenceLabel
            // 
            this.differenceLabel.AutoSize = true;
            this.mainPanel.SetColumnSpan(this.differenceLabel, 2);
            this.differenceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.differenceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.differenceLabel.Location = new System.Drawing.Point(3, 666);
            this.differenceLabel.Name = "differenceLabel";
            this.differenceLabel.Size = new System.Drawing.Size(994, 34);
            this.differenceLabel.TabIndex = 4;
            this.differenceLabel.Text = "Comparing templates...";
            this.differenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DiffViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.mainPanel);
            this.MinimizeBox = false;
            this.Name = "DiffViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Template Comparison";
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox currentTextBox;
        private System.Windows.Forms.TextBox versionTextBox;
        private System.Windows.Forms.Label currentLabel;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.TableLayoutPanel mainPanel;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label differenceLabel;
    }
}
