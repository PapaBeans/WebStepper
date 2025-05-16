using System;
using System.Drawing;
using System.Windows.Forms;

namespace InsuranceAutomation.UI.Dialogs
{
    /// <summary>
    /// A simple dialog for getting text input from the user.
    /// </summary>
    public partial class TextInputForm : Form
    {
        /// <summary>
        /// Gets the text entered by the user.
        /// </summary>
        public string InputText => inputTextBox.Text;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TextInputForm"/> class.
        /// </summary>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="prompt">The prompt to display.</param>
        /// <param name="defaultValue">The default value to pre-fill (optional).</param>
        public TextInputForm(string title, string prompt, string defaultValue = "")
        {
            InitializeComponent();
            
            this.Text = title;
            promptLabel.Text = prompt;
            inputTextBox.Text = defaultValue;
        }
    }
}
