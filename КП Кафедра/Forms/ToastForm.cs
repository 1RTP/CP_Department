using NLog.Targets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КП_Кафедра
{
    public partial class ToastForm : Form
    {
        int toastX, toastY;
        int targetY;
        bool waiting = false;
        public ToastForm(string type, string message)
        {
            InitializeComponent();
            IbType.Text = type;
            IbMessage.Text = message;
            switch (type) {
                case "SUCCESS":
                    IbBorder.BackColor = Color.FromArgb(57, 155, 53);
                    IbIcon.Image = Properties.Resources.icons8_галочка_64;
                    break;
                case "ERROR":
                    IbBorder.BackColor = Color.FromArgb(227, 50, 45);
                    IbIcon.Image = Properties.Resources.icons8_отмена_48;
                    break;
                case "INFO":
                    IbBorder.BackColor = Color.FromArgb(18, 136, 191);
                    IbIcon.Image = Properties.Resources.icons8_информация_48;
                    break;
                case "WARNING":
                    IbBorder.BackColor = Color.FromArgb(245, 171, 35);
                    IbIcon.Image = Properties.Resources.img_icons8;
                    break;
            }
            Position();
        }

        public static class Toast
        {
            public static void Show(string type, string message)
            {
                ToastForm toast = new ToastForm(type, message);
                toast.Show();
            }
        }

        private void ToastForm_Load(object sender, EventArgs e)
        {
            toastTimer.Interval = 15; 
            toastTimer.Start();
        }

        private void toastTimer_Tick(object sender, EventArgs e)
        {
            toastY -= 10;
            this.Location = new Point(toastX, toastY);
            if (toastY <= targetY) 
            {
                toastTimer.Stop();

                if (!waiting)
                {
                    waiting = true;
                    Timer delayTimer = new Timer();
                    delayTimer.Interval = 2500;
                    delayTimer.Tick += (s, ev) =>
                    {
                        delayTimer.Stop();
                        toastHide.Start();
                    };
                    delayTimer.Start();
                }
            }
        }

        int y = 100;
        private void toastHide_Tick(object sender, EventArgs e)
        {
            y--;
            toastY += 10;
            this.Location = new Point(toastX, toastY);
            if (toastY >= Screen.PrimaryScreen.WorkingArea.Height + this.Height)
            {
                toastHide.Stop();
                y = 100;
                this.Close();
            }
        }

        private void Position() 
        { 
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            int margin = 10; // відступ від краю екрана
            toastX = ScreenWidth - this.Width - margin;
            toastY = ScreenHeight + this.Height;
            targetY = ScreenHeight - this.Height - margin;

            this.Location = new Point(toastX, toastY);
        }


    }
}
