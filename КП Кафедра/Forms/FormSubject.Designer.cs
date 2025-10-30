namespace КП_Кафедра.Forms
{
    partial class FormSubject
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnClear = new КП_Кафедра.RoundButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDeleteSubject = new КП_Кафедра.RoundButton();
            this.btnUpdateSubject = new КП_Кафедра.RoundButton();
            this.btnAddSubject = new КП_Кафедра.RoundButton();
            this.txtTotalHours = new System.Windows.Forms.TextBox();
            this.txtSemester = new System.Windows.Forms.TextBox();
            this.txtSubjectName = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.panel10.Controls.Add(this.btnClear);
            this.panel10.Controls.Add(this.panel5);
            this.panel10.Controls.Add(this.panel4);
            this.panel10.Controls.Add(this.panel3);
            this.panel10.Controls.Add(this.btnDeleteSubject);
            this.panel10.Controls.Add(this.btnUpdateSubject);
            this.panel10.Controls.Add(this.btnAddSubject);
            this.panel10.Controls.Add(this.txtTotalHours);
            this.panel10.Controls.Add(this.txtSemester);
            this.panel10.Controls.Add(this.txtSubjectName);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(788, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(332, 734);
            this.panel10.TabIndex = 17;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnClear.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnClear.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnClear.BorderRadius = 10;
            this.btnClear.BorderSize = 0;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Image = global::КП_Кафедра.Properties.Resources.icons8_мусор_20;
            this.btnClear.Location = new System.Drawing.Point(279, 152);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(41, 40);
            this.btnClear.TabIndex = 30;
            this.btnClear.TextColor = System.Drawing.Color.White;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(15, 137);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(306, 2);
            this.panel5.TabIndex = 26;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(16, 85);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(306, 2);
            this.panel4.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(15, 34);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(306, 2);
            this.panel3.TabIndex = 24;
            // 
            // btnDeleteSubject
            // 
            this.btnDeleteSubject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteSubject.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteSubject.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDeleteSubject.BorderRadius = 10;
            this.btnDeleteSubject.BorderSize = 0;
            this.btnDeleteSubject.FlatAppearance.BorderSize = 0;
            this.btnDeleteSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSubject.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnDeleteSubject.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSubject.Location = new System.Drawing.Point(212, 199);
            this.btnDeleteSubject.Name = "btnDeleteSubject";
            this.btnDeleteSubject.Size = new System.Drawing.Size(110, 32);
            this.btnDeleteSubject.TabIndex = 23;
            this.btnDeleteSubject.Text = "Видалити";
            this.btnDeleteSubject.TextColor = System.Drawing.Color.White;
            this.btnDeleteSubject.UseVisualStyleBackColor = false;
            this.btnDeleteSubject.Click += new System.EventHandler(this.btnDeleteSubject_Click);
            // 
            // btnUpdateSubject
            // 
            this.btnUpdateSubject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateSubject.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateSubject.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateSubject.BorderRadius = 10;
            this.btnUpdateSubject.BorderSize = 1;
            this.btnUpdateSubject.FlatAppearance.BorderSize = 0;
            this.btnUpdateSubject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateSubject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateSubject.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnUpdateSubject.ForeColor = System.Drawing.Color.White;
            this.btnUpdateSubject.Location = new System.Drawing.Point(112, 199);
            this.btnUpdateSubject.Name = "btnUpdateSubject";
            this.btnUpdateSubject.Size = new System.Drawing.Size(93, 32);
            this.btnUpdateSubject.TabIndex = 21;
            this.btnUpdateSubject.Text = "Оновити";
            this.btnUpdateSubject.TextColor = System.Drawing.Color.White;
            this.btnUpdateSubject.UseVisualStyleBackColor = false;
            this.btnUpdateSubject.Click += new System.EventHandler(this.btnUpdateSubject_Click);
            // 
            // btnAddSubject
            // 
            this.btnAddSubject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddSubject.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddSubject.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddSubject.BorderRadius = 10;
            this.btnAddSubject.BorderSize = 1;
            this.btnAddSubject.FlatAppearance.BorderSize = 0;
            this.btnAddSubject.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddSubject.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddSubject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSubject.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnAddSubject.ForeColor = System.Drawing.Color.White;
            this.btnAddSubject.Location = new System.Drawing.Point(15, 199);
            this.btnAddSubject.Name = "btnAddSubject";
            this.btnAddSubject.Size = new System.Drawing.Size(90, 32);
            this.btnAddSubject.TabIndex = 20;
            this.btnAddSubject.Text = "Додати";
            this.btnAddSubject.TextColor = System.Drawing.Color.White;
            this.btnAddSubject.UseVisualStyleBackColor = false;
            this.btnAddSubject.Click += new System.EventHandler(this.btnAddSubject_Click);
            // 
            // txtTotalHours
            // 
            this.txtTotalHours.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtTotalHours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotalHours.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtTotalHours.ForeColor = System.Drawing.Color.White;
            this.txtTotalHours.Location = new System.Drawing.Point(15, 120);
            this.txtTotalHours.Name = "txtTotalHours";
            this.txtTotalHours.Size = new System.Drawing.Size(306, 17);
            this.txtTotalHours.TabIndex = 3;
            this.txtTotalHours.Text = "Загальна кількість годин";
            this.txtTotalHours.TextChanged += new System.EventHandler(this.txtTotalHours_TextChanged);
            // 
            // txtSemester
            // 
            this.txtSemester.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtSemester.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSemester.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtSemester.ForeColor = System.Drawing.Color.White;
            this.txtSemester.Location = new System.Drawing.Point(16, 68);
            this.txtSemester.Name = "txtSemester";
            this.txtSemester.Size = new System.Drawing.Size(306, 17);
            this.txtSemester.TabIndex = 1;
            this.txtSemester.Text = "Семестр";
            this.txtSemester.TextChanged += new System.EventHandler(this.txtSemester_TextChanged);
            // 
            // txtSubjectName
            // 
            this.txtSubjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtSubjectName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubjectName.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtSubjectName.ForeColor = System.Drawing.Color.White;
            this.txtSubjectName.Location = new System.Drawing.Point(15, 17);
            this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(306, 17);
            this.txtSubjectName.TabIndex = 0;
            this.txtSubjectName.Text = "Назва дисципліни";
            this.txtSubjectName.TextChanged += new System.EventHandler(this.txtSubjectName_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 8.8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(148)))), ((int)(((byte)(152)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial", 8.8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(781, 734);
            this.dataGridView1.TabIndex = 16;
            // 
            // FormSubject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(1120, 734);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Arial", 8.8F);
            this.Name = "FormSubject";
            this.Text = "FormSubject";
            this.Load += new System.EventHandler(this.FormSubject_Load);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private RoundButton btnDeleteSubject;
        private RoundButton btnUpdateSubject;
        private RoundButton btnAddSubject;
        private System.Windows.Forms.TextBox txtTotalHours;
        private System.Windows.Forms.TextBox txtSemester;
        private System.Windows.Forms.TextBox txtSubjectName;
        internal System.Windows.Forms.DataGridView dataGridView1;
        private RoundButton btnClear;
    }
}