using System;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;
using Newtonsoft.Json;

namespace WebStepper.UI.Controls
{
    public partial class TemplateEditorControl : UserControl
    {
        private Template _currentTemplate;
        private bool _isUpdating;
        private System.Timers.Timer _updateTimer;

        // Events
        public event EventHandler<TemplateEventArgs> TemplateChanged;

        public TemplateEditorControl()
        {
            InitializeComponent();

            // Set up FastColoredTextBox properties
            txtEditor.Language = Language.JS;
            txtEditor.BorderStyle = BorderStyle.Fixed3D;
            txtEditor.IndentBackColor = System.Drawing.Color.FromArgb(246, 246, 246);
            txtEditor.LineNumberColor = System.Drawing.Color.Gray;
            txtEditor.ServiceLinesColor = System.Drawing.Color.Silver;
            txtEditor.FoldingIndicatorColor = System.Drawing.Color.Green;
            txtEditor.SelectionColor = System.Drawing.Color.FromArgb(176, 228, 255);
            txtEditor.ShowFoldingLines = true;
            txtEditor.LeftBracket = '[';
            txtEditor.RightBracket = ']';
            txtEditor.LeftBracket2 = '{';
            txtEditor.RightBracket2 = '}';
            txtEditor.AutoCompleteBrackets = true;
            txtEditor.AutoIndent = true;
            txtEditor.AutoIndentExistingLines = true;

            // Set up text changed timer for debouncing
            _updateTimer = new System.Timers.Timer(500); // 500ms debounce
            _updateTimer.AutoReset = false;
            _updateTimer.Elapsed += OnUpdateTimerElapsed;

            // Set up events
            txtEditor.TextChanged += OnTextChanged;

            // Set up context menu
            var menu = new ContextMenuStrip();
            menu.Items.Add("Format JSON", null, OnFormatJson);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Cut", null, (s, e) => txtEditor.Cut());
            menu.Items.Add("Copy", null, (s, e) => txtEditor.Copy());
            menu.Items.Add("Paste", null, (s, e) => txtEditor.Paste());
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add("Select All", null, (s, e) => txtEditor.SelectAll());
            txtEditor.ContextMenuStrip = menu;
        }

        public void SetTemplate(Template template)
        {
            _currentTemplate = template;
            RefreshEditor();
        }

        public void InsertSelector(string selector)
        {
            if (string.IsNullOrEmpty(selector) || txtEditor.ReadOnly)
            {
                return;
            }

            txtEditor.InsertText(selector);
        }

        private void RefreshEditor()
        {
            if (_currentTemplate == null)
            {
                txtEditor.Text = string.Empty;
                return;
            }

            _isUpdating = true;
            txtEditor.Text = JsonConvert.SerializeObject(_currentTemplate, Formatting.Indented);
            _isUpdating = false;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating)
            {
                return;
            }

            // Reset the timer
            _updateTimer.Stop();
            _updateTimer.Start();
        }

        private void OnUpdateTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                try
                {
                    var template = JsonConvert.DeserializeObject<Template>(txtEditor.Text);

                    if (template != null)
                    {
                        _currentTemplate = template;
                        TemplateChanged?.Invoke(this, new TemplateEventArgs { Template = template });
                    }
                }
                catch (Exception)
                {
                    // JSON is invalid - don't update the template
                }
            }));
        }

        private void OnFormatJson(object sender, EventArgs e)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(txtEditor.Text);
                string formattedJson = JsonConvert.SerializeObject(obj, Formatting.Indented);

                _isUpdating = true;
                txtEditor.Text = formattedJson;
                _isUpdating = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error formatting JSON: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
