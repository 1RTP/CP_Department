using FastReport.RichTextParser;
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
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormTeacher : Form
    {
        private DataTable originalTable;
        private bool isLoading = false;
        private string dbPath = "Data/department.db";

        public FormTeacher()
        {
            InitializeComponent();
            InitializeBoxes();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void FormTeacher_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;
                LoadTeachers();
                isLoading = false;

            }
            catch (Exception ex) { LoggerService.LogError($"Помилка при завантаженні даних: {ex.Message}"); }
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

                var columnsToSearch = new[] { "emp_full_name", "emp_position", "email" }; // колонки для пошуку

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


        private void LoadTeachers()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"SELECT emp_id,
                                emp_full_name,
                                emp_position,
                                emp_hire_date,
                                phone_number,
                                email,
                                status
                            FROM teacher
                            ORDER BY status DESC, emp_full_name ASC";

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;
                        ApplyColumnLocalization(); // заголовки клонок

                        //if (dataGridView1.Columns.Contains("status")) // прибрати колонку статус з відображення
                        //    dataGridView1.Columns["status"].Visible = false;
                    }
                }
                DataService.Teachers = GetTeachersFromGrid();
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка при завантаженні викладачів: {ex.Message}");
            }
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

        private void InitializeBoxes()
        {
            txtName.GotFocus += txtName_GotFocus;
            txtName.LostFocus += txtName_LostFocus;
            txtName.TextChanged += txtName_TextChanged;

            txtPosition.GotFocus += txtPosition_GotFocus;
            txtPosition.LostFocus += txtPosition_LostFocus;
            txtPosition.TextChanged += txtPosition_TextChanged;

            txtPhone.GotFocus += txtPhone_GotFocus;
            txtPhone.LostFocus += txtPhone_LostFocus;
            txtPhone.TextChanged += txtPhone_TextChanged;

            txtEmail.GotFocus += txtEmail_GotFocus;
            txtEmail.LostFocus += txtEmail_LostFocus;
            txtEmail.TextChanged += txtEmail_TextChanged;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string input = txtName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "ПІБ" || input == "Full Name") return;
            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат введення ПІБ");
                txtName.Text = "";
            }
        }

        private void txtName_GotFocus(object sender, EventArgs e)
        {
            if (txtName.Text == "ПІБ" || txtName.Text == "Full Name")
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

        private void txtPosition_TextChanged(object sender, EventArgs e)
        {
            string input = txtPosition.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Посада" || input == "Position") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат введення Посади");
                txtPosition.Text = "";
            }
        }

        private void txtPosition_GotFocus(object sender, EventArgs e)
        {
            if (txtPosition.Text == "Посада" || txtPosition.Text == "Position")
            {
                txtPosition.Text = "";
            }
        }

        private void txtPosition_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                txtPosition.Text = LanguageManager.GetString("txtPosition");
            }
        }

        private void dtpHireDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            string input = txtPhone.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Телефон" || input == "Phone") return;

            const int maxLength = 15;
            if (input.Length > maxLength)
            {
                //Toast.Show("ERROR", $"Номер телефону не може бути довшим за {maxLength} символів.");
                txtPhone.Text = input.Substring(0, maxLength);
                txtPhone.SelectionStart = txtPhone.Text.Length;
                return;
            }
            string filtered = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (i == 0 && input[i] == '+') filtered += '+';
                else if (char.IsDigit(input[i])) filtered += input[i];
            }

            if (filtered != input)
            {
                txtPhone.Text = filtered;
                txtPhone.SelectionStart = txtPhone.Text.Length;
            }
        }

        private void txtPhone_GotFocus(object sender, EventArgs e)
        {
            if (txtPhone.Text == "Телефон" || txtPhone.Text == "Phone")
            {
                txtPhone.Text = "";
            }
        }

        private void txtPhone_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                txtPhone.Text = LanguageManager.GetString("txtPhone");
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string input = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            string placeholder = LanguageManager.GetString("txtEmail");
            if (input == placeholder || input == "Email" || input == "Електронна пошта") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9@._\-]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Email має містити лише англійські символи та допустимі знаки.");
                txtEmail.Text = "";
            }
        }

        private void txtEmail_GotFocus(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Електронна пошта" || txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
            }
        }

        private void txtEmail_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = LanguageManager.GetString("txtEmail");
            }
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
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при видаленні: {ex.Message}"); }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            btnAddTeacher.Text = LanguageManager.GetString("btnAddTeacher");
            btnDeactivateTeacher.Text = LanguageManager.GetString("btnDeactivateTeacher");
            btnUpdateTeacher.Text = LanguageManager.GetString("btnUpdateTeacher");
            txtName.Text = LanguageManager.GetString("txtName");
            txtPosition.Text = LanguageManager.GetString("txtPosition");
            txtPhone.Text = LanguageManager.GetString("txtPhone");
            txtEmail.Text = LanguageManager.GetString("txtEmail");
            chkActive.Text = LanguageManager.GetString("chkActive");
            //btnClear.Text = LanguageManager.GetString("btnClear");

            ApplyColumnLocalization();
        }

        private void ApplyColumnLocalization()
        {
            var columnMap = new Dictionary<string, string>
            {
                { "emp_id", "column_emp_id" },
                { "emp_full_name", "column_emp_full_name" },
                { "emp_position", "column_emp_position" },
                { "emp_hire_date", "column_emp_hire_date" },
                { "phone_number", "column_phone_number" },
                { "email", "column_email" },
                { "status", "column_status" },

            };

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (columnMap.TryGetValue(col.Name, out string langKey))
                {
                    col.HeaderText = LanguageManager.GetString(langKey);
                }
            }
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (isLoading) return;

            if (dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.IsNewRow)
            {
                ClearInputFields();
                return;
            }

            var row = dataGridView1.CurrentRow;

            txtName.Text = row.Cells["emp_full_name"].Value?.ToString() ?? LanguageManager.GetString("txtName");
            txtPosition.Text = row.Cells["emp_position"].Value?.ToString() ?? LanguageManager.GetString("txtPosition");
            txtPhone.Text = row.Cells["phone_number"].Value?.ToString() ?? LanguageManager.GetString("txtPhone");
            txtEmail.Text = row.Cells["email"].Value?.ToString() ?? LanguageManager.GetString("txtEmail");

            if (DateTime.TryParse(row.Cells["emp_hire_date"].Value?.ToString(), out DateTime hireDate)) dtpHireDate.Value = hireDate;
            else dtpHireDate.Value = DateTime.Today;

            if (dataGridView1.Columns.Contains("status") && int.TryParse(row.Cells["status"].Value?.ToString(), out int status))
            {
                chkActive.Checked = status == 1;
            }
            else
            {
                chkActive.Checked = false;
            }
        }

        private void ClearInputFields()
        {
            txtName.Text = LanguageManager.GetString("txtName"); 
            txtPosition.Text = LanguageManager.GetString("txtPosition");
            txtPhone.Text = LanguageManager.GetString("txtPhone");
            txtEmail.Text = LanguageManager.GetString("txtEmail");
            chkActive.Checked = false;
            dtpHireDate.Value = DateTime.Today;
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
