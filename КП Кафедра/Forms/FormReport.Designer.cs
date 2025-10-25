namespace КП_Кафедра.Forms
{
    partial class FormReport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReport = new КП_Кафедра.RoundButton();
            this.rbAssignmentsReport = new System.Windows.Forms.RadioButton();
            this.rbResearchReport = new System.Windows.Forms.RadioButton();
            this.rbSubjectsReport = new System.Windows.Forms.RadioButton();
            this.rbTeachersReport = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.rbAssignmentsReport);
            this.panel1.Controls.Add(this.rbResearchReport);
            this.panel1.Controls.Add(this.rbSubjectsReport);
            this.panel1.Controls.Add(this.rbTeachersReport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 102);
            this.panel1.TabIndex = 14;
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnReport.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReport.BorderRadius = 10;
            this.btnReport.BorderSize = 0;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(504, 9);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(114, 32);
            this.btnReport.TabIndex = 30;
            this.btnReport.Text = "Показати звіт";
            this.btnReport.TextColor = System.Drawing.Color.White;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // rbAssignmentsReport
            // 
            this.rbAssignmentsReport.AutoSize = true;
            this.rbAssignmentsReport.ForeColor = System.Drawing.Color.White;
            this.rbAssignmentsReport.Location = new System.Drawing.Point(371, 15);
            this.rbAssignmentsReport.Name = "rbAssignmentsReport";
            this.rbAssignmentsReport.Size = new System.Drawing.Size(117, 20);
            this.rbAssignmentsReport.TabIndex = 23;
            this.rbAssignmentsReport.TabStop = true;
            this.rbAssignmentsReport.Text = "Призначення";
            this.rbAssignmentsReport.UseVisualStyleBackColor = true;
            // 
            // rbResearchReport
            // 
            this.rbResearchReport.AutoSize = true;
            this.rbResearchReport.ForeColor = System.Drawing.Color.White;
            this.rbResearchReport.Location = new System.Drawing.Point(267, 15);
            this.rbResearchReport.Name = "rbResearchReport";
            this.rbResearchReport.Size = new System.Drawing.Size(84, 20);
            this.rbResearchReport.TabIndex = 22;
            this.rbResearchReport.TabStop = true;
            this.rbResearchReport.Text = "Проєкти";
            this.rbResearchReport.UseVisualStyleBackColor = true;
            // 
            // rbSubjectsReport
            // 
            this.rbSubjectsReport.AutoSize = true;
            this.rbSubjectsReport.ForeColor = System.Drawing.Color.White;
            this.rbSubjectsReport.Location = new System.Drawing.Point(140, 15);
            this.rbSubjectsReport.Name = "rbSubjectsReport";
            this.rbSubjectsReport.Size = new System.Drawing.Size(103, 20);
            this.rbSubjectsReport.TabIndex = 21;
            this.rbSubjectsReport.TabStop = true;
            this.rbSubjectsReport.Text = "Дисципліни";
            this.rbSubjectsReport.UseVisualStyleBackColor = true;
            // 
            // rbTeachersReport
            // 
            this.rbTeachersReport.AutoSize = true;
            this.rbTeachersReport.ForeColor = System.Drawing.Color.White;
            this.rbTeachersReport.Location = new System.Drawing.Point(22, 15);
            this.rbTeachersReport.Name = "rbTeachersReport";
            this.rbTeachersReport.Size = new System.Drawing.Size(95, 20);
            this.rbTeachersReport.TabIndex = 20;
            this.rbTeachersReport.TabStop = true;
            this.rbTeachersReport.Text = "Викладачі";
            this.rbTeachersReport.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(148)))), ((int)(((byte)(152)))));
            this.panel2.Location = new System.Drawing.Point(0, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 348);
            this.panel2.TabIndex = 15;
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormReport";
            this.Text = "FormReport";
            this.Load += new System.EventHandler(this.FormReport_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbAssignmentsReport;
        private System.Windows.Forms.RadioButton rbResearchReport;
        private System.Windows.Forms.RadioButton rbSubjectsReport;
        private System.Windows.Forms.RadioButton rbTeachersReport;
        private RoundButton btnReport;
    }
}