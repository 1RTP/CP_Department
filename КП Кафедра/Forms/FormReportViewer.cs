using FastReport;
using FastReport.Data;
using FastReport.Design;
using FastReport.Design.StandardDesigner;
using FastReport.Format;
using FastReport.Preview;
using FastReport.Utils;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormReportViewer : Form
    {
        private string _reportFile; 
        private PreviewControl pc; 

        public FormReportViewer(string reportFile)
        {
            InitializeComponent();
            _reportFile = reportFile;
        }

        private void FormReportViewer_Load(object sender, EventArgs e)
        {
            try
            {
                pc = new PreviewControl { Dock = DockStyle.Fill };
                this.Controls.Add(pc);

                string reportPath = Path.Combine(Application.StartupPath, "Reports", _reportFile);

                if (!File.Exists(reportPath))
                {
                    Toast.Show("ERROR", $"Шаблон звіту не знайдено!{reportPath}"); return;
                }

                string xmlPath = Path.Combine(Application.StartupPath, "Data", "department.xml");
                if (!File.Exists(xmlPath))
                {
                    Toast.Show("ERROR", $"Файл даних не знайдено!{xmlPath}");
                    return;
                }

                Report report = new Report();
                report.Load(reportPath);
                DataSet dataSet = new DataSet();
                dataSet.ReadXml(xmlPath);

                string tableName = DetectTableName();

                if (dataSet.Tables.Contains(tableName))
                {
                    report.RegisterData(dataSet.Tables[tableName], tableName);
                    report.Prepare();
                    report.Preview = pc;
                    report.ShowPrepared();
                }
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка при завантаженні звіту: {ex.Message}");
            }
        }

        private string DetectTableName()
        {
            if (_reportFile.Contains("Teachers")) return "teacher";
            if (_reportFile.Contains("Subjects")) return "subject";
            if (_reportFile.Contains("Assignments")) return "assignment";
            if (_reportFile.Contains("Research")) return "research";
            return "";
        }
    }
}
