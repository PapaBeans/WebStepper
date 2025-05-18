using FastColoredTextBoxNS;

namespace WebStepper.UI.Controls
{
    partial class TemplateEditorControl
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
            this.components = new System.ComponentModel.Container();
            this.txtEditor = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtEditor)).BeginInit();
            this.SuspendLayout();
            //
            // txtEditor
            //
            this.txtEditor.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtEditor.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*" +
    "(?<range>:)\\s*(?<range>[^;]+);";
            this.txtEditor.AutoScrollMinSize = new System.Drawing.Size(31, 18);
            this.txtEditor.BackBrush = null;
            this.txtEditor.CharHeight = 18;
            this.txtEditor.CharWidth = 10;
            this.txtEditor.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEditor.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEditor.IsReplaceMode = false;
            this.txtEditor.Location = new System.Drawing.Point(0, 0);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.Paddings = new System.Windows.Forms.Padding(0);
            this.txtEditor.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtEditor.ServiceColors = new ServiceColors();
            this.txtEditor.Size = new System.Drawing.Size(400, 300);
            this.txtEditor.TabIndex = 0;
            this.txtEditor.Zoom = 100;
            //
            // TemplateEditorControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtEditor);
            this.Name = "TemplateEditorControl";
            this.Size = new System.Drawing.Size(400, 300);
            ((System.ComponentModel.ISupportInitialize)(this.txtEditor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox txtEditor;
    }
}
