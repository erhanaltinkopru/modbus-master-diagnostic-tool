using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace Modbus_TCP_RTU
{
    public partial class FormConnection : Form
    {
        public FormConnection()
        {
            InitializeComponent();
        }

        private void FormConnection_Load(object sender, EventArgs e)
        {
            // COM Portları listele
            string[] portlar = SerialPort.GetPortNames();
            port_box.Items.Clear();
            foreach (string port in portlar) port_box.Items.Add(port);

            // Hafızadaki mevcut ayarları arayüze yükle
            tcp_btn.Checked = Baglanti.IsTcp;
            rtu_btn.Checked = !Baglanti.IsTcp;

            string[] ipParcalari = Baglanti.IpAdresi.Split('.');
            if (ipParcalari.Length == 4)
            {
                ip_box1.Text = ipParcalari[0];
                ip_box2.Text = ipParcalari[1];
                ip_box3.Text = ipParcalari[2];
                ip_box4.Text = ipParcalari[3];
            }

            // ComboBox varsayılan seçimleri
            if (port_box.Items.Count > 0) port_box.SelectedItem = Baglanti.PortName;
            baud_box.SelectedItem = Baglanti.baudrate_değer.ToString();
        }

        private void btn_Baglan_Click(object sender, EventArgs e)
        {
            // Ayarları kalıcı sınıfa kaydet
            Baglanti.IsTcp = tcp_btn.Checked;
            Baglanti.IpAdresi = $"{ip_box1.Text}.{ip_box2.Text}.{ip_box3.Text}.{ip_box4.Text}";

            if (port_box.SelectedItem != null)
                Baglanti.PortName = port_box.SelectedItem.ToString();

            if (baud_box.SelectedItem != null)
                Baglanti.baudrate_değer = Convert.ToInt32(baud_box.SelectedItem);

            // DataBits, Parity ve StopBits ComboBox indeks seçimlerini burada da Baglanti sınıfına aktarabilirsiniz.

            this.DialogResult = DialogResult.OK; // Ana forma "Ayarlar Tamam" sinyali gönderir
            this.Close();
        }

      

        // RadioButton değişimine göre panelleri gizle/göster yapabilirsiniz
       

        private void tcp_btn_CheckedChanged_1(object sender, EventArgs e)
        {
            ip_ayarlar_paneli.Visible = tcp_btn.Checked;
            seri_port_paneli.Visible = !tcp_btn.Checked;

        }

        private void kaydet_btn_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}