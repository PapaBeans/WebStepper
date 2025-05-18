namespace WebStepper.UI.Forms
{
    partial class StepEditorDialog
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
            this.lblStepName = new System.Windows.Forms.Label();
            this.txtStepName = new System.Windows.Forms.TextBox();
            this.lblStepType = new System.Windows.Forms.Label();
            this.cboStepType = new System.Windows.Forms.ComboBox();
            this.lblSelector = new System.Windows.Forms.Label();
            this.txtSelector = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblWaitBefore = new System.Windows.Forms.Label();
            this.numWaitBefore = new System.Windows.Forms.NumericUpDown();
            this.lblWaitAfter = new System.Windows.Forms.Label();
            this.numWaitAfter = new System.Windows.Forms.NumericUpDown();
            this.lblMaxWait = new System.Windows.Forms.Label();
            this.numMaxWait = new System.Windows.Forms.NumericUpDown();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxWait)).BeginInit();
            this.SuspendLayout();
            //
            // lblStepName
            //
            this.lblStepName.AutoSize = true;
            this.lblStepName.Location = new System.Drawing.Point(12, 15);
            this.lblStepName.Name = "lblStepName";
            this.lblStepName.Size = new System.Drawing.Size(38, 13);
            this.lblStepName.TabIndex = 0;
            this.lblStepName.Text = "Name:";
            //
            // txtStepName
            //
            this.txtStepName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStepName.Location = new System.Drawing.Point(92, 12);
            this.txtStepName.Name = "txtStepName";
            this.txtStepName.Size = new System.Drawing.Size(280, 20);
            this.txtStepName.TabIndex = 1;
            //
            // lblStepType
            //
            this.lblStepType.AutoSize = true;
            this.lblStepType.Location = new System.Drawing.Point(12, 41);
            this.lblStepType.Name = "lblStepType";
            this.lblStepType.Size = new System.Drawing.Size(34, 13);
            this.lblStepType.TabIndex = 2;
            this.lblStepType.Text = "Type:";
            //
            // cboStepType
            //
            this.cboStepType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStepType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStepType.FormattingEnabled = true;
            this.cboStepType.Location = new System.Drawing.Point(92, 38);
            this.cboStepType.Name = "cboStepType";
            this.cboStepType.Size = new System.Drawing.Size(280, 21);
            this.cboStepType.TabIndex = 3;
            //
            // lblSelector
            //
            this.lblSelector.AutoSize = true;
            this.lblSelector.Location = new System.Drawing.Point(12, 68);
            this.lblSelector.Name = "lblSelector";
            this.lblSelector.Size = new System.Drawing.Size(49, 13);
            this.lblSelector.TabIndex = 4;
            this.lblSelector.Text = "Selector:";
            //
            // txtSelector
            //
            this.txtSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelector.Location = new System.Drawing.Point(92, 65);
            this.txtSelector.Name = "txtSelector";
            this.txtSelector.Size = new System.Drawing.Size(280, 20);
            this.txtSelector.TabIndex = 5;
            //
            // lblValue
            //
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(12, 94);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(37, 13);
            this.lblValue.TabIndex = 6;
            this.lblValue.Text = "Value:";
            //
            // txtValue
            //
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.Location = new System.Drawing.Point(92, 91);
            this.txtValue.Multiline = true;
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(280, 60);
            this.txtValue.TabIndex = 7;
            //
            // lblWaitBefore
            //
            this.lblWaitBefore.AutoSize = true;
            this.lblWaitBefore.Location = new System.Drawing.Point(12, 161);
            this.lblWaitBefore.Name = "lblWaitBefore";
            this.lblWaitBefore.Size = new System.Drawing.Size(74, 13);
            this.lblWaitBefore.TabIndex = 8;
            this.lblWaitBefore.Text = "Wait Before (ms):";
            //
            // numWaitBefore
            //
            this.numWaitBefore.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numWaitBefore.Location = new System.Drawing.Point(92, 159);
            this.numWaitBefore.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numWaitBefore.Name = "numWaitBefore";
            this.numWaitBefore.Size = new System.Drawing.Size(120, 20);
            this.numWaitBefore.TabIndex = 9;
            //
            // lblWaitAfter
            //
            this.lblWaitAfter.AutoSize = true;
            this.lblWaitAfter.Location = new System.Drawing.Point(12, 187);
            this.lblWaitAfter.Name = "lblWaitAfter";
            this.lblWaitAfter.Size = new System.Drawing.Size(72, 13);
            this.lblWaitAfter.TabIndex = 10;
            this.lblWaitAfter.Text = "Wait After (ms):";
            //
            // numWaitAfter
            //
            this.numWaitAfter.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numWaitAfter.Location = new System.Drawing.Point(92, 185);
            this.numWaitAfter.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numWaitAfter.Name = "numWaitAfter";
            this.numWaitAfter.Size = new System.Drawing.Size(120, 20);
            this.numWaitAfter.TabIndex = 11;
            //
            // lblMaxWait
            //
            this.lblMaxWait.AutoSize = true;
            this.lblMaxWait.Location = new System.Drawing.Point(12, 213);
            this.lblMaxWait.Name = "lblMaxWait";
            this.lblMaxWait.Size = new System.Drawing.Size(76, 13);
            this.lblMaxWait.TabIndex = 12;
            this.lblMaxWait.Text = "Max Wait (ms):";
            //
            // numMaxWait
            //
            this.numMaxWait.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numMaxWait.Location = new System.Drawing.Point(92, 211);
            this.numMaxWait.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numMaxWait.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numMaxWait.Name = "numMaxWait";
            this.numMaxWait.Size = new System.Drawing.Size(120, 20);
            this.numMaxWait.TabIndex = 13;
            this.numMaxWait.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            //
            // lblDescription
            //
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 241);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 14;
            this.lblDescription.Text = "Description:";
            //
            // txtDescription
            //
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(92, 238);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(280, 60);
            this.txtDescription.TabIndex = 15;
            //
            // btnOK
            //
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(216, 315);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 315);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // StepEditorDialog
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 350);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.numMaxWait);
            this.Controls.Add(this.lblMaxWait);
            this.Controls.Add(this.numWaitAfter);
            this.Controls.Add(this.lblWaitAfter);
            this.Controls.Add(this.numWaitBefore);
            this.Controls.Add(this.lblWaitBefore);
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.txtSelector);
            this.Controls.Add(this.lblSelector);
            this.Controls.Add(this.cboStepType);
            this.Controls.Add(this.lblStepType);
            this.Controls.Add(this.txtStepName);
            this.Controls.Add(this.lblStepName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StepEditorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Step";
            ((System.ComponentModel.ISupportInitialize)(this.numWaitBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStepName;
        private System.Windows.Forms.TextBox txtStepName;
        private System.Windows.Forms.Label lblStepType;
        private System.Windows.Forms.ComboBox cboStepType;
        private System.Windows.Forms.Label lblSelector;
        private System.Windows.Forms.TextBox txtSelector;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblWaitBefore;
        private System.Windows.Forms.NumericUpDown numWaitBefore;
        private System.Windows.Forms.Label lblWaitAfter;
        private System.Windows.Forms.NumericUpDown numWaitAfter;
        private System.Windows.Forms.Label lblMaxWait;
        private System.Windows.Forms.NumericUpDown numMaxWait;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}
