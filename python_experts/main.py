"""Simple JSON CLI for the AI engine.

Usage:
  - Read JSON from stdin (recommended):
      echo '{"facts": ["A", "B"]}' | python main.py
  - Or pass an input file:
      python main.py --input-file input.json

The script loads `data/rules.json` from the same folder and calls the inference
entry point in `core.inference`. Output is printed as JSON to stdout.
"""

import sys
import os
import json
import argparse

# Make sure `core` can be imported when executing this script directly
sys.path.insert(0, os.path.dirname(__file__))

from core.inference import infer


def _load_rules():
    base = os.path.dirname(__file__)
    rules_path = os.path.join(base, "data", "rules.json")
    try:
        with open(rules_path, "r", encoding="utf-8") as f:
            return json.load(f).get("rules", [])
    except Exception:
        return []


def main():
    parser = argparse.ArgumentParser(description="Python AI JSON CLI")
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

    facts = data.get("facts", [])
    rules = _load_rules()

    try:
        result = infer(facts, rules)
    except Exception as e:
        print(json.dumps({"error": f"Inference failure: {e}"}))
        sys.exit(3)

    print(json.dumps(result, ensure_ascii=False))


if __name__ == "__main__":
    main()
