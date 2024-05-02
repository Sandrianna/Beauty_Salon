namespace administrator
{
    partial class ChangePrice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangePrice));
            textBox1 = new TextBox();
            buttonSave = new Button();
            comboBox1 = new ComboBox();
            buttonClose = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Georgia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(312, 131);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(163, 30);
            textBox1.TabIndex = 6;
            // 
            // buttonSave
            // 
            buttonSave.BackgroundImage = Properties.Resources._1627014998_8_p_fon_dlya_yutuba_rozovii_84;
            buttonSave.FlatAppearance.BorderColor = Color.White;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Comic Sans MS", 13F, FontStyle.Regular, GraphicsUnit.Point);
            buttonSave.Location = new Point(528, 129);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(133, 46);
            buttonSave.TabIndex = 12;
            buttonSave.Text = "Сохранить";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Georgia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(28, 129);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(248, 32);
            comboBox1.TabIndex = 13;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // buttonClose
            // 
            buttonClose.BackgroundImage = Properties.Resources._1627014998_8_p_fon_dlya_yutuba_rozovii_85;
            buttonClose.FlatAppearance.BorderColor = Color.White;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Comic Sans MS", 13.2000008F, FontStyle.Regular, GraphicsUnit.Point);
            buttonClose.Location = new Point(519, 312);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(142, 54);
            buttonClose.TabIndex = 14;
            buttonClose.Text = "Закрыть";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // ChangePrice
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._389bd4f206b256ee007a3bb31c2903e42;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(703, 401);
            Controls.Add(buttonClose);
            Controls.Add(comboBox1);
            Controls.Add(buttonSave);
            Controls.Add(textBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ChangePrice";
            Text = "Изменить цену";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private Button buttonSave;
        private ComboBox comboBox1;
        private Button buttonClose;
    }
}