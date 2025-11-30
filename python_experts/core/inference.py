# Minimal inference implementation for demo/testing.
from typing import List, Dict


def infer(facts: List[str], rules: List[Dict], weights=None):
    """A tiny inference stub that demonstrates output shape.

    This returns a list of result dictionaries, which the VB app can parse.
    Replace with real forward-chaining + certainty-factor logic later.
    """
    summary = {
        "category": "DemoCategory",
        "score": 0.9 if len(facts) > 0 else 0.0,
        "details": {
            "facts_received": facts,
            "rules_loaded": len(rules) if rules is not None else 0,
        },
    }
    return [summary]
