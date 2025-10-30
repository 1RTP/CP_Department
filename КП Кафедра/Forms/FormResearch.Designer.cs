namespace КП_Кафедра.Forms
{
    partial class FormResearch
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
            this.btnDeleteResearch = new КП_Кафедра.RoundButton();
            this.btnUpdateResearch = new КП_Кафедра.RoundButton();
            this.btnAddResearch = new КП_Кафедра.RoundButton();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.panel10.Controls.Add(this.btnClear);
            this.panel10.Controls.Add(this.btnDeleteResearch);
            this.panel10.Controls.Add(this.btnUpdateResearch);
            this.panel10.Controls.Add(this.btnAddResearch);
            this.panel10.Controls.Add(this.dtpEndDate);
            this.panel10.Controls.Add(this.panel3);
            this.panel10.Controls.Add(this.dtpStartDate);
            this.panel10.Controls.Add(this.txtProjectName);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel10.Location = new System.Drawing.Point(723, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(332, 682);
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
            this.btnClear.Location = new System.Drawing.Point(278, 135);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(41, 40);
            this.btnClear.TabIndex = 34;
            this.btnClear.TextColor = System.Drawing.Color.White;
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDeleteResearch
            // 
            this.btnDeleteResearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteResearch.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDeleteResearch.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDeleteResearch.BorderRadius = 10;
            this.btnDeleteResearch.BorderSize = 0;
            this.btnDeleteResearch.FlatAppearance.BorderSize = 0;
            this.btnDeleteResearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteResearch.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnDeleteResearch.ForeColor = System.Drawing.Color.White;
            this.btnDeleteResearch.Location = new System.Drawing.Point(209, 182);
            this.btnDeleteResearch.Name = "btnDeleteResearch";
            this.btnDeleteResearch.Size = new System.Drawing.Size(110, 32);
            this.btnDeleteResearch.TabIndex = 33;
            this.btnDeleteResearch.Text = "Видалити";
            this.btnDeleteResearch.TextColor = System.Drawing.Color.White;
            this.btnDeleteResearch.UseVisualStyleBackColor = false;
            this.btnDeleteResearch.Click += new System.EventHandler(this.btnDeleteResearch_Click);
            // 
            // btnUpdateResearch
            // 
            this.btnUpdateResearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateResearch.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateResearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnUpdateResearch.BorderRadius = 10;
            this.btnUpdateResearch.BorderSize = 1;
            this.btnUpdateResearch.FlatAppearance.BorderSize = 0;
            this.btnUpdateResearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateResearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnUpdateResearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateResearch.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnUpdateResearch.ForeColor = System.Drawing.Color.White;
            this.btnUpdateResearch.Location = new System.Drawing.Point(110, 182);
            this.btnUpdateResearch.Name = "btnUpdateResearch";
            this.btnUpdateResearch.Size = new System.Drawing.Size(93, 32);
            this.btnUpdateResearch.TabIndex = 32;
            this.btnUpdateResearch.Text = "Оновити";
            this.btnUpdateResearch.TextColor = System.Drawing.Color.White;
            this.btnUpdateResearch.UseVisualStyleBackColor = false;
            this.btnUpdateResearch.Click += new System.EventHandler(this.btnUpdateResearch_Click);
            // 
            // btnAddResearch
            // 
            this.btnAddResearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddResearch.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddResearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnAddResearch.BorderRadius = 10;
            this.btnAddResearch.BorderSize = 1;
            this.btnAddResearch.FlatAppearance.BorderSize = 0;
            this.btnAddResearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddResearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(130)))), ((int)(((byte)(26)))));
            this.btnAddResearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddResearch.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnAddResearch.ForeColor = System.Drawing.Color.White;
            this.btnAddResearch.Location = new System.Drawing.Point(14, 182);
            this.btnAddResearch.Name = "btnAddResearch";
            this.btnAddResearch.Size = new System.Drawing.Size(90, 32);
            this.btnAddResearch.TabIndex = 31;
            this.btnAddResearch.Text = "Додати";
            this.btnAddResearch.TextColor = System.Drawing.Color.White;
            this.btnAddResearch.UseVisualStyleBackColor = false;
            this.btnAddResearch.Click += new System.EventHandler(this.btnAddResearch_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(14, 102);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(306, 24);
            this.dtpEndDate.TabIndex = 25;
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
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(14, 61);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(306, 24);
            this.dtpStartDate.TabIndex = 2;
            // 
            // txtProjectName
            // 
            this.txtProjectName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.txtProjectName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProjectName.Font = new System.Drawing.Font("Arial", 8.8F);
            this.txtProjectName.ForeColor = System.Drawing.Color.White;
            this.txtProjectName.Location = new System.Drawing.Point(15, 17);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(306, 17);
            this.txtProjectName.TabIndex = 0;
            this.txtProjectName.Text = "Назва проєкту";
            this.txtProjectName.TextChanged += new System.EventHandler(this.txtProjectName_TextChanged);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(148)))), ((int)(((byte)(152)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.8F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(713, 682);
            this.dataGridView1.TabIndex = 16;
            // 
            // FormResearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(1055, 682);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Arial", 8.8F);
            this.Name = "FormResearch";
            this.Text = "FormResearch";
            this.Load += new System.EventHandler(this.FormResearch_Load);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.TextBox txtProjectName;
        internal System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private RoundButton btnClear;
        private RoundButton btnDeleteResearch;
        private RoundButton btnUpdateResearch;
        private RoundButton btnAddResearch;
    }
}