using FastReport;
using FastReport.DevComponents.DotNetBar.Controls;
using FastReport.Format;
using FastReport.Preview;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using ClosedXML.Excel;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormReport : Form
    {
        private string selectedReport = "TeachersReport.frx";
        private readonly string reportsFolderPath;
        private readonly string dbPath;
        RepExcel repExcel = new RepExcel();
        private readonly RepWord repWord = new RepWord();
        private string selectedExcelFilePath = string.Empty;
        public static FormReport Instance { get; private set; }

        public FormReport()
        {
            InitializeComponent();
            AppSettings.LoadSettings();
            AppSettings.ApplyStyle(this);
            AppSettings.StyleChanged += () => AppSettings.ApplyStyle(this);

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(exeDir, "..", ".."));
            reportsFolderPath = Path.Combine(projectRoot, "Reports");
            dbPath = Path.Combine(projectRoot, "Data", "department.db");

        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            rbTeachersReport.CheckedChanged += RadioButtons_CheckedChanged;
            rbSubjectsReport.CheckedChanged += RadioButtons_CheckedChanged;
            rbAssignmentsReport.CheckedChanged += RadioButtons_CheckedChanged;
            rbResearchReport.CheckedChanged += RadioButtons_CheckedChanged;

            rbTeachersReport.Checked = true;
            selectedReport = "TeachersReport.frx";

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            rbTeachersReport.Text = LanguageManager.GetString("rbTeachersReport");
            rbSubjectsReport.Text = LanguageManager.GetString("rbSubjectsReport");
            rbResearchReport.Text = LanguageManager.GetString("rbResearchReport");
            rbAssignmentsReport.Text = LanguageManager.GetString("rbAssignmentsReport");
            btnReport.Text = LanguageManager.GetString("btnReport");
            btnOpenExcelReport.Text = LanguageManager.GetString("btnOpenExcelReport");
            btnOpenWordReport.Text = LanguageManager.GetString("btnOpenWordReport");
            btnExportToExcel.Text = LanguageManager.GetString("btnExportToExcel");
            btnExportToWord.Text = LanguageManager.GetString("btnExportToWord");
            btnImportFromExcel.Text = LanguageManager.GetString("btnImportFromExcel");
            btnImportFromWord.Text = LanguageManager.GetString("btnImportFromWord");
            btnConvertToPdf_Click.Text = LanguageManager.GetString("btnConvertToPdf_Click");
        }

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rb) || !rb.Checked) return;

            if (rb == rbTeachersReport) selectedReport = "TeachersReport.frx";
            else if (rb == rbSubjectsReport) selectedReport = "SubjectsReport.frx";
            else if (rb == rbAssignmentsReport) selectedReport = "AssignmentsReport.frx";
            else if (rb == rbResearchReport) selectedReport = "ResearchReport.frx";
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OpenReportPreview(selectedReport);
        }

        private void OpenReportPreview(string reportFile)
        {

            panel2.Controls.Clear();

            FormReportViewer reportForm = new FormReportViewer(reportFile);
            reportForm.TopLevel = false;
            reportForm.FormBorderStyle = FormBorderStyle.None;
            reportForm.Dock = DockStyle.Fill;
            panel2.Controls.Add(reportForm);
            reportForm.Show();
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

            try
            {
                string query = "";
                string sheetName = "";

                if (rbTeachersReport.Checked)
                {
                    query = "SELECT * FROM teacher;";
                    sheetName = "Викладачі";
                }
                else if (rbSubjectsReport.Checked)
                {
                    query = "SELECT * FROM subjects;";
                    sheetName = "Дисципліни";
                }
                else if (rbAssignmentsReport.Checked)
                {
                    query = "SELECT * FROM assignment;";
                    sheetName = "Навантаження";
                }
                else if (rbResearchReport.Checked)
                {
                    query = "SELECT * FROM research;";
                    sheetName = "Наукові роботи";
                }
                else
                {
                    Toast.Show("WARNING", "Оберіть таблицю!");
                    LoggerService.LogError("Користувач не вибрав жодну таблицю для збереження.");
                    return;
                }

                DataTable dt = GetDataTable(query);
                if (dt.Rows.Count == 0)
                {
                    Toast.Show("WARNING", "Таблиця порожня.");
                    LoggerService.LogError($"Збереження скасовано — таблиця {sheetName} порожня.");
                    return;
                }

                repExcel.ExportToExcel(dt, sheetName);
                LoggerService.LogInfo($"Успішно збережено таблицю: {sheetName}");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка збереження у Excel");
                LoggerService.LogError($"Помилка збереження Excel: {ex.Message}");
            }
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel файли (*.xlsx)|*.xlsx";
            ofd.Title = "Оберіть Excel-файл";

            if (ofd.ShowDialog() != DialogResult.OK) return;
            string filePath = ofd.FileName;
            List<string> sheetNames = repExcel.GetSheetNames(filePath);

            cmbExcelSheets.DataSource = sheetNames;
            string selectedSheet = cmbExcelSheets.SelectedItem?.ToString();
            DataTable imported = repExcel.ImportFromExcel(filePath, selectedSheet);

            dataGridView1.DataSource = imported;

            //DataTable imported = repExcel.ImportFromExcel();
            //if (imported == null)
            //{
            //    LoggerService.LogError("Завантаження Excel скасовано користувачем.");
            //    return;
            //}
        }

        private void btnOpenExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                repExcel.OpenExcelFile();
                LoggerService.LogInfo("Користувач відкрив Excel-звіт.");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Не вдалося відкрити Excel.");
                LoggerService.LogError($"Помилка відкриття Excel: {ex.Message}");
            }
        }

        private DataTable GetDataTable(string query)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    using (var cmd = new SqliteCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка отримання даних.");
                LoggerService.LogError($"Помилка GetDataTable: {ex.Message} (Query: {query})");
                return new DataTable();
            }
        }

        private void btnExportToWord_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                string fileName = "";

                if (rbTeachersReport.Checked)
                {
                    query = "SELECT * FROM teacher;";
                    fileName = "Викладачі";
                }
                else if (rbSubjectsReport.Checked)
                {
                    query = "SELECT * FROM subjects;";
                    fileName = "Дисципліни";
                }
                else if (rbAssignmentsReport.Checked)
                {
                    query = "SELECT * FROM assignment;";
                    fileName = "Навантаження";
                }
                else if (rbResearchReport.Checked)
                {
                    query = "SELECT * FROM research;";
                    fileName = "Наукові роботи";
                }
                else
                {
                    Toast.Show("WARNING", "Оберіть таблицю для Word.");
                    LoggerService.LogError("Користувач не вибрав жодну таблицю для збереження у Word.");
                    return;
                }

                DataTable dt = GetDataTable(query);

                if (dt.Rows.Count == 0)
                {
                    Toast.Show("WARNING", "Таблиця порожня.");
                    LoggerService.LogError($"Збереження у Word скасовано — таблиця {fileName} порожня.");
                    return;
                }

                repWord.ExportToWord(dt, fileName);
                LoggerService.LogInfo($"Успішно збережено таблицю у Word: {fileName}");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка збереження у Word.");
                LoggerService.LogError($"Помилка збереження у Word: {ex.Message}");
            }
        }

        private void btnImportFromWord_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable imported = repWord.ImportFromWord();
                if (imported == null)
                {
                    LoggerService.LogError("Завантаження Word скасовано користувачем.");
                    return;
                }

                Toast.Show("SUCCESS", "Дані з Word завантажено.");
                LoggerService.LogInfo($"Завантажено {imported.Rows.Count} рядків із Word.");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка завантаження з Word.");
                LoggerService.LogError($"Помилка завантаження з Word: {ex.Message}");
            }
        }

        private void btnOpenWordReport_Click(object sender, EventArgs e)
        {
            try
            {
                repWord.OpenWordFile();
                LoggerService.LogInfo("Користувач відкрив Word-звіт.");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Не вдалося відкрити Word.");
                LoggerService.LogError($"Помилка відкриття Word: {ex.Message}");
            }
        }

        private void btnConvertToPdf_Click_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word файли (*.docx)|*.docx";
                ofd.Title = "Оберіть Word-файл для конвертації в PDF";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string inputPath = ofd.FileName;
                        string pdfPath = Path.ChangeExtension(inputPath, ".pdf");

                        Spire.Doc.Document doc = new Spire.Doc.Document();
                        doc.LoadFromFile(inputPath);
                        doc.SaveToFile(pdfPath, Spire.Doc.FileFormat.PDF);
                        doc.Close();

                        Toast.Show("SUCCESS", "Файл PDF створено!");
                        LoggerService.LogInfo($"Word → PDF: {pdfPath}");
                    }
                    catch (Exception ex)
                    {
                        Toast.Show("ERROR", "Не вдалося створити PDF.");
                        LoggerService.LogError($"Помилка під час створення PDF: {ex.Message}");
                    }
                }
            }
        }

        private void btnChooseExcelFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel файли (*.xlsx)|*.xlsx";
                ofd.Title = "Оберіть Excel-файл";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedExcelFilePath = ofd.FileName;

                    List<string> sheetNames = repExcel.GetSheetNames(selectedExcelFilePath);
                    if (sheetNames.Count == 0)
                    {
                        Toast.Show("WARNING", "Файл не містить листів.");
                        cmbExcelSheets.DataSource = null;
                        dataGridView1.DataSource = null;
                        return;
                    }

                    cmbExcelSheets.DataSource = sheetNames;
                    cmbExcelSheets.SelectedIndex = 0;
                }
            }
        }

        private void cmbExcelSheets_SelectedIndexChanged(object sender, EventArgs e) // можливо прибрати
        {
            if (string.IsNullOrEmpty(selectedExcelFilePath)) return;
            string selectedSheet = cmbExcelSheets.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedSheet)) return;
            DataTable imported = repExcel.ImportFromExcel(selectedExcelFilePath, selectedSheet);
            if (imported != null)
            {
                dataGridView1.DataSource = imported; 
                Toast.Show("SUCCESS", "Успішно завантажено!");
                LoggerService.LogInfo($"Завантаження Excel: {selectedExcelFilePath}, лист {selectedSheet}, рядків: {imported.Rows.Count}");
            }
            else
            {
                Toast.Show("ERROR", "Не вдалося завантажити дані.");
                LoggerService.LogError($"Помилка завантаження Excel: {selectedExcelFilePath}, лист {selectedSheet}");
            }
        }

        private void btnExportToExcel1_Click(object sender, EventArgs e) // можливо прибрати
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel файли (*.xlsx)|*.xlsx";
                sfd.Title = "Зберегти як Excel";
                sfd.FileName = "Збережені_дані.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    repExcel.ExportDataGridViewToExcel(dataGridView1, sfd.FileName);
                }
            }
        }
    }
}
