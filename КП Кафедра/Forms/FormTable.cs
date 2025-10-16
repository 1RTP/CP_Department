using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormTable : Form
    {
        private DataTable originalTable;
        private string dbPath = "Data/department.db";
        public static FormTable Instance { get; private set; }

        public FormTable()
        {
            InitializeComponent();

            txtSearch.GotFocus += TxtSearch_GotFocus;
            txtSearch.LostFocus += TxtSearch_LostFocus;
            txtSearch.TextChanged += txtSearch_TextChanged_1;
        }

        private void FormTable_Load(object sender, EventArgs e)
        {
            try
            {
                Instance = this;
                LoadTeachers();
                //UpdateGrid(DataService.Teachers);
            }
            catch (Exception ex) { LoggerService.LogError($"Помилка при завантаженні даних: {ex.Message}"); }

        }

        private void LoadTeachers()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "SELECT emp_id, emp_full_name, emp_position, emp_hire_date, phone_number, email FROM teacher";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;
                    }
                }
                DataService.Teachers = GetTeachersFromGrid();
            }
            catch (Exception ex) { MessageBox.Show("Помилка при завантаженні даних: " + ex.Message); }
        }
        private void LoadSubjects()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "SELECT subject_id, subject_name, semester, total_hours FROM subjects";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;
                    }
                }
                DataService.Subjects = GetSubjectsFromGrid();
            }
            catch (Exception ex) { MessageBox.Show("Помилка при завантаженні предметів: " + ex.Message); }
        }
        private void LoadAssignments()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"SELECT a.assignment_id, t.emp_full_name, s.subject_name, l.type_name, a.plan_hours, a.hours_taught
                             FROM assignment a
                             JOIN teacher t ON a.emp_id = t.emp_id
                             JOIN subjects s ON a.subject_id = s.subject_id
                             JOIN lesson_type l ON a.lesson_type_id = l.lesson_type_id";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;
                    }
                }
                DataService.Assignments = GetAssignmentsFromGrid();
            }
            catch (Exception ex) { MessageBox.Show("Помилка при завантаженні призначень: " + ex.Message); }
        }
        private void LoadResearch()
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "SELECT research_id, research_name, start_date, end_date FROM research";

                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;
                    }
                }
                DataService.Researches = GetResearchFromGrid();
            }
            catch (Exception ex) { MessageBox.Show("Помилка при завантаженні досліджень: " + ex.Message); }
        }

        public List<Teacher> GetTeachersFromGrid()
        {
            List<Teacher> teachers = new List<Teacher>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                teachers.Add(new Teacher
                {
                    EmpId = Convert.ToInt32(row.Cells["emp_id"].Value),
                    FullName = row.Cells["emp_full_name"].Value?.ToString(),
                    Position = row.Cells["emp_position"].Value?.ToString(),
                    HireDate = DateTime.TryParse(row.Cells["emp_hire_date"].Value?.ToString(), out DateTime hireDate) ? hireDate : DateTime.MinValue,
                    PhoneNumber = row.Cells["phone_number"].Value?.ToString(),
                    Email = row.Cells["email"].Value?.ToString()
                });
            }
            return teachers;
        }
        public List<Subject> GetSubjectsFromGrid()
        {
            List<Subject> subjects = new List<Subject>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                subjects.Add(new Subject
                {
                    SubjectId = Convert.ToInt32(row.Cells["subject_id"].Value),
                    Name = row.Cells["subject_name"].Value?.ToString(),
                    Semester = Convert.ToInt32(row.Cells["semester"].Value),
                    TotalHours = Convert.ToInt32(row.Cells["total_hours"].Value),
                    Status = true
                });
            }
            return subjects;
        }
        public List<Assignment> GetAssignmentsFromGrid()
        {
            List<Assignment> assignments = new List<Assignment>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                assignments.Add(new Assignment
                {
                    AssignmentId = Convert.ToInt32(row.Cells["assignment_id"].Value),
                    PlanHours = Convert.ToInt32(row.Cells["plan_hours"].Value),
                    TaughtHours = Convert.ToInt32(row.Cells["hours_taught"].Value),
                    LessonType = new LessonType { TypeName = row.Cells["type_name"].Value?.ToString() },
                    Teachers = new List<Teacher> { new Teacher { FullName = row.Cells["emp_full_name"].Value?.ToString() } },
                    Subjects = new List<Subject> { new Subject { Name = row.Cells["subject_name"].Value?.ToString() } },
                    HireDate = DateTime.MinValue,
                    Status = true
                });
            }

            return assignments;
        }
        public List<Research> GetResearchFromGrid()
        {
            List<Research> researches = new List<Research>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                researches.Add(new Research
                {
                    ResearchId = Convert.ToInt32(row.Cells["research_id"].Value),
                    ResearchName = row.Cells["research_name"].Value?.ToString(),
                    StartDate = DateTime.TryParse(row.Cells["start_date"].Value?.ToString(), out DateTime start) ? start : DateTime.MinValue,
                    EndDate = DateTime.TryParse(row.Cells["end_date"].Value?.ToString(), out DateTime end) ? end : DateTime.MinValue,
                    Participants = new List<Participation>()
                });
            }

            return researches;
        }
        
        public void UpdateGrid(List<Teacher> teachers)
        {
            DataTable table = new DataTable();
            table.Columns.Add("emp_id", typeof(int));
            table.Columns.Add("emp_full_name", typeof(string));
            table.Columns.Add("emp_position", typeof(string));
            table.Columns.Add("emp_hire_date", typeof(DateTime));
            table.Columns.Add("phone_number", typeof(string));
            table.Columns.Add("email", typeof(string));

            foreach (var t in teachers) table.Rows.Add(t.EmpId, t.FullName, t.Position, t.HireDate, t.PhoneNumber, t.Email);
            dataGridView1.DataSource = table;
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (originalTable == null) return;
                string input = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(input) || input == "Введіть ім’я" || input == "Enter name") { dataGridView1.DataSource = originalTable.Copy(); return; }
                var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
                if (!validPattern.IsMatch(input)) { Toast.Show("ERROR", "Невірний формат пошуку."); return; }
                DataTable filtered = originalTable.Clone();
                foreach (DataRow row in originalTable.Rows)
                {
                    string fullName = row["emp_full_name"]?.ToString() ?? "";
                    if (!string.IsNullOrEmpty(fullName) && fullName.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0) { filtered.ImportRow(row); }
                }
                dataGridView1.DataSource = filtered;
            }
            catch (Exception ex) { MessageBox.Show("Помилка під час пошуку: " + ex.Message); }
        }

        private void TxtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Введіть ім’я" || txtSearch.Text == "Enter name")
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

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            LoadTeachers();
        }

        private void btnAssignments_Click(object sender, EventArgs e)
        {
            LoadAssignments();
        }

        private void btnSubjects_Click(object sender, EventArgs e)
        {
            LoadSubjects();
        }

        private void btnResearches_Click(object sender, EventArgs e)
        {
            LoadResearch();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
            LoadTeachers();
        }

        private void ApplyLocalization()
        {
            txtSearch.Text = LanguageManager.GetString("txtSearch");
            btnTeachers.Text = LanguageManager.GetString("btnTeachers");
            btnAssignments.Text = LanguageManager.GetString("btnAssignments");
            btnResearches.Text = LanguageManager.GetString("btnResearches");
            btnSubjects.Text = LanguageManager.GetString("btnSubjects");
        }
    }
}
