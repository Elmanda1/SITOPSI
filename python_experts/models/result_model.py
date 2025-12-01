"""
Data models untuk hasil inference.

Note: Model ini opsional karena Python inference menggunakan dict biasa.
File ini disediakan untuk type hints dan dokumentasi struktur data.
"""

from dataclasses import dataclass
from typing import List, Dict, Optional


@dataclass
class CategoryScore:
    """Skor untuk satu kategori minat"""
    name: str
    cf_combined: float
    answer_count: int
    raw_cfs: List[float]


@dataclass
class RecommendedCategory:
    """Kategori yang direkomendasikan"""
    id: int
    name: str
    score: float
    answer_count: int


@dataclass
class InferenceResult:
    """Hasil lengkap dari inference engine"""
    user_id: int
    total_questions_answered: int
    category_scores: Dict[str, CategoryScore]
    recommended_category: RecommendedCategory
    interpretation: str
    next_steps: str
    topic_recommendations: Optional[List[Dict]] = None


# Example usage (for documentation):
if __name__ == "__main__":
    example = InferenceResult(
        user_id=123,
        total_questions_answered=100,
        category_scores={
            "1": CategoryScore(
                name="Backend & Software Engineering",
                cf_combined=0.8523,
                answer_count=35,
                raw_cfs=[0.6, 0.6, 0.7]
            )
        },
        recommended_category=RecommendedCategory(
            id=1,
            name="Backend & Software Engineering",
            score=0.8523,
            answer_count=35
        ),
        interpretation="Sangat Kuat",
        next_steps="Pelajari Laravel, Node.js..."
    )
    
    print(f"Example structure: {example}")

