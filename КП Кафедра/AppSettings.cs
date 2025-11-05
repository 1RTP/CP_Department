using Newtonsoft.Json;
using SerializerLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using КП_Кафедра.Properties;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра
{
    public static class AppSettings
    {
        public static Font DefaultFont { get; } = new Font("Arial", 8.8f, FontStyle.Regular);
        public static Color DefaultTextColor { get; } = Color.White;

        private static Font _currentFont = null;
        private static Color? _currentTextColor = null;
        public static event Action StyleChanged;

        public static void NotifyStyleChanged()
        {
            StyleChanged?.Invoke();
        }

        public static Font CurrentFont
        {
            get => _currentFont ?? DefaultFont;
            set => _currentFont = value ?? DefaultFont;
        }

        public static Color CurrentTextColor
        {
            get => _currentTextColor ?? DefaultTextColor;
            set => _currentTextColor = value;
        }
        public static void ApplyStyle(Control control)
        {
            if (control == null) return;
            if (control is DataGridView || control is ComboBox) return;

            if (control is Label || control is Button || control is TextBox || control is CheckBox || control is GroupBox || control is RadioButton || control is LinkLabel || control is TabPage)
            {
                control.Font = CurrentFont;
                control.ForeColor = CurrentTextColor;
            }

            foreach (Control child in control.Controls) ApplyStyle(child);
        }

        public static void ResetToDefault()
        {
            CurrentFont = DefaultFont;
            CurrentTextColor = DefaultTextColor;
            SaveSettings();
        }

        public static void SaveSettings()
        {
            try
            {
                Font f = CurrentFont ?? DefaultFont;
                Color c = CurrentTextColor;

                Properties.Settings.Default.FontName = f.Name ?? DefaultFont.Name;
                Properties.Settings.Default.FontSize = f.Size; 
                Properties.Settings.Default.TextColor = ColorTranslator.ToHtml(c);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"AppSettings.SaveSettings error: {ex.Message}");
            }
        }
        public static void LoadSettings()
        {
            try
            {
                string name = Properties.Settings.Default.FontName;
                float size = DefaultFont.Size;
                try { size = Properties.Settings.Default.FontSize; }
                catch
                {
                    try
                    {
                        var s = Properties.Settings.Default["FontSize"]?.ToString();
                        if (!string.IsNullOrEmpty(s)) size = float.Parse(s.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch { size = DefaultFont.Size; }
                }

                Color color = DefaultTextColor;
                try
                {
                    string colorHtml = Properties.Settings.Default.TextColor;
                    if (!string.IsNullOrEmpty(colorHtml))
                        color = ColorTranslator.FromHtml(colorHtml);
                }
                catch { color = DefaultTextColor; }

                if (!string.IsNullOrEmpty(name)) CurrentFont = new Font(name, size);
                else CurrentFont = DefaultFont;
                CurrentTextColor = color;
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"AppSettings.LoadSettings error: {ex.Message}");
                CurrentFont = DefaultFont;
                CurrentTextColor = DefaultTextColor;
            }
        }
    }
}
