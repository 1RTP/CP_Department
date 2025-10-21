using FastReport;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using System.Globalization;
using System.Reflection;

using WASMUserApp.Data;
using WASMUserApp.Models;

namespace WASMUserApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            AddFonts();

            SetupCulture();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddFastReport();

            builder.Services.AddSingleton<IReportService, ReportService>();
            builder.Services.AddSingleton<IReportFileProvider, ReportFileProvider>();
            var app = builder.Build();

            SetupLocalization(app);

            await app.RunAsync();
        }

        private static void AddFonts()
        {
            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (var resourceName in resources)
            {
                if (resourceName.StartsWith($"{nameof(WASMUserApp)}.Fonts"))
                    AddFont(resourceName);
            }
        }

        private static void AddFont(string assemblyResourcePath)
        {
            using var fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(assemblyResourcePath);

            FontManager.AddFont(fontStream);
#if DEBUG
            Console.WriteLine(assemblyResourcePath + " added");
#endif
        }

        private static void SetupCulture()
        {
            // Statically set the culture:
            var ruCulture = CultureInfo.CreateSpecificCulture(LocalizationConfig.DefaultCulture);
            CultureInfo.CurrentCulture = ruCulture;
            CultureInfo.DefaultThreadCurrentCulture = ruCulture;
            CultureInfo.DefaultThreadCurrentUICulture = ruCulture;
        }

        private static void SetupLocalization(WebAssemblyHost app)
        {
            var reportService = app.Services.GetRequiredService<IReportService>();
            var localeStream = reportService.GetLocale();
            FastReport.Utils.Res.LoadLocale(localeStream);
        }
    }
}