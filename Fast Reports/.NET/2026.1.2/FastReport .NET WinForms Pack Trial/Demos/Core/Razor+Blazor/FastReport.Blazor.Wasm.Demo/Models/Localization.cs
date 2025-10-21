using System.Globalization;

namespace WASMUserApp.Models;

internal static class LocalizationConfig
{
    public static string LogoUrl => "img/logo.svg";

    public static string ReportDatabase => "NorthWind";

    public static string Description =>
        "A multifunctional interactive demo application for the FastReport report generator.NET built on FastReport Blazor technology.";

    public static string TitleName => "Demo";

    public static string LinkToHome => "https://www.fast-report.com/en/product/fast-report-net/";

    public static string LabelToHome => "Back to FastReport .NET";

    public static string DefaultReport => "Simple List";


    public static void SetCulture(IConfiguration configuration)
    {
        CultureInfo culture = new CultureInfo("en-US");
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}

