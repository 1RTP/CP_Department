namespace КП_Кафедра.Forms
{
    partial class FormAssignment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btnClear = new КП_Кафедра.RoundButton();
            this.btnDeleteAssignment = new КП_Кафедра.RoundButton();
            this.btnUpdateAssignment = new КП_Кафедра.RoundButton();
            this.btnAddAssignment = new КП_Кафедра.RoundButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLessonType = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtHoursTaught = new System.Windows.Forms.TextBox();
            this.txtPlanHours = new System.Windows.Forms.TextBox();
            this.txtSubjectName = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.panel10.Controls.Add(this.btnClear);
            this.panel10.Controls.Add(this.btnDeleteAssignment);
            this.panel10.Controls.Add(this.btnUpdateAssignment);
            this.panel10.Controls.Add(this.btnAddAssignment);
            this.panel10.Controls.Add(this.panel1);
            this.panel10.Controls.Add(this.txtLessonType);
            this.panel10.Controls.Add(this.panel6);
            this.panel10.Controls.Add(this.panel5);
            this.panel10.Controls.Add(this.panel4);
            this.panel10.Controls.Add(this.panel3);
            this.panel10.Controls.Add(this.txtHoursTaught);
            this.panel10.Controls.Add(this.txtPlanHours);
            this.panel10.Controls.Add(this.txtSubjectName);
            this.panel10.Controls.Add(this.txtName);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(821, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(332, 647);
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
            this.btnClear.Location = new System.Drawing.Point(279, 227);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(41, 38);
            this.btnClear.TabIndex = 38;
            this.btnClear.TextColor = System.Drawing.Color.White;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDeleteAssignment
            // 
            this.btnDeleteAssignment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteAssignment.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteAssignment.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDeleteAssignment.BorderRadius = 10;
            this.btnDeleteAssignment.BorderSize = 0;
            this.btnDeleteAssignment.FlatAppearance.BorderSize = 0;
            this.btnDeleteAssignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteAssignment.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnDeleteAssignment.ForeColor = System.Drawing.Color.White;
            this.btnDeleteAssignment.Location = new System.Drawing.Point(211, 271);
            this.btnDeleteAssignment.Name = "btnDeleteAssignment";
            this.btnDeleteAssignment.Size = new System.Drawing.Size(110, 32);
            this.btnDeleteAssignment.TabIndex = 37;
            this.btnDeleteAssignment.Text = "Видалити";
            this.btnDeleteAssignment.TextColor = System.Drawing.Color.White;
            this.btnDeleteAssignment.UseVisualStyleBackColor = false;
            this.btnDeleteAssignment.Click += new System.EventHandler(this.btnDeleteAssignment_Click);
            // 
            // btnUpdateAssignment
            // 
            this.btnUpdateAssignment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateAssignment.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateAssignment.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateAssignment.BorderRadius = 10;
            this.btnUpdateAssignment.BorderSize = 1;
            this.btnUpdateAssignment.FlatAppearance.BorderSize = 0;
            this.btnUpdateAssignment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateAssignment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateAssignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateAssignment.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnUpdateAssignment.ForeColor = System.Drawing.Color.White;
            this.btnUpdateAssignment.Location = new System.Drawing.Point(111, 271);
            this.btnUpdateAssignment.Name = "btnUpdateAssignment";
            this.btnUpdateAssignment.Size = new System.Drawing.Size(93, 32);
            this.btnUpdateAssignment.TabIndex = 36;
            this.btnUpdateAssignment.Text = "Оновити";
            this.btnUpdateAssignment.TextColor = System.Drawing.Color.White;
            this.btnUpdateAssignment.UseVisualStyleBackColor = false;
            this.btnUpdateAssignment.Click += new System.EventHandler(this.btnUpdateAssignment_Click);
            // 
            // btnAddAssignment
            // 
            this.btnAddAssignment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddAssignment.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddAssignment.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddAssignment.BorderRadius = 10;
            this.btnAddAssignment.BorderSize = 1;
            this.btnAddAssignment.FlatAppearance.BorderSize = 0;
            this.btnAddAssignment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddAssignment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddAssignment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAssignment.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnAddAssignment.ForeColor = System.Drawing.Color.White;
            this.btnAddAssignment.Location = new System.Drawing.Point(16, 271);
            this.btnAddAssignment.Name = "btnAddAssignment";
            this.btnAddAssignment.Size = new System.Drawing.Size(90, 32);
            this.btnAddAssignment.TabIndex = 35;
            this.btnAddAssignment.Text = "Додати";
            this.btnAddAssignment.TextColor = System.Drawing.Color.White;
            this.btnAddAssignment.UseVisualStyleBackColor = false;
            this.btnAddAssignment.Click += new System.EventHandler(this.btnAddAssignment_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(16, 127);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 2);
            this.panel1.TabIndex = 29;
            // 
            // txtLessonType
            // 
            this.txtLessonType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtLessonType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLessonType.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtLessonType.ForeColor = System.Drawing.Color.White;
            this.txtLessonType.Location = new System.Drawing.Point(16, 111);
            this.txtLessonType.Name = "txtLessonType";
            this.txtLessonType.Size = new System.Drawing.Size(306, 17);
            this.txtLessonType.TabIndex = 28;
            this.txtLessonType.Text = "Тип заняття";
            this.txtLessonType.TextChanged += new System.EventHandler(this.txtLessonType_TextChanged);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Location = new System.Drawing.Point(15, 214);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(306, 2);
            this.panel6.TabIndex = 27;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(15, 170);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(306, 2);
            this.panel5.TabIndex = 26;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(16, 80);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(306, 2);
            this.panel4.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(15, 32);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(306, 2);
            this.panel3.TabIndex = 24;
            // 
            // txtHoursTaught
            // 
            this.txtHoursTaught.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtHoursTaught.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHoursTaught.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtHoursTaught.ForeColor = System.Drawing.Color.White;
            this.txtHoursTaught.Location = new System.Drawing.Point(15, 198);
            this.txtHoursTaught.Name = "txtHoursTaught";
            this.txtHoursTaught.Size = new System.Drawing.Size(306, 17);
            this.txtHoursTaught.TabIndex = 4;
            this.txtHoursTaught.Text = "Відпрацьовано годин";
            this.txtHoursTaught.TextChanged += new System.EventHandler(this.txtHoursTaught_TextChanged);
            // 
            // txtPlanHours
            // 
            this.txtPlanHours.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtPlanHours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlanHours.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtPlanHours.ForeColor = System.Drawing.Color.White;
            this.txtPlanHours.Location = new System.Drawing.Point(15, 154);
            this.txtPlanHours.Name = "txtPlanHours";
            this.txtPlanHours.Size = new System.Drawing.Size(306, 17);
            this.txtPlanHours.TabIndex = 3;
            this.txtPlanHours.Text = "Кількість годин за планом";
            this.txtPlanHours.TextChanged += new System.EventHandler(this.txtPlanHours_TextChanged);
            // 
            // txtSubjectName
            // 
            this.txtSubjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtSubjectName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubjectName.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtSubjectName.ForeColor = System.Drawing.Color.White;
            this.txtSubjectName.Location = new System.Drawing.Point(16, 64);
            this.txtSubjectName.Name = "txtSubjectName";
            this.txtSubjectName.Size = new System.Drawing.Size(306, 17);
            this.txtSubjectName.TabIndex = 1;
            this.txtSubjectName.Text = "Назва дисципліни";
            this.txtSubjectName.TextChanged += new System.EventHandler(this.txtSubjectName_TextChanged);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(15, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(306, 17);
            this.txtName.TabIndex = 0;
            this.txtName.Text = "ПІБ";
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(36)))), ((int)(((byte)(38)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(148)))), ((int)(((byte)(152)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(814, 647);
            this.dataGridView1.TabIndex = 16;
            // 
            // FormAssignment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(1153, 647);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormAssignment";
            this.Text = "FormAssignment";
            this.Load += new System.EventHandler(this.FormAssignment_Load);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtHoursTaught;
        private System.Windows.Forms.TextBox txtPlanHours;
        private System.Windows.Forms.TextBox txtSubjectName;
        private System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLessonType;
        private RoundButton btnClear;
        private RoundButton btnDeleteAssignment;
        private RoundButton btnUpdateAssignment;
        private RoundButton btnAddAssignment;
    }
}