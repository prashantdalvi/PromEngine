using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromotionEngine.Service;
using System;
using System.Windows.Forms;

namespace PromotionEngine
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = Host.CreateDefaultBuilder()
             .ConfigureAppConfiguration((context, builder) =>
             {
                 // Add other configuration files...
                 builder.AddJsonFile("appsettings.json", optional: true);
             })
             .ConfigureServices((context, services) =>
             {
                 ConfigureServices(context.Configuration, services);
             })
             .ConfigureLogging(logging =>
             {
                 // Add other loggers...
             })
             .Build();

            var services = host.Services;
            var mainForm = services.GetRequiredService<PromotionEngine>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IConfiguration configuration, Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.Configure<AppSettings>
            (configuration.GetSection(nameof(AppSettings)));
            services.AddSingleton<PromotionEngine>();
            services.AddScoped<IblActivePromotion, blActivePromotion>();
            services.AddScoped<IblSkuIdPrice, blSkuIdPrice>();
        }
    }
}
