# Step 008: Run Telemetry Logger

```json
{
  "stepId": "Step-008",
  "taskId": "5.4",
  "name": "Run Telemetry Logger",
  "category": "Telemetry",
  "priority": "P2",
  "effort": "S",
  "risk": "Low",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["telemetry & logging"], "roadmap": ["5.4"], "gaps": ["GAP-002"] }
}
```

Overview
--------
Document event schemas and locations for telemetry hook insertion; supply sample payloads and validation tasks to ensure seed/version presence.

Detailed Implementation Steps
----------------------------
1. Define JSON schemas for core events (cardPlayed, damageDealt, runEnd).
2. Provide sample payloads and unit tests to validate schema compliance.
3. Create a telemetry wiring document describing where to emit events in Combat and Card flows.

Completion Checklist
--------------------
- [ ] Define schemas
- [ ] Add sample payload tests
- [ ] Publish wiring doc
