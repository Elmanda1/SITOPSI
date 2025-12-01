from typing import List, Dict, Any
from collections import defaultdict

def calculate_cf_gejala(cf_user: float, cf_pakar: float) -> float:
    """TAHAP 1: CFGejala = CFUser x CFPakar"""
    if not (0.0 <= cf_user <= 1.0):
        raise ValueError(f"CFUser must be between 0 and 1, got {cf_user}")
    if not (0.0 <= cf_pakar <= 1.0):
        raise ValueError(f"CFPakar must be between 0 and 1, got {cf_pakar}")
    return round(cf_user * cf_pakar, 4)

def combine_cf(cf_lama: float, cf_gejala: float) -> float:
    """TAHAP 2: CFBaru = CFLama + CFGejala x (1 - CFLama)"""
    if not (0.0 <= cf_lama <= 1.0):
        raise ValueError(f"CFLama must be between 0 and 1, got {cf_lama}")
    if not (0.0 <= cf_gejala <= 1.0):
        raise ValueError(f"CFGejala must be between 0 and 1, got {cf_gejala}")
    return round(cf_lama + (cf_gejala * (1 - cf_lama)), 4)

def combine_multiple_cf(cf_gejala_list: List[float]) -> float:
    """Combine multiple CF secara berurutan"""
    if not cf_gejala_list:
        return 0.0
    if len(cf_gejala_list) == 1:
        return cf_gejala_list[0]
    cf_gabung = cf_gejala_list[0]
    for cf_gejala in cf_gejala_list[1:]:
        cf_gabung = combine_cf(cf_gabung, cf_gejala)
    return round(cf_gabung, 4)

def calculate_category_cf(answers: List[Dict[str, Any]]) -> Dict[str, Dict[str, Any]]:
    """Hitung CF untuk setiap kategori (2 TAHAP)"""
    category_cf_gejalas = defaultdict(list)
    for answer in answers:
        cat_id = str(answer.get("category_id"))
        cf_user = float(answer.get("cf_user", 0.0))
        cf_pakar = float(answer.get("cf_pakar", 0.0))
        if 0.0 < cf_user <= 1.0 and 0.0 < cf_pakar <= 1.0:
            cf_gejala = calculate_cf_gejala(cf_user, cf_pakar)
            category_cf_gejalas[cat_id].append({
                "cf_user": cf_user,
                "cf_pakar": cf_pakar,
                "cf_gejala": cf_gejala,
                "question_id": answer.get("question_id")
            })
    result = {}
    for cat_id, gejala_list in category_cf_gejalas.items():
        cf_gejala_values = [g["cf_gejala"] for g in gejala_list]
        cf_gabung = combine_multiple_cf(cf_gejala_values)
        avg_cf_user = sum(g["cf_user"] for g in gejala_list) / len(gejala_list)
        avg_cf_pakar = sum(g["cf_pakar"] for g in gejala_list) / len(gejala_list)
        result[cat_id] = {
            "cf_combined": cf_gabung,
            "answer_count": len(gejala_list),
            "cf_gejala_values": cf_gejala_values[:10],
            "avg_cf_user": round(avg_cf_user, 4),
            "avg_cf_pakar": round(avg_cf_pakar, 4),
            "detail_calculation": gejala_list[:5]
        }
    return result

def get_interpretation(cf_score: float) -> str:
    """Interpretasi verbal dari skor CF"""
    if cf_score >= 0.8:
        return "Sangat Kuat - Minat Anda sangat sesuai dengan kategori ini"
    elif cf_score >= 0.6:
        return "Kuat - Minat Anda cukup sesuai dengan kategori ini"
    elif cf_score >= 0.4:
        return "Sedang - Ada beberapa kecocokan dengan kategori ini"
    elif cf_score >= 0.2:
        return "Lemah - Kecocokan dengan kategori ini masih rendah"
    else:
        return "Sangat Lemah - Pertimbangkan kategori lain yang lebih sesuai"

if __name__ == "__main__":
    import json
    print("=== CERTAINTY FACTOR - METODOLOGI KELOMPOK ===\n")
    print("TEST 1: CFGejala = CFUser x CFPakar")
    print("User 'Yakin' (0.8) x CFPakar 0.8 =", calculate_cf_gejala(0.8, 0.8))
    print("\nTEST 2: Combine 0.64 + 0.48")
    print("Result:", combine_cf(0.64, 0.48))
    print("\nTEST 3: 5 Soal Backend")
    answers = [
        {"question_id": 1, "category_id": 1, "cf_user": 0.8, "cf_pakar": 0.8},
        {"question_id": 2, "category_id": 1, "cf_user": 0.8, "cf_pakar": 0.6},
        {"question_id": 3, "category_id": 1, "cf_user": 0.6, "cf_pakar": 0.7},
        {"question_id": 4, "category_id": 1, "cf_user": 1.0, "cf_pakar": 0.6},
        {"question_id": 5, "category_id": 1, "cf_user": 0.8, "cf_pakar": 0.7}
    ]
    result = calculate_category_cf(answers)
    print("CF Gabung:", result['1']['cf_combined'])
    print("Interpretasi:", get_interpretation(result['1']['cf_combined']))
