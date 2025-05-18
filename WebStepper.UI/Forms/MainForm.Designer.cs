namespace WebStepper.UI.Forms
{
    partial class MainForm
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
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftTabControl = new System.Windows.Forms.TabControl();
            this.tabTemplateEditor = new System.Windows.Forms.TabPage();
            this.templateEditorControl = new WebStepper.UI.Controls.TemplateEditorControl();
            this.tabStepTree = new System.Windows.Forms.TabPage();
            this.stepTreeControl = new WebStepper.UI.Controls.StepTreeControl();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.bottomTabControl = new System.Windows.Forms.TabControl();
            this.tabLogs = new System.Windows.Forms.TabPage();
            this.logViewerControl = new WebStepper.UI.Controls.LogViewerControl();
            this.tabStepTracker = new System.Windows.Forms.TabPage();
            this.stepTrackerControl = new WebStepper.UI.Controls.StepTrackerControl();
            this.tabSelectorPanel = new System.Windows.Forms.TabPage();
            this.selectorPanelControl = new WebStepper.UI.Controls.SelectorPanelControl();
            this.toolbarControl = new WebStepper.UI.Controls.ToolbarControl();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.leftTabControl.SuspendLayout();
            this.tabTemplateEditor.SuspendLayout();
            this.tabStepTree.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.bottomTabControl.SuspendLayout();
            this.tabLogs.SuspendLayout();
            this.tabStepTracker.SuspendLayout();
            this.tabSelectorPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // mainSplitContainer
            //
            this.mainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 40);
            this.mainSplitContainer.Name = "mainSplitContainer";
            //
            // mainSplitContainer.Panel1
            //
            this.mainSplitContainer.Panel1.Controls.Add(this.leftTabControl);
            this.mainSplitContainer.Panel1MinSize = 300;
            //
            // mainSplitContainer.Panel2
            //
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1264, 681);
            this.mainSplitContainer.SplitterDistance = 350;
            this.mainSplitContainer.TabIndex = 0;
            //
            // leftTabControl
            //
            this.leftTabControl.Controls.Add(this.tabTemplateEditor);
            this.leftTabControl.Controls.Add(this.tabStepTree);
            this.leftTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftTabControl.Location = new System.Drawing.Point(0, 0);
            this.leftTabControl.Name = "leftTabControl";
            this.leftTabControl.SelectedIndex = 0;
            this.leftTabControl.Size = new System.Drawing.Size(350, 681);
            this.leftTabControl.TabIndex = 0;
            //
            // tabTemplateEditor
            //
            this.tabTemplateEditor.Controls.Add(this.templateEditorControl);
            this.tabTemplateEditor.Location = new System.Drawing.Point(4, 22);
            this.tabTemplateEditor.Name = "tabTemplateEditor";
            this.tabTemplateEditor.Padding = new System.Windows.Forms.Padding(3);
            this.tabTemplateEditor.Size = new System.Drawing.Size(342, 655);
            this.tabTemplateEditor.TabIndex = 0;
            this.tabTemplateEditor.Text = "Template Editor";
            this.tabTemplateEditor.UseVisualStyleBackColor = true;
            //
            // templateEditorControl
            //
            this.templateEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.templateEditorControl.Location = new System.Drawing.Point(3, 3);
            this.templateEditorControl.Name = "templateEditorControl";
            this.templateEditorControl.Size = new System.Drawing.Size(336, 649);
            this.templateEditorControl.TabIndex = 0;
            //
            // tabStepTree
            //
            this.tabStepTree.Controls.Add(this.stepTreeControl);
            this.tabStepTree.Location = new System.Drawing.Point(4, 22);
            this.tabStepTree.Name = "tabStepTree";
            this.tabStepTree.Padding = new System.Windows.Forms.Padding(3);
            this.tabStepTree.Size = new System.Drawing.Size(342, 655);
            this.tabStepTree.TabIndex = 1;
            this.tabStepTree.Text = "Step Tree";
            this.tabStepTree.UseVisualStyleBackColor = true;
            //
            // stepTreeControl
            //
            this.stepTreeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepTreeControl.Location = new System.Drawing.Point(3, 3);
            this.stepTreeControl.Name = "stepTreeControl";
            this.stepTreeControl.Size = new System.Drawing.Size(336, 649);
            this.stepTreeControl.TabIndex = 0;
            //
            // rightSplitContainer
            //
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Name = "rightSplitContainer";
            this.rightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //
            // rightSplitContainer.Panel1
            //
            this.rightSplitContainer.Panel1.Controls.Add(this.webView);
            this.rightSplitContainer.Panel1MinSize = 300;
            //
            // rightSplitContainer.Panel2
            //
            this.rightSplitContainer.Panel2.Controls.Add(this.bottomTabControl);
            this.rightSplitContainer.Panel2MinSize = 150;
            this.rightSplitContainer.Size = new System.Drawing.Size(910, 681);
            this.rightSplitContainer.SplitterDistance = 480;
            this.rightSplitContainer.TabIndex = 0;
            //
            // webView
            //
            this.webView.AllowExternalDrop = true;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(910, 480);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            //
            // bottomTabControl
            //
            this.bottomTabControl.Controls.Add(this.tabLogs);
            this.bottomTabControl.Controls.Add(this.tabStepTracker);
            this.bottomTabControl.Controls.Add(this.tabSelectorPanel);
            this.bottomTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomTabControl.Location = new System.Drawing.Point(0, 0);
            this.bottomTabControl.Name = "bottomTabControl";
            this.bottomTabControl.SelectedIndex = 0;
            this.bottomTabControl.Size = new System.Drawing.Size(910, 197);
            this.bottomTabControl.TabIndex = 0;
            //
            // tabLogs
            //
            this.tabLogs.Controls.Add(this.logViewerControl);
            this.tabLogs.Location = new System.Drawing.Point(4, 22);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.Padding = new System.Windows.Forms.Padding(3);
            this.tabLogs.Size = new System.Drawing.Size(902, 171);
            this.tabLogs.TabIndex = 0;
            this.tabLogs.Text = "Logs";
            this.tabLogs.UseVisualStyleBackColor = true;
            //
            // logViewerControl
            //
            this.logViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logViewerControl.Location = new System.Drawing.Point(3, 3);
            this.logViewerControl.Name = "logViewerControl";
            this.logViewerControl.Size = new System.Drawing.Size(896, 165);
            this.logViewerControl.TabIndex = 0;
            //
            // tabStepTracker
            //
            this.tabStepTracker.Controls.Add(this.stepTrackerControl);
            this.tabStepTracker.Location = new System.Drawing.Point(4, 22);
            this.tabStepTracker.Name = "tabStepTracker";
            this.tabStepTracker.Padding = new System.Windows.Forms.Padding(3);
            this.tabStepTracker.Size = new System.Drawing.Size(902, 171);
            this.tabStepTracker.TabIndex = 1;
            this.tabStepTracker.Text = "Step Tracker";
            this.tabStepTracker.UseVisualStyleBackColor = true;
            //
            // stepTrackerControl
            //
            this.stepTrackerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepTrackerControl.Location = new System.Drawing.Point(3, 3);
            this.stepTrackerControl.Name = "stepTrackerControl";
            this.stepTrackerControl.Size = new System.Drawing.Size(896, 165);
            this.stepTrackerControl.TabIndex = 0;
            //
            // tabSelectorPanel
            //
            this.tabSelectorPanel.Controls.Add(this.selectorPanelControl);
            this.tabSelectorPanel.Location = new System.Drawing.Point(4, 22);
            this.tabSelectorPanel.Name = "tabSelectorPanel";
            this.tabSelectorPanel.Padding = new System.Windows.Forms.Padding(3);
            this.tabSelectorPanel.Size = new System.Drawing.Size(902, 171);
            this.tabSelectorPanel.TabIndex = 2;
            this.tabSelectorPanel.Text = "Selector Panel";
            this.tabSelectorPanel.UseVisualStyleBackColor = true;
            //
            // selectorPanelControl
            //
            this.selectorPanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectorPanelControl.Location = new System.Drawing.Point(3, 3);
            this.selectorPanelControl.Name = "selectorPanelControl";
            this.selectorPanelControl.Size = new System.Drawing.Size(896, 165);
            this.selectorPanelControl.TabIndex = 0;
            //
            // toolbarControl
            //
            this.toolbarControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolbarControl.Location = new System.Drawing.Point(0, 0);
            this.toolbarControl.Name = "toolbarControl";
            this.toolbarControl.Size = new System.Drawing.Size(1264, 40);
            this.toolbarControl.TabIndex = 1;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 721);
            this.Controls.Add(this.toolbarControl);
            this.Controls.Add(this.mainSplitContainer);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insurance Quoting Automation Tool";
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftTabControl.ResumeLayout(false);
            this.tabTemplateEditor.ResumeLayout(false);
            this.tabStepTree.ResumeLayout(false);
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.bottomTabControl.ResumeLayout(false);
            this.tabLogs.ResumeLayout(false);
            this.tabStepTracker.ResumeLayout(false);
            this.tabSelectorPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.TabControl leftTabControl;
        private System.Windows.Forms.TabPage tabTemplateEditor;
        private System.Windows.Forms.TabPage tabStepTree;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.TabControl bottomTabControl;
        private System.Windows.Forms.TabPage tabLogs;
        private System.Windows.Forms.TabPage tabStepTracker;
        private System.Windows.Forms.TabPage tabSelectorPanel;
        private Controls.ToolbarControl toolbarControl;
        private Controls.TemplateEditorControl templateEditorControl;
        private Controls.StepTreeControl stepTreeControl;
        private Controls.LogViewerControl logViewerControl;
        private Controls.StepTrackerControl stepTrackerControl;
        private Controls.SelectorPanelControl selectorPanelControl;
    }
}
