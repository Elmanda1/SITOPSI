# Python Experts - AI Inference Engine

## Overview
Folder ini berisi mesin inferensi AI berbasis **Certainty Factor** untuk sistem tes minat bakat IT.

## Struktur Folder

```
python_experts/
├── main.py                  # Entry point - CLI yang dipanggil oleh VB.NET
├── core/                    # Core inference logic
│   ├── inference.py         # Implementasi Certainty Factor algorithm
│   ├── forward_chaining.py  # (Reserved for future implementation)
│   ├── certainty_factor.py  # (Reserved for future implementation)
│   └── utils.py             # Helper functions
├── data/                    # Optional data files
│   └── rules.json           # Aturan tambahan (opsional)
└── models/                  # Data models (type hints only)
    └── result_model.py      # Structure definition untuk hasil inference
```

## Data Source

**PENTING**: Semua data utama disimpan di **database MySQL**, bukan di file JSON:

| Data | Lokasi Database |
|------|----------------|
| **Pertanyaan** | Tabel `questions` (100 soal) |
| **Opsi Jawaban** | Tabel `question_options` (400 opsi) |
| **Bobot CF** | Kolom `cf_pakar` di `question_options` (0.6, 0.7, 0.75, 0.8) |
| **Kategori** | Tabel `categories` (4 kategori: Backend, Frontend, UI/UX, Data/Cyber) |
| **Topik Skripsi** | Tabel `topics` (240+ topik) |

File JSON di folder `data/` hanya untuk aturan tambahan jika diperlukan di masa depan.

## Input Format

JSON yang dikirim dari VB.NET via stdin:

```json
{
  "user_id": 123,
  "answers": [
    {
      "question_id": 1,
      "option_id": 2,
      "category_id": 2,
      "cf_value": 0.6
    },
    ...
  ]
}
```

**Note**: Data `cf_value` diambil VB dari kolom `question_options.cf_pakar` di database.

## Output Format

```json
{
  "user_id": 123,
  "total_questions_answered": 100,
  "category_scores": {
    "1": {
      "name": "Backend & Software Engineering",
      "cf_combined": 0.8523,
      "answer_count": 35,
      "raw_cfs": [0.6, 0.6, 0.7, 0.7, 0.75]
    },
    ...
  },
  "recommended_category": {
    "id": 1,
    "name": "Backend & Software Engineering",
    "score": 0.8523,
    "answer_count": 35
  },
  "interpretation": "Sangat Kuat - Minat Anda sangat sesuai dengan kategori ini",
  "next_steps": "Pelajari framework seperti Laravel, Node.js/Express..."
}
```

## Algoritma

### Certainty Factor Combination

```python
def combine_certainty_factors(cf_list):
    cf_combined = cf_list[0]
    for cf in cf_list[1:]:
        cf_combined = cf_combined + cf * (1 - cf_combined)
    return cf_combined
```

**Contoh**:
- User menjawab 30 soal kategori Backend (CF = 0.6 semua)
- CF[1] = 0.6
- CF[2] = 0.6 + 0.6*(1-0.6) = 0.84
- CF[3] = 0.84 + 0.6*(1-0.84) = 0.936
- ...
- CF[30] ≈ 0.9999 (mendekati 1.0)

## Usage

### Via Command Line

```bash
# Test dengan file
python main.py --input-file test_input.json

# Test dengan stdin (cara VB memanggil)
echo '{"user_id":1,"answers":[...]}' | python main.py
```

### Via VB.NET (Production)

VB.NET memanggil via `PythonBridge.vb`:
1. Query database untuk ambil jawaban user + CF
2. Build JSON payload
3. Start Python process dengan redirect stdin/stdout
4. Kirim JSON via stdin
5. Baca JSON result dari stdout
6. Parse dan tampilkan di UI

## Testing

```bash
# Test inference module langsung
cd python_experts
python core/inference.py

# Test CLI
python main.py -i test_input.json
```

## Dependencies

- **Python 3.8+** (built-in libraries only)
- Tidak ada external dependencies (murni Python standard library)

## Notes

- Engine ini **stateless** - tidak menyimpan data apapun
- Semua persistensi data dilakukan oleh VB.NET ke MySQL
- File ini hanya melakukan kalkulasi CF dan memberikan rekomendasi
- Topik skripsi di-query langsung oleh VB dari database

## Integration Flow

```
[VB Form] → [Query DB] → [Build JSON] 
    ↓
[PythonBridge.vb] → [main.py] → [inference.py]
    ↓
[Calculate CF] → [Return JSON] → [Parse VB] → [Display Result]
```

---

**Last Updated**: Desember 2025
**Status**: ✅ Production Ready
