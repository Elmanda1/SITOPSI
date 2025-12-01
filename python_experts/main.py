"""Simple JSON CLI for the AI engine.

Usage:
  - Read JSON from stdin (recommended):
      echo '{"user_id": 1, "answers": [...]}' | python main.py
  - Or pass an input file:
      python main.py --input-file input.json

Input JSON format:
{
    "user_id": 123,
    "answers": [
        {"question_id": 1, "option_id": 2, "category_id": 2, "cf_value": 0.6},
        {"question_id": 2, "option_id": 5, "category_id": 1, "cf_value": 0.6},
        ...
    ]
}

The script calls the inference engine in `core.inference` and prints JSON results to stdout.
"""

import sys
import os
import json
import argparse

# Make sure `core` can be imported when executing this script directly
sys.path.insert(0, os.path.dirname(__file__))

from core.inference import infer_from_answers


def main():
    parser = argparse.ArgumentParser(description="Python AI JSON CLI for Minat Bakat IT")
    parser.add_argument("--input-file", "-i", help="Path to JSON input file (optional)")
    args = parser.parse_args()

    try:
        if args.input_file:
            with open(args.input_file, "r", encoding="utf-8") as f:
                data = json.load(f)
        else:
            # read from stdin
            data = json.load(sys.stdin)
    except Exception as e:
        print(json.dumps({"error": f"Failed to read input JSON: {e}"}))
        sys.exit(2)

    # Extract user_id and answers
    user_id = data.get("user_id", 0)
    answers = data.get("answers", [])
    
    if not answers:
        print(json.dumps({
            "error": "No answers provided",
            "expected_format": {
                "user_id": 123,
                "answers": [
                    {"question_id": 1, "option_id": 2, "category_id": 2, "cf_value": 0.6}
                ]
            }
        }))
        sys.exit(3)

    try:
        result = infer_from_answers(user_id, answers)
    except Exception as e:
        print(json.dumps({"error": f"Inference failure: {e}"}))
        sys.exit(4)

    print(json.dumps(result, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
