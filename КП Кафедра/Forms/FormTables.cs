using FastReport.DevComponents.DotNetBar.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormTables : Form
    {
        public Form activeForm { get; private set; }
        private bool isLoading = false;
        public static FormTables Instance { get; private set; }

        public FormTables()
        {
            InitializeComponent();
            AppSettings.LoadSettings();
            AppSettings.ApplyStyle(this);
            AppSettings.StyleChanged += () => AppSettings.ApplyStyle(this);

            InitializeBoxes();
        }

        private void FormTables_Load(object sender, EventArgs e)
        {
            try
            {
                Instance = this;
                isLoading = true;
                OpenChildForm(new FormTeacher());
                isLoading = false;

            }
            catch (Exception ex) { LoggerService.LogError($"Помилка при завантаженні даних: {ex.Message}"); }

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (contentPanel.Controls.Count == 0) return;

                var active = contentPanel.Controls[0];
                string input = txtSearch.Text.Trim();

                if (string.IsNullOrEmpty(input) || input == "Пошук" || input == "Search")
                {
                    switch (active)
                    {
                        case FormTeacher ft when ft.HasDataTable(): ft.ClearFilter(); break;
                        case FormSubject fs when fs.HasDataTable(): fs.ClearFilter(); break;
                        case FormAssignment fa when fa.HasDataTable(): fa.ClearFilter(); break;
                        case FormResearch fr when fr.HasDataTable(): fr.ClearFilter(); break;
                        case FormParticipation fp when fp.HasDataTable(): fp.ClearFilter(); break;
                    }
                    return;
                }

                var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
                if (!validPattern.IsMatch(input))
                {
                    Toast.Show("ERROR", "Невірний формат пошуку.");
                    return;
                }

                switch (active)
                {
                    case FormTeacher teacherForm: teacherForm.Search(input); break;
                    case FormSubject subjectForm: subjectForm.Search(input); break;
                    case FormAssignment assignmentForm: assignmentForm.Search(input); break;
                    case FormResearch researchForm: researchForm.Search(input); break;
                    case FormParticipation participationForm: participationForm.Search(input); break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка під час пошуку: {ex.Message}");
            }
        }

        private void TxtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Пошук" || txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
            } 
        }

        private void TxtSearch_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = LanguageManager.GetString("txtSearch");
            }
        }

        private void btnResearches_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormResearch());
        }

        private void btnSubjects_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormSubject());
        }

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTeacher());
        }

        private void btnAssignments_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAssignment());
        }

        private void btnParticipation_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormParticipation());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            txtSearch.Text = LanguageManager.GetString("txtSearch");
            btnTeachers.Text = LanguageManager.GetString("btnTeachers");
            btnAssignments.Text = LanguageManager.GetString("btnAssignments");
            btnResearches.Text = LanguageManager.GetString("btnResearches");
            btnSubjects.Text = LanguageManager.GetString("btnSubjects");
            btnParticipation.Text = LanguageManager.GetString("btnParticipation");
        }

        private void InitializeBoxes()
        {
            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;
            txtSearch.TextChanged += txtSearch_TextChanged;
        }

        //private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (dataGridView1.Columns.Contains("status"))
        //    {
        //        var row = dataGridView1.Rows[e.RowIndex];
        //        if (row.Cells["status"].Value != null &&
        //            int.TryParse(row.Cells["status"].Value.ToString(), out int status))
        //        {
        //            if (status == 0)
        //            {
        //                // неактивні викладачі
        //                row.DefaultCellStyle.BackColor = Color.LightGray;
        //                row.DefaultCellStyle.ForeColor = Color.DarkSlateGray;
        //                row.DefaultCellStyle.SelectionBackColor = Color.Gray;
        //                row.DefaultCellStyle.SelectionForeColor = Color.White;
        //            }
        //            else
        //            {
        //                // активні викладачі
        //                row.DefaultCellStyle.BackColor = Color.White;
        //                row.DefaultCellStyle.ForeColor = Color.Black;
        //                row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        //                row.DefaultCellStyle.SelectionForeColor = Color.White;
        //            }
        //        }
        //    }
        //}

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(childForm);
            contentPanel.Tag = childForm;
            childForm.BringToFront();
            contentPanel.BringToFront();
            childForm.Show();


        }
    }
}
