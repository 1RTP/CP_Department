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
using КП_Кафедра.Forms;
using КП_Кафедра.Properties;
using static КП_Кафедра.ToastForm;
using System.Globalization;
using System.Threading;


namespace КП_Кафедра
{
    public partial class FormMainMenu : Form
    {
        private Form activeForm = null;

        private Boolean showPanel = false;
        private Boolean showPanel2 = false;

        public FormMainMenu()
        {
            InitializeComponent();
        }

        private void FormMainMenu_Load(object sender, EventArgs e)
        {
            try
            {
                tooglePanels();
            }
            catch (Exception ex) { LoggerService.LogError($"Помилка при завантаженні даних: {ex.Message}"); }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void showToast(string type, string message) 
        { 
            ToastForm toast = new ToastForm(type, message); 
            toast.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            showPanel2 = false;
            showPanel = !showPanel;
            tooglePanels();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showPanel = false;
            showPanel2 = !showPanel2;
            tooglePanels();
        }

        private void tooglePanels()
        {
            if (showPanel)
            {
                panel4.Height = 96;
            }
            else
            {
                panel4.Height = 0;
            }

            if (showPanel2)
            {
                panel5.Height = 64;
            }
            else
            {
                panel5.Height = 0;
            }
        }

        private void btnHead_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTables());
        }

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel6.Controls.Add(childForm);
            panel6.Tag = childForm;
            childForm.BringToFront();
            panel6.BringToFront();
            childForm.Show();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormSettings());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LanguageManager.LanguageChanged += ApplyLocalization;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            btnHead.Text = LanguageManager.GetString("btnHead");
            btnSettings.Text = LanguageManager.GetString("btnSettings");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormReport());
        }
    }
}
