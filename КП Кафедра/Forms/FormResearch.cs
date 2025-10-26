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
    public partial class FormResearch : Form
    {
        private bool isLoading = false;
        private DataTable originalTable;
        private string dbPath = "Data/department.db";

        public FormResearch()
        {
            InitializeComponent();
            InitializeBoxes();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void FormResearch_Load(object sender, EventArgs e)
        {
            try
            {
                isLoading = true;
                LoadResearch();
                isLoading = false;
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Помилка при завантаженні досліджень: {ex.Message}");
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

                var columnsToSearch = new[] { "research_name" }; // колонки для пошуку

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


        private void LoadResearch()
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"SELECT research_id,
                                    research_name,
                                    start_date,
                                    end_date
                             FROM research";

                    using (var command = new SQLiteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        originalTable = table.Copy();
                        dataGridView1.DataSource = table;
                        ApplyColumnLocalization();
                    }
                }
                DataService.Researches = GetResearchFromGrid();
            }
            catch (Exception ex) { Toast.Show("ERROR", $"Помилка при завантаженні досліджень: {ex.Message}"); }
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            btnAddResearch.Text = LanguageManager.GetString("btnAddResearch");
            btnDeleteResearch.Text = LanguageManager.GetString("btnDeleteResearch");
            btnUpdateResearch.Text = LanguageManager.GetString("btnUpdateResearch");
            txtProjectName.Text = LanguageManager.GetString("txtProjectName");
            //btnClear.Text = LanguageManager.GetString("btnClear");

            ApplyColumnLocalization();
        }

        private void ApplyColumnLocalization()
        {
            var columnMap = new Dictionary<string, string>
            {
                { "research_id", "column_research_id" },
                { "research_name", "column_research_name" },
                { "start_date", "column_start_date" },
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

        private void InitializeBoxes()
        {
            txtProjectName.GotFocus += txtProjectName_GotFocus;
            txtProjectName.LostFocus += txtProjectName_LostFocus;
            txtProjectName.TextChanged += txtProjectName_TextChanged;
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

        private void txtProjectName_GotFocus(object sender, EventArgs e)
        {
            if (txtProjectName.Text == "Назва проєкту" || txtProjectName.Text == "Project name")
            {
                txtProjectName.Text = "";
            }
        }

        private void txtProjectName_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                txtProjectName.Text = LanguageManager.GetString("txtProjectName");
            }
        }

        private void btnAddResearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"INSERT INTO research (research_name, start_date, end_date)
                                     VALUES (@name, @start, @end)";
                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtProjectName.Text.Trim());
                        cmd.Parameters.AddWithValue("@start", dtpStartDate.Value);
                        cmd.Parameters.AddWithValue("@end", dtpEndDate.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadResearch();
                Toast.Show("SUCCESS", "Дослідження додано успішно!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка при додаванні: {ex.Message}");
            }
        }

        private void btnUpdateResearch_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["research_id"].Value);

            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = @"UPDATE research
                                     SET research_name=@name, start_date=@start, end_date=@end
                                     WHERE research_id=@id";
                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", txtProjectName.Text.Trim());
                        cmd.Parameters.AddWithValue("@start", dtpStartDate.Value);
                        cmd.Parameters.AddWithValue("@end", dtpEndDate.Value);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadResearch();
                Toast.Show("SUCCESS", "Дослідження оновлено успішно!");
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка при оновленні: {ex.Message}");
            }
        }

        private void btnDeleteResearch_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["research_id"].Value);

            var result = MessageBox.Show("Ви впевнені, що хочете видалити це дослідження?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    string query = "DELETE FROM research WHERE research_id=@id";
                    using (var cmd = new SQLiteCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadResearch();
                Toast.Show("SUCCESS", "Дослідження видалено!");
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

            txtProjectName.Text = dataGridView1.CurrentRow.Cells["research_name"].Value?.ToString();
            DateTime.TryParse(dataGridView1.CurrentRow.Cells["start_date"].Value?.ToString(), out DateTime start);
            DateTime.TryParse(dataGridView1.CurrentRow.Cells["end_date"].Value?.ToString(), out DateTime end);
            dtpStartDate.Value = start == DateTime.MinValue ? DateTime.Now : start;
            dtpEndDate.Value = end == DateTime.MinValue ? DateTime.Now : end;
        }

        private void ClearInputFields()
        {
            txtProjectName.Text = LanguageManager.GetString("txtProjectName");
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
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
