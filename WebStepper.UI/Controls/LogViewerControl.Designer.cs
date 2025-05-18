namespace WebStepper.UI.Controls
{
    partial class LogViewerControl
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
            this.lvLogs = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            //
            // lvLogs
            //
            this.lvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogs.HideSelection = false;
            this.lvLogs.Location = new System.Drawing.Point(0, 0);
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.Size = new System.Drawing.Size(400, 300);
            this.lvLogs.TabIndex = 0;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            //
            // LogViewerControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvLogs);
            this.Name = "LogViewerControl";
            this.Size = new System.Drawing.Size(400, 300);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvLogs;
    }
}
