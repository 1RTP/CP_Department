using System.Globalization;

namespace WASMUserApp.Models;

internal static class LocalizationConfig
{
    public static string LogoUrl => "img/logo.svg";

    public static string ReportDatabase => "NorthWind";

    public static string Description =>
        "Многофункциональное интерактивное демо приложение для генератора отчетов FastReport .NET построенное на технологии FastReport Blazor.";

    public static string TitleName => "Демо";

    public static string LinkToHome => "https://быстрыеотчеты.рф/products/fast-report-net";

    public static string LabelToHome => "Вернуться к FastReport .NET";

    public static string DefaultReport => "Список Сотрудников";

    public static string DefaultLocalization => "Russian.frl";

    public static string DefaultCulture => "ru-RU";
}

