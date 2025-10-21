using System.Globalization;

namespace AspNet.Demo.Models;

internal static class LocalizationConfig
{
    public static string LogoUrl => "img/logo-bo.svg" ;

    public static string SubFolder => "Rus";

    public static string WebReportLocalization => Path.Combine(AppContext.BaseDirectory, "Russian.frl");

    public static string DesignerLocale => "ru";

    public static string ReportDatabase => "NorthWind";

    public static string TitleName => "Демо";

    public static string LinkToHome => "https://быстрыеотчеты.рф/products/fast-report-net";

    public static string LabelToHome => "Вернуться к FastReport .NET";

    public static string DefaultReport => "Список Сотрудников.frx";


    public static void SetCulture(IConfiguration configuration)
    {
        CultureInfo culture = new CultureInfo("ru-RU");
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        SetupLocalization();
    }

    private static void SetupLocalization()
    {
        FastReport.Utils.Res.LoadLocale(WebReportLocalization);
    }
}
