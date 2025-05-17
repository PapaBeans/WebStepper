using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using InsuranceAutomation.Models;

namespace InsuranceAutomation.UI.Controls
{
    /// <summary>
    /// Custom control for editing different types of step properties.
    /// </summary>
    public partial class PropertyTypeEditorControl : UserControl
    {
        private AutomationStep currentStep;
        private string propertyName;
        private TextBox textEditor;
        private ComboBox typeEditor;
        private Label propertyLabel;
        private Panel mainPanel;
        private Button validationButton;
        private Label validationMessageLabel;

        /// <summary>
        /// Event raised when a property value is changed.
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> PropertyValueChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTypeEditorControl"/> class.
        /// </summary>
        public PropertyTypeEditorControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the automation step and property to edit.
        /// </summary>
        /// <param name="step">The automation step.</param>
        /// <param name="propertyName">The name of the property to edit.</param>
        public void SetProperty(AutomationStep step, string propertyName)
        {
            this.currentStep = step;
            this.propertyName = propertyName;
            
            // Configure the editor based on the property type
            ConfigureEditor();
            
            // Load the current value
            LoadValue();
        }

        private void InitializeComponent()
        {
            this.mainPanel = new System.Windows.Forms.Panel();
            this.propertyLabel = new System.Windows.Forms.Label();
            this.textEditor = new System.Windows.Forms.TextBox();
            this.typeEditor = new System.Windows.Forms.ComboBox();
            this.validationButton = new System.Windows.Forms.Button();
            this.validationMessageLabel = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.validationMessageLabel);
            this.mainPanel.Controls.Add(this.validationButton);
            this.mainPanel.Controls.Add(this.typeEditor);
            this.mainPanel.Controls.Add(this.textEditor);
            this.mainPanel.Controls.Add(this.propertyLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(300, 80);
            this.mainPanel.TabIndex = 0;
            // 
            // propertyLabel
            // 
            this.propertyLabel.AutoSize = true;
            this.propertyLabel.Location = new System.Drawing.Point(3, 7);
            this.propertyLabel.Name = "propertyLabel";
            this.propertyLabel.Size = new System.Drawing.Size(86, 13);
            this.propertyLabel.TabIndex = 0;
            this.propertyLabel.Text = "Property Name:";
            // 
            // textEditor
            // 
            this.textEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditor.Location = new System.Drawing.Point(95, 4);
            this.textEditor.Name = "textEditor";
            this.textEditor.Size = new System.Drawing.Size(170, 20);
            this.textEditor.TabIndex = 1;
            this.textEditor.TextChanged += new System.EventHandler(this.TextEditor_TextChanged);
            // 
            // typeEditor
            // 
            this.typeEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.typeEditor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeEditor.FormattingEnabled = true;
            this.typeEditor.Location = new System.Drawing.Point(95, 4);
            this.typeEditor.Name = "typeEditor";
            this.typeEditor.Size = new System.Drawing.Size(170, 21);
            this.typeEditor.TabIndex = 2;
            this.typeEditor.SelectedIndexChanged += new System.EventHandler(this.TypeEditor_SelectedIndexChanged);
            // 
            // validationButton
            // 
            this.validationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.validationButton.Location = new System.Drawing.Point(271, 4);
            this.validationButton.Name = "validationButton";
            this.validationButton.Size = new System.Drawing.Size(25, 21);
            this.validationButton.TabIndex = 3;
            this.validationButton.Text = "...";
            this.validationButton.UseVisualStyleBackColor = true;
            this.validationButton.Click += new System.EventHandler(this.ValidationButton_Click);
            // 
            // validationMessageLabel
            // 
            this.validationMessageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.validationMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.validationMessageLabel.Location = new System.Drawing.Point(95, 27);
            this.validationMessageLabel.Name = "validationMessageLabel";
            this.validationMessageLabel.Size = new System.Drawing.Size(202, 35);
            this.validationMessageLabel.TabIndex = 4;
            this.validationMessageLabel.Text = "Validation Message";
            this.validationMessageLabel.Visible = false;
            // 
            // PropertyTypeEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainPanel);
            this.Name = "PropertyTypeEditorControl";
            this.Size = new System.Drawing.Size(300, 80);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        private void ConfigureEditor()
        {
            if (currentStep == null || string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            // Set the property label text
            propertyLabel.Text = GetFriendlyPropertyName(propertyName) + ":";

            // Configure the appropriate editor based on the property
            switch (propertyName.ToLower())
            {
                case "type":
                    ConfigureTypeEditor();
                    break;
                case "script":
                    ConfigureScriptEditor();
                    break;
                case "selector":
                    ConfigureSelectorEditor();
                    break;
                default:
                    ConfigureDefaultEditor();
                    break;
            }
        }

        private string GetFriendlyPropertyName(string property)
        {
            // Convert camelCase or snake_case to Title Case with spaces
            string result = property;
            result = result.Replace("_", " ");
            
            // Add spaces before capitals
            for (int i = result.Length - 2; i >= 0; i--)
            {
                if (char.IsLower(result[i]) && char.IsUpper(result[i + 1]))
                {
                    result = result.Insert(i + 1, " ");
                }
            }
            
            // Capitalize first letter of each word
            System.Globalization.TextInfo textInfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(result.ToLower());
        }

        private void ConfigureTypeEditor()
        {
            // Show the type combo box, hide other editors
            typeEditor.Visible = true;
            textEditor.Visible = false;
            validationButton.Visible = false;
            
            // Clear and populate the type combo box
            typeEditor.Items.Clear();
            typeEditor.Items.AddRange(new object[] {
                "wait_for_element",
                "click_button",
                "fill_form",
                "execute_script"
            });
        }

        private void ConfigureScriptEditor()
        {
            // Show the text editor with validation button
            textEditor.Visible = true;
            typeEditor.Visible = false;
            validationButton.Visible = true;
            validationButton.Text = "...";
            
            // Configure for multi-line text
            textEditor.Multiline = true;
            textEditor.Height = 80;
            textEditor.ScrollBars = ScrollBars.Vertical;
            
            // Update control height
            this.Height = 120;
        }

        private void ConfigureSelectorEditor()
        {
            // Show the text editor with validation button
            textEditor.Visible = true;
            typeEditor.Visible = false;
            validationButton.Visible = true;
            validationButton.Text = "?";
            
            // Configure for single-line text
            textEditor.Multiline = false;
            textEditor.Height = 20;
            textEditor.ScrollBars = ScrollBars.None;
            
            // Reset control height
            this.Height = 80;
        }

        private void ConfigureDefaultEditor()
        {
            // Show the text editor, hide other editors
            textEditor.Visible = true;
            typeEditor.Visible = false;
            validationButton.Visible = false;
            
            // Configure for single-line text
            textEditor.Multiline = false;
            textEditor.Height = 20;
            textEditor.ScrollBars = ScrollBars.None;
            
            // Reset control height
            this.Height = 80;
        }

        private void LoadValue()
        {
            if (currentStep == null || string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            switch (propertyName.ToLower())
            {
                case "type":
                    typeEditor.SelectedItem = currentStep.Type;
                    break;
                case "selector":
                    textEditor.Text = currentStep.Selector ?? string.Empty;
                    break;
                case "value":
                    textEditor.Text = currentStep.Value ?? string.Empty;
                    break;
                case "script":
                    textEditor.Text = currentStep.Script ?? string.Empty;
                    break;
                case "description":
                    textEditor.Text = currentStep.Description ?? string.Empty;
                    break;
                default:
                    textEditor.Text = string.Empty;
                    break;
            }
        }

        private void SaveValue()
        {
            if (currentStep == null || string.IsNullOrEmpty(propertyName))
            {
                return;
            }

            string oldValue = GetCurrentPropertyValue();
            string newValue = GetEditorValue();

            if (oldValue == newValue)
            {
                return; // No change
            }

            // Update the step property
            switch (propertyName.ToLower())
            {
                case "type":
                    currentStep.Type = newValue;
                    break;
                case "selector":
                    currentStep.Selector = newValue;
                    break;
                case "value":
                    currentStep.Value = newValue;
                    break;
                case "script":
                    currentStep.Script = newValue;
                    break;
                case "description":
                    currentStep.Description = newValue;
                    break;
            }

            // Notify listeners that the property has changed
            OnPropertyValueChanged(newValue);
        }

        private string GetCurrentPropertyValue()
        {
            if (currentStep == null || string.IsNullOrEmpty(propertyName))
            {
                return string.Empty;
            }

            switch (propertyName.ToLower())
            {
                case "type":
                    return currentStep.Type ?? string.Empty;
                case "selector":
                    return currentStep.Selector ?? string.Empty;
                case "value":
                    return currentStep.Value ?? string.Empty;
                case "script":
                    return currentStep.Script ?? string.Empty;
                case "description":
                    return currentStep.Description ?? string.Empty;
                default:
                    return string.Empty;
            }
        }

        private string GetEditorValue()
        {
            if (typeEditor.Visible)
            {
                return typeEditor.SelectedItem?.ToString() ?? string.Empty;
            }
            else if (textEditor.Visible)
            {
                return textEditor.Text;
            }
            
            return string.Empty;
        }

        private void TextEditor_TextChanged(object sender, EventArgs e)
        {
            SaveValue();
            ValidateProperty();
        }

        private void TypeEditor_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveValue();
        }

        private void ValidationButton_Click(object sender, EventArgs e)
        {
            if (propertyName.ToLower() == "script")
            {
                ShowScriptEditor();
            }
            else if (propertyName.ToLower() == "selector")
            {
                ShowSelectorHelp();
            }
        }

        private void ShowScriptEditor()
        {
            // Create a script editor dialog (this could be extended to a more sophisticated editor)
            using (var form = new Form())
            {
                form.Text = "Script Editor";
                form.Size = new Size(600, 400);
                form.StartPosition = FormStartPosition.CenterParent;
                
                var textBox = new TextBox
                {
                    Multiline = true,
                    Dock = DockStyle.Fill,
                    ScrollBars = ScrollBars.Both,
                    AcceptsTab = true,
                    AcceptsReturn = true,
                    Text = textEditor.Text,
                    Font = new Font("Consolas", 10)
                };
                
                var panel = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    RowCount = 2,
                    ColumnCount = 1
                };
                
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                
                var buttonPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft
                };
                
                var okButton = new Button
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Size = new Size(80, 30),
                    Margin = new Padding(5)
                };
                
                var cancelButton = new Button
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Size = new Size(80, 30),
                    Margin = new Padding(5)
                };
                
                buttonPanel.Controls.Add(cancelButton);
                buttonPanel.Controls.Add(okButton);
                
                panel.Controls.Add(textBox, 0, 0);
                panel.Controls.Add(buttonPanel, 0, 1);
                
                form.Controls.Add(panel);
                form.AcceptButton = okButton;
                form.CancelButton = cancelButton;
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    textEditor.Text = textBox.Text;
                }
            }
        }

        private void ShowSelectorHelp()
        {
            MessageBox.Show(
                "CSS Selector Help:\n\n" +
                "- ID selector: #elementId\n" +
                "- Class selector: .className\n" +
                "- Element selector: div\n" +
                "- Descendant selector: div p\n" +
                "- Child selector: div > p\n" +
                "- Attribute selector: [name='value']\n" +
                "- Pseudo-class selector: a:hover\n\n" +
                "Examples:\n" +
                "#loginButton - Selects element with ID 'loginButton'\n" +
                "input[name='username'] - Selects input with name 'username'\n" +
                ".form-control - Selects elements with class 'form-control'",
                "CSS Selector Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ValidateProperty()
        {
            bool isValid = true;
            string message = string.Empty;

            if (string.IsNullOrEmpty(GetEditorValue()))
            {
                isValid = false;
                message = "This field cannot be empty";
            }
            else if (propertyName.ToLower() == "selector")
            {
                // Simple selector validation
                string selector = GetEditorValue();
                if (selector.Contains("..") || selector.Contains(",,") || 
                    selector.Contains("[[") || selector.Contains("))"))
                {
                    isValid = false;
                    message = "Invalid selector format";
                }
            }

            validationMessageLabel.Visible = !isValid;
            validationMessageLabel.Text = message;
        }

        protected virtual void OnPropertyValueChanged(string newValue)
        {
            PropertyValueChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}