using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using КП_Кафедра.Properties;

namespace КП_Кафедра
{
    internal class LanguageManager
    {
        public static event Action LanguageChanged;

        public static void ApplyLanguage(string lang)
        {
            string culture = lang == "English" ? "en" : "uk";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            Settings.Default.Language = lang;
            Settings.Default.Save();
            LanguageChanged?.Invoke(); // зміна мови у всіх формах
        }

        public static string GetString(string key)
        {
            return Resources.Strings.ResourceManager.GetString(key);
        }
    }

}
