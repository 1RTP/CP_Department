using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static КП_Кафедра.ToastForm;
using System.Drawing.Drawing2D;

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

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
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
                    string query = "SELECT emp_id, emp_full_name, emp_position, emp_hire_date, phone_number, email, status FROM teacher ORDER BY status DESC, emp_full_name ASC";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;

                        //if (dataGridView1.Columns.Contains("status")) // прибрати колонку статус з відображення
                        //    dataGridView1.Columns["status"].Visible = false;
                    }
                }
                DataService.Teachers = GetTeachersFromGrid();
            }
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при завантаженні викладачів: {ex.Message}"); }
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
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при завантаженні предметів: {ex.Message}"); }
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
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при завантаженні призначень: {ex.Message}"); }
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
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при завантаженні досліджень: {ex.Message}"); }
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
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка під час пошуку: {ex.Message}"); }
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

        private void btnResearches_Click(object sender, EventArgs e)
        {
            LoadResearch();
        }

        private void btnSubjects_Click(object sender, EventArgs e)
        {
            LoadSubjects();
        }

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            LoadTeachers();
        }

        private void btnAssignments_Click(object sender, EventArgs e)
        {
            LoadAssignments();
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
            btnAddTeacher.Text = LanguageManager.GetString("btnAddTeacher");
            btnDeactivateTeacher.Text = LanguageManager.GetString("btnDeactivateTeacher");
            btnUpdateTeacher.Text = LanguageManager.GetString("btnUpdateTeacher");
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpHireDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"INSERT INTO teacher (emp_full_name, emp_position, emp_hire_date, phone_number, email, status) VALUES (@name, @position, @hireDate, @phone, @mail, 1)";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim());
                        cmd.Parameters.AddWithValue("@hireDate", dtpHireDate.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@mail", txtEmail.Text.Trim());
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadTeachers();
                Toast.Show("SUCCESS", "Викладача додано успішно!"); 
            }
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при додаванні: {ex.Message}"); }
        }

        private void btnUpdateTeacher_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["emp_id"].Value);

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"UPDATE teacher SET
                    emp_full_name = @name,
                    emp_position = @position,
                    emp_hire_date = @hireDate,
                    phone_number = @phone,
                    email = @mail,
                    status = @status
                WHERE emp_id = @id";

                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim());
                        cmd.Parameters.AddWithValue("@hireDate", dtpHireDate.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@mail", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", chkActive.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadTeachers();
                Toast.Show("SUCCESS", "Дані викладача оновлено успішно!");
            }
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при оновленні: {ex.Message}"); }
        }

        private void btnDeactivateTeacher_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["emp_id"].Value);

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "UPDATE teacher SET status = 0 WHERE emp_id = @id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadTeachers();
                Toast.Show("SUCCESS", "Викладача деактивовано.");
            }
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при деактивації: {ex.Message}"); }
        }

        private void btnDeleteTeacher_Click(object sender, EventArgs e) // повне видалення викладача
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["emp_id"].Value);
            var result = MessageBox.Show("Ви впевнені, що хочете повністю видалити цього викладача?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "DELETE FROM teacher WHERE emp_id = @id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadTeachers();
                Toast.Show("SUCCESS", "Викладача повністю видалено!");
            }
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при видаленні: {ex.Message}");}
        } 

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns.Contains("status"))
            {
                var row = dataGridView1.Rows[e.RowIndex];
                if (row.Cells["status"].Value != null &&
                    int.TryParse(row.Cells["status"].Value.ToString(), out int status))
                {
                    if (status == 0)
                    {
                        // неактивні викладачі
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.ForeColor = Color.DarkSlateGray;
                        row.DefaultCellStyle.SelectionBackColor = Color.Gray;
                        row.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                    else
                    {
                        // активні викладачі
                        row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        row.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                        row.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            txtName.Text = dataGridView1.CurrentRow.Cells["emp_full_name"].Value?.ToString();
            txtPosition.Text = dataGridView1.CurrentRow.Cells["emp_position"].Value?.ToString();
            txtPhone.Text = dataGridView1.CurrentRow.Cells["phone_number"].Value?.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value?.ToString();

            //if (DateTime.TryParse(dataGridView1.CurrentRow.Cells["emp_hire_date"].Value?.ToString(), out DateTime d)) dtpHireDate.Value = d;
            dtpHireDate.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells["emp_hire_date"].Value.ToString());

            int status = Convert.ToInt32(dataGridView1.CurrentRow.Cells["status"].Value);
            chkActive.Checked = status == 1;
        }

        private void roundButton3_Click(object sender, EventArgs e)
        {

        }

        private void roundButton2_Click(object sender, EventArgs e)
        {

        }

        private void roundButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
