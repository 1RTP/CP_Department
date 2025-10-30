using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using SerializerLib;
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
using System.Xml.Linq;
using static КП_Кафедра.ToastForm;

namespace КП_Кафедра.Forms
{
    public partial class FormLogin : Form
    {
        private string filePath = "Data/admin.json";

        public FormLogin()
        {
            InitializeComponent();
            InitializeBoxes();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtEmail.Text = "Email";
            txtPassword.Text = "Password";

        }

        private void InitializeBoxes()
        {
            txtEmail.GotFocus += txtEmail_GotFocus;
            txtEmail.LostFocus += txtEmail_LostFocus;
            txtEmail.TextChanged += txtEmail_TextChanged;

            txtPassword.GotFocus += txtPassword_GotFocus;
            txtPassword.LostFocus += txtPassword_LostFocus;
            txtPassword.TextChanged += txtPassword_TextChanged;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            string input = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Email") return;

            var validPattern = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9@\.\-_]+$");
            if (!validPattern.IsMatch(input))
            {
                Toast.Show("ERROR", "Невірний формат електронної пошти");
                txtEmail.Text = "";
            }
        }

        private void txtEmail_GotFocus(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email") txtEmail.Text = "";
        }

        private void txtEmail_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) txtEmail.Text = "Email";
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            string input = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(input) || input == "Password") return;
        }

        private void txtPassword_GotFocus(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
            }
        }

        private void txtPassword_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.Text = "Password";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (email == "" || email == "Email" || password == "" || password == "Password")
                {
                    Toast.Show("ERROR", "Будь ласка, заповніть усі поля.");
                    return;
                }

                if (!File.Exists(filePath)) { Toast.Show("ERROR", "Файл з обліковими даними не знайдено."); return; }

                var adminData = ClassSerializeJSON1.DeserializeFromJSON<AdminCredentials>(filePath, msg => LoggerService.LogInfo(msg));

                if (adminData == null) { Toast.Show("ERROR", "Некоректний формат даних авторизації."); return; }

                if (email.Equals(adminData.Email, StringComparison.OrdinalIgnoreCase) && password == adminData.Password)
                {
                    Toast.Show("SUCCESS", "Вхід виконано успішно.");
                    FormMainMenu main = new FormMainMenu(adminData.Name, adminData.Email);
                    main.Show();
                    this.Hide();
                }
                else
                {
                    Toast.Show("ERROR", "Неправильна пошта або пароль.");
                }
            }
            catch (Exception ex)
            {
                Toast.Show("ERROR", $"Помилка авторизації: {ex.Message}");
            }
        }

        private class AdminCredentials
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
        }

       

        
    }
}

