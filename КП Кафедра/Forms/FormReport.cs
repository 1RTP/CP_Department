using FastReport;
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
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormReport : Form
    {
        private Form activeForm = null;
        private string selectedReport = "TeachersReport.frx";

        public FormReport()
        {
            InitializeComponent();
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
    }
}
