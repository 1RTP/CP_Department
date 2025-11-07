namespace КП_Кафедра.Forms
{
    partial class FormTables
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
                LanguageManager.LanguageChanged -= ApplyLocalization;
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnParticipation = new КП_Кафедра.RoundButton();
            this.btnAssignments = new КП_Кафедра.RoundButton();
            this.btnTeachers = new КП_Кафедра.RoundButton();
            this.btnSubjects = new КП_Кафедра.RoundButton();
            this.btnResearches = new КП_Кафедра.RoundButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.panel1.Controls.Add(this.btnParticipation);
            this.panel1.Controls.Add(this.btnAssignments);
            this.panel1.Controls.Add(this.btnTeachers);
            this.panel1.Controls.Add(this.btnSubjects);
            this.panel1.Controls.Add(this.btnResearches);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 108);
            this.panel1.TabIndex = 12;
            // 
            // btnParticipation
            // 
            this.btnParticipation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnParticipation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnParticipation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnParticipation.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnParticipation.BorderRadius = 10;
            this.btnParticipation.BorderSize = 0;
            this.btnParticipation.FlatAppearance.BorderSize = 0;
            this.btnParticipation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParticipation.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnParticipation.ForeColor = System.Drawing.Color.White;
            this.btnParticipation.Location = new System.Drawing.Point(586, 58);
            this.btnParticipation.Name = "btnParticipation";
            this.btnParticipation.Size = new System.Drawing.Size(156, 32);
            this.btnParticipation.TabIndex = 32;
            this.btnParticipation.Text = "Участь у проєктах";
            this.btnParticipation.TextColor = System.Drawing.Color.White;
            this.btnParticipation.UseVisualStyleBackColor = false;
            this.btnParticipation.Click += new System.EventHandler(this.btnParticipation_Click);
            // 
            // btnAssignments
            // 
            this.btnAssignments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAssignments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAssignments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAssignments.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAssignments.BorderRadius = 10;
            this.btnAssignments.BorderSize = 0;
            this.btnAssignments.FlatAppearance.BorderSize = 0;
            this.btnAssignments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignments.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnAssignments.ForeColor = System.Drawing.Color.White;
            this.btnAssignments.Location = new System.Drawing.Point(748, 58);
            this.btnAssignments.Name = "btnAssignments";
            this.btnAssignments.Size = new System.Drawing.Size(112, 32);
            this.btnAssignments.TabIndex = 28;
            this.btnAssignments.Text = "Призначення";
            this.btnAssignments.TextColor = System.Drawing.Color.White;
            this.btnAssignments.UseVisualStyleBackColor = false;
            this.btnAssignments.Click += new System.EventHandler(this.btnAssignments_Click);
            // 
            // btnTeachers
            // 
            this.btnTeachers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTeachers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnTeachers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnTeachers.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnTeachers.BorderRadius = 10;
            this.btnTeachers.BorderSize = 0;
            this.btnTeachers.FlatAppearance.BorderSize = 0;
            this.btnTeachers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeachers.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnTeachers.ForeColor = System.Drawing.Color.White;
            this.btnTeachers.Location = new System.Drawing.Point(262, 58);
            this.btnTeachers.Name = "btnTeachers";
            this.btnTeachers.Size = new System.Drawing.Size(102, 32);
            this.btnTeachers.TabIndex = 29;
            this.btnTeachers.Text = "Викладачі";
            this.btnTeachers.TextColor = System.Drawing.Color.White;
            this.btnTeachers.UseVisualStyleBackColor = false;
            this.btnTeachers.Click += new System.EventHandler(this.btnTeachers_Click);
            // 
            // btnSubjects
            // 
            this.btnSubjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnSubjects.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnSubjects.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSubjects.BorderRadius = 10;
            this.btnSubjects.BorderSize = 0;
            this.btnSubjects.FlatAppearance.BorderSize = 0;
            this.btnSubjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubjects.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnSubjects.ForeColor = System.Drawing.Color.White;
            this.btnSubjects.Location = new System.Drawing.Point(370, 58);
            this.btnSubjects.Name = "btnSubjects";
            this.btnSubjects.Size = new System.Drawing.Size(102, 32);
            this.btnSubjects.TabIndex = 30;
            this.btnSubjects.Text = "Дисципліни";
            this.btnSubjects.TextColor = System.Drawing.Color.White;
            this.btnSubjects.UseVisualStyleBackColor = false;
            this.btnSubjects.Click += new System.EventHandler(this.btnSubjects_Click);
            // 
            // btnResearches
            // 
            this.btnResearches.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResearches.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnResearches.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnResearches.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnResearches.BorderRadius = 10;
            this.btnResearches.BorderSize = 0;
            this.btnResearches.FlatAppearance.BorderSize = 0;
            this.btnResearches.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResearches.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnResearches.ForeColor = System.Drawing.Color.White;
            this.btnResearches.Location = new System.Drawing.Point(478, 58);
            this.btnResearches.Name = "btnResearches";
            this.btnResearches.Size = new System.Drawing.Size(102, 32);
            this.btnResearches.TabIndex = 31;
            this.btnResearches.Text = "Проєкти";
            this.btnResearches.TextColor = System.Drawing.Color.White;
            this.btnResearches.UseVisualStyleBackColor = false;
            this.btnResearches.Click += new System.EventHandler(this.btnResearches_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::КП_Кафедра.Properties.Resources.icons8_поиск_25;
            this.pictureBox1.Location = new System.Drawing.Point(12, 58);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(45, 81);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 2);
            this.panel2.TabIndex = 14;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Arial", 8.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(45, 66);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 14);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.Text = "Пошук";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // contentPanel
            // 
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 108);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(872, 483);
            this.contentPanel.TabIndex = 13;
            // 
            // FormTables
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(872, 591);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 8.8F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(888, 600);
            this.Name = "FormTables";
            this.Text = "FormTable";
            this.Load += new System.EventHandler(this.FormTables_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private RoundButton btnAssignments;
        private RoundButton btnTeachers;
        private RoundButton btnSubjects;
        private RoundButton btnResearches;
        private RoundButton btnParticipation;
        private System.Windows.Forms.Panel contentPanel;
    }
}