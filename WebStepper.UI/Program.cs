using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using WebStepper.Core.Application;
using WebStepper.Core.Interfaces;
using WebStepper.Infrastructure;
using WebStepper.UI.Forms;

namespace WebStepper.UI
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

            // Configure services
            var services = new ServiceCollection();
            ConfigureServices(services);

            // Build service provider
            var serviceProvider = services.BuildServiceProvider();

            // Start the application with the main form
            Application.Run(serviceProvider.GetRequiredService<MainForm>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Core services
            services.AddSingleton<ILogService, LoggingService>();
            services.AddSingleton<ITemplateService, TemplateService>();

            // Register services without WebView2Bridge dependency
            services.AddSingleton<IElementPickerService, ElementPickerService>();
            services.AddSingleton<IPageTrackerService, PageTrackerService>();
            services.AddSingleton<IAutomationEngine, AutomationEngine>();

            // Infrastructure services
            services.AddSingleton<JSScriptLoader>();

            // Determine templates directory
            string templatesDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "WebStepper",
                "Templates");

            services.AddSingleton<ITemplateRepository>(provider =>
                new FileTemplateRepository(templatesDirectory, provider.GetRequiredService<ILogService>()));

            // UI forms and controls (WebView2Bridge is created in the MainForm)
            services.AddSingleton<MainForm>();
        }
    }
}
