# Step 009: Deterministic RNG Seed Harness

```json
{
  "stepId": "Step-009",
  "taskId": "5.5",
  "name": "Deterministic RNG Seed Harness",
  "category": "Testing",
  "priority": "P2",
  "effort": "S",
  "risk": "Low",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["deterministic RNG"], "roadmap": ["5.5"], "gaps": ["GAP-004"] }
}
```

Overview
--------
Plan tasks to integrate a SeedManager and create repeatable run validation scenarios. Focus on tests that assert identical event sequences given identical seed+content version.

Detailed Implementation Steps
----------------------------
1. Document the SeedManager responsibilities and API surface.
2. Add round-trip tests that run a simulated run with a given seed and compare event logs between runs.

Completion Checklist
--------------------
- [ ] Document API
- [ ] Add round-trip tests
