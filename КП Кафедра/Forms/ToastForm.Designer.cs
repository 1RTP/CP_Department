namespace КП_Кафедра
{
    partial class ToastForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.IbBorder = new System.Windows.Forms.Panel();
            this.IbMessage = new System.Windows.Forms.Label();
            this.IbType = new System.Windows.Forms.Label();
            this.IbIcon = new System.Windows.Forms.PictureBox();
            this.toastTimer = new System.Windows.Forms.Timer(this.components);
            this.toastHide = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.IbIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // IbBorder
            // 
            this.IbBorder.BackColor = System.Drawing.Color.DarkGreen;
            this.IbBorder.Location = new System.Drawing.Point(-2, -2);
            this.IbBorder.Name = "IbBorder";
            this.IbBorder.Size = new System.Drawing.Size(14, 65);
            this.IbBorder.TabIndex = 0;
            // 
            // IbMessage
            // 
            this.IbMessage.AutoSize = true;
            this.IbMessage.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IbMessage.Location = new System.Drawing.Point(67, 27);
            this.IbMessage.Name = "IbMessage";
            this.IbMessage.Size = new System.Drawing.Size(85, 15);
            this.IbMessage.TabIndex = 3;
            this.IbMessage.Text = "Toast Message";
            // 
            // IbType
            // 
            this.IbType.AutoSize = true;
            this.IbType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IbType.Location = new System.Drawing.Point(67, 7);
            this.IbType.Name = "IbType";
            this.IbType.Size = new System.Drawing.Size(37, 17);
            this.IbType.TabIndex = 2;
            this.IbType.Text = "Type";
            // 
            // IbIcon
            // 
            this.IbIcon.Image = global::КП_Кафедра.Properties.Resources.icons8_галочка_64;
            this.IbIcon.Location = new System.Drawing.Point(27, 15);
            this.IbIcon.Name = "IbIcon";
            this.IbIcon.Size = new System.Drawing.Size(25, 25);
            this.IbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IbIcon.TabIndex = 1;
            this.IbIcon.TabStop = false;
            // 
            // toastTimer
            // 
            this.toastTimer.Enabled = true;
            this.toastTimer.Interval = 10;
            this.toastTimer.Tick += new System.EventHandler(this.toastTimer_Tick);
            // 
            // toastHide
            // 
            this.toastHide.Interval = 10;
            this.toastHide.Tick += new System.EventHandler(this.toastHide_Tick);
            // 
            // ToastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 59);
            this.Controls.Add(this.IbMessage);
            this.Controls.Add(this.IbType);
            this.Controls.Add(this.IbIcon);
            this.Controls.Add(this.IbBorder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToastForm";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.ToastForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.IbIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel IbBorder;
        private System.Windows.Forms.Label IbMessage;
        private System.Windows.Forms.Label IbType;
        private System.Windows.Forms.PictureBox IbIcon;
        private System.Windows.Forms.Timer toastTimer;
        private System.Windows.Forms.Timer toastHide;
    }
}