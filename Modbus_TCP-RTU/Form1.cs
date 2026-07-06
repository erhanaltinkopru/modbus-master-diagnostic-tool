using Modbus.Device;
using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO.Ports;

namespace Modbus_TCP_RTU
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listele.FullRowSelect = true;
            listele.DoubleClick += Listele_DoubleClick;
        }

        public static SerialPort SeriPort = new SerialPort();
        private TcpClient tcpClient;
        private ModbusIpMaster ipMaster;
        private IModbusSerialMaster rtuMaster;

        bool hat_aktif = false;
        ushort startAddress = 0;
        ushort numInputs = 10;
        byte slaveId = 1;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();

            // input_box kontrolü (Eğer içi boşsa hata vermemesi için koruma ekliyoruz)
            if (input_box.Items.Count > 0)
            {
                input_box.SelectedIndex = 0;
            }

            // ÖNCE İÇİNİ DOLDURUYORUZ (Hatanın kesin çözümü)
            combo_Function.Items.Clear();
            combo_Function.Items.Add("01 Read Coils (0x)");
            combo_Function.Items.Add("02 Read Discrete Inputs (1x)");
            combo_Function.Items.Add("03 Read Holding Registers (4x)");
            combo_Function.Items.Add("04 Read Input Registers (3x)");

            // ŞİMDİ GÜVENLE SEÇEBİLİRİZ
            combo_Function.SelectedIndex = 2; // Default: Holding Register
        }

        // "BAĞLANTI AYARLARI" BUTONU - POP-UP PENCEREYİ AÇAR
        private void btn_Ayarlar_Click(object sender, EventArgs e)
        {
            using (FormConnection popUp = new FormConnection())
            {
                // Pop-up penceresini aç ve kullanıcı "BAĞLAN" butonuna basana kadar bekle
                if (popUp.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        BaglantiyiKapat(); // Önceki açık bir bağlantı varsa sıfırla

                        if (Baglanti.IsTcp) // --- TCP/IP BAĞLANTISI ---
                        {
                            tcpClient = new TcpClient();
                            IAsyncResult result = tcpClient.BeginConnect(Baglanti.IpAdresi, Baglanti.Port, null, null);

                            if (result.AsyncWaitHandle.WaitOne(2500) && tcpClient.Connected)
                            {
                                tcpClient.EndConnect(result);
                                ipMaster = ModbusIpMaster.CreateIp(tcpClient);
                                ipMaster.Transport.ReadTimeout = 2000;
                                hat_aktif = true;
                                MessageBox.Show($"✅ TCP BAĞLANTI BAŞARILI!\nIP: {Baglanti.IpAdresi}", "Modbus Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ModbusSorguCalistir();
                            }
                            else
                            {
                                hat_aktif = false;
                                MessageBox.Show("❌ TCP Bağlantı Zaman Aşımı! Cihaza ulaşılamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else // --- RTU (SERİ PORT) BAĞLANTISI ---
                        {
                            SeriPort.PortName = Baglanti.PortName;
                            SeriPort.BaudRate = Baglanti.baudrate_değer;
                            SeriPort.DataBits = Baglanti.DataBits;
                            SeriPort.Parity = Baglanti.Parity;
                            SeriPort.StopBits = Baglanti.StopBits;
                            SeriPort.ReadTimeout = 1000;

                            SeriPort.Open();
                            rtuMaster = ModbusSerialMaster.CreateRtu(SeriPort);
                            hat_aktif = true;
                            MessageBox.Show($"✅ SERİ PORT AÇILDI!\nPort: {Baglanti.PortName}", "Modbus Master", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ModbusSorguCalistir();
                        }
                    }
                    catch (Exception ex)
                    {
                        hat_aktif = false;
                        MessageBox.Show($"❌ Bağlantı Hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // MODBUS SORGULAMA MOTORU
        void ModbusSorguCalistir()
        {
            try
            {
                // Arayüzdeki başlangıç adresi kutusu boşsa 0 kabul et, doluysa ushort'a çevir
                startAddress = string.IsNullOrEmpty(input_startAddress.Text) ? (ushort)0 : Convert.ToUInt16(input_startAddress.Text);

                if (input_box.SelectedItem == null) return;

                // BAŞINA "int" EKLEYEREK DEĞİŞKENİ BURADA TANIMLIYORUZ (Hatanın kesin çözümü)
                int deger = Convert.ToInt32(input_box.SelectedItem);
                numInputs = Convert.ToUInt16(deger);
                // =================================================================
                // TAMPON (BUFFER) TEMİZLEME KATMANI (KAYMA VE ADRES BİRİKTİRME HATASINI ÇÖZER)
                // =================================================================
                if (Baglanti.IsTcp && tcpClient != null && tcpClient.Connected)
                {
                    // TCP soketinde birikmiş, okunmamış artık baytlar varsa tamamen temizle
                    if (tcpClient.Available > 0)
                    {
                        NetworkStream stream = tcpClient.GetStream();
                        byte[] junk = new byte[tcpClient.Available];
                        stream.Read(junk, 0, junk.Length);
                    }
                }
                else if (!Baglanti.IsTcp && SeriPort != null && SeriPort.IsOpen)
                {
                    // Seri portun giriş ve çıkış tampon belleğini tamamen sıfırla
                    SeriPort.DiscardInBuffer();
                    SeriPort.DiscardOutBuffer();
                }
                // =================================================================

                ushort[] registers = null;
                bool[] coils = null;
                int seciliFonksiyon = combo_Function.SelectedIndex;

                // --- TCP/IP PROTOKOLÜ OKUMA ---
                if (Baglanti.IsTcp)
                {
                    if (tcpClient == null || !tcpClient.Connected || ipMaster == null) return;

                    if (seciliFonksiyon == 0) coils = ipMaster.ReadCoils(slaveId, startAddress, numInputs);
                    else if (seciliFonksiyon == 1) coils = ipMaster.ReadInputs(slaveId, startAddress, numInputs);
                    else if (seciliFonksiyon == 2) registers = ipMaster.ReadHoldingRegisters(slaveId, startAddress, numInputs);
                    else if (seciliFonksiyon == 3) registers = ipMaster.ReadInputRegisters(slaveId, startAddress, numInputs);
                }
                // --- RTU (SERİ PORT) PROTOKOLÜ OKUMA ---
                else
                {
                    if (!SeriPort.IsOpen || rtuMaster == null) return;

                    if (seciliFonksiyon == 0) coils = rtuMaster.ReadCoils(slaveId, startAddress, numInputs);
                    else if (seciliFonksiyon == 1) coils = rtuMaster.ReadInputs(slaveId, startAddress, numInputs);
                    else if (seciliFonksiyon == 2) registers = rtuMaster.ReadHoldingRegisters(slaveId, startAddress, numInputs);
                    else if (seciliFonksiyon == 3) registers = rtuMaster.ReadInputRegisters(slaveId, startAddress, numInputs);
                }

                // --- GÖRSEL LİSTELEME MOTORU (İKİ SIRA YAN YANA GÖSTERİM) ---
                listele.BeginUpdate();

                // Yenileme esnasında kullanıcının kaydırma (Scroll) pozisyonunu hafızaya al
                int topIndex = listele.TopItem != null ? listele.TopItem.Index : 0;
                listele.Items.Clear();

                // Eğer register (16-bit word) okunduysa (Fonksiyon 03 veya 04)
                if (registers != null)
                {
                    int toplamKayit = registers.Length;

                    // Verileri yan yana ikişerli basmak için döngüyü i += 2 adımıyla çalıştırıyoruz
                    for (int i = 0; i < toplamKayit; i += 2)
                    {
                        // 1. SIRA (Sol Taraf: Adres_listele, deger_int, deger_hex)
                        ListViewItem item = new ListViewItem((startAddress + i).ToString("D5"));
                        item.SubItems.Add(registers[i].ToString());
                        item.SubItems.Add($"0x{registers[i]:X4}");

                        // 2. SIRA (Sağ Taraf: Adres_listele1, deger_int1, deger_hex1)
                        if (i + 1 < toplamKayit)
                        {
                            item.SubItems.Add((startAddress + i + 1).ToString("D5"));
                            item.SubItems.Add(registers[i + 1].ToString());
                            item.SubItems.Add($"0x{registers[i + 1]:X4}");
                        }
                        else
                        {
                            // Eğer toplam kayıt tek sayı bittiyse sağ tarafı boş geçiyoruz
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                        }
                        listele.Items.Add(item);
                    }
                }
                // Eğer coil (1-bit binary) okunduysa (Fonksiyon 01 veya 02)
                else if (coils != null)
                {
                    int toplamKayit = coils.Length;

                    for (int i = 0; i < toplamKayit; i += 2)
                    {
                        // 1. SIRA (Sol Taraf)
                        ListViewItem item = new ListViewItem((startAddress + i).ToString("D5"));
                        item.SubItems.Add(coils[i] ? "1" : "0");
                        item.SubItems.Add(coils[i] ? "ON" : "OFF");

                        // 2. SIRA (Sağ Taraf)
                        if (i + 1 < toplamKayit)
                        {
                            item.SubItems.Add((startAddress + i + 1).ToString("D5"));
                            item.SubItems.Add(coils[i + 1] ? "1" : "0");
                            item.SubItems.Add(coils[i + 1] ? "ON" : "OFF");
                        }
                        else
                        {
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                            item.SubItems.Add("");
                        }
                        listele.Items.Add(item);
                    }
                }

                // Yenileme bittikten sonra kaydırma çubuğunu (Scroll) eski kaldığı satıra geri konumlandır
                if (listele.Items.Count > 0 && topIndex < listele.Items.Count)
                {
                    listele.TopItem = listele.Items[topIndex];
                }

                // SÜTUNLARI SABİT ÖLÇÜDE EŞİT DAĞITMA (1022px pencere genişliğine göre milimetrik hizalama)
                foreach (ColumnHeader ch in listele.Columns)
                {
                    ch.Width = 164;
                }

                listele.EndUpdate();
            }
            catch (Exception ex)
            {
                try { listele.EndUpdate(); } catch { }

                // Metni çok uzatıp etiketi taşıran gereksiz kısımları kırpıyoruz
                string kisaMesaj = ex.Message;

                if (ex.Message.Contains("Exception Code"))
                {
                    // 'Modbus.SlaveException türünde özel durum...' kısmını atıp doğrudan özünü alıyoruz
                    int codeIndex = ex.Message.IndexOf("Exception Code:");
                    if (codeIndex != -1)
                    {
                        kisaMesaj = ex.Message.Substring(codeIndex);
                    }
                }

                // DURUM kelimesini ve hatayı tek satırda net şekilde birleştiriyoruz
                label8.Text = $"DURUM: HATA ({kisaMesaj})";
                label8.ForeColor = Color.Red;
            }
        }







        // ÇİFT TIKLAYARAK VERİ YAZMA
        private void Listele_DoubleClick(object sender, EventArgs e)
        {
            // Bağlantı aktif değilse veya listede seçili eleman yoksa işlem yapma
            if (listele.SelectedItems.Count == 0 || !hat_aktif) return;

            ListViewItem seciliSatir = listele.SelectedItems[0];
            Point tıklamaNoktası = listele.PointToClient(Cursor.Position);
            ListViewHitTestInfo hitTest = listele.HitTest(tıklamaNoktası);

            ushort tiklananAdres = 0;
            string mevcutDeger = "";

            // Tıklanan sütun indeksini bul (Sağ sıra mı sol sıra mı?)
            if (hitTest.SubItem != null && seciliSatir.SubItems.IndexOf(hitTest.SubItem) >= 3)
            {
                if (seciliSatir.SubItems.Count <= 3 || string.IsNullOrEmpty(seciliSatir.SubItems[3].Text)) return;
                tiklananAdres = Convert.ToUInt16(seciliSatir.SubItems[3].Text);
                mevcutDeger = seciliSatir.SubItems[4].Text;
            }
            else
            {
                tiklananAdres = Convert.ToUInt16(seciliSatir.Text);
                mevcutDeger = seciliSatir.SubItems[1].Text;
            }

            // Seçili fonksiyon modunu alıyoruz 
            // (0: Coils, 1: Discrete Inputs, 2: Holding Registers, 3: Input Registers)
            int seciliFonksiyon = combo_Function.SelectedIndex;

            // =================================================================
            // STANDART MODBUS KORUMASI: 1x (İndeks 1) ve 3x (İndeks 3) SALT OKUNURDUR!
            // =================================================================
            if (seciliFonksiyon == 1 || seciliFonksiyon == 3)
            {
                string alanAdi = seciliFonksiyon == 1 ? "Discrete Inputs (1x)" : "Input Registers (3x)";
                MessageBox.Show($"{alanAdi} sadece okunabilir bir alandır, üzerine veri yazılamaz!", "Modbus Yazma Koruması", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // =================================================================

            // Kullanıcıdan yeni değeri almak için InputBox açıyoruz
            string inputDeğer = Microsoft.VisualBasic.Interaction.InputBox(
                seciliFonksiyon == 0 ? $"📌 {tiklananAdres} nolu Coil durumunu değiştirin (0 veya 1):" : $"📌 {tiklananAdres} nolu Register değerini değiştirin:",
                "Modbus Veri Yaz",
                mevcutDeger);

            // Eğer kullanıcı İptal'e bastıysa veya boş bıraktıysa çık
            if (string.IsNullOrEmpty(inputDeğer)) return;

            try
            {
                // -------------------------------------------------------------
                // COIL (0x) YAZMA LOJİĞİ (Fonksiyon 01 / İndeks 0 seçili ise)
                // -------------------------------------------------------------
                if (seciliFonksiyon == 0)
                {
                    if (inputDeğer == "0" || inputDeğer == "1")
                    {
                        bool coilDeger = inputDeğer == "1";

                        if (Baglanti.IsTcp && ipMaster != null)
                        {
                            ipMaster.WriteSingleCoil(slaveId, tiklananAdres, coilDeger);
                        }
                        else if (!Baglanti.IsTcp && rtuMaster != null)
                        {
                            rtuMaster.WriteSingleCoil(slaveId, tiklananAdres, coilDeger);
                        }
                        MessageBox.Show("Coil durumu başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ModbusSorguCalistir(); // Ekranı hemen güncelle
                    }
                    else
                    {
                        MessageBox.Show("Coil alanına sadece '0' (OFF) veya '1' (ON) yazabilirsiniz!", "Hatalı Değer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                // -------------------------------------------------------------
                // HOLDING REGISTER (4x) YAZMA LOJİĞİ (Fonksiyon 03 / İndeks 2 seçili ise)
                // -------------------------------------------------------------
                else if (seciliFonksiyon == 2)
                {
                    if (ushort.TryParse(inputDeğer, out ushort yeniDeger))
                    {
                        if (Baglanti.IsTcp && ipMaster != null)
                        {
                            ipMaster.WriteSingleRegister(slaveId, tiklananAdres, yeniDeger);
                        }
                        else if (!Baglanti.IsTcp && rtuMaster != null)
                        {
                            rtuMaster.WriteSingleRegister(slaveId, tiklananAdres, yeniDeger);
                        }
                        MessageBox.Show("Register değeri başarıyla yazıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ModbusSorguCalistir(); // Ekranı hemen güncelle
                    }
                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir nümerik (0 - 65535 arası) değer girin!", "Hatalı Değer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Yazma İşlemi Başarısız: {ex.Message}", "Modbus Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }









        private void btn_Kopar_Click(object sender, EventArgs e) { BaglantiyiKapat(); }
        private void Oku_btn_Click(object sender, EventArgs e) { ModbusSorguCalistir(); }

        private void BaglantiyiKapat()
        {
            try
            {
                if (SeriPort.IsOpen) SeriPort.Close();
                if (tcpClient != null) tcpClient.Close();
                if (ipMaster != null) ipMaster.Dispose();
                if (rtuMaster != null) rtuMaster.Dispose();
            }
            catch { }
            hat_aktif = false;
            label8.Text = "BAĞLI DEĞİL";
            label8.ForeColor = Color.Red;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (hat_aktif)
            {
                label8.Text = Baglanti.IsTcp ? "BAĞLI (TCP)" : "BAĞLI (RTU)";
                label8.ForeColor = Color.Green;
                if (chk_AutoRead.Checked) ModbusSorguCalistir();
            }
            else
            {
                label8.Text = "BAĞLI DEĞİL";
                label8.ForeColor = Color.Red;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}