using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsuranceAutomation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set up global exception handling
            Application.ThreadException += (sender, e) =>
                HandleUnhandledException(e.Exception, "Thread Exception");

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                HandleUnhandledException(e.ExceptionObject as Exception, "Unhandled Exception");

            try
            {
                Application.Run(new UI.MainForm());
            }
            catch (Exception ex)
            {
                HandleUnhandledException(ex, "Application Exception");
            }
        }

        private static void HandleUnhandledException(Exception ex, string source)
        {
            // Log to a file if possible
            try
            {
                string logPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "crash_log.txt");

                System.IO.File.AppendAllText(logPath,
                    $"{DateTime.Now}: {source} - {ex?.Message}\n{ex?.StackTrace}\n\n");
            }
            catch
            {
                // If logging fails, just show the message box
            }

            MessageBox.Show($"An {source} occurred:\n\n{ex?.Message}\n\nThe error has been logged. " +
                $"Please restart the application.", "Application Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}