using System;
using System.Drawing;
using System.Windows.Forms;
using WebStepper.Core.Interfaces;

namespace WebStepper.UI.Controls
{
    public partial class LogViewerControl : UserControl
    {
        private readonly ILogService _logService;
        private bool _autoScroll = true;

        public LogViewerControl()
        {
            InitializeComponent();

            // Setup ListView
            SetupListView();

            // Setup context menu
            SetupContextMenu();
        }

        public LogViewerControl(ILogService logService) : this()
        {
            _logService = logService;

            // Populate existing logs
            if (_logService != null)
            {
                foreach (var logEntry in _logService.GetLogEntries())
                {
                    AddLogEntry(logEntry);
                }
            }
        }

        public void AddLogEntry(LogEntry logEntry)
        {
            if (logEntry == null)
            {
                return;
            }

            // Create list view item
            var item = new ListViewItem(logEntry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            // Add the level and message
            item.SubItems.Add(logEntry.Level.ToString());
            item.SubItems.Add(logEntry.Message);

            // Set the item color based on the log level
            switch (logEntry.Level)
            {
                case LogLevel.Error:
                    item.ForeColor = Color.Red;
                    break;
                case LogLevel.Warning:
                    item.ForeColor = Color.Orange;
                    break;
                case LogLevel.Info:
                    item.ForeColor = Color.Black;
                    break;
            }

            // Add the item to the list view
            lvLogs.Items.Add(item);

            // Auto-scroll to the bottom
            if (_autoScroll)
            {
                lvLogs.EnsureVisible(lvLogs.Items.Count - 1);
            }
        }

        public void ClearLogs()
        {
            lvLogs.Items.Clear();
        }

        private void SetupListView()
        {
            // Configure list view
            lvLogs.View = View.Details;
            lvLogs.FullRowSelect = true;
            lvLogs.GridLines = true;

            // Add columns
            lvLogs.Columns.Add("Time", 150);
            lvLogs.Columns.Add("Level", 80);
            lvLogs.Columns.Add("Message", 400);

            // Auto resize the message column to fill the remaining space
            lvLogs.Columns[2].Width = -2; // -2 means fill the remaining space
        }

        private void SetupContextMenu()
        {
            var menu = new ContextMenuStrip();

            // Add clear menu item
            menu.Items.Add("Clear", null, (s, e) => ClearLogs());

            // Add auto scroll menu item
            var autoScrollItem = new ToolStripMenuItem("Auto Scroll", null, (s, e) =>
            {
                _autoScroll = !_autoScroll;
                ((ToolStripMenuItem)s).Checked = _autoScroll;
            });
            autoScrollItem.Checked = _autoScroll;
            menu.Items.Add(autoScrollItem);

            // Add copy menu item
            menu.Items.Add("Copy", null, (s, e) =>
            {
                if (lvLogs.SelectedItems.Count > 0)
                {
                    var selectedItem = lvLogs.SelectedItems[0];
                    var text = $"{selectedItem.Text} - {selectedItem.SubItems[1].Text} - {selectedItem.SubItems[2].Text}";
                    Clipboard.SetText(text);
                }
            });

            // Add copy all menu item
            menu.Items.Add("Copy All", null, (s, e) =>
            {
                var text = string.Empty;
                foreach (ListViewItem item in lvLogs.Items)
                {
                    text += $"{item.Text} - {item.SubItems[1].Text} - {item.SubItems[2].Text}\r\n";
                }
                Clipboard.SetText(text);
            });

            // Set the context menu
            lvLogs.ContextMenuStrip = menu;
        }
    }
}
