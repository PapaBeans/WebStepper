using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace InsuranceAutomation.UI
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
        private WebView2 webView;
        private ListBox stepsListBox;
        private TextBox logTextBox;
        private Button loadTemplateButton;
        private Button newTemplateButton;
        private Button editTemplateButton;
        private Button versionHistoryButton;
        private Button startButton;
        private Button pauseButton;
        private Button resumeButton;
        private Button stepButton;
        private Button resetButton;
        private Button exportLogsButton;
        private TextBox urlTextBox;
        private Button navigateButton;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newTemplateToolStripMenuItem;
        private ToolStripMenuItem loadTemplateToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exportLogsToolStripMenuItem;
        private Panel buttonsPanel;
        private Panel logPanel;
        private Panel automationButtonsPanel;
        private Panel navigationPanel;
        private ToolStripMenuItem automationToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem pauseToolStripMenuItem;
        private ToolStripMenuItem resumeToolStripMenuItem;
        private ToolStripMenuItem stepToolStripMenuItem;
        private ToolStripMenuItem resetToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TableLayoutPanel mainPanel;
        private TableLayoutPanel rightPanel;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.stepsListBox = new System.Windows.Forms.ListBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.loadTemplateButton = new System.Windows.Forms.Button();
            this.newTemplateButton = new System.Windows.Forms.Button();
            this.editTemplateButton = new System.Windows.Forms.Button();
            this.versionHistoryButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.resumeButton = new System.Windows.Forms.Button();
            this.stepButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.exportLogsButton = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.navigateButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.automationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.rightPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.logPanel = new System.Windows.Forms.Panel();
            this.automationButtonsPanel = new System.Windows.Forms.Panel();
            this.navigationPanel = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.logPanel.SuspendLayout();
            this.automationButtonsPanel.SuspendLayout();
            this.navigationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = true;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(3, 3);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(1152, 740);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            // 
            // stepsListBox
            // 
            this.stepsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepsListBox.FormattingEnabled = true;
            this.stepsListBox.IntegralHeight = false;
            this.stepsListBox.Location = new System.Drawing.Point(3, 3);
            this.stepsListBox.Name = "stepsListBox";
            this.stepsListBox.Size = new System.Drawing.Size(485, 274);
            this.stepsListBox.TabIndex = 0;
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.White;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(491, 420);
            this.logTextBox.TabIndex = 0;
            // 
            // loadTemplateButton
            // 
            this.loadTemplateButton.Location = new System.Drawing.Point(10, 7);
            this.loadTemplateButton.Name = "loadTemplateButton";
            this.loadTemplateButton.Size = new System.Drawing.Size(110, 23);
            this.loadTemplateButton.TabIndex = 0;
            this.loadTemplateButton.Text = "Load Template";
            this.loadTemplateButton.UseVisualStyleBackColor = true;
            this.loadTemplateButton.Click += new System.EventHandler(this.LoadTemplateButton_Click);
            // 
            // newTemplateButton
            // 
            this.newTemplateButton.Location = new System.Drawing.Point(125, 7);
            this.newTemplateButton.Name = "newTemplateButton";
            this.newTemplateButton.Size = new System.Drawing.Size(110, 23);
            this.newTemplateButton.TabIndex = 1;
            this.newTemplateButton.Text = "New Template";
            this.newTemplateButton.UseVisualStyleBackColor = true;
            this.newTemplateButton.Click += new System.EventHandler(this.NewTemplateButton_Click);
            // 
            // editTemplateButton
            // 
            this.editTemplateButton.Enabled = false;
            this.editTemplateButton.Location = new System.Drawing.Point(240, 7);
            this.editTemplateButton.Name = "editTemplateButton";
            this.editTemplateButton.Size = new System.Drawing.Size(110, 23);
            this.editTemplateButton.TabIndex = 2;
            this.editTemplateButton.Text = "Edit Template";
            this.editTemplateButton.UseVisualStyleBackColor = true;
            this.editTemplateButton.Click += new System.EventHandler(this.EditTemplateButton_Click);
            // 
            // versionHistoryButton
            // 
            this.versionHistoryButton.Enabled = false;
            this.versionHistoryButton.Location = new System.Drawing.Point(355, 7);
            this.versionHistoryButton.Name = "versionHistoryButton";
            this.versionHistoryButton.Size = new System.Drawing.Size(110, 23);
            this.versionHistoryButton.TabIndex = 3;
            this.versionHistoryButton.Text = "Version History";
            this.versionHistoryButton.UseVisualStyleBackColor = true;
            this.versionHistoryButton.Click += new System.EventHandler(this.VersionHistoryButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(5, 5);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(70, 25);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(80, 5);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(70, 25);
            this.pauseButton.TabIndex = 1;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // resumeButton
            // 
            this.resumeButton.Enabled = false;
            this.resumeButton.Location = new System.Drawing.Point(155, 5);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(70, 25);
            this.resumeButton.TabIndex = 2;
            this.resumeButton.Text = "Resume";
            this.resumeButton.UseVisualStyleBackColor = true;
            this.resumeButton.Click += new System.EventHandler(this.ResumeButton_Click);
            // 
            // stepButton
            // 
            this.stepButton.Enabled = false;
            this.stepButton.Location = new System.Drawing.Point(230, 5);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(70, 25);
            this.stepButton.TabIndex = 3;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(305, 5);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(70, 25);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // exportLogsButton
            // 
            this.exportLogsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportLogsButton.Location = new System.Drawing.Point(418, 5);
            this.exportLogsButton.Name = "exportLogsButton";
            this.exportLogsButton.Size = new System.Drawing.Size(70, 25);
            this.exportLogsButton.TabIndex = 5;
            this.exportLogsButton.Text = "Export Logs";
            this.exportLogsButton.UseVisualStyleBackColor = true;
            this.exportLogsButton.Click += new System.EventHandler(this.ExportLogsButton_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(68, 11);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(1481, 20);
            this.urlTextBox.TabIndex = 0;
            // 
            // navigateButton
            // 
            this.navigateButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.navigateButton.Location = new System.Drawing.Point(1555, 0);
            this.navigateButton.Name = "navigateButton";
            this.navigateButton.Size = new System.Drawing.Size(100, 40);
            this.navigateButton.TabIndex = 1;
            this.navigateButton.Text = "Navigate";
            this.navigateButton.UseVisualStyleBackColor = true;
            this.navigateButton.Click += new System.EventHandler(this.NavigateButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 810);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1655, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Ready";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.automationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1655, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTemplateToolStripMenuItem,
            this.loadTemplateToolStripMenuItem,
            this.toolStripSeparator1,
            this.exportLogsToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newTemplateToolStripMenuItem
            // 
            this.newTemplateToolStripMenuItem.Name = "newTemplateToolStripMenuItem";
            this.newTemplateToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newTemplateToolStripMenuItem.Text = "&New Template...";
            this.newTemplateToolStripMenuItem.Click += new System.EventHandler(this.NewTemplateButton_Click);
            // 
            // loadTemplateToolStripMenuItem
            // 
            this.loadTemplateToolStripMenuItem.Name = "loadTemplateToolStripMenuItem";
            this.loadTemplateToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.loadTemplateToolStripMenuItem.Text = "&Load Template...";
            this.loadTemplateToolStripMenuItem.Click += new System.EventHandler(this.LoadTemplateButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // exportLogsToolStripMenuItem
            // 
            this.exportLogsToolStripMenuItem.Name = "exportLogsToolStripMenuItem";
            this.exportLogsToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exportLogsToolStripMenuItem.Text = "&Export Logs...";
            this.exportLogsToolStripMenuItem.Click += new System.EventHandler(this.ExportLogsButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(158, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitApplication);
            // 
            // automationToolStripMenuItem
            // 
            this.automationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.resumeToolStripMenuItem,
            this.stepToolStripMenuItem,
            this.resetToolStripMenuItem});
            this.automationToolStripMenuItem.Name = "automationToolStripMenuItem";
            this.automationToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.automationToolStripMenuItem.Text = "&Automation";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.startToolStripMenuItem.Text = "&Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.pauseToolStripMenuItem.Text = "&Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.resumeToolStripMenuItem.Text = "&Resume";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.ResumeButton_Click);
            // 
            // stepToolStripMenuItem
            // 
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.stepToolStripMenuItem.Text = "S&tep";
            this.stepToolStripMenuItem.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.resetToolStripMenuItem.Text = "R&eset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 2;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.mainPanel.Controls.Add(this.webView, 0, 0);
            this.mainPanel.Controls.Add(this.rightPanel, 1, 0);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 64);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 1;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.Size = new System.Drawing.Size(1655, 746);
            this.mainPanel.TabIndex = 2;
            // 
            // rightPanel
            // 
            this.rightPanel.ColumnCount = 1;
            this.rightPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.rightPanel.Controls.Add(this.stepsListBox, 0, 0);
            this.rightPanel.Controls.Add(this.buttonsPanel, 0, 1);
            this.rightPanel.Controls.Add(this.logPanel, 0, 2);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(1161, 3);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.RowCount = 3;
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.rightPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.rightPanel.Size = new System.Drawing.Size(491, 740);
            this.rightPanel.TabIndex = 1;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.loadTemplateButton);
            this.buttonsPanel.Controls.Add(this.newTemplateButton);
            this.buttonsPanel.Controls.Add(this.editTemplateButton);
            this.buttonsPanel.Controls.Add(this.versionHistoryButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.Location = new System.Drawing.Point(0, 280);
            this.buttonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(491, 40);
            this.buttonsPanel.TabIndex = 1;
            // 
            // logPanel
            // 
            this.logPanel.Controls.Add(this.logTextBox);
            this.logPanel.Controls.Add(this.automationButtonsPanel);
            this.logPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logPanel.Location = new System.Drawing.Point(0, 320);
            this.logPanel.Margin = new System.Windows.Forms.Padding(0);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(491, 420);
            this.logPanel.TabIndex = 2;
            // 
            // automationButtonsPanel
            // 
            this.automationButtonsPanel.Controls.Add(this.startButton);
            this.automationButtonsPanel.Controls.Add(this.pauseButton);
            this.automationButtonsPanel.Controls.Add(this.resumeButton);
            this.automationButtonsPanel.Controls.Add(this.stepButton);
            this.automationButtonsPanel.Controls.Add(this.resetButton);
            this.automationButtonsPanel.Controls.Add(this.exportLogsButton);
            this.automationButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.automationButtonsPanel.Location = new System.Drawing.Point(0, 420);
            this.automationButtonsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.automationButtonsPanel.Name = "automationButtonsPanel";
            this.automationButtonsPanel.Size = new System.Drawing.Size(491, 0);
            this.automationButtonsPanel.TabIndex = 0;
            // 
            // navigationPanel
            // 
            this.navigationPanel.Controls.Add(this.btnHome);
            this.navigationPanel.Controls.Add(this.navigateButton);
            this.navigationPanel.Controls.Add(this.urlTextBox);
            this.navigationPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationPanel.Location = new System.Drawing.Point(0, 24);
            this.navigationPanel.Name = "navigationPanel";
            this.navigationPanel.Size = new System.Drawing.Size(1655, 40);
            this.navigationPanel.TabIndex = 1;
            // 
            // btnHome
            // 
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnHome.Location = new System.Drawing.Point(0, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(62, 40);
            this.btnHome.TabIndex = 2;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1655, 832);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.navigationPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Insurance Quote Automation";
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.buttonsPanel.ResumeLayout(false);
            this.logPanel.ResumeLayout(false);
            this.logPanel.PerformLayout();
            this.automationButtonsPanel.ResumeLayout(false);
            this.navigationPanel.ResumeLayout(false);
            this.navigationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void ExitApplication(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void AboutMenuItem_Click(object sender, System.EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(
                "Insurance Quote Automation Tool\nVersion 1.0\n\n© " + System.DateTime.Now.Year,
                "About", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        private Button btnHome;
    }
}
