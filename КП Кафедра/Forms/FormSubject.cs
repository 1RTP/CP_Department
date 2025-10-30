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
using System.Xml.Linq;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormSubject : Form
    {
        private bool isLoading = false;
        private DataTable originalTable;
        private string dbPath = "Data/department.db";

        public FormSubject()
        {
            InitializeComponent();
            InitializeBoxes();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void FormSubject_Load(object sender, EventArgs e)
        {
            try
            {
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

                var columnsToSearch = new[] { "subject_name" }; // колонки для пошуку

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
                Toast.Show("ERROR", $"Помилка в Search(): {ex.Message}");
            }
        }


        private void LoadSubjects()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"SELECT subject_id, subject_name, semester, total_hours FROM subjects";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;
                        ApplyColumnLocalization();
                    }
                }
                DataService.Subjects = GetSubjectsFromGrid();
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при завантаженні дисциплін");
                LoggerService.LogError($"Помилка при завантаженні дисциплін (LoadSubjects): {ex.Message}");
            }
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            btnAddSubject.Text = LanguageManager.GetString("btnAddSubject");
            btnDeleteSubject.Text = LanguageManager.GetString("btnDeleteSubject");
            btnUpdateSubject.Text = LanguageManager.GetString("btnUpdateSubject");
            txtSemester.Text = LanguageManager.GetString("txtSemester");
            txtSubjectName.Text = LanguageManager.GetString("txtSubjectName");
            txtTotalHours.Text = LanguageManager.GetString("txtTotalHours");
            //btnClear.Text = LanguageManager.GetString("btnClear");

            ApplyColumnLocalization();
        }

        private void ApplyColumnLocalization()
        {
            var columnMap = new Dictionary<string, string>
            {
                { "subject_id", "column_subject_id" },
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
        }

        private void txtSubjectName_TextChanged(object sender, EventArgs e)
        {
            string input = txtSubjectName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Назва предмету" || input == "Subject name") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ0-9'\-\s\(\),\.]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат назви дисципліни");
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

            if (string.IsNullOrEmpty(input) || input == LanguageManager.GetString("txtSemester") ||
                input == "Семестр" || input == "Semester")
                return;

            string filtered = new string(input.Where(char.IsDigit).ToArray());
            if (filtered != input)
            {
                txtSemester.Text = filtered;
                txtSemester.SelectionStart = txtSemester.Text.Length;
            }
        }

        private void txtSemester_GotFocus(object sender, EventArgs e)
        {
            if (txtSemester.Text == "Семестр" || txtSemester.Text == "Semester" || txtSemester.Text == LanguageManager.GetString("txtSemester"))
                txtSemester.Text = "";
        }

        private void txtSemester_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSemester.Text)) txtSemester.Text = LanguageManager.GetString("txtSemester");
        }

        private void txtTotalHours_TextChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            string input = txtTotalHours.Text.Trim();

            if (string.IsNullOrEmpty(input) || input == LanguageManager.GetString("txtTotalHours") ||
                input == "Загальна кількість годин" || input == "Total hours")
                return;

            string filtered = new string(input.Where(char.IsDigit).ToArray());
            if (filtered != input)
            {
                txtTotalHours.Text = filtered;
                txtTotalHours.SelectionStart = txtTotalHours.Text.Length;
            }
        }

        private void txtTotalHours_GotFocus(object sender, EventArgs e)
        {
            if (txtTotalHours.Text == "Загальна кількість годин" || txtTotalHours.Text == "Total hours" ||
                txtTotalHours.Text == LanguageManager.GetString("txtTotalHours"))
                txtTotalHours.Text = "";
        }

        private void txtTotalHours_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTotalHours.Text))
                txtTotalHours.Text = LanguageManager.GetString("txtTotalHours");
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
        }

        private void ClearInputFields()
        {
            txtSubjectName.Text = LanguageManager.GetString("txtSubjectName");
            txtSemester.Text = LanguageManager.GetString("txtSemester");
            txtTotalHours.Text = LanguageManager.GetString("txtTotalHours");
        }

        private void btnAddSubject_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"INSERT INTO subjects (subject_name, semester, total_hours) VALUES (@name, @semester, @hours)";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtSubjectName.Text.Trim());
                        cmd.Parameters.AddWithValue("@semester", int.Parse(txtSemester.Text.Trim()));
                        cmd.Parameters.AddWithValue("@hours", int.Parse(txtTotalHours.Text.Trim()));
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSubjects();
                Toast.Show("SUCCESS", "Дисципліну додано успішно!");
            }
            catch (Exception ex) 
            { 
                Toast.Show("ERROR", "Помилка при додаванні");
                LoggerService.LogError($"Помилка при додаванні: {ex.Message}");
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
                    string query = @"UPDATE subjects SET subject_name=@name, semester=@semester, total_hours=@hours WHERE subject_id=@id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtSubjectName.Text.Trim());
                        cmd.Parameters.AddWithValue("@semester", int.Parse(txtSemester.Text.Trim()));
                        cmd.Parameters.AddWithValue("@hours", int.Parse(txtTotalHours.Text.Trim()));
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSubjects();
                Toast.Show("SUCCESS", "Дані предмету оновлено успішно!");
            }
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при оновленні: {ex.Message}"); }
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
                Toast.Show("SUCCESS", "Предмет видалено!");
            }
            catch (Exception ex) 
            {
                Toast.Show("ERROR", "Помилка при видаленні");
                LoggerService.LogError($"Помилка при завантаженні даних: {ex.Message}");
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
