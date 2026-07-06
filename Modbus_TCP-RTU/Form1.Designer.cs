namespace Modbus_TCP_RTU
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button3 = new System.Windows.Forms.Button();
            this.Oku_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.input_box = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.listele = new System.Windows.Forms.ListView();
            this.Adres_listele = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deger_int = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deger_hex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Adres_listele1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deger_int1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.deger_hex1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chk_AutoRead = new System.Windows.Forms.CheckBox();
            this.input_startAddress = new System.Windows.Forms.NumericUpDown();
            this.combo_Function = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_Ayarlar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.input_startAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(335, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "KAYDET";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Oku_btn
            // 
            this.Oku_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.Oku_btn.FlatAppearance.BorderSize = 0;
            this.Oku_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Oku_btn.ForeColor = System.Drawing.Color.White;
            this.Oku_btn.Location = new System.Drawing.Point(12, 516);
            this.Oku_btn.Name = "Oku_btn";
            this.Oku_btn.Size = new System.Drawing.Size(153, 39);
            this.Oku_btn.TabIndex = 21;
            this.Oku_btn.Text = "OKU";
            this.Oku_btn.UseVisualStyleBackColor = false;
            this.Oku_btn.Click += new System.EventHandler(this.Oku_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(7, 558);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 29);
            this.label2.TabIndex = 25;
            this.label2.Text = "DURUM";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(114, 558);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(915, 127);
            this.label8.TabIndex = 27;
            this.label8.Text = "......";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // input_box
            // 
            this.input_box.FormattingEnabled = true;
            this.input_box.ItemHeight = 21;
            this.input_box.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30"});
            this.input_box.Location = new System.Drawing.Point(887, 469);
            this.input_box.Name = "input_box";
            this.input_box.Size = new System.Drawing.Size(100, 29);
            this.input_box.TabIndex = 32;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(618, 472);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(239, 23);
            this.label11.TabIndex = 33;
            this.label11.Text = "OKUNACAK ADRES UZUNLUK";
            // 
            // listele
            // 
            this.listele.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Adres_listele,
            this.deger_int,
            this.deger_hex,
            this.Adres_listele1,
            this.deger_int1,
            this.deger_hex1});
            this.listele.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listele.FullRowSelect = true;
            this.listele.HideSelection = false;
            this.listele.Location = new System.Drawing.Point(7, 81);
            this.listele.Name = "listele";
            this.listele.Size = new System.Drawing.Size(1022, 382);
            this.listele.TabIndex = 34;
            this.listele.UseCompatibleStateImageBehavior = false;
            this.listele.View = System.Windows.Forms.View.Details;
            // 
            // Adres_listele
            // 
            this.Adres_listele.Text = "ADRESLER";
            this.Adres_listele.Width = 229;
            // 
            // deger_int
            // 
            this.deger_int.Text = "İNTEGER";
            this.deger_int.Width = 264;
            // 
            // deger_hex
            // 
            this.deger_hex.Text = "HEXADECİMAL";
            this.deger_hex.Width = 207;
            // 
            // Adres_listele1
            // 
            this.Adres_listele1.Text = "ADRESLER";
            this.Adres_listele1.Width = 136;
            // 
            // deger_int1
            // 
            this.deger_int1.Text = "İNTEGER";
            this.deger_int1.Width = 88;
            // 
            // deger_hex1
            // 
            this.deger_hex1.Text = "HEXADECİMAL";
            this.deger_hex1.Width = 63;
            // 
            // chk_AutoRead
            // 
            this.chk_AutoRead.AutoSize = true;
            this.chk_AutoRead.Location = new System.Drawing.Point(171, 523);
            this.chk_AutoRead.Name = "chk_AutoRead";
            this.chk_AutoRead.Size = new System.Drawing.Size(160, 27);
            this.chk_AutoRead.TabIndex = 35;
            this.chk_AutoRead.Text = "Otomatik okuma";
            this.chk_AutoRead.UseVisualStyleBackColor = true;
            // 
            // input_startAddress
            // 
            this.input_startAddress.Location = new System.Drawing.Point(508, 469);
            this.input_startAddress.Name = "input_startAddress";
            this.input_startAddress.Size = new System.Drawing.Size(80, 29);
            this.input_startAddress.TabIndex = 36;
            // 
            // combo_Function
            // 
            this.combo_Function.FormattingEnabled = true;
            this.combo_Function.Location = new System.Drawing.Point(128, 469);
            this.combo_Function.Name = "combo_Function";
            this.combo_Function.Size = new System.Drawing.Size(199, 29);
            this.combo_Function.TabIndex = 37;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 471);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 23);
            this.label7.TabIndex = 38;
            this.label7.Text = "Okuma Türü";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(357, 472);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(121, 23);
            this.label13.TabIndex = 39;
            this.label13.Text = "başlagıç adresi";
            // 
            // btn_Ayarlar
            // 
            this.btn_Ayarlar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Ayarlar.BackgroundImage")));
            this.btn_Ayarlar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Ayarlar.Location = new System.Drawing.Point(7, 2);
            this.btn_Ayarlar.Name = "btn_Ayarlar";
            this.btn_Ayarlar.Size = new System.Drawing.Size(158, 73);
            this.btn_Ayarlar.TabIndex = 40;
            this.btn_Ayarlar.UseVisualStyleBackColor = true;
            this.btn_Ayarlar.Click += new System.EventHandler(this.btn_Ayarlar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1037, 685);
            this.Controls.Add(this.btn_Ayarlar);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.combo_Function);
            this.Controls.Add(this.input_startAddress);
            this.Controls.Add(this.chk_AutoRead);
            this.Controls.Add(this.listele);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.input_box);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Oku_btn);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modbus";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.input_startAddress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button Oku_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox input_box;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListView listele;
        private System.Windows.Forms.ColumnHeader Adres_listele;
        private System.Windows.Forms.ColumnHeader deger_int;
        private System.Windows.Forms.ColumnHeader deger_hex;
        private System.Windows.Forms.CheckBox chk_AutoRead;
        private System.Windows.Forms.NumericUpDown input_startAddress;
        private System.Windows.Forms.ComboBox combo_Function;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ColumnHeader Adres_listele1;
        private System.Windows.Forms.ColumnHeader deger_int1;
        private System.Windows.Forms.ColumnHeader deger_hex1;
        private System.Windows.Forms.Button btn_Ayarlar;
    }
}

