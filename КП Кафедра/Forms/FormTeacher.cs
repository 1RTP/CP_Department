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
        private readonly string dbPath;

        public FormTeacher()
        {
            InitializeComponent();

            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRoot = Path.GetFullPath(Path.Combine(exeDir, "..", ".."));
            dbPath = Path.Combine(projectRoot, "Data", "department.db");

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
            catch (Exception ex) { LoggerService.LogError($"Помилка при завантаженні викладачів (FormTeacher_Load): {ex.Message}"); }
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

                var columnsToSearch = new[] { "emp_full_name", "emp_position", "email", "specialty_name" }; // колонки для пошуку

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
                Toast.Show("ERROR", $"Помилка в Search()");
                LoggerService.LogError($"Помилка в Search(): {ex.Message}");
            }
        }


        private void LoadTeachers()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    t.emp_id,
                    t.emp_full_name,
                    t.emp_position,
                    t.emp_hire_date,
                    t.phone_number,
                    t.email,
                    t.status,
                    s.specialty_id,
                    s.specialty_name
                FROM teacher t
                LEFT JOIN specialty s ON t.specialty_id = s.specialty_id
                ORDER BY t.status DESC, t.emp_id ASC;
            ";

                    DataTable table = new DataTable();
                    table.Columns.Add("emp_id", typeof(int));
                    table.Columns.Add("emp_full_name", typeof(string));
                    table.Columns.Add("specialty_name", typeof(string));
                    table.Columns.Add("emp_position", typeof(string));
                    table.Columns.Add("emp_hire_date", typeof(string));
                    table.Columns.Add("phone_number", typeof(string));
                    table.Columns.Add("email", typeof(string));
                    table.Columns.Add("status", typeof(int));

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Specialty specialty = new Specialty
                            {
                                SpecialtyId = reader["specialty_id"] != DBNull.Value ? Convert.ToInt32(reader["specialty_id"]) : 0,
                                SpecialtyName = reader["specialty_name"] != DBNull.Value ? reader["specialty_name"].ToString() : ""
                            };

                            table.Rows.Add(
                                Convert.ToInt32(reader["emp_id"]),
                                reader["emp_full_name"]?.ToString(),
                                reader["specialty_name"] is DBNull ? null : reader["specialty_name"].ToString(),
                                reader["emp_position"]?.ToString(),
                                reader["emp_hire_date"]?.ToString(),
                                reader["phone_number"]?.ToString(),
                                reader["email"]?.ToString(),
                                reader["status"] == DBNull.Value ? 0 : Convert.ToInt32(reader["status"])
                            );
                        }
                    }

                    originalTable = table.Copy();
                    dataGridView1.DataSource = table;
                    ApplyColumnLocalization();

                    //if (dataGridView1.Columns.Contains("status")) // прибрати колонку статус з відображення
                    //    dataGridView1.Columns["status"].Visible = false;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells["status"].Value != null && row.Cells["status"].Value.ToString() == "0")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                            row.DefaultCellStyle.ForeColor = Color.DimGray;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                }

                DataService.Teachers = GetTeachersFromGrid();
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при завантаженні викладачів");
                LoggerService.LogError($"Помилка при завантаженні викладачів (LoadTeachers): {ex.Message}");
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
                    Specialty = new Specialty { SpecialtyName = row.Cells["specialty_name"].Value?.ToString() },
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

            txtSpecialty.GotFocus += txtSpecialty_GotFocus;
            txtSpecialty.LostFocus += txtSpecialty_LostFocus;
            txtSpecialty.TextChanged += txtSpecialty_TextChanged;

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
            if (string.IsNullOrEmpty(input) || input == "ПІБ" || input == "Full name") return;
            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат введення ПІБ");
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

        private void txtSpecialty_TextChanged(object sender, EventArgs e)
        {
            string input = txtSpecialty.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Спеціальність" || input == "Specialty") return;
            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат введення Спеціальності");
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

                    int specialtyId = 0;
                    string specialtyName = txtSpecialty.Text.Trim();

                    if (!string.IsNullOrEmpty(specialtyName))
                    {
                        string findQuery = "SELECT specialty_id FROM specialty WHERE specialty_name = @name LIMIT 1";
                        using (var findCmd = new SqliteCommand(findQuery, connection))
                        {
                            findCmd.Parameters.AddWithValue("@name", specialtyName);
                            object result = findCmd.ExecuteScalar();

                            if (result != null) { specialtyId = Convert.ToInt32(result); }
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
                    string query = @" INSERT INTO teacher (emp_full_name, emp_position, emp_hire_date, phone_number, email, status, specialty_id) VALUES (@name, @position, @hireDate, @phone, @mail, 1, @specialtyId)";

                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim());
                        cmd.Parameters.AddWithValue("@hireDate", dtpHireDate.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@mail", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadTeachers();
                Toast.Show("SUCCESS", "Викладача додано успішно!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при додаванні викладача");
                LoggerService.LogError($"Помилка при додаванні викладача: {ex.Message}");
            }
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

                    int specialtyId = 0;
                    string specialtyName = txtSpecialty.Text.Trim();

                    if (!string.IsNullOrEmpty(specialtyName))
                    {
                        string findQuery = "SELECT specialty_id FROM specialty WHERE specialty_name = @name LIMIT 1";
                        using (var findCmd = new SqliteCommand(findQuery, connection))
                        {
                            findCmd.Parameters.AddWithValue("@name", specialtyName);
                            object result = findCmd.ExecuteScalar();

                            if (result != null) { specialtyId = Convert.ToInt32(result); }
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
                    string query = @"UPDATE teacher 
                        SET emp_full_name = @name,
                            emp_position = @position,
                            emp_hire_date = @hireDate,
                            phone_number = @phone,
                            email = @mail,
                            status = @status,
                            specialty_id = @specialtyId
                        WHERE emp_id = @id";

                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@position", txtPosition.Text.Trim());
                        cmd.Parameters.AddWithValue("@hireDate", dtpHireDate.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@mail", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@status", chkActive.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@specialtyId", specialtyId);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadTeachers();
                Toast.Show("SUCCESS", "Дані викладача оновлено успішно!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при оновленні даних викладача");
                LoggerService.LogError($"Помилка при оновленні даних викладача: {ex.Message}");
            }
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
            catch (Exception ex) 
            { 
                Toast.Show("ERROR", "Помилка при деактивації");
                LoggerService.LogError($"Помилка при деактивації: {ex.Message}");
            }
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
            catch (Exception ex) 
            { 
                Toast.Show("ERROR", "Помилка при видаленні");
                LoggerService.LogError($"Помилка при видаленні: {ex.Message}");
            }
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
            txtSpecialty.Text = LanguageManager.GetString("txtSpecialty");
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
                { "specialty_name", "column_specialty_name" },
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
            if (originalTable == null) return;

            DataTable table = originalTable.Clone();
            foreach (var t in teachers)
            {
                table.Rows.Add( t.EmpId, t.FullName, t.Specialty?.SpecialtyName ?? "", t.Position, t.HireDate, t.PhoneNumber, t.Email, 1 );
            }

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
            txtSpecialty.Text = row.Cells["specialty_name"].Value?.ToString() ?? "";
            txtPosition.Text = row.Cells["emp_position"].Value?.ToString() ?? LanguageManager.GetString("txtPosition");
            txtPhone.Text = row.Cells["phone_number"].Value?.ToString() ?? LanguageManager.GetString("txtPhone");
            txtEmail.Text = row.Cells["email"].Value?.ToString() ?? LanguageManager.GetString("txtEmail");

            if (DateTime.TryParse(row.Cells["emp_hire_date"].Value?.ToString(), out DateTime hireDate)) dtpHireDate.Value = hireDate;
            else dtpHireDate.Value = DateTime.Today;

            if (dataGridView1.Columns.Contains("status") && int.TryParse(row.Cells["status"].Value?.ToString(), out int status))
            {
                chkActive.Checked = status == 1;
            }
            else { chkActive.Checked = false; }
        }

        private void ClearInputFields()
        {
            txtName.Text = LanguageManager.GetString("txtName");
            txtSpecialty.Text = LanguageManager.GetString("txtSpecialty");
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
