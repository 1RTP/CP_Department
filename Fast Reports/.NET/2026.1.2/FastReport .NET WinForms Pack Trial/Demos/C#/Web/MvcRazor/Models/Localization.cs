using System;
using System.Configuration;
using System.IO;

namespace MvcRazor.Models
{
    public static class LocalizationConfig
    {
        public static string LogoUrl => "img/logo-fr.svg";

        public static string SubFolder => "Eng";

        public static string WebReportLocalization => string.Empty;

        public static string ReportDatabase => "NorthWind";

        public static string TitleName => "Demo";

        public static string LinkToHome => "https://www.fast-report.com/product/fast-report-net/";

        public static string LabelToHome => "Back to FastReport .NET";

        public static string DefaultReport => "Simple List.frx";
    }
}
