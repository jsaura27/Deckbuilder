# Step 003: Blessing System

```json
{
  "stepId": "Step-003",
  "taskId": "3.4",
  "name": "Blessing System",
  "category": "System",
  "priority": "P2",
  "effort": "M",
  "risk": "Medium",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["blessingsSystem"], "roadmap": ["3.4"], "gaps": [] }
}
```

Overview
--------
Provide validation scaffolds and data fixtures to exercise blessing acquisition, stacking rules, and evolution stages using designer-friendly triggers.

Prerequisites
-------------
- `BlessingManager` and `BlessingDefinition` runtime structures must exist.

Deliverables & Placement
------------------------
- `Assets/Scenes/Dev/BlessingPlayground.unity` — scene for scenario execution.
- `Assets/Tests/Blessings/BlessingEvolutionTests.cs` — unit tests.
This planning step does not create these files; they are targets for implementation.

Detailed Implementation Steps
----------------------------
1. Create `BlessingPlayground.unity` with a simple altar interaction GameObject.
2. Add designer-accessible triggers to simulate events (e.g., "win X battles without damage").
3. Seed the scene with 3 sample `BlessingDefinition` SOs that include evolution stages.
4. Implement a `BlessingDebugger` (editor-only) to force-evaluate evolution conditions and display results.
5. Write unit tests verifying evolution path progression for sample blessings.

Data & Configuration Plan
-------------------------
- BlessingDefinition fields: id, name, rarity, baseEffect, evolutionStages (array with condition expressions).
- Define a minimal condition expression language or map to code-driven predicates.

Testing Strategy
----------------
- Unit tests: EvolutionCondition_EvaluatesCorrectly, StackingRules_Enforced
- Play-mode scenario: BlessingPlayground_EvolutionFlow

Acceptance Criteria
-------------------
1. Evolution conditions evaluate as expected for sample blessings.
2. Stacking and exclusivity rules are enforced in simulation.

Telemetry / Observability Hooks
------------------------------
- Log `blessingAcquired` and `blessingEvolved` events with blessingId and run context.

Risks & Mitigations
-------------------
- Risk: Complex condition expressions are hard to test. Mitigation: start with explicit predicates and defer DSL support.

Completion Checklist
--------------------
- [ ] Add sample blessing SOs
- [ ] Create BlessingPlayground scene
- [ ] Implement BlessingDebugger (editor-only)
- [ ] Add unit tests for evolution logic

Changelog Stub
-------------
Record implementer notes when done.
