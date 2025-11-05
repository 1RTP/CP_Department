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

public static class SqliteDataReaderExtensions
{
    public static int GetInt32OrDefault(this SqliteDataReader reader, string columnName)
    {
        int ordinal = reader.GetOrdinal(columnName);
        return !reader.IsDBNull(ordinal) ? reader.GetInt32(ordinal) : 0;
    }
}

namespace КП_Кафедра.Forms
{
    public partial class FormParticipation : Form
    {
        private bool isLoading = false;
        private DataTable originalTable;
        private readonly string dbPath;
        public static FormParticipation Instance { get; private set; }

        public FormParticipation()
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

        private void FormParticipation_Load(object sender, EventArgs e)
        {
            try
            {
                Instance = this;
                isLoading = true;
                LoadParticipation();
                isLoading = false;
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при завантаженні участі у дослідженнях: {ex.Message}");
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

                var columnsToSearch = new[] { "emp_full_name", "research_name", "specialty_name" }; // колонки для пошуку

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


        private void LoadParticipation()
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string query = @"SELECT 
                            pir.participation_id,
                            t.emp_id,
                            t.emp_full_name,
                            r.research_id,
                            r.research_name,
                            sp.specialty_id,
                            sp.specialty_name,
                            r.start_date,
                            r.end_date
                        FROM participation_in_research pir
                        JOIN teacher t ON pir.emp_id = t.emp_id
                        JOIN research r ON pir.research_id = r.research_id
                        LEFT JOIN specialty sp ON r.specialty_id = sp.specialty_id;";

                    DataTable table = new DataTable();
                    table.Columns.Add("participation_id", typeof(int));
                    table.Columns.Add("emp_id", typeof(int));
                    table.Columns.Add("emp_full_name", typeof(string));
                    table.Columns.Add("research_id", typeof(int));
                    table.Columns.Add("research_name", typeof(string));
                    table.Columns.Add("specialty_id", typeof(int));
                    table.Columns.Add("specialty_name", typeof(string));
                    table.Columns.Add("start_date", typeof(string));
                    table.Columns.Add("end_date", typeof(string));

                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            table.Rows.Add(
                                reader.GetInt32OrDefault("participation_id"),
                                reader.GetInt32OrDefault("emp_id"),
                                reader["emp_full_name"]?.ToString() ?? "",
                                reader.GetInt32OrDefault("research_id"),
                                reader["research_name"]?.ToString() ?? "",
                                reader.GetInt32OrDefault("specialty_id"),
                                reader["specialty_name"]?.ToString() ?? "",
                                reader["start_date"]?.ToString() ?? "",
                                reader["end_date"]?.ToString() ?? ""
                            );
                        }
                    }

                    originalTable = table.Copy();
                    dataGridView1.DataSource = table;
                    ApplyColumnLocalization();

                    string[] hiddenCols = { "emp_id", "research_id", "specialty_id" };
                    foreach (var col in hiddenCols)
                    {
                        if (dataGridView1.Columns.Contains(col)) dataGridView1.Columns[col].Visible = false;
                    }
                }

                DataService.Participations = GetParticipationFromGrid();
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", "Помилка при завантаженні участі у дослідженнях");
                LoggerService.LogError($"Помилка при завантаженні участі у дослідженнях (LoadParticipation): {ex.Message}");
            }
        }


        public List<Participation> GetParticipationFromGrid()
        {
            var participations = new List<Participation>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                participations.Add(new Participation
                {
                    ParticipationId = Convert.ToInt32(row.Cells["participation_id"].Value),
                    Teacher = new Teacher
                    {
                        EmpId = Convert.ToInt32(row.Cells["emp_id"].Value),
                        FullName = row.Cells["emp_full_name"].Value?.ToString() ?? ""
                    },
                    Project = new Research
                    {
                        ResearchId = Convert.ToInt32(row.Cells["research_id"].Value),
                        ResearchName = row.Cells["research_name"].Value?.ToString() ?? "",
                        Specialty = new Specialty
                        {
                            SpecialtyId = Convert.ToInt32(row.Cells["specialty_id"].Value),
                            SpecialtyName = row.Cells["specialty_name"].Value?.ToString() ?? ""
                        },
                        StartDate = DateTime.TryParse(row.Cells["start_date"].Value?.ToString(), out DateTime startDate) ? startDate : DateTime.MinValue,
                        EndDate = DateTime.TryParse(row.Cells["end_date"].Value?.ToString(), out DateTime endDate) ? endDate : DateTime.MinValue
                    }
                });
            }

            return participations;
        }

        private void ApplyColumnLocalization()
        {
            var columnMap = new Dictionary<string, string>
            {
                { "participation_id", "column_participation_id" },
                { "emp_full_name", "column_emp_full_name" },
                { "research_name", "column_research_name" },
                { "start_date", "column_start_date" },
                { "specialty_name", "column_specialty_name" },
                { "end_date", "column_end_date" }
            };

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (columnMap.TryGetValue(col.Name, out string langKey))
                {
                    col.HeaderText = LanguageManager.GetString(langKey);
                }
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
            btnAddParticipation.Text = LanguageManager.GetString("btnAddParticipation");
            btnUpdateParticipation.Text = LanguageManager.GetString("btnUpdateParticipation");
            btnDeleteParticipation.Text = LanguageManager.GetString("btnDeleteParticipation");
            //btnClear.Text = LanguageManager.GetString("btnClear");

            txtName.Text = LanguageManager.GetString("txtName");
            txtProjectName.Text = LanguageManager.GetString("txtProjectName");

            ApplyColumnLocalization();
        }

        private void InitializeBoxes()
        {
            txtName.GotFocus += txtBox_GotFocus;
            txtName.LostFocus += txtBox_LostFocus;

            txtProjectName.GotFocus += txtBox_GotFocus;
            txtProjectName.LostFocus += txtBox_LostFocus;

            txtName.TextChanged += txtName_TextChanged;
            txtProjectName.TextChanged += txtProjectName_TextChanged;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string input = txtName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "ПІБ" || input == "Teacher name") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ'\-\s]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат ПІБ");
                txtName.Text = "";
            }
        }

        private void txtProjectName_TextChanged(object sender, EventArgs e)
        {
            string input = txtProjectName.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Назва проєкту" || input == "Project name") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-zА-ЯІЇЄҐа-яіїєґ0-9'\-\s\(\),\.]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат назви проєкту");
                txtProjectName.Text = "";
            }
        }

        private void txtBox_GotFocus(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (box.Text == LanguageManager.GetString(box.Name)) box.Text = "";
        }

        private void txtBox_LostFocus(object sender, EventArgs e)
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrWhiteSpace(box.Text)) box.Text = LanguageManager.GetString(box.Name);
        }

        private void btnAddParticipation_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    int empId = GetTeacherIdByName(connection, txtName.Text.Trim());
                    int researchId = GetResearchIdByName(connection, txtProjectName.Text.Trim());

                    if (empId == 0 || researchId == 0)
                    {
                        Toast.Show("ERROR", "Не знайдено викладача або дослідження");
                        return;
                    }

                    object specialtyParam = DBNull.Value;

                    string query = @"INSERT INTO participation_in_research (emp_id, research_id, specialty_id) VALUES (@emp, @res, @spec)";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@emp", empId);
                        cmd.Parameters.AddWithValue("@res", researchId);
                        cmd.Parameters.AddWithValue("@spec", specialtyParam);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadParticipation();
                Toast.Show("SUCCESS", "Участь успішно додано!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка при додаванні: {ex.Message}");
                LoggerService.LogError($"Помилка при додаванні участі: {ex.Message}");
            }
        }

        private void btnUpdateParticipation_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["participation_id"].Value);

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    int empId = GetTeacherIdByName(connection, txtName.Text.Trim());
                    int researchId = GetResearchIdByName(connection, txtProjectName.Text.Trim());

                    if (empId == 0 || researchId == 0)
                    {
                        Toast.Show("ERROR", "Не знайдено викладача або дослідження");
                        return;
                    }

                    object specialtyParam = DBNull.Value;

                    string query = @"UPDATE participation_in_research SET emp_id=@emp, research_id=@res, specialty_id=@spec WHERE participation_id=@id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@emp", empId);
                        cmd.Parameters.AddWithValue("@res", researchId);
                        cmd.Parameters.AddWithValue("@spec", specialtyParam);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadParticipation();
                Toast.Show("SUCCESS", "Участь оновлено успішно!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка при оновленні: {ex.Message}");
                LoggerService.LogError($"Помилка при оновленні участі: {ex.Message}");
            }
        }

        private void btnDeleteParticipation_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["participation_id"].Value);

            var result = MessageBox.Show("Ви впевнені, що хочете видалити запис участі?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    string query = "DELETE FROM participation_in_research WHERE participation_id=@id";
                    using (var cmd = new SqliteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadParticipation();
                Toast.Show("SUCCESS", "Участь видалено!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка при видаленні: {ex.Message}");
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

            txtName.Text = dataGridView1.CurrentRow.Cells["emp_full_name"].Value?.ToString();
            txtProjectName.Text = dataGridView1.CurrentRow.Cells["research_name"].Value?.ToString();
            DateTime.TryParse(dataGridView1.CurrentRow.Cells["start_date"].Value?.ToString(), out DateTime start);
            DateTime.TryParse(dataGridView1.CurrentRow.Cells["end_date"].Value?.ToString(), out DateTime end);
         
        }

        private void ClearInputFields()
        {
            txtName.Text = LanguageManager.GetString("txtName");
            txtProjectName.Text = LanguageManager.GetString("txtProjectName");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            isLoading = true;
            ClearInputFields();
            dataGridView1.ClearSelection();
            isLoading = false;
        }

        private int GetTeacherIdByName(SqliteConnection connection, string fullName)
        {
            string query = "SELECT emp_id FROM teacher WHERE emp_full_name = @name";
            using (var cmd = new SqliteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", fullName);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private int GetResearchIdByName(SqliteConnection connection, string name)
        {
            string query = "SELECT research_id FROM research WHERE research_name = @name";
            using (var cmd = new SqliteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }


    }
}
