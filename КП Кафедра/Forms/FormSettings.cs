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

namespace КП_Кафедра.Forms
{
    public partial class FormSettings : Form
    {
        private string currentFormat = "XML"; // формат за замовчуванням
        private string dataFolder = Path.Combine(Application.StartupPath, "Data");

        public FormSettings()
        {
            InitializeComponent();
            UpdateButtonStates();

            LanguageManager.LanguageChanged += ApplyLocalization;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            try
            {
                EnsureDataFolderExists();
                cmbLanguage.Items.Add("Українська");
                cmbLanguage.Items.Add("English");
                cmbLanguage.SelectedItem = Settings.Default.Language;
                ApplyLocalization();
            }
            catch (Exception ex) { LoggerService.LogError($"Помилка при завантаженні даних: {ex.Message}"); }
        }

        private void EnsureDataFolderExists()
        {
            Directory.CreateDirectory(dataFolder); // створюємо папку Data, якщо нема
            LoggerService.LogInfo("Папка Data перевірена/створена.");
        }

        private void UpdateButtonStates()
        {
            btnXml.BackColor = currentFormat == "XML" ? Color.LightGreen : SystemColors.Control;
            btnJson.BackColor = currentFormat == "JSON" ? Color.LightGreen : SystemColors.Control;
            btnBin.BackColor = currentFormat == "BIN" ? Color.LightGreen : SystemColors.Control;
        }

        private void btnXml_Click(object sender, EventArgs e)
        {
            currentFormat = "XML";
            LoggerService.LogInfo("Обрано формат XML.");
            Toast.Show("INFO", $"Обрано формат XML.");
            UpdateButtonStates();
        }

        private void btnJson_Click(object sender, EventArgs e)
        {
            currentFormat = "JSON";
            LoggerService.LogInfo("Обрано формат JSON.");
            Toast.Show("INFO", $"Обрано формат JSON.");
            UpdateButtonStates();
        }

        private void btnBin_Click(object sender, EventArgs e)
        {
            currentFormat = "BIN";
            LoggerService.LogInfo("Обрано формат BIN.");
            Toast.Show("INFO", $"Обрано формат BIN.");
            UpdateButtonStates();
        }

        private string GetFilePath(string entityName, string format)
        {
            Directory.CreateDirectory(dataFolder); // створюємо папку
            return Path.Combine(dataFolder, $"{entityName}.{format.ToLower()}");
        }

        private void SaveData<T>(List<T> data, string entityName, string format)
        {
            string path = GetFilePath(entityName, format);
            switch (format.ToUpper())
            {
                case "XML": 
                    if (entityName == "teachers") 
                    { 
                        XmlHelper.SerializeTeachers(data.Cast<Teacher>().ToList(), path); 
                    }
                    else 
                    { 
                        ClassSerializeXML.SerializeToXml(data, path); 
                    } 
                    break;
                case "JSON": ClassSerializeJSON.SerializeToJSON(data, path); break;
                case "BIN": ClassSerializeBIN.SerializeToBIN(data, path); break;
            }
        }

        private List<T> LoadData<T>(string entityName, string format)
        {
            string path = GetFilePath(entityName, format);
            if (!File.Exists(path)) return new List<T>();

            switch (format.ToUpper())
            {
                case "XML":
                    if (entityName == "teachers") return XmlHelper.DeserializeTeachers(path).Cast<T>().ToList();
                    else return ClassSerializeXML.DeserializeFromXml<List<T>>(path);
                case "JSON": return ClassSerializeJSON.DeserializeFromJSON<T>(path);
                case "BIN": return ClassSerializeBIN.DeserializeFromBIN<T>(path);
                default: return new List<T>();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (FormTable.Instance == null) return;
                var loadedTeachers = LoadData<Teacher>("teachers", currentFormat);
                FormTable.Instance.UpdateGrid(loadedTeachers);
                DataService.Teachers = new List<Teacher>(loadedTeachers);
                Toast.Show("INFO", $"Дані завантажено з файлу ({currentFormat}).");
                LoggerService.LogInfo($"Десеріалізовано {loadedTeachers.Count} викладачів з {currentFormat}");
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при завантаженні ({currentFormat}): {ex.Message}");
                Toast.Show("ERROR", $"Помилка при завантаженні ({currentFormat}).");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var teachersToSave = DataService.Teachers; // беремо дані з DataService
                if (teachersToSave == null || teachersToSave.Count == 0) return;
                SaveData(teachersToSave, "teachers", currentFormat);
                Toast.Show("SUCCESS", $"Дані збережено ({currentFormat}).");
                LoggerService.LogInfo($"Дані успішно збережено у форматі {currentFormat}");
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при збереженні ({currentFormat}): {ex.Message}");
                Toast.Show("ERROR", $"Помилка при збереженні ({currentFormat}).");
            }
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLanguage.SelectedItem == null) return;

            string selectedLang = cmbLanguage.SelectedItem.ToString();
            LanguageManager.ApplyLanguage(selectedLang);

            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            btnSave.Text = LanguageManager.GetString("btnSave");
            btnLoad.Text = LanguageManager.GetString("btnLoad");
            lbLanguage.Text = LanguageManager.GetString("lbLanguage");
        }

    }
    
}
