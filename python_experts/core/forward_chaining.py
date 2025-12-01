from typing import List, Dict, Any
from .certainty_factor import calculate_category_cf


def collect_facts(answers: List[Dict[str, Any]]) -> Dict[str, Any]:
    facts = {
        "total_answers": len(answers),
        "answers_by_category": {},
        "question_ids": [],
        "raw_answers": answers
    }
    
    # Group by category
    for answer in answers:
        cat_id = str(answer.get("category_id"))
        if cat_id not in facts["answers_by_category"]:
            facts["answers_by_category"][cat_id] = []
        facts["answers_by_category"][cat_id].append(answer)
        facts["question_ids"].append(answer.get("question_id"))
    
    return facts


def apply_rules(facts: Dict[str, Any]) -> Dict[str, Any]:
    # Calculate CF untuk setiap kategori
    category_scores = calculate_category_cf(facts["raw_answers"])
    
    # Tambahkan metadata
    for cat_id in category_scores:
        category_scores[cat_id]["facts_used"] = len(facts["answers_by_category"].get(cat_id, []))
    
    return category_scores


def derive_conclusion(category_scores: Dict[str, Any], category_names: Dict[str, str]) -> Dict[str, Any]:
    if not category_scores:
        return {
            "id": 0,
            "name": "Unknown",
            "score": 0.0,
            "answer_count": 0,
            "conclusion": "Tidak ada data yang cukup untuk menentukan kategori"
        }
    
    # Cari kategori dengan CF tertinggi
    # Jika ada tie, pilih yang punya answer_count lebih banyak
    best_cat_id = max(
        category_scores.items(),
        key=lambda x: (x[1]["cf_combined"], x[1]["answer_count"])
    )[0]
    
    best_score = category_scores[best_cat_id]
    
    return {
        "id": int(best_cat_id),
        "name": category_names.get(best_cat_id, f"Category {best_cat_id}"),
        "score": best_score["cf_combined"],
        "answer_count": best_score["answer_count"],
        "conclusion": f"Berdasarkan {best_score['answer_count']} jawaban, minat Anda paling sesuai dengan kategori ini"
    }


def forward_chain(answers: List[Dict[str, Any]], category_names: Dict[str, str] = None) -> Dict[str, Any]:
    if category_names is None:
        category_names = {
            "1": "Backend & Software Engineering",
            "2": "Frontend & Web Design",
            "3": "UI/UX Design",
            "4": "Data, Cyber & System Thinking"
        }
    
    # Step 1: Collect Facts
    facts = collect_facts(answers)
    
    # Step 2: Apply Rules
    category_scores = apply_rules(facts)
    
    # Step 3: Derive Conclusion
    conclusion = derive_conclusion(category_scores, category_names)
    
    # Return complete forward chaining result
    return {
        "facts_collected": {
            "total_answers": facts["total_answers"],
            "categories_involved": list(facts["answers_by_category"].keys())
        },
        "rules_applied": {
            "category_scores": category_scores,
            "total_categories": len(category_scores)
        },
        "conclusion": conclusion
    }


# Testing
if __name__ == "__main__":
    import json
    
    print("=== Forward Chaining Test ===\n")
    
    # Sample data
    sample_answers = [
        {"question_id": 1, "option_id": 1, "category_id": 1, "cf_value": 0.6},
        {"question_id": 2, "option_id": 5, "category_id": 1, "cf_value": 0.6},
        {"question_id": 3, "option_id": 9, "category_id": 1, "cf_value": 0.7},
        {"question_id": 4, "option_id": 13, "category_id": 1, "cf_value": 0.7},
        {"question_id": 5, "option_id": 18, "category_id": 2, "cf_value": 0.6},
        {"question_id": 6, "option_id": 22, "category_id": 2, "cf_value": 0.6},
    ]
    
    # Run forward chaining
    result = forward_chain(sample_answers)
    
    print(json.dumps(result, indent=2, ensure_ascii=False))
    
    print("\n=== Conclusion ===")
    print(f"Kategori: {result['conclusion']['name']}")
    print(f"Skor CF: {result['conclusion']['score']}")
    print(f"Jumlah Jawaban: {result['conclusion']['answer_count']}")

