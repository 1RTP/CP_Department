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
using SerializerLib;

namespace КП_Кафедра.Forms
{
    public partial class FormSettings : Form
    {
        private string currentFormat = "XML"; // формат за замовчуванням
        private bool isInitializing = true;
        //private string dataFolder = Path.Combine(Application.StartupPath, "Data");
        private readonly string dataFolder;

        public FormSettings()
        {
            InitializeComponent();

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(exeDir, "..", ".."));
            dataFolder = Path.Combine(projectRoot, "Data");

            LanguageManager.LanguageChanged += ApplyLocalization;
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            try
            {
                EnsureDataFolderExists();

                isInitializing = true;
                rbXML.Checked = true;
                isInitializing = false;

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

        private string GetFilePath(string entityName, string format)
        {
            Directory.CreateDirectory(dataFolder); // створюємо папку
            return Path.Combine(dataFolder, $"{entityName}.{format.ToLower()}");
        }

        private void SaveAllData(DepartmentData data, string format)
        {
            string path = GetFilePath("department", format);
            switch (format.ToUpper())
            {
                case "XML": ClassSerializeXML1.SerializeToXml(data, path, msg => LoggerService.LogInfo(msg)); break;
                case "JSON": ClassSerializeJSON1.SerializeToJSON(data, path, msg => LoggerService.LogInfo(msg)); break;
                case "BIN": ClassSerializeBIN1.SerializeToBIN(data, path, msg => LoggerService.LogInfo(msg)); break;
            }
        }

        private DepartmentData LoadAllData(string format)
        {
            string path = GetFilePath("department", format);
            if (!File.Exists(path)) return new DepartmentData();

            switch (format.ToUpper())
            {
                case "XML": return ClassSerializeXML1.DeserializeFromXml<DepartmentData>(path, msg => LoggerService.LogInfo(msg));
                case "JSON": return ClassSerializeJSON1.DeserializeFromJSON<DepartmentData>(path, msg => LoggerService.LogInfo(msg));
                case "BIN": return ClassSerializeBIN1.DeserializeFromBIN<DepartmentData>(path, msg => LoggerService.LogInfo(msg));
                default: return new DepartmentData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DepartmentData data = new DepartmentData
                {
                    Teachers = DataService.Teachers,
                    Subjects = DataService.Subjects,
                    Assignments = DataService.Assignments,
                    Researches = DataService.Researches,
                    Participations = DataService.Participations
                };
                SaveAllData(data, currentFormat);
                Toast.Show("SUCCESS", $"Дані збережено у форматі {currentFormat}.");
                LoggerService.LogInfo($"Дані успішно збережено у форматі {currentFormat}");
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при збереженні у формат {currentFormat}: {ex.Message}");
                Toast.Show("ERROR", $"Помилка при збереженні у формат {currentFormat}.");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                DepartmentData data = LoadAllData(currentFormat);

                DataService.Teachers = data.Teachers ?? new List<Teacher>();
                DataService.Subjects = data.Subjects ?? new List<Subject>();
                DataService.Assignments = data.Assignments ?? new List<Assignment>();
                DataService.Researches = data.Researches ?? new List<Research>();
                DataService.Participations = data.Participations ?? new List<Participation>();

                if (FormTables.Instance != null && FormTables.Instance.activeForm is FormTeacher teacherForm)
                {
                    teacherForm.UpdateGrid(DataService.Teachers);
                }
                Toast.Show("INFO", $"Дані завантажено з файлу у форматі{currentFormat}.");
                LoggerService.LogInfo($"Десеріалізовано всі дані з {currentFormat}.");
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при завантаженні у формат {currentFormat}: {ex.Message}");
                Toast.Show("ERROR", $"Помилка при завантаженні у формат {currentFormat}.");
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
            label1.Text = LanguageManager.GetString("label1");
        }

        private void rbXML_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing || !rbXML.Checked) return;

            currentFormat = "XML";
            LoggerService.LogInfo("Обрано формат XML.");
            Toast.Show("INFO", $"Обрано формат XML.");
        }

        private void rbJSON_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing || !rbJSON.Checked) return;

            currentFormat = "JSON";
            LoggerService.LogInfo("Обрано формат JSON.");
            Toast.Show("INFO", $"Обрано формат JSON.");
        }

        private void rbBIN_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing || !rbBIN.Checked) return;

            currentFormat = "BIN";
            LoggerService.LogInfo("Обрано формат BIN.");
            Toast.Show("INFO", $"Обрано формат BIN.");
        }

    }
    
}
