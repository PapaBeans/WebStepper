namespace WebStepper.UI.Controls
{
    partial class SelectorPanelControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView lvSelectors;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnAddStep;
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
        ///
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lvSelectors = new System.Windows.Forms.ListView();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnAddStep = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            //
            // splitContainer.Panel1
            //
            this.splitContainer.Panel1.Controls.Add(this.lvSelectors);
            //
            // splitContainer.Panel2
            //
            this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer.Size = new System.Drawing.Size(700, 150);
            this.splitContainer.SplitterDistance = 500;
            this.splitContainer.TabIndex = 0;
            //
            // lvSelectors
            //
            this.lvSelectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSelectors.HideSelection = false;
            this.lvSelectors.Location = new System.Drawing.Point(0, 0);
            this.lvSelectors.Name = "lvSelectors";
            this.lvSelectors.Size = new System.Drawing.Size(500, 150);
            this.lvSelectors.TabIndex = 0;
            this.lvSelectors.UseCompatibleStateImageBehavior = false;
            //
            // propertyGrid
            //
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(196, 150);
            this.propertyGrid.TabIndex = 0;
            //
            // btnInsert
            //
            this.btnInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsert.Enabled = false;
            this.btnInsert.Location = new System.Drawing.Point(622, 3);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 1;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            //
            // btnAddStep
            //
            this.btnAddStep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddStep.Enabled = false;
            this.btnAddStep.Location = new System.Drawing.Point(460, 3);
            this.btnAddStep.Name = "btnAddStep";
            this.btnAddStep.Size = new System.Drawing.Size(75, 23);
            this.btnAddStep.TabIndex = 2;
            this.btnAddStep.Text = "Add Step";
            this.btnAddStep.UseVisualStyleBackColor = true;
            //
            // btnTest
            //
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Enabled = false;
            this.btnTest.Location = new System.Drawing.Point(541, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            //
            // panel1
            //
            this.panel1.Controls.Add(this.btnInsert);
            this.panel1.Controls.Add(this.btnAddStep);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 30);
            this.panel1.TabIndex = 1;
            //
            // SelectorPanelControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panel1);
            this.Name = "SelectorPanelControl";
            this.Size = new System.Drawing.Size(700, 180);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


    }
}
