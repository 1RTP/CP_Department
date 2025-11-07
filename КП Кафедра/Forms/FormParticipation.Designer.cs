namespace КП_Кафедра.Forms
{
    partial class FormParticipation
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
            this.btnDeleteParticipation = new КП_Кафедра.RoundButton();
            this.btnUpdateParticipation = new КП_Кафедра.RoundButton();
            this.btnAddParticipation = new КП_Кафедра.RoundButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtProjectName = new System.Windows.Forms.TextBox();
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
            this.panel10.Controls.Add(this.btnDeleteParticipation);
            this.panel10.Controls.Add(this.btnUpdateParticipation);
            this.panel10.Controls.Add(this.btnAddParticipation);
            this.panel10.Controls.Add(this.panel4);
            this.panel10.Controls.Add(this.panel3);
            this.panel10.Controls.Add(this.txtProjectName);
            this.panel10.Controls.Add(this.txtName);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(569, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(247, 530);
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
            this.btnClear.Location = new System.Drawing.Point(210, 91);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(31, 31);
            this.btnClear.TabIndex = 34;
            this.btnClear.TextColor = System.Drawing.Color.White;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDeleteParticipation
            // 
            this.btnDeleteParticipation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteParticipation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteParticipation.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDeleteParticipation.BorderRadius = 10;
            this.btnDeleteParticipation.BorderSize = 0;
            this.btnDeleteParticipation.FlatAppearance.BorderSize = 0;
            this.btnDeleteParticipation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteParticipation.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnDeleteParticipation.ForeColor = System.Drawing.Color.White;
            this.btnDeleteParticipation.Location = new System.Drawing.Point(148, 128);
            this.btnDeleteParticipation.Name = "btnDeleteParticipation";
            this.btnDeleteParticipation.Size = new System.Drawing.Size(93, 26);
            this.btnDeleteParticipation.TabIndex = 33;
            this.btnDeleteParticipation.Text = "Видалити";
            this.btnDeleteParticipation.TextColor = System.Drawing.Color.White;
            this.btnDeleteParticipation.UseVisualStyleBackColor = false;
            this.btnDeleteParticipation.Click += new System.EventHandler(this.btnDeleteParticipation_Click);
            // 
            // btnUpdateParticipation
            // 
            this.btnUpdateParticipation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateParticipation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateParticipation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateParticipation.BorderRadius = 10;
            this.btnUpdateParticipation.BorderSize = 1;
            this.btnUpdateParticipation.FlatAppearance.BorderSize = 0;
            this.btnUpdateParticipation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateParticipation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateParticipation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateParticipation.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnUpdateParticipation.ForeColor = System.Drawing.Color.White;
            this.btnUpdateParticipation.Location = new System.Drawing.Point(74, 128);
            this.btnUpdateParticipation.Name = "btnUpdateParticipation";
            this.btnUpdateParticipation.Size = new System.Drawing.Size(70, 26);
            this.btnUpdateParticipation.TabIndex = 32;
            this.btnUpdateParticipation.Text = "Оновити";
            this.btnUpdateParticipation.TextColor = System.Drawing.Color.White;
            this.btnUpdateParticipation.UseVisualStyleBackColor = false;
            this.btnUpdateParticipation.Click += new System.EventHandler(this.btnUpdateParticipation_Click);
            // 
            // btnAddParticipation
            // 
            this.btnAddParticipation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddParticipation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddParticipation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddParticipation.BorderRadius = 10;
            this.btnAddParticipation.BorderSize = 1;
            this.btnAddParticipation.FlatAppearance.BorderSize = 0;
            this.btnAddParticipation.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddParticipation.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddParticipation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddParticipation.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnAddParticipation.ForeColor = System.Drawing.Color.White;
            this.btnAddParticipation.Location = new System.Drawing.Point(11, 128);
            this.btnAddParticipation.Name = "btnAddParticipation";
            this.btnAddParticipation.Size = new System.Drawing.Size(57, 26);
            this.btnAddParticipation.TabIndex = 31;
            this.btnAddParticipation.Text = "Додати";
            this.btnAddParticipation.TextColor = System.Drawing.Color.White;
            this.btnAddParticipation.UseVisualStyleBackColor = false;
            this.btnAddParticipation.Click += new System.EventHandler(this.btnAddParticipation_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(11, 82);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(230, 2);
            this.panel4.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(11, 41);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(230, 2);
            this.panel3.TabIndex = 24;
            // 
            // txtProjectName
            // 
            this.txtProjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtProjectName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProjectName.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtProjectName.ForeColor = System.Drawing.Color.White;
            this.txtProjectName.Location = new System.Drawing.Point(11, 65);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(230, 14);
            this.txtProjectName.TabIndex = 1;
            this.txtProjectName.Text = "Назва проєкту";
            this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(11, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(230, 14);
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
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
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
            this.dataGridView1.Size = new System.Drawing.Size(562, 530);
            this.dataGridView1.TabIndex = 16;
            // 
            // FormParticipation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(816, 530);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Arial", 8.8F);
            this.Name = "FormParticipation";
            this.Text = "FormParticipation";
            this.Load += new System.EventHandler(this.FormParticipation_Load);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtProjectName;
        private System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.DataGridView dataGridView1;
        private RoundButton btnClear;
        private RoundButton btnDeleteParticipation;
        private RoundButton btnUpdateParticipation;
        private RoundButton btnAddParticipation;
    }
}