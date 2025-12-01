# SITOPSI - Sistem Informasi Tes Orientasi Profesi Sistem Informasi

Aplikasi untuk membantu mahasiswa menentukan minat dan bakat di bidang IT melalui tes berbasis **Forward Chaining** dan **Certainty Factor**, dengan rekomendasi topik skripsi yang sesuai.

---

## 📁 Struktur Direktori

```
SITOPSI/
├── vb_app/                          # Aplikasi VB.NET (Frontend & Database)
│   ├── ProjectPBL.sln               # Solution Visual Studio
│   └── ProjectPBL/                  # Project utama VB.NET
│       ├── *.vb                     # Form-form aplikasi
│       ├── *.Designer.vb            # Designer files
│       ├── *.resx                   # Resource files
│       ├── modules/                 # Module tambahan
│       │   └── PythonBridge.vb      # Bridge untuk memanggil Python AI
│       ├── My Project/              # Project properties
│       ├── bin/                     # Build output
│       └── obj/                     # Object files
│
├── python_experts/                  # Mesin AI Python (Inference Engine)
│   ├── main.py                      # Entry point CLI (dipanggil VB)
│   ├── core/                        # Modul inti inference
│   │   ├── inference.py             # Logika Certainty Factor & Forward Chaining
│   │   ├── forward_chaining.py      # (Reserved untuk implementasi lanjut)
│   │   ├── certainty_factor.py      # (Reserved untuk implementasi lanjut)
│   │   └── utils.py                 # Utility functions
│   ├── data/                        # Data tambahan (opsional)
│   │   └── rules.json               # Aturan tambahan (opsional, data utama di DB)
│   ├── models/                      # Model data (untuk type hints)
│   │   └── result_model.py          # Struktur data hasil inference
│   └── README_PROJECT.md            # Dokumentasi Python AI
│
├── dbsitopsi.sql                    # Database MySQL (schema & data)
├── README.md                        # File ini
└── README_PROJECT.md                # Dokumentasi untuk skripsi
```

---

## 🎯 Fitur Utama

### 1. **Aplikasi VB.NET** (`vb_app/`)
- **LandingPage**: Halaman awal aplikasi
- **LoginForm**: Autentikasi user (admin & mahasiswa)
- **RegisterForm**: Registrasi mahasiswa baru
- **DashboardAdmin**: Panel admin untuk mengelola data
- **DashboardUser**: Panel mahasiswa
- **TesMinatBakat**: Form untuk mengerjakan tes (100 pertanyaan)
- **HasilTesMinatBakat**: Menampilkan hasil tes dan kategori minat
- **GenerateTopikSkripsi**: Rekomendasi topik skripsi berdasarkan hasil tes
- **ChangePassword**: Ubah password user

### 2. **Mesin AI Python** (`python_experts/`)
- **Inference Engine**: Menghitung skor minat menggunakan **Certainty Factor (CF)**
- **Forward Chaining**: Menggabungkan CF dari jawaban user untuk tiap kategori
- **JSON CLI**: Menerima input dari VB via stdin/file, mengembalikan hasil JSON

---

## 🗄️ Database (`dbsitopsi.sql`)

### Tabel Utama

| Tabel | Deskripsi |
|-------|-----------|
| `roles` | Role user (admin, mahasiswa) |
| `users` | Data pengguna sistem |
| `categories` | 4 Kategori minat: <br>A = Backend & Software Engineering<br>B = Frontend & Web Design<br>C = UI/UX Design<br>D = Data, Cyber & System Thinking |
| `topics` | 240+ topik skripsi (60 per kategori) |
| `questions` | 100 pertanyaan tes minat bakat |
| `question_options` | Opsi jawaban (A/B/C/D) dengan bobot CF per kategori |

### Bobot Certainty Factor (CF)
- **Soal 1-30** (Behavioral): CF = **0.6**
- **Soal 31-60** (Skill): CF = **0.7**
- **Soal 61-80** (Interest): CF = **0.75**
- **Soal 81-100** (Passion): CF = **0.8**

---

## 🔗 Cara Kerja Integrasi VB ↔ Python

```
[VB.NET Form] 
    ↓ (User menjawab 100 pertanyaan)
[Collect Answers]
    ↓ (Build JSON payload)
{
  "user_id": 123,
  "answers": [
    {"question_id": 1, "option_id": 2, "category_id": 2, "cf_value": 0.6},
    ...
  ]
}
    ↓ (Call via PythonBridge.vb)
[Python main.py] ← stdin
    ↓ (Proses di inference.py)
[Certainty Factor Calculation]
    - Gabungkan CF per kategori: CF_combined = CF1 + CF2*(1-CF1)
    - Tentukan kategori tertinggi
    ↓
[Return JSON Result]
{
  "recommended_category": {"id": 1, "name": "Backend", "score": 0.87},
  "category_scores": {...},
  "topic_recommendations": [...]
}
    ↓ (Parse di VB)
[Display di HasilTesMinatBakat.vb]
[Show Topics di GenerateTopikSkripsi.vb]
```

---

## 🚀 Cara Menjalankan

### Prerequisites
- **Visual Studio 2022** (atau lebih baru) dengan workload VB.NET
- **.NET 8.0** (atau sesuai target di `.vbproj`)
- **Python 3.8+** (pastikan ada di PATH atau gunakan full path)
- **MySQL Server** (untuk database)

### 1. Setup Database
```bash
# Buat database dan import schema
mysql -u root -p < dbsitopsi.sql
```

### 2. Konfigurasi VB.NET
1. Buka `vb_app/ProjectPBL.sln` di Visual Studio
2. Update connection string di `App.config` atau di module database
3. Update path Python di `modules/PythonBridge.vb`:
   ```vb
   Private ReadOnly PythonExe As String = "python"  ' atau "C:\Python39\python.exe"
   Private ReadOnly ScriptPath As String = "c:\Users\lunox\Documents\SITOPSI\python_experts\main.py"
   ```

### 3. Test Python CLI (Opsional)
```powershell
# Test manual dari PowerShell
$json = '{"user_id":1,"answers":[{"question_id":1,"option_id":1,"category_id":1,"cf_value":0.6}]}'
echo $json | python "c:\Users\lunox\Documents\SITOPSI\python_experts\main.py"
```

### 4. Run Aplikasi VB
1. Build solution di Visual Studio (Ctrl+Shift+B)
2. Run (F5)
3. Login sebagai admin:
   - Username: `admin`
   - Password: `Admin@123`

---

## 📊 Algoritma Inference

### Certainty Factor Combination
Ketika user memilih beberapa opsi yang mengarah ke kategori yang sama, CF digabung menggunakan formula:

```
CF_combined(CF1, CF2) = CF1 + CF2 * (1 - CF1)
```

Contoh:
- User menjawab 30 soal kategori Backend, masing-masing CF=0.6
- CF gabungan akan mendekati ~0.99 (sangat kuat)

### Penentuan Kategori
Kategori dengan **CF tertinggi** menjadi rekomendasi utama.

---

## 📝 Format JSON Input/Output

### Input (dari VB ke Python)
```json
{
  "user_id": 123,
  "answers": [
    {
      "question_id": 1,
      "option_id": 2,
      "category_id": 2,
      "cf_value": 0.6
    }
  ]
}
```

### Output (dari Python ke VB)
```json
{
  "user_id": 123,
  "total_questions_answered": 100,
  "category_scores": {
    "1": {
      "name": "Backend & Software Engineering",
      "cf_combined": 0.8523,
      "answer_count": 35
    },
    "2": {...},
    "3": {...},
    "4": {...}
  },
  "recommended_category": {
    "id": 1,
    "name": "Backend & Software Engineering",
    "score": 0.8523,
    "answer_count": 35
  },
  "topic_recommendations": [...],
  "interpretation": "Sangat Kuat - Minat Anda sangat sesuai dengan kategori ini",
  "next_steps": "Pelajari framework seperti Laravel, Node.js/Express..."
}
```

---

## 🛠️ Troubleshooting

### Python tidak ditemukan
```vb
' Di PythonBridge.vb, gunakan full path
Private ReadOnly PythonExe As String = "C:\Python39\python.exe"
```

### Error JSON parsing
- Pastikan input JSON valid (gunakan online JSON validator)
- Cek encoding (harus UTF-8)

### Database connection error
- Pastikan MySQL service running
- Cek username/password di connection string
- Pastikan database `dbsitopsi` sudah ter-import

---

## 📚 Untuk Skripsi

### Metode yang Digunakan
1. **Forward Chaining**: Mengumpulkan fakta (jawaban user) untuk mencapai kesimpulan (kategori minat)
2. **Certainty Factor (CF)**: Mengukur tingkat keyakinan setiap jawaban terhadap kategori
3. **Hybrid Approach**: Kombinasi VB.NET (UI/DB) + Python (AI Engine)

### Referensi Implementasi
- `python_experts/core/inference.py` - Implementasi CF & Forward Chaining
- `vb_app/ProjectPBL/modules/PythonBridge.vb` - Integrasi VB-Python via subprocess
- `dbsitopsi.sql` - Knowledge base (rules dalam bentuk pertanyaan & bobot CF)

---

## 👥 Kontributor

- **Developer**: [Nama Anda]
- **Pembimbing**: [Nama Dosen]
- **Institusi**: [Nama Kampus]

---

## 📄 Lisensi

Proyek ini dibuat untuk keperluan skripsi. Silakan gunakan sebagai referensi dengan mencantumkan sumber.

---

## 📞 Kontak

Jika ada pertanyaan atau issue, hubungi melalui:
- Email: [email anda]
- GitHub: [repo link jika ada]

---

**Status**: ✅ Production Ready
**Last Updated**: Desember 2025
