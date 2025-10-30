namespace КП_Кафедра.Forms
{
    partial class FormSettings
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
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lbLanguage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.rbBIN = new System.Windows.Forms.RadioButton();
            this.rbXML = new System.Windows.Forms.RadioButton();
            this.rbJSON = new System.Windows.Forms.RadioButton();
            this.btnLoad = new КП_Кафедра.RoundButton();
            this.btnSave = new КП_Кафедра.RoundButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(20, 47);
            this.cmbLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(160, 24);
            this.cmbLanguage.TabIndex = 12;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // lbLanguage
            // 
            this.lbLanguage.AutoSize = true;
            this.lbLanguage.Font = new System.Drawing.Font("Arial", 8.8F);
            this.lbLanguage.ForeColor = System.Drawing.Color.White;
            this.lbLanguage.Location = new System.Drawing.Point(17, 16);
            this.lbLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLanguage.Name = "lbLanguage";
            this.lbLanguage.Size = new System.Drawing.Size(126, 17);
            this.lbLanguage.TabIndex = 13;
            this.lbLanguage.Text = "Мова застосунку:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.cmbLanguage);
            this.panel1.Controls.Add(this.lbLanguage);
            this.panel1.Controls.Add(this.rbBIN);
            this.panel1.Controls.Add(this.rbXML);
            this.panel1.Controls.Add(this.rbJSON);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 528);
            this.panel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 451);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(365, 17);
            this.label1.TabIndex = 31;
            this.label1.Text = "Завантаження та збереження даних у різні формати:";
            // 
            // rbBIN
            // 
            this.rbBIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbBIN.AutoSize = true;
            this.rbBIN.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbBIN.ForeColor = System.Drawing.Color.White;
            this.rbBIN.Location = new System.Drawing.Point(170, 490);
            this.rbBIN.Name = "rbBIN";
            this.rbBIN.Size = new System.Drawing.Size(52, 21);
            this.rbBIN.TabIndex = 17;
            this.rbBIN.TabStop = true;
            this.rbBIN.Text = "BIN";
            this.rbBIN.UseVisualStyleBackColor = true;
            this.rbBIN.CheckedChanged += new System.EventHandler(this.rbBIN_CheckedChanged);
            // 
            // rbXML
            // 
            this.rbXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbXML.AutoSize = true;
            this.rbXML.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbXML.ForeColor = System.Drawing.Color.White;
            this.rbXML.Location = new System.Drawing.Point(20, 490);
            this.rbXML.Name = "rbXML";
            this.rbXML.Size = new System.Drawing.Size(57, 21);
            this.rbXML.TabIndex = 15;
            this.rbXML.TabStop = true;
            this.rbXML.Text = "XML";
            this.rbXML.UseVisualStyleBackColor = true;
            this.rbXML.CheckedChanged += new System.EventHandler(this.rbXML_CheckedChanged);
            // 
            // rbJSON
            // 
            this.rbJSON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbJSON.AutoSize = true;
            this.rbJSON.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbJSON.ForeColor = System.Drawing.Color.White;
            this.rbJSON.Location = new System.Drawing.Point(89, 490);
            this.rbJSON.Name = "rbJSON";
            this.rbJSON.Size = new System.Drawing.Size(68, 21);
            this.rbJSON.TabIndex = 16;
            this.rbJSON.TabStop = true;
            this.rbJSON.Text = "JSON";
            this.rbJSON.UseVisualStyleBackColor = true;
            this.rbJSON.CheckedChanged += new System.EventHandler(this.rbJSON_CheckedChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnLoad.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnLoad.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLoad.BorderRadius = 10;
            this.btnLoad.BorderSize = 0;
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Location = new System.Drawing.Point(242, 485);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(128, 32);
            this.btnLoad.TabIndex = 29;
            this.btnLoad.Text = "Завантажити";
            this.btnLoad.TextColor = System.Drawing.Color.White;
            this.btnLoad.UseVisualStyleBackColor = false;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnSave.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnSave.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSave.BorderRadius = 10;
            this.btnSave.BorderSize = 0;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(376, 485);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 32);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "Зберегти";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormSettings";
            this.Text = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Label lbLanguage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbXML;
        private System.Windows.Forms.RadioButton rbJSON;
        private System.Windows.Forms.RadioButton rbBIN;
        private RoundButton btnLoad;
        private RoundButton btnSave;
        private System.Windows.Forms.Label label1;
    }
}