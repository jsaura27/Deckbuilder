# Step 010: Data Integrity Checks

```json
{
  "stepId": "Step-010",
  "taskId": "5.6",
  "name": "Data Integrity Checks",
  "category": "Validation",
  "priority": "P1",
  "effort": "M",
  "risk": "Low",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["data validation"], "roadmap": ["5.6"], "gaps": ["GAP-007"] }
}
```

Overview
--------
Create validation tasks and editor checks that scan content for duplicate IDs, missing cross-references, and schema non-compliance. Provide sample broken fixtures for test coverage.

Detailed Implementation Steps
----------------------------
1. Enumerate content roots to scan (`Assets/Content/`, `Assets/Entities/`, `Assets/ScriptsableObjects/` if present).
2. Implement validation rules and unit tests that assert detection of known broken fixtures.
3. Add a scriptable validation report generator writing human-readable summaries.

Completion Checklist
--------------------
- [ ] Create broken fixtures
- [ ] Implement validation tests
- [ ] Produce validation report sample
