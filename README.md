<<<<<<< HEAD
# Modbus TCP/RTU Diagnostic Tool

This repository contains a Windows Forms application for Modbus TCP/RTU diagnostic operations.

## Project
- Target framework: .NET Framework 4.7.2
- Solution file: `Modbus_TCP-RTU.sln`

## Getting Started
1. Open `Modbus_TCP-RTU.sln` in Visual Studio.
2. Restore NuGet packages.
3. Build and run the project.
=======
# Modbus Master (TCP & RTU) Data Logger & Diagnostic Tool

🌐 [English](#english-version) | 🇹🇷 [Türkçe Versiyon](#türkçe-versiyon)

---

## English Version

This project is a high-performance **Modbus Master (Diagnostic) software** developed to test, monitor, and analyze real-time data traffic for **Modbus TCP/IP** and **Modbus RTU (RS485/Serial Port)** communication protocols used in industrial automation systems (PLCs, HMIs, Drives, Sensors).

### 🚀 Key Features
*   **Dual Protocol Support:** Perform TCP/IP socket communication and Serial Port (RTU) communication through a unified interface.
*   **Smart Buffer Management:** Automatically flushes serial port (`DiscardInBuffer/OutBuffer`) and TCP socket buffers before each cycle, preventing byte-shift errors and cumulative lag during high-speed reads.
*   **High-Performance Dual-Column Grid:** Built with a non-blocking ListView architecture (`BeginUpdate/EndUpdate`) to prevent UI flickering during rapid data updates, displaying registers in a clean, dual-column efficient layout.
*   **Industrial Exception Handling:** Native Modbus Slave Exceptions (Codes 1, 2, 3, 4) and connection timeouts are captured and translated into clear, localized diagnostics for fast troubleshooting on the field.
*   **Secure Smart Writing:** Dynamically triggers `WriteSingleCoil` or `WriteSingleRegister` based on the selected mode. Built-in write protection prevents accidents on read-only areas (1x Discrete Inputs & 3x Input Registers).

### 💻 Technologies Used
*   **Language & Framework:** C# (.NET Windows Forms)
*   **Communication Engine:** NModbus / NModbus4 wrappers
*   **Architecture:** Synced connection wrappers, 16-bit word/1-bit binary decoders.

---

## Türkçe Versiyon

Bu proje; endüstriyel otomasyon sistemlerinde (PLC, HMI, Sürücüler, Sensörler) kullanılan **Modbus TCP/IP** ve **Modbus RTU (RS485/Seri Port)** haberleşme protokollerini test etmek, sahadaki verileri anlık olarak izlemek ve veri trafiğini analiz etmek amacıyla geliştirilmiş, yüksek performanslı bir **Modbus Master (Diagnostic)** yazılımıdır.

### 🚀 Öne Çıkan Özellikler
*   **Çift Protokol Desteği:** Aynı arayüz üzerinden hem TCP/IP soket haberleşmesi hem de Seri Port (RTU) haberleşmesi gerçekleştirebilme.
*   **Akıllı Tampon (Buffer) Yönetimi:** Seri port (`DiscardInBuffer/OutBuffer`) ve TCP soket tampon belleğini her çevrimde otomatik temizleyerek, yüksek hızlı okumalarda yaşanan paket kayması ve veri birikmesi (byte-shift) hatalarının önüne geçilmiştir.
*   **Performanslı Çift Sütunlu Grid Sistemi:** ListView arayüzü `BeginUpdate` ve `EndUpdate` mimarisiyle kilitlenerek, asenkron yenilemelerde ekran titremesi (flickering) önlenmiş ve veriler yan yana çift sıra halinde yüksek görsel verimlilikle listelenmiştir.
*   **Gelişmiş Endüstriyel Hata Motoru:** Modbus standardına ait `Slave Exception` (Hata Kodları 1, 2, 3, 4) ve fiziksel zaman aşımı (Timeout) durumları yakalanarak, sahada hızlı aksiyon alınabilmesi için doğrudan Türkçe arıza teşhis mesajlarına dönüştürülmüştür.
*   **Çift Tıklama ile Akıllı Veri Yazma:** Seçili fonksiyona göre dinamik olarak `WriteSingleCoil` veya `WriteSingleRegister` tetiklenir. Salt okunur alanlar (1x Discrete Inputs ve 3x Input Registers) için standart veri yazma koruması entegre edilmiştir.

### 🛠 Teknik Protokol Haritası

| Fonksiyon Kodu | Modbus Veri Tipi | Adres Aralığı | İzin Tipi |
| :--- | :--- | :--- | :--- |
| **01 (0x)** | Read Coils | 00001 - 09999 | Okunabilir / Yazılabilir |
| **02 (1x)** | Read Discrete Inputs | 10001 - 19999 | **Salt Okunur (Yazma Korumalı)** |
| **03 (4x)** | Read Holding Registers | 40001 - 49999 | Okunabilir / Yazılabilir |
| **04 (3x)** | Read Input Registers | 30001 - 39999 | **Salt Okunur (Yazma Korumalı)** |

---

## 📸 Ekran Görüntüleri / Screenshots
<img width="1296" height="887" alt="image" src="https://github.com/user-attachments/assets/adb889f8-ba02-47a5-95ab-41259f463ed1" />
<img width="1292" height="887" alt="image" src="https://github.com/user-attachments/assets/9c490abe-de06-440e-8f28-77e505a69a79" />


>>>>>>> origin/main
