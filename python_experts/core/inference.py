from typing import List, Dict, Any
from .forward_chaining import forward_chain
from .certainty_factor import calculate_category_cf, get_interpretation
from .utils import (
    get_category_names, 
    get_next_steps, 
    validate_answers,
    format_percentage
)


def infer_from_answers(user_id: int, answers: List[Dict]) -> Dict[str, Any]:
    # Validasi input
    is_valid, error_msg = validate_answers(answers)
    if not is_valid:
        return {
            "error": error_msg,
            "user_id": user_id,
            "total_questions_answered": 0
        }
    
    # Get category names mapping
    category_names = get_category_names()
    
    # Run forward chaining
    fc_result = forward_chain(answers, category_names)
    
    # Calculate detailed scores untuk semua kategori
    category_scores_raw = calculate_category_cf(answers)
    
    # Enrich dengan nama kategori
    category_scores = {}
    for cat_id in category_names.keys():
        if cat_id in category_scores_raw:
            category_scores[cat_id] = {
                "name": category_names[cat_id],
                "cf_combined": category_scores_raw[cat_id]["cf_combined"],
                "answer_count": category_scores_raw[cat_id]["answer_count"],
                "avg_cf_user": category_scores_raw[cat_id]["avg_cf_user"],
                "avg_cf_pakar": category_scores_raw[cat_id]["avg_cf_pakar"],
                "cf_gejala_values": category_scores_raw[cat_id]["cf_gejala_values"]
            }
        else:
            # Kategori tidak dipilih sama sekali
            category_scores[cat_id] = {
                "name": category_names[cat_id],
                "cf_combined": 0.0,
                "answer_count": 0,
                "avg_cf_user": 0.0,
                "avg_cf_pakar": 0.0,
                "cf_gejala_values": []
            }
    
    # Recommended category dari forward chaining conclusion
    recommended = fc_result["conclusion"]
    
    # Build final result
    result = {
        "user_id": user_id,
        "total_questions_answered": len(answers),
        "category_scores": category_scores,
        "recommended_category": {
            "id": recommended["id"],
            "name": recommended["name"],
            "score": recommended["score"],
            "answer_count": recommended["answer_count"]
        },
        "interpretation": get_interpretation(recommended["score"]),
        "next_steps": get_next_steps(recommended["name"]),
        "forward_chaining_trace": {
            "facts_collected": fc_result["facts_collected"],
            "total_categories_scored": fc_result["rules_applied"]["total_categories"]
        },
        "summary": {
            "top_score": format_percentage(recommended["score"]),
            "confidence_level": "Tinggi" if recommended["score"] >= 0.8 else "Sedang" if recommended["score"] >= 0.6 else "Rendah",
            "recommendation": recommended["conclusion"]
        }
    }
    
    return result


def infer(facts: List[str], rules: List[Dict], weights=None) -> List[Dict[str, Any]]:
    return [{
        "message": "Please use infer_from_answers() function",
        "example_input": {
            "user_id": 123,
            "answers": [
                {"question_id": 1, "option_id": 1, "category_id": 1, "cf_value": 0.6}
            ]
        }
    }]


# Testing
if __name__ == "__main__":
    import json
    
    print("=== Inference Engine Test ===\n")
    
    # Sample test data: User dengan kecenderungan Backend
    sample_answers = [
        {"question_id": 1, "option_id": 1, "category_id": 1, "cf_user": 0.8, "cf_pakar": 0.8},
        {"question_id": 2, "option_id": 5, "category_id": 1, "cf_user": 0.8, "cf_pakar": 0.6},
        {"question_id": 3, "option_id": 9, "category_id": 1, "cf_user": 0.6, "cf_pakar": 0.7},
        {"question_id": 4, "option_id": 13, "category_id": 1, "cf_user": 1.0, "cf_pakar": 0.6},
        {"question_id": 5, "option_id": 17, "category_id": 1, "cf_user": 0.8, "cf_pakar": 0.7},
        {"question_id": 6, "option_id": 22, "category_id": 2, "cf_user": 0.6, "cf_pakar": 0.6},
        {"question_id": 7, "option_id": 26, "category_id": 2, "cf_user": 0.4, "cf_pakar": 0.6},
    ]
    
    result = infer_from_answers(user_id=999, answers=sample_answers)
    
    print(json.dumps(result, indent=2, ensure_ascii=False))
    
    print("\n=== Summary ===")
    print(f"User ID: {result['user_id']}")
    print(f"Total Jawaban: {result['total_questions_answered']}")
    print(f"Kategori Rekomendasi: {result['recommended_category']['name']}")
    print(f"Skor: {result['summary']['top_score']}")
    print(f"Confidence: {result['summary']['confidence_level']}")
    print(f"\nInterpretasi: {result['interpretation']}")
    print(f"\nLangkah Selanjutnya:\n{result['next_steps'][:150]}...")

