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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormAssignment : Form
    {
        private bool isLoading = false;
        private DataTable originalTable;
        private readonly string dbPath;
        public static FormAssignment Instance { get; private set; }

        public FormAssignment()
        {
            InitializeComponent();
            AppSettings.LoadSettings();
            AppSettings.ApplyStyle(this);
            AppSettings.StyleChanged += () => AppSettings.ApplyStyle(this);

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(exeDir, "..", ".."));
            dbPath = Path.Combine(projectRoot, "Data", "department.db");

            InitializeBoxes();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void FormAssignment_Load(object sender, EventArgs e)
        {
            try
            {
                Instance = this;
                isLoading = true;
                LoadAssignments();
                isLoading = false;
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при завантаженні призначень (FormAssignment_Load): {ex.Message}");
            }
        }

        public bool HasDataTable()
        {
            return dataGridView1?.DataSource is DataTable;
        }

        public void ClearFilter()
        {
            if (dataGridView1?.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = "";
            }
        }

        public void Search(string query)
        {
            try
            {
                if (!(dataGridView1?.DataSource is DataTable dt)) return;
                string safe = query.Replace("'", "''");

                var columnsToSearch = new[] { "emp_full_name", "subject_name", "type_name" }; // колонки для пошуку

                var parts = new List<string>();
                foreach (var col in columnsToSearch)
                {
                    if (!dt.Columns.Contains(col)) continue;
                    parts.Add($"{col} LIKE '%{safe}%'");
                }

                if (parts.Count == 0)
                {
                    dt.DefaultView.RowFilter = ""; // немає колонок — скидаємо фільтр
                    return;
                }

                dt.DefaultView.RowFilter = string.Join(" OR ", parts);
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка в Search()");
                LoggerService.LogError($"Помилка в Search(): {ex.Message}");
            }
        }
        private void LoadAssignments()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string query = @"SELECT 
                            a.assignment_id,
                            t.emp_id,
                            t.emp_full_name,
                            s.subject_id,
                            s.subject_name,
                            sp.specialty_id,
                            sp.specialty_name,
                            l.lesson_type_id,
                            l.type_name,
                            a.plan_hours,
                            a.hours_taught
                        FROM assignment a
                        JOIN teacher t ON a.emp_id = t.emp_id
                        JOIN subjects s ON a.subject_id = s.subject_id
                        LEFT JOIN specialty sp ON s.specialty_id = sp.specialty_id
                        JOIN lesson_type l ON a.lesson_type_id = l.lesson_type_id
                        ORDER BY a.assignment_id ASC;";

                    DataTable table = new DataTable();
                    table.Columns.Add("assignment_id", typeof(int));
                    table.Columns.Add("emp_id", typeof(int));
                    table.Columns.Add("emp_full_name", typeof(string));
                    table.Columns.Add("subject_id", typeof(int));
                    table.Columns.Add("subject_name", typeof(string));
                    table.Columns.Add("specialty_id", typeof(int));
                    table.Columns.Add("specialty_name", typeof(string));
                    table.Columns.Add("lesson_type_id", typeof(int));
                    table.Columns.Add("type_name", typeof(string));
                    table.Columns.Add("plan_hours", typeof(int));
                    table.Columns.Add("hours_taught", typeof(int));

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            table.Rows.Add(
                                reader["assignment_id"] != DBNull.Value ? Convert.ToInt32(reader["assignment_id"]) : 0,
                                reader["emp_id"] != DBNull.Value ? Convert.ToInt32(reader["emp_id"]) : 0,
                                reader["emp_full_name"]?.ToString(),
                                reader["subject_id"] != DBNull.Value ? Convert.ToInt32(reader["subject_id"]) : 0,
                                reader["subject_name"]?.ToString(),
                                reader["specialty_id"] != DBNull.Value ? Convert.ToInt32(reader["specialty_id"]) : (int?)null,
                                reader["specialty_name"]?.ToString(),
                                reader["lesson_type_id"] != DBNull.Value ? Convert.ToInt32(reader["lesson_type_id"]) : 0,
                                reader["type_name"]?.ToString(),
                                reader["plan_hours"] != DBNull.Value ? Convert.ToInt32(reader["plan_hours"]) : 0,
                                reader["hours_taught"] != DBNull.Value ? Convert.ToInt32(reader["hours_taught"]) : 0
                            );
                        }
                    }

                    originalTable = table.Copy();
                    dataGridView1.DataSource = table;
                    ApplyColumnLocalization();

                    string[] hiddenCols = { "emp_id", "subject_id", "specialty_id", "lesson_type_id" };
                    foreach (var col in hiddenCols)
                    {
                        if (dataGridView1.Columns.Contains(col)) dataGridView1.Columns[col].Visible = false;
                    }
                }

                DataService.Assignments = GetAssignmentsFromGrid();
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при завантаженні призначень");
                LoggerService.LogError($"Помилка при завантаженні призначень (LoadAssignments): {ex.Message}");
            }
        }

        public List<Assignment> GetAssignmentsFromGrid()
        {
            var assignments = new List<Assignment>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                assignments.Add(new Assignment
                {
                    AssignmentId = Convert.ToInt32(row.Cells["assignment_id"].Value),
                    PlanHours = Convert.ToInt32(row.Cells["plan_hours"].Value),
                    TaughtHours = Convert.ToInt32(row.Cells["hours_taught"].Value),

                    Teacher = new Teacher
                    {
                        EmpId = Convert.ToInt32(row.Cells["emp_id"].Value),
                        FullName = row.Cells["emp_full_name"].Value?.ToString()
                    },

                    Subject = new Subject
                    {
                        SubjectId = Convert.ToInt32(row.Cells["subject_id"].Value),
                        Name = row.Cells["subject_name"].Value?.ToString(),
                        Specialty = new Specialty
                        {
                            SpecialtyId = Convert.ToInt32(row.Cells["specialty_id"].Value),
                            SpecialtyName = row.Cells["specialty_name"].Value?.ToString()
                        }
                    },

                    LessonType = new LessonType
                    {
                        LessonTypeId = Convert.ToInt32(row.Cells["lesson_type_id"].Value),
                        TypeName = row.Cells["type_name"].Value?.ToString()
                    }
                });
            }

            return assignments;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            LanguageManager.LanguageChanged -= ApplyLocalization;
            Instance = null;
        }

        private void ApplyLocalization()
        {
            btnAddAssignment.Text = LanguageManager.GetString("btnAddAssignment");
            btnUpdateAssignment.Text = LanguageManager.GetString("btnUpdateAssignment");
            btnDeleteAssignment.Text = LanguageManager.GetString("btnDeleteAssignment");
            //btnClear.Text = LanguageManager.GetString("btnClear");

            txtName.Text = LanguageManager.GetString("txtName");
            txtSubjectName.Text = LanguageManager.GetString("txtSubjectName");
            txtLessonType.Text = LanguageManager.GetString("txtLessonType");
            txtPlanHours.Text = LanguageManager.GetString("txtPlanHours");
            txtHoursTaught.Text = LanguageManager.GetString("txtHoursTaught");
            txtSpecialty.Text = LanguageManager.GetString("txtSpecialty");

            ApplyColumnLocalization();
        }

        private void ApplyColumnLocalization()
        {
            var columnMap = new Dictionary<string, string>
            {
                { "assignment_id", "column_assignment_id" },
                { "emp_full_name", "column_emp_full_name" },
                { "subject_name", "column_subject_name" },
                { "type_name", "column_type_name" },
                { "plan_hours", "column_plan_hours" },
                { "specialty_name", "column_specialty_name" },
                { "hours_taught", "column_hours_taught" }
            };

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (columnMap.TryGetValue(col.Name, out string langKey))
                {
                    col.HeaderText = LanguageManager.GetString(langKey);
                }
            }
        }

        private void btnAddAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    int empId = GetTeacherIdByName(connection, txtName.Text.Trim());
                    int subjId = GetSubjectIdByName(connection, txtSubjectName.Text.Trim());
                    int typeId = GetLessonTypeIdByName(connection, txtLessonType.Text.Trim());

                    if (empId == 0 || subjId == 0 || typeId == 0)
                    {
                        Toast.Show("ERROR", "Перевірте введені дані.");
                        return;
                    }

                    if (!int.TryParse(txtPlanHours.Text, out int plan) || !int.TryParse(txtHoursTaught.Text, out int taught))
                    {
                        Toast.Show("ERROR", "Некоректні години.");
                        return;
                    }

                    string query = @"INSERT INTO assignment (emp_id, subject_id, lesson_type_id, plan_hours, hours_taught) VALUES (@emp, @subj, @type, @plan, @taught)";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@emp", empId);
                        cmd.Parameters.AddWithValue("@subj", subjId);
                        cmd.Parameters.AddWithValue("@type", typeId);
                        cmd.Parameters.AddWithValue("@plan", plan);
                        cmd.Parameters.AddWithValue("@taught", taught);
                        cmd.ExecuteNonQuery();
                    }

                    string specialtyQuery = @"SELECT sp.specialty_name FROM subjects s LEFT JOIN specialty sp ON s.specialty_id = sp.specialty_id WHERE s.subject_id = @subjId";
                    using (var cmd = new SqliteCommand(specialtyQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@subjId", subjId);
                        object res = cmd.ExecuteScalar();
                        txtSpecialty.Text = res != null ? res.ToString() : "";
                    }
                }

                LoadAssignments();
                Toast.Show("SUCCESS", "Успішно додано!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при додаванні");
                LoggerService.LogError($"Помилка при додаванні призначення (btnAddAssignment_Click): {ex.Message}");
            }
        }

        private void btnUpdateAssignment_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                Toast.Show("INFO", "Оберіть запис для оновлення");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["assignment_id"].Value);

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    int empId = GetTeacherIdByName(connection, txtName.Text.Trim());
                    int subjId = GetSubjectIdByName(connection, txtSubjectName.Text.Trim());
                    int typeId = GetLessonTypeIdByName(connection, txtLessonType.Text.Trim());

                    if (empId == 0 || subjId == 0 || typeId == 0)
                    {
                        Toast.Show("ERROR", "Не знайдено дані.");
                        return;
                    }

                    if (!int.TryParse(txtPlanHours.Text, out int plan) || !int.TryParse(txtHoursTaught.Text, out int taught))
                    {
                        Toast.Show("ERROR", "Некоректні години.");
                        return;
                    }

                    string query = @"UPDATE assignment SET emp_id=@emp, subject_id=@subj, lesson_type_id=@type, plan_hours=@plan, hours_taught=@taught WHERE assignment_id=@id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@emp", empId);
                        cmd.Parameters.AddWithValue("@subj", subjId);
                        cmd.Parameters.AddWithValue("@type", typeId);
                        cmd.Parameters.AddWithValue("@plan", plan);
                        cmd.Parameters.AddWithValue("@taught", taught);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    string specialtyQuery = @"SELECT sp.specialty_name FROM subjects s LEFT JOIN specialty sp ON s.specialty_id = sp.specialty_id WHERE s.subject_id = @subjId";
                    using (var cmd = new SqliteCommand(specialtyQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@subjId", subjId);
                        object res = cmd.ExecuteScalar();
                        txtSpecialty.Text = res != null ? res.ToString() : "";
                    }
                }

                LoadAssignments();
                Toast.Show("SUCCESS", "Успішно оновлено!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при оновленні");
                LoggerService.LogError($"Помилка при оновленні призначення (btnUpdateAssignment_Click): {ex.Message}");
            }
        }

        private void btnDeleteAssignment_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                Toast.Show("INFO", "Оберіть запис для видалення");
                return;
            }

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["assignment_id"].Value);
            var result = MessageBox.Show("Ви впевнені, що хочете видалити це призначення?","Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "DELETE FROM assignment WHERE assignment_id=@id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadAssignments();
                Toast.Show("SUCCESS", "Успішно видалено!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при видаленні");
                LoggerService.LogError($"Помилка при видаленні призначення (btnDeleteAssignment_Click): {ex.Message}");
            }
        }

        private void ClearInputFields()
        {
            txtName.Text = LanguageManager.GetString("txtName");
            txtSubjectName.Text = LanguageManager.GetString("txtSubjectName");
            txtLessonType.Text = LanguageManager.GetString("txtLessonType");
            txtPlanHours.Text = LanguageManager.GetString("txtPlanHours");
            txtHoursTaught.Text = LanguageManager.GetString("txtHoursTaught");
            txtSpecialty.Text = LanguageManager.GetString("txtSpecialty");
        }
               
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                ClearInputFields();
                return;
            }

            txtName.Text = dataGridView1.CurrentRow.Cells["emp_full_name"].Value?.ToString();
            txtSubjectName.Text = dataGridView1.CurrentRow.Cells["subject_name"].Value?.ToString();
            txtLessonType.Text = dataGridView1.CurrentRow.Cells["type_name"].Value?.ToString();
            txtPlanHours.Text = dataGridView1.CurrentRow.Cells["plan_hours"].Value?.ToString();
            txtHoursTaught.Text = dataGridView1.CurrentRow.Cells["hours_taught"].Value?.ToString();

            if (dataGridView1.Columns.Contains("specialty_name") && dataGridView1.CurrentRow.Cells["specialty_name"].Value != DBNull.Value)
            {
                txtSpecialty.Text = dataGridView1.CurrentRow.Cells["specialty_name"].Value?.ToString();
            }
            else { txtSpecialty.Text = LanguageManager.GetString("txtSpecialty"); }
        }

        private int GetTeacherIdByName(SqliteConnection con, string name)
        {
            string sql = "SELECT emp_id FROM teacher WHERE emp_full_name=@n";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@n", name);
                object res = cmd.ExecuteScalar();
                return res != null ? Convert.ToInt32(res) : 0;
            }
        }

        private int GetSubjectIdByName(SqliteConnection con, string name)
        {
            string sql = "SELECT subject_id FROM subjects WHERE subject_name=@n";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@n", name);
                object res = cmd.ExecuteScalar();
                return res != null ? Convert.ToInt32(res) : 0;
            }
        }

        private int GetLessonTypeIdByName(SqliteConnection con, string name)
        {
            string sql = "SELECT lesson_type_id FROM lesson_type WHERE type_name=@n";
            using (var cmd = new SqliteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@n", name);
                object res = cmd.ExecuteScalar();
                return res != null ? Convert.ToInt32(res) : 0;
            }
        }

        private void InitializeBoxes()
        {
            txtName.GotFocus += txtName_GotFocus;
            txtName.LostFocus += txtName_LostFocus;
            txtName.TextChanged += txtName_TextChanged;

            txtSubjectName.GotFocus += txtSubjectName_GotFocus;
            txtSubjectName.LostFocus += txtSubjectName_LostFocus;
            txtSubjectName.TextChanged += txtSubjectName_TextChanged;

            txtLessonType.GotFocus += txtLessonType_GotFocus;
            txtLessonType.LostFocus += txtLessonType_LostFocus;
            txtLessonType.TextChanged += txtLessonType_TextChanged;

            txtPlanHours.GotFocus += txtPlanHours_GotFocus;
            txtPlanHours.LostFocus += txtPlanHours_LostFocus;
            txtPlanHours.TextChanged += txtPlanHours_TextChanged;

            txtHoursTaught.GotFocus += txtHoursTaught_GotFocus;
            txtHoursTaught.LostFocus += txtHoursTaught_LostFocus;
            txtHoursTaught.TextChanged += txtHoursTaught_TextChanged;

            txtSpecialty.GotFocus += txtSpecialty_GotFocus;
            txtSpecialty.LostFocus += txtSpecialty_LostFocus;
            txtSpecialty.TextChanged += txtSpecialty_TextChanged;
        }

        private void txtSpecialty_TextChanged(object sender, EventArgs e)
        {
            string input = txtSpecialty.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Спеціальність" || input == "Specialty") return;
            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Помилка у полі 'Спеціальність'.");
                txtSpecialty.Text = "";
            }
        }

        private void txtSpecialty_GotFocus(object sender, EventArgs e)
        {
            if (txtSpecialty.Text == "Спеціальність" || txtSpecialty.Text == "Specialty")
            {
                txtSpecialty.Text = "";
            }
        }

        private void txtSpecialty_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSpecialty.Text))
            {
                txtSpecialty.Text = LanguageManager.GetString("txtSpecialty");
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string input = txtName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "ПІБ" || input == "Full Name") return;
            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Помилка у полі 'ПІБ'.");
                txtName.Text = "";
            }
        }

        private void txtName_GotFocus(object sender, EventArgs e)
        {
            if (txtName.Text == "ПІБ" || txtName.Text == "Full name")
            {
                txtName.Text = "";
            }
        }

        private void txtName_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                txtName.Text = LanguageManager.GetString("txtName");
            }
        }

        private void txtSubjectName_TextChanged(object sender, EventArgs e)
        {
            string input = txtSubjectName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Назва предмету" || input == "Subject name") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ0-9'\-\s\(\),\.]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Помилка у полі 'Дисципліна'.");
                txtSubjectName.Text = "";
            }
        }

        private void txtSubjectName_GotFocus(object sender, EventArgs e)
        {
            if (txtSubjectName.Text == "Назва дисципліни" || txtSubjectName.Text == "Subject name")
            {
                txtSubjectName.Text = "";
            }
        }

        private void txtSubjectName_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text))
            {
                txtSubjectName.Text = LanguageManager.GetString("txtSubjectName");
            }
        }

        private void txtLessonType_TextChanged(object sender, EventArgs e)
        {
            string input = txtSubjectName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Тип заняття" || input == "Lesson type") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ0-9'\-\s\(\),\.]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Помилка у полі 'Тип заняття'.");
                txtLessonType.Text = "";
            }
        }

        private void txtLessonType_GotFocus(object sender, EventArgs e)
        {
            if (txtLessonType.Text == "Тип заняття" || txtLessonType.Text == "Lesson type")
            {
                txtLessonType.Text = "";
            }
        }

        private void txtLessonType_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLessonType.Text))
            {
                txtLessonType.Text = LanguageManager.GetString("txtLessonType");
            }
        }

        private void txtPlanHours_TextChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            string input = txtPlanHours.Text.Trim();

            if (string.IsNullOrEmpty(input) || input == LanguageManager.GetString("txtPlanHours") || input == "Кількість годин за планом" || input == "Plan hours") return;

            string filtered = new string(input.Where(char.IsDigit).ToArray());
            if (filtered != input)
            {
                txtPlanHours.Text = filtered;
                txtPlanHours.SelectionStart = txtPlanHours.Text.Length;
            }
        }

        private void txtPlanHours_GotFocus(object sender, EventArgs e)
        {
            if (txtPlanHours.Text == "Кількість годин за планом" || txtPlanHours.Text == "Plan hours" || txtPlanHours.Text == LanguageManager.GetString("txtPlanHours")) txtPlanHours.Text = "";
        }

        private void txtPlanHours_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPlanHours.Text)) txtPlanHours.Text = LanguageManager.GetString("txtPlanHours");
        }

        private void txtHoursTaught_TextChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            string input = txtHoursTaught.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == LanguageManager.GetString("txtTotalHours") || input == "Відпрацьовано годин" || input == "Hours taught") return;

            string filtered = new string(input.Where(char.IsDigit).ToArray());
            if (filtered != input)
            {
                txtHoursTaught.Text = filtered;
                txtHoursTaught.SelectionStart = txtHoursTaught.Text.Length;
            }
        }

        private void txtHoursTaught_GotFocus(object sender, EventArgs e)
        {
            if (txtHoursTaught.Text == "Відпрацьовано годин" || txtHoursTaught.Text == "Hours taught" || txtHoursTaught.Text == LanguageManager.GetString("txtHoursTaught")) txtHoursTaught.Text = "";
        }

        private void txtHoursTaught_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoursTaught.Text)) txtHoursTaught.Text = LanguageManager.GetString("txtHoursTaught");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            isLoading = true;
            ClearInputFields();
            dataGridView1.ClearSelection();
            isLoading = false;
        }
    }
}
