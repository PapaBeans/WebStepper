namespace WebStepper.UI.Controls
{
    partial class ToolbarControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolbarControl));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnPause = new System.Windows.Forms.ToolStripButton();
            this.btnResume = new System.Windows.Forms.ToolStripButton();
            this.btnStep = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPickElement = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            //
            // toolStrip
            //
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.toolStripSeparator1,
            this.btnStart,
            this.btnPause,
            this.btnResume,
            this.btnStep,
            this.btnReset,
            this.toolStripSeparator2,
            this.btnPickElement});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 38);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            //
            // btnNew
            //
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(35, 35);
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            //
            // btnOpen
            //
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(40, 35);
            this.btnOpen.Text = "Open";
            this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            //
            // btnSave
            //
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 35);
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // toolStripSeparator1
            //
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            //
            // btnStart
            //
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(35, 35);
            this.btnStart.Text = "Start";
            this.btnStart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //
            // btnPause
            //
            this.btnPause.Image = ((System.Drawing.Image)(resources.GetObject("btnPause.Image")));
            this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(42, 35);
            this.btnPause.Text = "Pause";
            this.btnPause.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            //
            // btnResume
            //
            this.btnResume.Image = ((System.Drawing.Image)(resources.GetObject("btnResume.Image")));
            this.btnResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(55, 35);
            this.btnResume.Text = "Resume";
            this.btnResume.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            //
            // btnStep
            //
            this.btnStep.Image = ((System.Drawing.Image)(resources.GetObject("btnStep.Image")));
            this.btnStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStep.Name = "btnStep";
            this.btnStep.Size = new System.Drawing.Size(34, 35);
            this.btnStep.Text = "Step";
            this.btnStep.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            //
            // btnReset
            //
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(39, 35);
            this.btnReset.Text = "Reset";
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // toolStripSeparator2
            //
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            //
            // btnPickElement
            //
            this.btnPickElement.Image = ((System.Drawing.Image)(resources.GetObject("btnPickElement.Image")));
            this.btnPickElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPickElement.Name = "btnPickElement";
            this.btnPickElement.Size = new System.Drawing.Size(39, 35);
            this.btnPickElement.Text = "Select";
            this.btnPickElement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPickElement.ToolTipText = "Select Element";
            this.btnPickElement.Click += new System.EventHandler(this.btnPickElement_Click);
            //
            // ToolbarControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "ToolbarControl";
            this.Size = new System.Drawing.Size(800, 40);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnPause;
        private System.Windows.Forms.ToolStripButton btnResume;
        private System.Windows.Forms.ToolStripButton btnStep;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnPickElement;
    }
}
