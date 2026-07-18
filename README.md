# SISTEM INFORMASI TOPIK SKRIPSI BERBASIS SISTEM PAKAR

**SITOPSI** — Aplikasi desktop untuk membantu mahasiswa Program Studi Teknik Informatika menentukan minat dan bakat di bidang IT melalui tes berbasis **Certainty Factor (CF)** dan **Forward Chaining**, kemudian memberikan rekomendasi topik skripsi yang sesuai.

---

## DAFTAR ISI

- [Sekilas Tentang SITOPSI](#sekilas-tentang-sitopsi)
- [Fitur Aplikasi (Screenshots)](#fitur-aplikasi-screenshots)
- [Struktur Proyek](#struktur-proyek)
- [Teknologi yang Digunakan](#teknologi-yang-digunakan)
- [Alur Sistem](#alur-sistem)
- [Algoritma Certainty Factor](#algoritma-certainty-factor)
- [Integrasi VB.NET dengan Python](#integrasi-vbnet-dengan-python)
- [Cara Menjalankan](#cara-menjalankan)
- [Lisensi](#lisensi)

---

## SEKILAS TENTANG SITOPSI

SITOPSI adalah sistem pakar berbasis desktop yang dirancang untuk:

1. **Mengidentifikasi minat dan bakat mahasiswa** di bidang Teknik Informatika menggunakan metode Certainty Factor.
2. **Memberikan rekomendasi topik skripsi** yang sesuai dengan hasil tes minat bakat.
3. **Menyediakan dashboard manajemen** bagi admin untuk mengelola data pertanyaan, rules, topik, dan user.

Sistem mengklasifikasikan minat mahasiswa ke dalam **4 kategori utama**:

| Kategori | Bidang |
|----------|--------|
| A | Backend & Software Engineering |
| B | Frontend & Web Design |
| C | UI/UX Design |
| D | Data, Cyber & System Thinking |

Dengan **100+ pertanyaan** yang terbagi dalam 4 tingkatan (Behavioral, Skill, Interest, Passion) dan **240+ topik skripsi** yang siap direkomendasikan.

---

## FITUR APLIKASI (SCREENSHOTS)

### 1. Log Aktivitas
Riwayat aktivitas user (login/logout) dengan waktu, untuk audit admin.

![Log Aktivitas](docs/Sitopsi%20(1).png)

---

### 2. Landing Page SITOPSI
Halaman awal dengan logo, judul "Sistem Informasi Topik Skripsi Berbasis Sistem Pakar", tombol Login & Register.

![Landing Page](docs/Sitopsi%20(2).png)

---

### 3. Login ke Akun
Form login (Username, Password) + link "Lupa Password?".

![Login](docs/Sitopsi%20(3).png)

---

### 4. Reset Password
Form ganti password (Username, Password Baru, Konfirmasi Password Baru).

![Reset Password](docs/Sitopsi%20(4).png)

---

### 5. Daftar Akun Baru
Form registrasi (Nama Lengkap, No. Telepon, Email, Username, Password, Jenis Kelamin).

![Registrasi](docs/Sitopsi%20(5).png)

---

### 6. Dashboard Mahasiswa
Halaman utama setelah login, menu: Tes Minat Bakat, Lihat Hasil Tes, Generate Topik, Ubah Password, Profile Saya.

![Dashboard Mahasiswa](docs/Sitopsi%20(6).png)

---

### 7. Rekomendasi Topik Skripsi
Daftar 10 rekomendasi topik skripsi hasil generate berdasarkan minat bakat, + tombol Generate Ulang / Print Preview / Export PDF.

![Rekomendasi Topik](docs/Sitopsi%20(7).png)

---

### 8. Hasil Tes Minat Bakat
Peringkat minat bakat user dengan nilai CF & persentase, plus detail jawaban tiap pertanyaan.

![Hasil Tes Minat Bakat](docs/Sitopsi%20(8).png)

---

### 9. Tes Minat Bakat (Soal)
Halaman kuesioner, 1 pertanyaan per halaman dari 15 soal, pilihan tingkat keyakinan.

![Tes Minat Bakat](docs/Sitopsi%20(9).png)

---

### 10. Admin Dashboard
Menu manajemen admin: User Management, Manajemen Pertanyaan, Rules, Rules Combination, Topics Management, Activity Logs.

![Admin Dashboard](docs/Sitopsi%20(10).png)

---

### 11. Manajemen User
Tabel data semua user (nama, username, email, role, minat bakat, status, tanggal daftar).

![Manajemen User](docs/Sitopsi%20(11).png)

---

### 12. Manajemen Pertanyaan (CF)
Form kelola pertanyaan tes minat bakat beserta kategori target & nilai CF Pakar.

![Manajemen Pertanyaan](docs/Sitopsi%20(12).png)

---

### 13. Manajemen Kategori (Rules)
Kelola kategori/bidang minat (kode, nama, deskripsi) seperti Backend, Frontend, UI/UX, dll.

![Manajemen Kategori](docs/Sitopsi%20(13).png)

---

### 14. Manajemen Kombinasi Rules
Kelola aturan kombinasi 2-3 kategori untuk menghasilkan "role" hasil (misal Product Analyst dari Frontend + Data).

![Manajemen Kombinasi Rules](docs/Sitopsi%20(14).png)

---

### 15. Manajemen Topik Skripsi
Kelola daftar topik skripsi berdasarkan kategori & target role, status Aktif/Layak.

![Manajemen Topik Skripsi](docs/Sitopsi%20(15).png)

---

## STRUKTUR PROYEK

```
SITOPSI/
├── docs/                              # Screenshots aplikasi (15 gambar)
│   ├── Sitopsi (1).png  s.d. (15).png
│
├── python_experts/                    # Mesin AI Python (Inference Engine)
│   ├── main.py                        # Entry point CLI
│   ├── core/
│   │   ├── inference.py               # Orchestrator CF & Forward Chaining
│   │   ├── forward_chaining.py        # Logika Forward Chaining
│   │   ├── certainty_factor.py        # Kalkulasi Certainty Factor
│   │   └── utils.py                   # Fungsi bantu
│   ├── models/
│   │   └── result_model.py            # Dataclass hasil inference
│   └── test_input.json                # Sample input untuk testing
│
├── vb_app/                            # Aplikasi VB.NET (Frontend & Database)
│   ├── ProjectPBL.sln                 # Solution Visual Studio
│   └── ProjectPBL/
│       ├── ProjectPBL.vbproj          # .NET 8.0 Windows Forms
│       ├── Program.vb                 # Entry point aplikasi
│       ├── GlobalModule.vb            # Session variabel & koneksi DB
│       ├── *.vb / *.resx              # Form-form aplikasi
│       ├── modules/
│       │   └── PythonBridge.vb        # Jembatan VB ke Python via subprocess
│       ├── RoundedButton.vb           # Custom control tombol rounded
│       ├── RoundedPanel.vb            # Custom control panel rounded
│       └── RoundedTextBox.vb          # Custom control textbox rounded
│
├── .gitignore
└── README.md
```

---

## TEKNOLOGI YANG DIGUNAKAN

| Komponen | Teknologi |
|----------|-----------|
| Frontend | VB.NET Windows Forms (.NET 8.0) |
| AI Engine | Python 3.8+ (standard library only) |
| Database | MySQL 8.0 |
| Integrasi | Subprocess (stdin/stdout JSON) |
| PDF Export | Headless Microsoft Edge / Chrome |
| Metode | Certainty Factor + Forward Chaining |

---

## ALUR SISTEM

```
Mahasiswa
    │
    ├── 1. Register / Login
    │
    ├── 2. Tes Minat Bakat
    │       └── Menjawab 15 pertanyaan dengan tingkat keyakinan
    │
    ├── 3. Python AI Engine (subprocess)
    │       ├── Forward Chaining → kumpulkan fakta per kategori
    │       ├── Certainty Factor → hitung skor tiap kategori
    │       └── Output → kategori dengan CF tertinggi
    │
    ├── 4. Lihat Hasil Tes
    │       └── Peringkat minat bakat + nilai CF + detail jawaban
    │
    └── 5. Generate Topik Skripsi
            └── 10 rekomendasi topik berdasarkan kategori minat

Admin
    │
    └── Dashboard
            ├── Manajemen User
            ├── Manajemen Pertanyaan & CF
            ├── Manajemen Kategori (Rules)
            ├── Manajemen Kombinasi Rules
            ├── Manajemen Topik Skripsi
            └── Log Aktivitas
```

---

## ALGORITMA CERTAINTY FACTOR

Certainty Factor (CF) digunakan untuk mengukur tingkat keyakinan minat user terhadap suatu kategori.

### Rumus Kombinasi CF

```
CF_combined(CF1, CF2) = CF1 + CF2 × (1 - CF1)
```

### Tahapan Perhitungan

**Tahap 1 — CF Gejala**

```
CF_gejala = CF_user × CF_pakar
```

- `CF_user` = tingkat keyakinan user saat menjawab (0–1)
- `CF_pakar` = bobot expert untuk pertanyaan tersebut (0.6, 0.7, 0.75, 0.8)

**Tahap 2 — CF Kombinasi**

Semua CF_gejala dalam satu kategori digabung secara berurutan:

```
CF_baru = CF_lama + CF_gejala × (1 - CF_lama)
```

**Tahap 3 — Interpretasi**

| Rentang CF | Interpretasi |
|------------|--------------|
| 0.9 – 1.0 | Sangat Kuat |
| 0.7 – 0.9 | Kuat |
| 0.5 – 0.7 | Sedang |
| 0.3 – 0.5 | Lemah |
| 0.0 – 0.3 | Sangat Lemah |

### Bobot CF Pakar per Tingkatan Soal

| Rentang Soal | Tipe | CF Pakar |
|--------------|------|----------|
| 1 – 30 | Behavioral | 0.6 |
| 31 – 60 | Skill | 0.7 |
| 61 – 80 | Interest | 0.75 |
| 81 – 100 | Passion | 0.8 |

---

## INTEGRASI VB.NET DENGAN PYTHON

VB.NET memanggil Python AI Engine melalui subprocess dengan protokol JSON via stdin/stdout.

### Diagram Alur

```
┌──────────────────────┐
│    VB.NET Form       │
│ (TesMinatBakat.vb)   │
│         │            │
│  Query jawaban user  │
│  Build JSON payload  │
│         │            │
│         ▼            │
│ PythonBridge.vb      │
│  (Process.Start)     │
│         │            │
│  stdin ──► main.py   │
│         │            │
│  main.py ◄── stdout  │
│         │            │
│         ▼            │
│ Parse JSON result    │
│ Display di UI        │
└──────────────────────┘
```

### Format Input (VB → Python)

```json
{
  "user_id": 1,
  "answers": [
    {
      "question_id": 1,
      "category_id": 1,
      "cf_user": 0.8,
      "cf_pakar": 0.6
    }
  ]
}
```

### Format Output (Python → VB)

```json
{
  "user_id": 1,
  "total_questions_answered": 15,
  "category_scores": {
    "1": {
      "name": "Backend & Software Engineering",
      "cf_combined": 0.87,
      "answer_count": 8
    },
    "2": {
      "name": "Frontend & Web Design",
      "cf_combined": 0.65,
      "answer_count": 4
    },
    "3": { "...": "..." },
    "4": { "...": "..." }
  },
  "recommended_category": {
    "id": 1,
    "name": "Backend & Software Engineering",
    "score": 0.87,
    "answer_count": 8
  },
  "interpretation": "Sangat Kuat - Minat Anda sangat sesuai dengan kategori ini",
  "next_steps": "Pelajari framework seperti Laravel, Node.js/Express, atau Spring Boot"
}
```

---

## CARA MENJALANKAN

### Prasyarat

- Visual Studio 2022+ dengan workload .NET Desktop
- .NET 8.0 SDK
- Python 3.8+
- MySQL Server

### 1. Setup Database

Buat database `dbsitopsi` dan import struktur tabel beserta data master.

### 2. Konfigurasi Koneksi Database

Sesuaikan connection string di `vb_app/ProjectPBL/GlobalModule.vb`:

```vb
Public Shared ConnString As String = "Server=localhost;Database=dbsitopsi;User ID=root;Password=;Port=3306;"
```

### 3. Konfigurasi Path Python

Sesuaikan path Python executable dan script di `vb_app/ProjectPBL/modules/PythonBridge.vb`:

```vb
Private ReadOnly PythonExe As String = "python"
Private ReadOnly ScriptPath As String = "C:\path\to\python_experts\main.py"
```

### 4. Build & Run

1. Buka `vb_app/ProjectPBL.sln` di Visual Studio
2. Build solution (Ctrl+Shift+B)
3. Run (F5)

### 5. Login

| Role | Username | Password |
|------|----------|----------|
| Admin | admin | Admin@123 |
| Mahasiswa | (registrasi sendiri) | (user-defined) |

---

## LISENSI

```
MIT License

Copyright (c) 2025 SITOPSI

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

**Status:** Production Ready  
**Tahun:** 2025  
**Institusi:** Politeknik Negeri Jakarta — Teknik Informatika
