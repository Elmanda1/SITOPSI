import json
import os
from typing import Dict, Any, Optional


def load_json(path: str) -> Dict[str, Any]:

    with open(path, 'r', encoding='utf-8') as f:
        return json.load(f)


def save_json(data: Dict[str, Any], path: str, indent: int = 2) -> None:
    with open(path, 'w', encoding='utf-8') as f:
        json.dump(data, f, indent=indent, ensure_ascii=False)


def get_category_names(custom_names: Dict[str, str] = None) -> Dict[str, str]:
    """
    Get category names. Bisa dari parameter (query DB dari VB) atau default.
    
    Args:
        custom_names: Dict category names dari database (optional)
                     Format: {"1": "Backend", "2": "Frontend", ...}
    
    Returns:
        Dict of category_id -> category_name
    """
    if custom_names:
        return custom_names
    
    # Default fallback jika tidak ada custom names
    return {
        "1": "Backend & Software Engineering",
        "2": "Frontend & Web Design",
        "3": "UI/UX Design",
        "4": "Data, Cyber & System Thinking"
    }

def get_next_steps(category_name: str) -> str:
    suggestions = {
        "Backend & Software Engineering": 
            "Pelajari framework seperti Laravel, Node.js/Express, atau Spring Boot. "
            "Kuasai database (MySQL, PostgreSQL, MongoDB) dan API design (REST, GraphQL). "
            "Pahami konsep microservices, Docker, dan CI/CD.",
        
        "Frontend & Web Design": 
            "Fokus pada React/Vue/Angular, CSS modern (Tailwind CSS, Sass), dan responsive design. "
            "Pelajari Web Performance Optimization, Progressive Web Apps (PWA). "
            "Bangun portofolio visual yang menarik.",
        
        "UI/UX Design": 
            "Kuasai tools design seperti Figma, Adobe XD, atau Sketch. "
            "Pelajari design thinking, user research, dan usability testing. "
            "Pahami prinsip visual hierarchy, typography, dan color theory.",
        
        "Data, Cyber & System Thinking": 
            "Pelajari Python untuk data science (Pandas, NumPy, Scikit-learn). "
            "Kuasai SQL advanced dan database optimization. "
            "Untuk cybersecurity: pelajari network security, penetration testing. "
            "Untuk system thinking: pahami systems analysis dan enterprise architecture."
    }
    
    return suggestions.get(
        category_name, 
        "Konsultasikan dengan dosen pembimbing untuk menentukan topik skripsi yang sesuai."
    )


def validate_answer_format(answer: Dict[str, Any]) -> bool:
    """
    Validate format jawaban user.
    
    Format BARU (dengan 2 TAHAP CF):
    {
        "question_id": 1,
        "option_id": 2,
        "category_id": 1,
        "cf_user": 0.8,      # Tingkat keyakinan user
        "cf_pakar": 0.6      # Bobot dari database
    }
    
    Legacy format (deprecated):
    {
        "question_id": 1,
        "option_id": 2,
        "category_id": 1,
        "cf_value": 0.6      # Sudah tidak digunakan
    }
    """
    # Check format baru (prioritas)
    if "cf_user" in answer and "cf_pakar" in answer:
        required_keys = ["question_id", "option_id", "category_id", "cf_user", "cf_pakar"]
        
        # Check semua key ada
        if not all(key in answer for key in required_keys):
            return False
        
        # Check tipe data
        try:
            int(answer["question_id"])
            int(answer["option_id"])
            int(answer["category_id"])
            cf_user = float(answer["cf_user"])
            cf_pakar = float(answer["cf_pakar"])
            
            # CF harus 0-1
            if not (0.0 <= cf_user <= 1.0):
                return False
            if not (0.0 <= cf_pakar <= 1.0):
                return False
            
            return True
        except (ValueError, TypeError):
            return False
    
    # Legacy format (backward compatibility)
    elif "cf_value" in answer:
        required_keys = ["question_id", "option_id", "category_id", "cf_value"]
        
        if not all(key in answer for key in required_keys):
            return False
        
        try:
            int(answer["question_id"])
            int(answer["option_id"])
            int(answer["category_id"])
            cf = float(answer["cf_value"])
            
            if cf < 0 or cf > 1:
                return False
            
            return True
        except (ValueError, TypeError):
            return False
    
    return False


def validate_answers(answers: list) -> tuple[bool, Optional[str]]:

    if not answers:
        return False, "No answers provided"
    
    if not isinstance(answers, list):
        return False, "Answers must be a list"
    
    for i, answer in enumerate(answers):
        if not isinstance(answer, dict):
            return False, f"Answer {i+1} is not a dictionary"
        
        if not validate_answer_format(answer):
            return False, f"Answer {i+1} has invalid format. Required keys: question_id, option_id, category_id, cf_value"
    
    return True, None


def format_percentage(value: float, decimals: int = 1) -> str:
    return f"{value * 100:.{decimals}f}%"


def get_script_dir() -> str:
    return os.path.dirname(os.path.abspath(__file__))


def get_project_root() -> str:
    return os.path.dirname(get_script_dir())


# Testing
if __name__ == "__main__":
    print("=== Utils Test ===\n")
    
    # Test 1: Category names
    print("Test 1: Category Names")
    categories = get_category_names()
    for cat_id, name in categories.items():
        print(f"  {cat_id}: {name}")
    
    # Test 2: Next steps
    print("\nTest 2: Next Steps")
    print(get_next_steps("Backend & Software Engineering")[:100] + "...")
    
    # Test 3: Validate answer
    print("\nTest 3: Answer Validation")
    valid_answer = {
        "question_id": 1,
        "option_id": 2,
        "category_id": 1,
        "cf_value": 0.6
    }
    invalid_answer = {
        "question_id": 1,
        "cf_value": 1.5  # Invalid CF
    }
    
    print(f"Valid answer: {validate_answer_format(valid_answer)}")
    print(f"Invalid answer: {validate_answer_format(invalid_answer)}")
    
    # Test 4: Validate answers list
    print("\nTest 4: Answers List Validation")
    valid, error = validate_answers([valid_answer])
    print(f"Valid list: {valid}, Error: {error}")
    
    valid, error = validate_answers([invalid_answer])
    print(f"Invalid list: {valid}, Error: {error}")
    
    # Test 5: Format percentage
    print("\nTest 5: Format Percentage")
    print(f"0.8523 = {format_percentage(0.8523)}")
    print(f"0.936 = {format_percentage(0.936, 2)}")
    
    # Test 6: Paths
    print("\nTest 6: Paths")
    print(f"Script dir: {get_script_dir()}")
    print(f"Project root: {get_project_root()}")