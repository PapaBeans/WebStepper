using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using InsuranceAutomation.Models;
using InsuranceAutomation.Services;
using Microsoft.Web.WebView2.WinForms;

namespace InsuranceAutomation.UI.Dialogs
{
    /// <summary>
    /// Dialog for selecting and testing CSS selectors.
    /// </summary>
    public partial class SelectorPickerDialog : Form
    {
        private readonly WebView2 webView;
        private readonly ElementPickerService elementPickerService;
        private readonly string initialSelector;
        private readonly List<string> alternativeSelectors = new List<string>();
        private bool isUpdatingUI = false;

        /// <summary>
        /// Gets the selected CSS selector.
        /// </summary>
        public string SelectedSelector { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorPickerDialog"/> class.
        /// </summary>
        /// <param name="webView">The WebView2 control to use for element picking.</param>
        /// <param name="initialSelector">The initial selector value (optional).</param>
        public SelectorPickerDialog(WebView2 webView, string initialSelector = null)
        {
            InitializeComponent();
            SetupButtonHandlers();

            this.webView = webView ?? throw new ArgumentNullException(nameof(webView));
            this.initialSelector = initialSelector;
            this.elementPickerService = ElementPickerService.Instance;
            
            // Subscribe to element picker events
            elementPickerService.ElementSelected += ElementPickerService_ElementSelected;
            elementPickerService.PickingCanceled += ElementPickerService_PickingCanceled;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Initialize the element picker service
            try
            {
                await elementPickerService.InitializeAsync(webView);
                
                // Set initial selector if provided
                if (!string.IsNullOrEmpty(initialSelector))
                {
                    selectorTextBox.Text = initialSelector;
                    await TestCurrentSelector();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing element picker: {ex.Message}", 
                    "Element Picker Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            
            // Unsubscribe from events
            elementPickerService.ElementSelected -= ElementPickerService_ElementSelected;
            elementPickerService.PickingCanceled -= ElementPickerService_PickingCanceled;
            
            // Stop picking if active
            if (elementPickerService.IsPickingActive)
            {
                elementPickerService.StopPickingAsync().ConfigureAwait(false);
            }
        }

        private void InitializeComponent()
        {
            this.selectorLabel = new System.Windows.Forms.Label();
            this.selectorTextBox = new System.Windows.Forms.TextBox();
            this.pickElementButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.alternativesLabel = new System.Windows.Forms.Label();
            this.alternativesListBox = new System.Windows.Forms.ListBox();
            this.resultPanel = new System.Windows.Forms.Panel();
            this.matchCountLabel = new System.Windows.Forms.Label();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();
            this.resultPanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // selectorLabel
            // 
            this.selectorLabel.AutoSize = true;
            this.selectorLabel.Location = new System.Drawing.Point(3, 9);
            this.selectorLabel.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.selectorLabel.Name = "selectorLabel";
            this.selectorLabel.Size = new System.Drawing.Size(82, 13);
            this.selectorLabel.TabIndex = 0;
            this.selectorLabel.Text = "CSS Selector:";
            // 
            // selectorTextBox
            // 
            this.selectorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectorTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectorTextBox.Location = new System.Drawing.Point(91, 6);
            this.selectorTextBox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.selectorTextBox.Name = "selectorTextBox";
            this.selectorTextBox.Size = new System.Drawing.Size(274, 22);
            this.selectorTextBox.TabIndex = 1;
            this.selectorTextBox.TextChanged += new System.EventHandler(this.SelectorTextBox_TextChanged);
            // 
            // pickElementButton
            // 
            this.pickElementButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pickElementButton.Location = new System.Drawing.Point(371, 5);
            this.pickElementButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pickElementButton.Name = "pickElementButton";
            this.pickElementButton.Size = new System.Drawing.Size(75, 23);
            this.pickElementButton.TabIndex = 2;
            this.pickElementButton.Text = "Pick Element";
            this.pickElementButton.UseVisualStyleBackColor = true;
            this.pickElementButton.Click += new System.EventHandler(this.PickElementButton_Click);
            // 
            // testButton
            // 
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Location = new System.Drawing.Point(452, 5);
            this.testButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 3;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // alternativesLabel
            // 
            this.alternativesLabel.AutoSize = true;
            this.alternativesLabel.Location = new System.Drawing.Point(3, 41);
            this.alternativesLabel.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.alternativesLabel.Name = "alternativesLabel";
            this.alternativesLabel.Size = new System.Drawing.Size(68, 13);
            this.alternativesLabel.TabIndex = 4;
            this.alternativesLabel.Text = "Alternatives:";
            // 
            // alternativesListBox
            // 
            this.mainPanel.SetColumnSpan(this.alternativesListBox, 3);
            this.alternativesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alternativesListBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alternativesListBox.FormattingEnabled = true;
            this.alternativesListBox.IntegralHeight = false;
            this.alternativesListBox.ItemHeight = 14;
            this.alternativesListBox.Location = new System.Drawing.Point(91, 37);
            this.alternativesListBox.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.alternativesListBox.Name = "alternativesListBox";
            this.alternativesListBox.Size = new System.Drawing.Size(436, 150);
            this.alternativesListBox.TabIndex = 5;
            this.alternativesListBox.SelectedIndexChanged += new System.EventHandler(this.AlternativesListBox_SelectedIndexChanged);
            // 
            // resultPanel
            // 
            this.mainPanel.SetColumnSpan(this.resultPanel, 4);
            this.resultPanel.Controls.Add(this.matchCountLabel);
            this.resultPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultPanel.Location = new System.Drawing.Point(3, 193);
            this.resultPanel.Name = "resultPanel";
            this.resultPanel.Size = new System.Drawing.Size(524, 30);
            this.resultPanel.TabIndex = 6;
            // 
            // matchCountLabel
            // 
            this.matchCountLabel.AutoSize = true;
            this.matchCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.matchCountLabel.Location = new System.Drawing.Point(6, 7);
            this.matchCountLabel.Name = "matchCountLabel";
            this.matchCountLabel.Size = new System.Drawing.Size(167, 15);
            this.matchCountLabel.TabIndex = 0;
            this.matchCountLabel.Text = "No elements match this selector";
            // 
            // buttonsPanel
            // 
            this.mainPanel.SetColumnSpan(this.buttonsPanel, 4);
            this.buttonsPanel.Controls.Add(this.cancelButton);
            this.buttonsPanel.Controls.Add(this.okButton);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(3, 229);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(524, 40);
            this.buttonsPanel.TabIndex = 7;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(446, 5);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 28);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(365, 5);
            this.okButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 28);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.ColumnCount = 4;
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.mainPanel.Controls.Add(this.selectorLabel, 0, 0);
            this.mainPanel.Controls.Add(this.selectorTextBox, 1, 0);
            this.mainPanel.Controls.Add(this.pickElementButton, 2, 0);
            this.mainPanel.Controls.Add(this.testButton, 3, 0);
            this.mainPanel.Controls.Add(this.alternativesLabel, 0, 1);
            this.mainPanel.Controls.Add(this.alternativesListBox, 1, 1);
            this.mainPanel.Controls.Add(this.resultPanel, 0, 2);
            this.mainPanel.Controls.Add(this.buttonsPanel, 0, 3);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(12, 12);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.RowCount = 4;
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.mainPanel.Size = new System.Drawing.Size(530, 272);
            this.mainPanel.TabIndex = 8;
            // 
            // SelectorPickerDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(554, 296);
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectorPickerDialog";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CSS Selector Picker";
            this.resultPanel.ResumeLayout(false);
            this.resultPanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private void SetupButtonHandlers()
        {
            // Assuming you have an OK button named btnOK and a Cancel button named btnCancel
            okButton.Click += (sender, e) =>
            {
                DialogResult = DialogResult.OK;
                Close(); // Explicitly close the form
            };

            cancelButton.Click += (sender, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close(); // Explicitly close the form
            };
        }
        private async void PickElementButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Start element picking
                await elementPickerService.StartPickingAsync();
                
                // Disable pick button while picking
                pickElementButton.Enabled = false;
                matchCountLabel.Text = "Picking element... Click on an element in the page.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting element picker: {ex.Message}", 
                    "Element Picker Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            await TestCurrentSelector();
        }

        private async void SelectorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI) return;
            
            // Clear the match count when typing
            matchCountLabel.Text = "Type a selector and click Test to check it";
        }

        private void AlternativesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (alternativesListBox.SelectedIndex >= 0 && !isUpdatingUI)
            {
                isUpdatingUI = true;
                selectorTextBox.Text = alternativesListBox.SelectedItem.ToString();
                isUpdatingUI = false;
                
                // Test the new selector
                TestCurrentSelector().ConfigureAwait(false);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SelectedSelector = selectorTextBox.Text;
        }

        private async void ElementPickerService_ElementSelected(object sender, ElementSelectedEventArgs e)
        {
            // Re-enable pick button
            pickElementButton.Enabled = true;
            
            if (e.SelectorInfo == null) return;
            
            // Update the UI with the selected element's information
            isUpdatingUI = true;
            
            try
            {
                // Clear previous alternatives
                alternativeSelectors.Clear();
                alternativesListBox.Items.Clear();
                
                // Add the optimal selector to the text box
                selectorTextBox.Text = e.SelectorInfo.Optimal;
                
                // Add all alternatives to the list box
                if (e.SelectorInfo.Alternatives != null)
                {
                    foreach (string selector in e.SelectorInfo.Alternatives)
                    {
                        if (!string.IsNullOrEmpty(selector) && !alternativeSelectors.Contains(selector))
                        {
                            alternativeSelectors.Add(selector);
                            alternativesListBox.Items.Add(selector);
                        }
                    }
                }
                
                // Test the selected selector
                await TestCurrentSelector();
            }
            finally
            {
                isUpdatingUI = false;
            }
        }

        private void ElementPickerService_PickingCanceled(object sender, EventArgs e)
        {
            // Re-enable pick button
            pickElementButton.Enabled = true;
            matchCountLabel.Text = "Element picking canceled";
        }

        private async Task TestCurrentSelector()
        {
            string selector = selectorTextBox.Text;
            
            if (string.IsNullOrWhiteSpace(selector))
            {
                matchCountLabel.Text = "Please enter a selector to test";
                return;
            }
            
            try
            {
                // First test if the selector is valid and count matching elements
                var testResult = await elementPickerService.TestSelectorAsync(selector);
                
                if (!testResult.IsValid)
                {
                    matchCountLabel.Text = testResult.Message;
                    matchCountLabel.ForeColor = Color.Red;
                    return;
                }
                
                // Update match count
                if (testResult.Count == 0)
                {
                    matchCountLabel.Text = "No elements match this selector";
                    matchCountLabel.ForeColor = Color.Red;
                }
                else if (testResult.Count == 1)
                {
                    matchCountLabel.Text = "1 element matches this selector (Ideal!)";
                    matchCountLabel.ForeColor = Color.Green;
                }
                else
                {
                    matchCountLabel.Text = $"{testResult.Count} elements match this selector";
                    matchCountLabel.ForeColor = testResult.Count <= 3 ? Color.DarkOrange : Color.Red;
                }
                
                // Highlight the matching elements
                await elementPickerService.HighlightMatchingElementsAsync(selector);
            }
            catch (Exception ex)
            {
                matchCountLabel.Text = $"Error testing selector: {ex.Message}";
                matchCountLabel.ForeColor = Color.Red;
            }
        }

        private Label selectorLabel;
        private TextBox selectorTextBox;
        private Button pickElementButton;
        private Button testButton;
        private Label alternativesLabel;
        private ListBox alternativesListBox;
        private Panel resultPanel;
        private Label matchCountLabel;
        private FlowLayoutPanel buttonsPanel;
        private Button cancelButton;
        private Button okButton;
        private TableLayoutPanel mainPanel;
    }
}