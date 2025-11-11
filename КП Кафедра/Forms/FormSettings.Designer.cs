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
            this.btnDefaultSettings = new КП_Кафедра.RoundButton();
            this.btnSaveSettings = new КП_Кафедра.RoundButton();
            this.btnTextColor = new КП_Кафедра.RoundButton();
            this.btnFont = new КП_Кафедра.RoundButton();
            this.lblPreview = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoad = new КП_Кафедра.RoundButton();
            this.btnSave = new КП_Кафедра.RoundButton();
            this.rbBIN = new System.Windows.Forms.RadioButton();
            this.rbXML = new System.Windows.Forms.RadioButton();
            this.rbJSON = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(128, 11);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cmbLanguage.TabIndex = 12;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // lbLanguage
            // 
            this.lbLanguage.AutoSize = true;
            this.lbLanguage.Font = new System.Drawing.Font("Arial", 8.8F);
            this.lbLanguage.ForeColor = System.Drawing.Color.White;
            this.lbLanguage.Location = new System.Drawing.Point(13, 13);
            this.lbLanguage.Name = "lbLanguage";
            this.lbLanguage.Size = new System.Drawing.Size(102, 15);
            this.lbLanguage.TabIndex = 13;
            this.lbLanguage.Text = "Мова застосунку:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(54)))), ((int)(((byte)(85)))));
            this.panel1.Controls.Add(this.btnDefaultSettings);
            this.panel1.Controls.Add(this.btnSaveSettings);
            this.panel1.Controls.Add(this.btnTextColor);
            this.panel1.Controls.Add(this.btnFont);
            this.panel1.Controls.Add(this.lblPreview);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLoad);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.cmbLanguage);
            this.panel1.Controls.Add(this.lbLanguage);
            this.panel1.Controls.Add(this.rbBIN);
            this.panel1.Controls.Add(this.rbXML);
            this.panel1.Controls.Add(this.rbJSON);
            this.panel1.Location = new System.Drawing.Point(10, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(782, 469);
            this.panel1.TabIndex = 14;
            // 
            // btnDefaultSettings
            // 
            this.btnDefaultSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefaultSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDefaultSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnDefaultSettings.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDefaultSettings.BorderRadius = 10;
            this.btnDefaultSettings.BorderSize = 0;
            this.btnDefaultSettings.FlatAppearance.BorderSize = 0;
            this.btnDefaultSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaultSettings.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnDefaultSettings.ForeColor = System.Drawing.Color.White;
            this.btnDefaultSettings.Location = new System.Drawing.Point(618, 434);
            this.btnDefaultSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnDefaultSettings.Name = "btnDefaultSettings";
            this.btnDefaultSettings.Size = new System.Drawing.Size(152, 26);
            this.btnDefaultSettings.TabIndex = 37;
            this.btnDefaultSettings.Text = "Скинути налаштування";
            this.btnDefaultSettings.TextColor = System.Drawing.Color.White;
            this.btnDefaultSettings.UseVisualStyleBackColor = false;
            this.btnDefaultSettings.Click += new System.EventHandler(this.btnDefaultSettings_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnSaveSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnSaveSettings.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSaveSettings.BorderRadius = 10;
            this.btnSaveSettings.BorderSize = 0;
            this.btnSaveSettings.FlatAppearance.BorderSize = 0;
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this.btnSaveSettings.Location = new System.Drawing.Point(263, 48);
            this.btnSaveSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(96, 26);
            this.btnSaveSettings.TabIndex = 36;
            this.btnSaveSettings.Text = "Зберегти";
            this.btnSaveSettings.TextColor = System.Drawing.Color.White;
            this.btnSaveSettings.UseVisualStyleBackColor = false;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // btnTextColor
            // 
            this.btnTextColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnTextColor.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnTextColor.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnTextColor.BorderRadius = 10;
            this.btnTextColor.BorderSize = 0;
            this.btnTextColor.FlatAppearance.BorderSize = 0;
            this.btnTextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextColor.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnTextColor.ForeColor = System.Drawing.Color.White;
            this.btnTextColor.Location = new System.Drawing.Point(128, 48);
            this.btnTextColor.Margin = new System.Windows.Forms.Padding(2);
            this.btnTextColor.Name = "btnTextColor";
            this.btnTextColor.Size = new System.Drawing.Size(131, 26);
            this.btnTextColor.TabIndex = 34;
            this.btnTextColor.Text = "Змінити колір тексту";
            this.btnTextColor.TextColor = System.Drawing.Color.White;
            this.btnTextColor.UseVisualStyleBackColor = false;
            this.btnTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
            // 
            // btnFont
            // 
            this.btnFont.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnFont.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.btnFont.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnFont.BorderRadius = 10;
            this.btnFont.BorderSize = 0;
            this.btnFont.FlatAppearance.BorderSize = 0;
            this.btnFont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFont.Font = new System.Drawing.Font("Arial", 8.8F);
            this.btnFont.ForeColor = System.Drawing.Color.White;
            this.btnFont.Location = new System.Drawing.Point(15, 48);
            this.btnFont.Margin = new System.Windows.Forms.Padding(2);
            this.btnFont.Name = "btnFont";
            this.btnFont.Size = new System.Drawing.Size(109, 26);
            this.btnFont.TabIndex = 33;
            this.btnFont.Text = "Змінити шрифт";
            this.btnFont.TextColor = System.Drawing.Color.White;
            this.btnFont.UseVisualStyleBackColor = false;
            this.btnFont.Click += new System.EventHandler(this.btnFont_Click);
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Font = new System.Drawing.Font("Arial", 8.8F);
            this.lblPreview.ForeColor = System.Drawing.Color.White;
            this.lblPreview.Location = new System.Drawing.Point(374, 55);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(127, 15);
            this.lblPreview.TabIndex = 32;
            this.lblPreview.Text = "Налаштування тексту";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8.8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 15);
            this.label1.TabIndex = 31;
            this.label1.Text = "Завантаження та збереження даних у різні формати:";
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
            this.btnLoad.Location = new System.Drawing.Point(182, 434);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(96, 26);
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
            this.btnSave.Location = new System.Drawing.Point(282, 434);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(96, 26);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "Зберегти";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rbBIN
            // 
            this.rbBIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbBIN.AutoSize = true;
            this.rbBIN.Font = new System.Drawing.Font("Arial", 8.8F);
            this.rbBIN.ForeColor = System.Drawing.Color.White;
            this.rbBIN.Location = new System.Drawing.Point(128, 436);
            this.rbBIN.Margin = new System.Windows.Forms.Padding(2);
            this.rbBIN.Name = "rbBIN";
            this.rbBIN.Size = new System.Drawing.Size(45, 19);
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
            this.rbXML.Location = new System.Drawing.Point(15, 436);
            this.rbXML.Margin = new System.Windows.Forms.Padding(2);
            this.rbXML.Name = "rbXML";
            this.rbXML.Size = new System.Drawing.Size(48, 19);
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
            this.rbJSON.Location = new System.Drawing.Point(67, 436);
            this.rbJSON.Margin = new System.Windows.Forms.Padding(2);
            this.rbJSON.Name = "rbJSON";
            this.rbJSON.Size = new System.Drawing.Size(57, 19);
            this.rbJSON.TabIndex = 16;
            this.rbJSON.TabStop = true;
            this.rbJSON.Text = "JSON";
            this.rbJSON.UseVisualStyleBackColor = true;
            this.rbJSON.CheckedChanged += new System.EventHandler(this.rbJSON_CheckedChanged);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(100)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(800, 490);
            this.Controls.Add(this.panel1);
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
        private RoundButton btnTextColor;
        private RoundButton btnFont;
        private System.Windows.Forms.Label lblPreview;
        private RoundButton btnDefaultSettings;
        private RoundButton btnSaveSettings;
    }
}