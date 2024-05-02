namespace administrator
{
    partial class AddService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddService));
            button1 = new Button();
            label1 = new Label();
            textName = new TextBox();
            textPrice = new TextBox();
            label2 = new Label();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackgroundImage = Properties.Resources._1627014998_8_p_fon_dlya_yutuba_rozovii_83;
            button1.FlatAppearance.BorderColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(584, 257);
            button1.Name = "button1";
            button1.Size = new Size(121, 44);
            button1.TabIndex = 0;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe Print", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Image = Properties.Resources._1627014998_8_p_fon_dlya_yutuba_rozovii_8;
            label1.Location = new Point(12, 78);
            label1.Name = "label1";
            label1.Size = new Size(348, 35);
            label1.TabIndex = 1;
            label1.Text = "Введите название новой услуги:";
            // 
            // textName
            // 
            textName.BackColor = SystemColors.Window;
            textName.Font = new Font("Georgia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textName.Location = new Point(400, 83);
            textName.Name = "textName";
            textName.Size = new Size(305, 30);
            textName.TabIndex = 2;
            // 
            // textPrice
            // 
            textPrice.Font = new Font("Georgia", 12F, FontStyle.Regular, GraphicsUnit.Point);
            textPrice.Location = new Point(400, 191);
            textPrice.Name = "textPrice";
            textPrice.Size = new Size(305, 30);
            textPrice.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe Print", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Image = Properties.Resources._1627014998_8_p_fon_dlya_yutuba_rozovii_81;
            label2.Location = new Point(12, 186);
            label2.Name = "label2";
            label2.Size = new Size(304, 35);
            label2.TabIndex = 4;
            label2.Text = "Введите цену новой услуги:";
            // 
            // button2
            // 
            button2.FlatAppearance.BorderColor = Color.White;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Image = Properties.Resources._1627014998_8_p_fon_dlya_yutuba_rozovii_82;
            button2.Location = new Point(55, 346);
            button2.Name = "button2";
            button2.Size = new Size(116, 38);
            button2.TabIndex = 5;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // AddService
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._5c3ad78aa1eee16845d9f5a71;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(774, 429);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textPrice);
            Controls.Add(textName);
            Controls.Add(label1);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AddService";
            Text = "Добавить услугу";
            Load += AddService_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox textName;
        private TextBox textPrice;
        private Label label2;
        private Button button2;
    }
}