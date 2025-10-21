using System;
using System.IO;
using System.Web;

namespace MvcRazor.Models
{
    public static class LocalizationConfig
    {
        public static string LogoUrl => "img/logo-bo.svg";

        public static string SubFolder => "Rus";

        public static string WebReportLocalization => Path.Combine(HttpRuntime.BinDirectory, "Russian.frl");

        public static string ReportDatabase => "NorthWind";

        public static string Description =>
            "Многофункциональное интерактивное демо приложение для генератора отчетов FastReport .NET построенное на технологии FastReport Blazor.";

        public static string TitleName => "Демо";

        public static string LinkToHome => "https://быстрыеотчеты.рф/products/fast-report-net";

        public static string LabelToHome => "Вернуться к FastReport .NET";

        public static string DefaultReport => "Список Сотрудников.frx";
    }
}
