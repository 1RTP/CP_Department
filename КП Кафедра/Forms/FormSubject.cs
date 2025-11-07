using Microsoft.Data.Sqlite;
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
using System.Xml.Linq;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormSubject : Form
    {
        private bool isLoading = false;
        private DataTable originalTable;
        private readonly string dbPath;
        public static FormSubject Instance { get; private set; }

        public FormSubject()
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

        private void FormSubject_Load(object sender, EventArgs e)
        {
            try
            {
                Instance = this;
                isLoading = true;
                LoadSubjects();
                isLoading = false;
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при завантаженні предметів (FormSubject_Load): {ex.Message}");
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

                var columnsToSearch = new[] { "subject_name", "specialty_name" }; // колонки для пошуку

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


        private void LoadSubjects()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"SELECT 
                                s.subject_id,
                                s.subject_name,
                                sp.specialty_id,
                                sp.specialty_name,
                                s.semester,
                                s.total_hours
                            FROM subjects s
                            LEFT JOIN specialty sp ON s.specialty_id = sp.specialty_id;";

                    DataTable table = new DataTable();
                    table.Columns.Add("subject_id", typeof(int));
                    table.Columns.Add("subject_name", typeof(string));
                    table.Columns.Add("specialty_id", typeof(int));
                    table.Columns.Add("specialty_name", typeof(string));
                    table.Columns.Add("semester", typeof(int));
                    table.Columns.Add("total_hours", typeof(int));

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            table.Rows.Add(
                                reader["subject_id"] != DBNull.Value ? Convert.ToInt32(reader["subject_id"]) : 0,
                                reader["subject_name"]?.ToString(),
                                reader["specialty_id"] != DBNull.Value ? Convert.ToInt32(reader["specialty_id"]) : 0,
                                reader["specialty_name"]?.ToString(),
                                reader["semester"] != DBNull.Value ? Convert.ToInt32(reader["semester"]) : 0,
                                reader["total_hours"] != DBNull.Value ? Convert.ToInt32(reader["total_hours"]) : 0
                            );
                        }
                    }

                    originalTable = table.Copy();
                    dataGridView1.DataSource = table;
                    ApplyColumnLocalization();

                    if (dataGridView1.Columns.Contains("specialty_id"))
                        dataGridView1.Columns["specialty_id"].Visible = false;
                }

                DataService.Subjects = GetSubjectsFromGrid();
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при завантаженні");
                LoggerService.LogError($"Помилка при завантаженні дисциплін (LoadSubjects): {ex.Message}");
            }
        }

        public List<Subject> GetSubjectsFromGrid()
        {
            var subjects = new List<Subject>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                subjects.Add(new Subject
                {
                    SubjectId = Convert.ToInt32(row.Cells["subject_id"].Value),
                    Name = row.Cells["subject_name"].Value?.ToString(),
                    Specialty = new Specialty
                    {
                        SpecialtyId = row.Cells["specialty_id"].Value == DBNull.Value ? 0 : Convert.ToInt32(row.Cells["specialty_id"].Value),
                        SpecialtyName = row.Cells["specialty_name"].Value?.ToString() ?? ""
                    },
                    Semester = Convert.ToInt32(row.Cells["semester"].Value),
                    TotalHours = Convert.ToInt32(row.Cells["total_hours"].Value),
                    Status = true
                });
            }

            return subjects;
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
            btnAddSubject.Text = LanguageManager.GetString("btnAddSubject");
            btnDeleteSubject.Text = LanguageManager.GetString("btnDeleteSubject");
            btnUpdateSubject.Text = LanguageManager.GetString("btnUpdateSubject");
            txtSemester.Text = LanguageManager.GetString("txtSemester");
            txtSubjectName.Text = LanguageManager.GetString("txtSubjectName");
            txtTotalHours.Text = LanguageManager.GetString("txtTotalHours");
            txtSpecialty.Text = LanguageManager.GetString("txtSpecialty");
            //btnClear.Text = LanguageManager.GetString("btnClear");

            ApplyColumnLocalization();
        }

        private void ApplyColumnLocalization()
        {
            var columnMap = new Dictionary<string, string>
            {
                { "subject_id", "column_subject_id" },
                { "specialty_name", "column_specialty" },
                { "subject_name", "column_subject_name" },
                { "semester", "column_semester" },
                { "total_hours", "column_total_hours" }
            };

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (columnMap.TryGetValue(col.Name, out string langKey))
                {
                    col.HeaderText = LanguageManager.GetString(langKey);
                }
            }
        }

        private void InitializeBoxes()
        {
            txtSubjectName.GotFocus += txtSubjectName_GotFocus;
            txtSubjectName.LostFocus += txtSubjectName_LostFocus;
            txtSubjectName.TextChanged += txtSubjectName_TextChanged;

            txtSemester.GotFocus += txtSemester_GotFocus;
            txtSemester.LostFocus += txtSemester_LostFocus;
            txtSemester.TextChanged += txtSemester_TextChanged;

            txtTotalHours.GotFocus += txtTotalHours_GotFocus;
            txtTotalHours.LostFocus += txtTotalHours_LostFocus;
            txtTotalHours.TextChanged += txtTotalHours_TextChanged;

            txtSpecialty.GotFocus += txtSpecialty_GotFocus;
            txtSpecialty.LostFocus += txtSpecialty_LostFocus;
            txtSpecialty.TextChanged += txtSpecialty_TextChanged;
        }

        private void txtSubjectName_TextChanged(object sender, EventArgs e)
        {
            string input = txtSubjectName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Назва дисципліни" || input == "Subject name") return;

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

        private void txtSemester_TextChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            string input = txtSemester.Text.Trim();

            if (string.IsNullOrEmpty(input) || input == LanguageManager.GetString("txtSemester") || input == "Семестр" || input == "Semester") return;

            string filtered = new string(input.Where(char.IsDigit).ToArray());
            if (filtered != input)
            {
                txtSemester.Text = filtered;
                txtSemester.SelectionStart = txtSemester.Text.Length;
            }
        }

        private void txtSemester_GotFocus(object sender, EventArgs e)
        {
            if (txtSemester.Text == "Семестр" || txtSemester.Text == "Semester" || txtSemester.Text == LanguageManager.GetString("txtSemester")) txtSemester.Text = "";
        }

        private void txtSemester_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSemester.Text)) txtSemester.Text = LanguageManager.GetString("txtSemester");
        }

        private void txtTotalHours_TextChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            string input = txtTotalHours.Text.Trim();

            if (string.IsNullOrEmpty(input) || input == LanguageManager.GetString("txtTotalHours") || input == "Загальна кількість годин" || input == "Total hours") return;

            string filtered = new string(input.Where(char.IsDigit).ToArray());
            if (filtered != input)
            {
                txtTotalHours.Text = filtered;
                txtTotalHours.SelectionStart = txtTotalHours.Text.Length;
            }
        }

        private void txtTotalHours_GotFocus(object sender, EventArgs e)
        {
            if (txtTotalHours.Text == "Загальна кількість годин" || txtTotalHours.Text == "Total hours" || txtTotalHours.Text == LanguageManager.GetString("txtTotalHours")) txtTotalHours.Text = "";
        }

        private void txtTotalHours_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTotalHours.Text)) txtTotalHours.Text = LanguageManager.GetString("txtTotalHours");
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading) return;
            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                ClearInputFields();
                return;
            }

            var row = dataGridView1.CurrentRow;
            txtSubjectName.Text = row.Cells["subject_name"].Value?.ToString() ?? LanguageManager.GetString("txtSubjectName");
            txtSemester.Text = row.Cells["semester"].Value?.ToString() ?? LanguageManager.GetString("txtSemester");
            txtTotalHours.Text = row.Cells["total_hours"].Value?.ToString() ?? LanguageManager.GetString("txtTotalHours");
            txtSpecialty.Text = row.Cells["specialty_name"].Value?.ToString() ?? LanguageManager.GetString("txtSpecialty");

        }

        private void ClearInputFields()
        {
            txtSubjectName.Text = LanguageManager.GetString("txtSubjectName");
            txtSemester.Text = LanguageManager.GetString("txtSemester");
            txtTotalHours.Text = LanguageManager.GetString("txtTotalHours");
            txtSpecialty.Text = LanguageManager.GetString("txtSpecialty");
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    int specialtyId = 0;
                    string specialtyName = txtSpecialty.Text.Trim();

                    if (!string.IsNullOrEmpty(specialtyName))
                    {
                        string findQuery = "SELECT specialty_id FROM specialty WHERE specialty_name = @name LIMIT 1";
                        using (var findCmd = new SqliteCommand(findQuery, connection))
                        {
                            findCmd.Parameters.AddWithValue("@name", specialtyName);
                            object result = findCmd.ExecuteScalar();

                            if (result != null) specialtyId = Convert.ToInt32(result);
                            else
                            {
                                string insertSpec = "INSERT INTO specialty (specialty_name) VALUES (@name)";
                                using (var insertCmd = new SqliteCommand(insertSpec, connection))
                                {
                                    insertCmd.Parameters.AddWithValue("@name", specialtyName);
                                    insertCmd.ExecuteNonQuery();
                                }
                                string getId = "SELECT last_insert_rowid()";
                                using (var getCmd = new SqliteCommand(getId, connection))
                                {
                                    specialtyId = Convert.ToInt32(getCmd.ExecuteScalar());
                                }
                            }
                        }
                    }

                    string query = @"INSERT INTO subjects (subject_name, semester, total_hours, specialty_id) VALUES (@name, @semester, @hours, @specialtyId)";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtSubjectName.Text.Trim());
                        cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
                        cmd.Parameters.AddWithValue("@semester", int.Parse(txtSemester.Text.Trim()));
                        cmd.Parameters.AddWithValue("@hours", int.Parse(txtTotalHours.Text.Trim()));
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadSubjects();
                Toast.Show("SUCCESS", "Успішно додано!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при додаванні");
                LoggerService.LogError($"Помилка при додаванні дисципліни: {ex.Message}");
            }
        }

        private void btnUpdateSubject_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["subject_id"].Value);

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    int specialtyId = 0;
                    string specialtyName = txtSpecialty.Text.Trim();

                    if (!string.IsNullOrEmpty(specialtyName))
                    {
                        string findQuery = "SELECT specialty_id FROM specialty WHERE specialty_name = @name LIMIT 1";
                        using (var findCmd = new SqliteCommand(findQuery, connection))
                        {
                            findCmd.Parameters.AddWithValue("@name", specialtyName);
                            object result = findCmd.ExecuteScalar();

                            if (result != null) specialtyId = Convert.ToInt32(result);
                            else
                            {
                                string insertSpec = "INSERT INTO specialty (specialty_name) VALUES (@name)";
                                using (var insertCmd = new SqliteCommand(insertSpec, connection))
                                {
                                    insertCmd.Parameters.AddWithValue("@name", specialtyName);
                                    insertCmd.ExecuteNonQuery();
                                }
                                string getId = "SELECT last_insert_rowid()";
                                using (var getCmd = new SqliteCommand(getId, connection))
                                {
                                    specialtyId = Convert.ToInt32(getCmd.ExecuteScalar());
                                }
                            }
                        }
                    }

                    string query = @"UPDATE subjects SET subject_name = @name, specialty_id = @specialtyId, semester = @semester, total_hours = @hours WHERE subject_id = @id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtSubjectName.Text.Trim());
                        cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
                        cmd.Parameters.AddWithValue("@semester", int.Parse(txtSemester.Text.Trim()));
                        cmd.Parameters.AddWithValue("@hours", int.Parse(txtTotalHours.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadSubjects();
                Toast.Show("SUCCESS", "Успішно оновлено!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при оновленні");
                LoggerService.LogError($"Помилка при оновленні дисципліни: {ex.Message}");
            }
        }

        private void btnDeleteSubject_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["subject_id"].Value);
            var result = MessageBox.Show("Ви впевнені, що хочете видалити цю дисципліну?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "DELETE FROM subjects WHERE subject_id=@id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSubjects();
                Toast.Show("SUCCESS", "Успішно видалено!");
            }
            catch (Exception ex) 
            {
                Toast.Show("ERROR", "Помилка при видаленні");
                LoggerService.LogError($"Помилка при видаленні: {ex.Message}");
            }
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
