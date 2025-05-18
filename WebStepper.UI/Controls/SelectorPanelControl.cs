using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;
using WebStepper.UI.Forms;

namespace WebStepper.UI.Controls
{
    public partial class SelectorPanelControl : UserControl
    {
        private readonly IElementPickerService _elementPickerService;
        private List<SelectorOption> _currentOptions;

        // Events
        public event EventHandler<SelectorEventArgs> SelectorInserted;
        public event EventHandler<StepAddedEventArgs> StepAdded;

        public SelectorPanelControl()
        {
            InitializeComponent();

            // Configure the list view
            lvSelectors.View = View.Details;
            lvSelectors.FullRowSelect = true;
            lvSelectors.MultiSelect = false;
            lvSelectors.GridLines = true;

            // Add columns
            lvSelectors.Columns.Add("Type", 80);
            lvSelectors.Columns.Add("Selector", 300);
            lvSelectors.Columns.Add("Specificity", 80);
            lvSelectors.Columns.Add("Matches", 80);

            // Setup element info property grid
            propertyGrid.ToolbarVisible = false;
            propertyGrid.PropertySort = PropertySort.Alphabetical;

            // Setup events
            lvSelectors.SelectedIndexChanged += OnSelectorListSelectedIndexChanged;
            btnTest.Click += OnTestButtonClick;
            btnInsert.Click += OnInsertButtonClick;
            btnAddStep.Click += OnAddStepButtonClick;
        }

        public SelectorPanelControl(IElementPickerService elementPickerService) : this()
        {
            _elementPickerService = elementPickerService;
        }

        public void DisplaySelectorOptions(List<SelectorOption> options, Dictionary<string, object> elementInfo)
        {
            if (options == null || elementInfo == null)
            {
                return;
            }

            // Store the options
            _currentOptions = options;

            // Clear the list view
            lvSelectors.Items.Clear();

            // Add each option to the list view
            foreach (var option in options)
            {
                var item = new ListViewItem(option.Type);
                item.SubItems.Add(option.Value);
                item.SubItems.Add(option.SpecificityScore.ToString());
                item.SubItems.Add(option.MatchCount.ToString());
                item.Tag = option;

                // Highlight options that uniquely identify an element
                if (option.MatchCount == 1)
                {
                    item.BackColor = Color.LightGreen;
                }

                lvSelectors.Items.Add(item);
            }

            // Display the element info
            propertyGrid.SelectedObject = new ElementInfoWrapper(elementInfo);

            // Enable/disable buttons
            UpdateButtonState();
        }

        private void OnSelectorListSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private async void OnTestButtonClick(object sender, EventArgs e)
        {
            if (lvSelectors.SelectedItems.Count == 0 || _elementPickerService == null)
            {
                return;
            }

            var option = (SelectorOption)lvSelectors.SelectedItems[0].Tag;

            try
            {
                // Disable the button to prevent multiple clicks
                btnTest.Enabled = false;

                // Test the selector
                await _elementPickerService.TestSelector(option.Value, option.Type == "XPath" ? "xpath" : "css");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error testing selector: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Re-enable the button
                btnTest.Enabled = true;
            }
        }

        private void OnInsertButtonClick(object sender, EventArgs e)
        {
            if (lvSelectors.SelectedItems.Count == 0)
            {
                return;
            }

            var option = (SelectorOption)lvSelectors.SelectedItems[0].Tag;

            // Raise the selector inserted event
            SelectorInserted?.Invoke(this, new SelectorEventArgs
            {
                SelectorType = option.Type,
                SelectorValue = option.Value
            });
        }

        private void UpdateButtonState()
        {
            bool hasSelection = lvSelectors.SelectedItems.Count > 0;
            btnTest.Enabled = hasSelection && _elementPickerService != null;
            btnInsert.Enabled = hasSelection;
            btnAddStep.Enabled = hasSelection;
        }

        private void OnAddStepButtonClick(object sender, EventArgs e)
        {
            if (lvSelectors.SelectedItems.Count == 0)
            {
                return;
            }

            var option = (SelectorOption)lvSelectors.SelectedItems[0].Tag;

            using (var dialog = new StepEditorDialog(option.Value))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Raise the step added event
                    StepAdded?.Invoke(this, new StepAddedEventArgs
                    {
                        Step = dialog.Step
                    });
                }
            }
        }
    }

    public class SelectorEventArgs : EventArgs
    {
        public string SelectorType { get; set; }
        public string SelectorValue { get; set; }
    }

    public class StepAddedEventArgs : EventArgs
    {
        public Step Step { get; set; }
    }

    public class ElementInfoWrapper
    {
        private readonly Dictionary<string, object> _info;

        public ElementInfoWrapper(Dictionary<string, object> info)
        {
            _info = info ?? new Dictionary<string, object>();
        }

        [System.ComponentModel.Category("Basic")]
        [System.ComponentModel.DisplayName("Tag Name")]
        public string TagName => _info.ContainsKey("tagName") ? (string)_info["tagName"] : null;

        [System.ComponentModel.Category("Basic")]
        [System.ComponentModel.DisplayName("Text Content")]
        public string TextContent => _info.ContainsKey("textContent") ? (string)_info["textContent"] : null;

        [System.ComponentModel.Category("Attributes")]
        public string Id => GetAttribute("id");

        [System.ComponentModel.Category("Attributes")]
        public string Class => GetAttribute("class");

        [System.ComponentModel.Category("Attributes")]
        public string Name => GetAttribute("name");

        [System.ComponentModel.Category("Attributes")]
        public string Type => GetAttribute("type");

        [System.ComponentModel.Category("Attributes")]
        public string Value => GetAttribute("value");

        private string GetAttribute(string name)
        {
            if (_info.ContainsKey("attributes") && _info["attributes"] is Dictionary<string, string> attributes)
            {
                if (attributes.ContainsKey(name))
                {
                    return attributes[name];
                }
            }

            return null;
        }
    }
}
