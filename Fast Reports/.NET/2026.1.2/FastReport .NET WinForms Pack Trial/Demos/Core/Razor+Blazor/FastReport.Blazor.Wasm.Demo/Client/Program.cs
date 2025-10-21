using FastReport;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using System.Globalization;
using System.Reflection;
using WASMUserApp.Data;

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
            await builder.Build().RunAsync();
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
            var enUsCulture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo.CurrentCulture = enUsCulture;
            CultureInfo.DefaultThreadCurrentCulture = enUsCulture;
            CultureInfo.DefaultThreadCurrentUICulture = enUsCulture;
        }
    }
}