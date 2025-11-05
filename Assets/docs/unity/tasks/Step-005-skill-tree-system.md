# Step 005: Skill Tree System

```json
{
  "stepId": "Step-005",
  "taskId": "3.5",
  "name": "Skill Tree System",
  "category": "System",
  "priority": "P1",
  "effort": "M",
  "risk": "Medium",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["skillTree"], "roadmap": ["3.5"], "gaps": [] }
}
```

Overview
--------
Create a designer-facing skill tree selection scene and fixture data to validate branch selection and node prerequisites. Ensure evolution triggers are testable.

Detailed Implementation Steps
----------------------------
1. Produce `SkillTreePlayground.unity` with UI placeholders for four branches.
2. Add sample `SkillTreeDefinition` SOs with nodes, costs, and prerequisites.
3. Implement a minimal selection controller that enforces "choose one branch per run".
4. Write unit tests for node prerequisite enforcement and evolution triggers.

Completion Checklist
--------------------
- [ ] Create playground scene
- [ ] Add sample SOs
- [ ] Implement selection controller
- [ ] Add tests
