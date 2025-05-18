namespace WebStepper.UI.Controls
{
    partial class StepTrackerControl
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPage = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPageIdentifier = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStepType = new System.Windows.Forms.Label();
            this.lblStepSelector = new System.Windows.Forms.Label();
            this.lblStepValue = new System.Windows.Forms.Label();
            this.lblStepWait = new System.Windows.Forms.Label();
            this.txtStepDescription = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPage, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblStep, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblPageIdentifier, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 80);
            this.tableLayoutPanel1.TabIndex = 0;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Page:";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current Step:";
            //
            // lblPage
            //
            this.lblPage.AutoSize = true;
            this.lblPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPage.Location = new System.Drawing.Point(123, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(374, 25);
            this.lblPage.TabIndex = 2;
            this.lblPage.Text = "No page selected";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblStep
            //
            this.lblStep.AutoSize = true;
            this.lblStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStep.Location = new System.Drawing.Point(123, 25);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(374, 25);
            this.lblStep.TabIndex = 3;
            this.lblStep.Text = "No step selected";
            this.lblStep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Page Identifier:";
            //
            // lblPageIdentifier
            //
            this.lblPageIdentifier.AutoSize = true;
            this.lblPageIdentifier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageIdentifier.Location = new System.Drawing.Point(123, 50);
            this.lblPageIdentifier.Name = "lblPageIdentifier";
            this.lblPageIdentifier.Size = new System.Drawing.Size(374, 30);
            this.lblPageIdentifier.TabIndex = 5;
            this.lblPageIdentifier.Text = "No page identifier";
            this.lblPageIdentifier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // tableLayoutPanel2
            //
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblStepType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblStepSelector, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblStepValue, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblStepWait, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtStepDescription, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 4);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 80);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(500, 170);
            this.tableLayoutPanel2.TabIndex = 1;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type:";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Selector:";
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Value:";
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Wait:";
            //
            // lblStepType
            //
            this.lblStepType.AutoSize = true;
            this.lblStepType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStepType.Location = new System.Drawing.Point(123, 0);
            this.lblStepType.Name = "lblStepType";
            this.lblStepType.Size = new System.Drawing.Size(374, 25);
            this.lblStepType.TabIndex = 4;
            this.lblStepType.Text = "N/A";
            this.lblStepType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblStepSelector
            //
            this.lblStepSelector.AutoSize = true;
            this.lblStepSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStepSelector.Location = new System.Drawing.Point(123, 25);
            this.lblStepSelector.Name = "lblStepSelector";
            this.lblStepSelector.Size = new System.Drawing.Size(374, 25);
            this.lblStepSelector.TabIndex = 5;
            this.lblStepSelector.Text = "N/A";
            this.lblStepSelector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblStepValue
            //
            this.lblStepValue.AutoSize = true;
            this.lblStepValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStepValue.Location = new System.Drawing.Point(123, 50);
            this.lblStepValue.Name = "lblStepValue";
            this.lblStepValue.Size = new System.Drawing.Size(374, 25);
            this.lblStepValue.TabIndex = 6;
            this.lblStepValue.Text = "N/A";
            this.lblStepValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblStepWait
            //
            this.lblStepWait.AutoSize = true;
            this.lblStepWait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStepWait.Location = new System.Drawing.Point(123, 75);
            this.lblStepWait.Name = "lblStepWait";
            this.lblStepWait.Size = new System.Drawing.Size(374, 25);
            this.lblStepWait.TabIndex = 7;
            this.lblStepWait.Text = "N/A";
            this.lblStepWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtStepDescription
            //
            this.txtStepDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStepDescription.Location = new System.Drawing.Point(123, 103);
            this.txtStepDescription.Multiline = true;
            this.txtStepDescription.Name = "txtStepDescription";
            this.txtStepDescription.ReadOnly = true;
            this.txtStepDescription.Size = new System.Drawing.Size(374, 64);
            this.txtStepDescription.TabIndex = 8;
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Description:";
            //
            // StepTrackerControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "StepTrackerControl";
            this.Size = new System.Drawing.Size(500, 250);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPageIdentifier;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblStepType;
        private System.Windows.Forms.Label lblStepSelector;
        private System.Windows.Forms.Label lblStepValue;
        private System.Windows.Forms.Label lblStepWait;
        private System.Windows.Forms.TextBox txtStepDescription;
        private System.Windows.Forms.Label label8;
    }
}
