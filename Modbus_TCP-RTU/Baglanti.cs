using System;

namespace Modbus_TCP_RTU
{
    public static class Baglanti
    {
        // RTU Ayarları
        public static string PortName = "COM1";
        public static int baudrate_değer = 9600;
        public static int DataBits = 8;
        public static System.IO.Ports.Parity Parity = System.IO.Ports.Parity.None;
        public static System.IO.Ports.StopBits StopBits = System.IO.Ports.StopBits.One;

        // TCP Ayarları
        public static string IpAdresi = "192.168.1.181";
        public static int Port = 502;

        // Aktif Seçim (True: TCP, False: RTU)
        public static bool IsTcp = true;
    }
}