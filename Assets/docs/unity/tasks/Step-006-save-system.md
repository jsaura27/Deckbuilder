# Step 006: Save System & Migration

```json
{
  "stepId": "Step-006",
  "taskId": "4.3",
  "name": "Save System & Migration",
  "category": "Persistence",
  "priority": "P2",
  "effort": "M",
  "risk": "Medium",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["save system"], "roadmap": ["4.3"], "gaps": [] }
}
```

Overview
--------
Plan verification and test cases for meta-save persistence and migration stubs. Focus on deterministic serialization for meta-progression.

Detailed Implementation Steps
----------------------------
1. Document expected save schema for meta-progression.
2. Create unit tests that serialize sample meta progression, deserialize, and assert equality.
3. Add migration stub tests that simulate older versions moving to current.

Completion Checklist
--------------------
- [ ] Create schema doc
- [ ] Add round-trip tests
- [ ] Implement migration tests
