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
            this.btnConvertToPdf = new КП_Кафедра.RoundButton();
            this.btnExportToWord = new КП_Кафедра.RoundButton();
            this.btnImportFromWord = new КП_Кафедра.RoundButton();
            this.btnOpenWordReport = new КП_Кафедра.RoundButton();
            this.btnImportFromExcel = new КП_Кафедра.RoundButton();
            this.btnExportToExcel = new КП_Кафедра.RoundButton();
            this.btnOpenExcelReport = new КП_Кафедра.RoundButton();
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
            this.panel1.Controls.Add(this.btnConvertToPdf);
            this.panel1.Controls.Add(this.btnExportToWord);
            this.panel1.Controls.Add(this.btnImportFromWord);
            this.panel1.Controls.Add(this.btnOpenWordReport);
            this.panel1.Controls.Add(this.btnImportFromExcel);
            this.panel1.Controls.Add(this.btnExportToExcel);
            this.panel1.Controls.Add(this.btnOpenExcelReport);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.rbAssignmentsReport);
            this.panel1.Controls.Add(this.rbResearchReport);
            this.panel1.Controls.Add(this.rbSubjectsReport);
            this.panel1.Controls.Add(this.rbTeachersReport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(928, 128);
            this.panel1.TabIndex = 14;
            // 
            // btnConvertToPdf
            // 
            this.btnConvertToPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvertToPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnConvertToPdf.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnConvertToPdf.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnConvertToPdf.BorderRadius = 10;
            this.btnConvertToPdf.BorderSize = 0;
            this.btnConvertToPdf.FlatAppearance.BorderSize = 0;
            this.btnConvertToPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvertToPdf.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnConvertToPdf.ForeColor = System.Drawing.Color.White;
            this.btnConvertToPdf.Location = new System.Drawing.Point(757, 90);
            this.btnConvertToPdf.Name = "btnConvertToPdf";
            this.btnConvertToPdf.Size = new System.Drawing.Size(159, 32);
            this.btnConvertToPdf.TabIndex = 37;
            this.btnConvertToPdf.Text = "Конвертувати Word у PDF";
            this.btnConvertToPdf.TextColor = System.Drawing.Color.White;
            this.btnConvertToPdf.UseVisualStyleBackColor = false;
            this.btnConvertToPdf.Click += new System.EventHandler(this.btnConvertToPdf_Click);
            // 
            // btnExportToWord
            // 
            this.btnExportToWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnExportToWord.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnExportToWord.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnExportToWord.BorderRadius = 10;
            this.btnExportToWord.BorderSize = 0;
            this.btnExportToWord.FlatAppearance.BorderSize = 0;
            this.btnExportToWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToWord.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnExportToWord.ForeColor = System.Drawing.Color.White;
            this.btnExportToWord.Location = new System.Drawing.Point(12, 90);
            this.btnExportToWord.Name = "btnExportToWord";
            this.btnExportToWord.Size = new System.Drawing.Size(131, 32);
            this.btnExportToWord.TabIndex = 36;
            this.btnExportToWord.Text = "Зберегти у Word";
            this.btnExportToWord.TextColor = System.Drawing.Color.White;
            this.btnExportToWord.UseVisualStyleBackColor = false;
            this.btnExportToWord.Click += new System.EventHandler(this.btnExportToWord_Click);
            // 
            // btnImportFromWord
            // 
            this.btnImportFromWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnImportFromWord.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnImportFromWord.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnImportFromWord.BorderRadius = 10;
            this.btnImportFromWord.BorderSize = 0;
            this.btnImportFromWord.FlatAppearance.BorderSize = 0;
            this.btnImportFromWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromWord.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnImportFromWord.ForeColor = System.Drawing.Color.White;
            this.btnImportFromWord.Location = new System.Drawing.Point(149, 90);
            this.btnImportFromWord.Name = "btnImportFromWord";
            this.btnImportFromWord.Size = new System.Drawing.Size(131, 32);
            this.btnImportFromWord.TabIndex = 35;
            this.btnImportFromWord.Text = "Завантажити з Word";
            this.btnImportFromWord.TextColor = System.Drawing.Color.White;
            this.btnImportFromWord.UseVisualStyleBackColor = false;
            this.btnImportFromWord.Click += new System.EventHandler(this.btnImportFromWord_Click);
            // 
            // btnOpenWordReport
            // 
            this.btnOpenWordReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenWordReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnOpenWordReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnOpenWordReport.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnOpenWordReport.BorderRadius = 10;
            this.btnOpenWordReport.BorderSize = 0;
            this.btnOpenWordReport.FlatAppearance.BorderSize = 0;
            this.btnOpenWordReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenWordReport.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnOpenWordReport.ForeColor = System.Drawing.Color.White;
            this.btnOpenWordReport.Location = new System.Drawing.Point(785, 9);
            this.btnOpenWordReport.Name = "btnOpenWordReport";
            this.btnOpenWordReport.Size = new System.Drawing.Size(131, 32);
            this.btnOpenWordReport.TabIndex = 34;
            this.btnOpenWordReport.Text = "Відкрити Word-звіт";
            this.btnOpenWordReport.TextColor = System.Drawing.Color.White;
            this.btnOpenWordReport.UseVisualStyleBackColor = false;
            this.btnOpenWordReport.Click += new System.EventHandler(this.btnOpenWordReport_Click);
            // 
            // btnImportFromExcel
            // 
            this.btnImportFromExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnImportFromExcel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnImportFromExcel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnImportFromExcel.BorderRadius = 10;
            this.btnImportFromExcel.BorderSize = 0;
            this.btnImportFromExcel.FlatAppearance.BorderSize = 0;
            this.btnImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportFromExcel.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnImportFromExcel.ForeColor = System.Drawing.Color.White;
            this.btnImportFromExcel.Location = new System.Drawing.Point(149, 52);
            this.btnImportFromExcel.Name = "btnImportFromExcel";
            this.btnImportFromExcel.Size = new System.Drawing.Size(131, 32);
            this.btnImportFromExcel.TabIndex = 33;
            this.btnImportFromExcel.Text = "Завантажити з Excel";
            this.btnImportFromExcel.TextColor = System.Drawing.Color.White;
            this.btnImportFromExcel.UseVisualStyleBackColor = false;
            this.btnImportFromExcel.Click += new System.EventHandler(this.btnImportFromExcel_Click);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnExportToExcel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnExportToExcel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnExportToExcel.BorderRadius = 10;
            this.btnExportToExcel.BorderSize = 0;
            this.btnExportToExcel.FlatAppearance.BorderSize = 0;
            this.btnExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportToExcel.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnExportToExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportToExcel.Location = new System.Drawing.Point(12, 52);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(131, 32);
            this.btnExportToExcel.TabIndex = 32;
            this.btnExportToExcel.Text = "Зберегти у Excel";
            this.btnExportToExcel.TextColor = System.Drawing.Color.White;
            this.btnExportToExcel.UseVisualStyleBackColor = false;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnOpenExcelReport
            // 
            this.btnOpenExcelReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenExcelReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnOpenExcelReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnOpenExcelReport.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnOpenExcelReport.BorderRadius = 10;
            this.btnOpenExcelReport.BorderSize = 0;
            this.btnOpenExcelReport.FlatAppearance.BorderSize = 0;
            this.btnOpenExcelReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenExcelReport.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnOpenExcelReport.ForeColor = System.Drawing.Color.White;
            this.btnOpenExcelReport.Location = new System.Drawing.Point(648, 9);
            this.btnOpenExcelReport.Name = "btnOpenExcelReport";
            this.btnOpenExcelReport.Size = new System.Drawing.Size(131, 32);
            this.btnOpenExcelReport.TabIndex = 31;
            this.btnOpenExcelReport.Text = "Відкрити Excel-звіт";
            this.btnOpenExcelReport.TextColor = System.Drawing.Color.White;
            this.btnOpenExcelReport.UseVisualStyleBackColor = false;
            this.btnOpenExcelReport.Click += new System.EventHandler(this.btnOpenExcelReport_Click);
            // 
            // btnReport
            // 
            this.btnReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnReport.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReport.BorderRadius = 10;
            this.btnReport.BorderSize = 0;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(528, 9);
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
            this.rbAssignmentsReport.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbAssignmentsReport.ForeColor = System.Drawing.Color.White;
            this.rbAssignmentsReport.Location = new System.Drawing.Point(276, 16);
            this.rbAssignmentsReport.Name = "rbAssignmentsReport";
            this.rbAssignmentsReport.Size = new System.Drawing.Size(102, 19);
            this.rbAssignmentsReport.TabIndex = 23;
            this.rbAssignmentsReport.TabStop = true;
            this.rbAssignmentsReport.Text = "Призначення";
            this.rbAssignmentsReport.UseVisualStyleBackColor = true;
            // 
            // rbResearchReport
            // 
            this.rbResearchReport.AutoSize = true;
            this.rbResearchReport.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbResearchReport.ForeColor = System.Drawing.Color.White;
            this.rbResearchReport.Location = new System.Drawing.Point(198, 16);
            this.rbResearchReport.Name = "rbResearchReport";
            this.rbResearchReport.Size = new System.Drawing.Size(72, 19);
            this.rbResearchReport.TabIndex = 22;
            this.rbResearchReport.TabStop = true;
            this.rbResearchReport.Text = "Проєкти";
            this.rbResearchReport.UseVisualStyleBackColor = true;
            // 
            // rbSubjectsReport
            // 
            this.rbSubjectsReport.AutoSize = true;
            this.rbSubjectsReport.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbSubjectsReport.ForeColor = System.Drawing.Color.White;
            this.rbSubjectsReport.Location = new System.Drawing.Point(101, 16);
            this.rbSubjectsReport.Name = "rbSubjectsReport";
            this.rbSubjectsReport.Size = new System.Drawing.Size(91, 19);
            this.rbSubjectsReport.TabIndex = 21;
            this.rbSubjectsReport.TabStop = true;
            this.rbSubjectsReport.Text = "Дисципліни";
            this.rbSubjectsReport.UseVisualStyleBackColor = true;
            // 
            // rbTeachersReport
            // 
            this.rbTeachersReport.AutoSize = true;
            this.rbTeachersReport.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbTeachersReport.ForeColor = System.Drawing.Color.White;
            this.rbTeachersReport.Location = new System.Drawing.Point(12, 16);
            this.rbTeachersReport.Name = "rbTeachersReport";
            this.rbTeachersReport.Size = new System.Drawing.Size(83, 19);
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
            this.panel2.Location = new System.Drawing.Point(0, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(928, 350);
            this.panel2.TabIndex = 15;
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 478);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 8.8F);
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
        private RoundButton btnImportFromExcel;
        private RoundButton btnExportToExcel;
        private RoundButton btnOpenExcelReport;
        private RoundButton btnOpenWordReport;
        private RoundButton btnExportToWord;
        private RoundButton btnImportFromWord;
        private RoundButton btnConvertToPdf;
    }
}