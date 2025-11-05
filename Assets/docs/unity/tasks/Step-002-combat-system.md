# Step 002: Combat System

```json
{
  "stepId": "Step-002",
  "taskId": "3.2",
  "name": "Combat System",
  "category": "System",
  "priority": "P1",
  "effort": "L",
  "risk": "Medium",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["combatSystem","status effects"], "roadmap": ["3.2","3.7"], "gaps": [] }
}
```

Overview
--------
Create a minimal combat playground scene to exercise turn flow, status effect application/decay, and simple AI patterns. This helps validate CombatManager, TurnManager, and StatusEffectManager integration.

Prerequisites
-------------
- `CombatManager`, `TurnManager`, and `StatusEffectManager` must exist in project code.
- Card System (Step-001) ideally available to feed actions into combat.

Deliverables & Placement
------------------------
- `Assets/Scenes/Dev/CombatPlayground.unity` — dev scene.
- `Assets/Tests/Combat/CombatFlowTests.cs` — unit/integration tests.
This planning step does not create these files; they are targets for implementation.

Detailed Implementation Steps
----------------------------
1. Create `CombatPlayground.unity` with a `CombatManager` GameObject configured for development.
2. Add two simple actors: `PlayerActor` and `EnemyActor` with minimal health and action hooks.
3. Wire a simple patterned AI for `EnemyActor` (e.g., attack every other turn) for deterministic testing.
4. Create a `CombatLogger` that subscribes to combat events and writes a concise trace to console for validation.
5. Add automated play-mode scenario to run N turns and assert no exceptions.

Data & Configuration Plan
-------------------------
- Actor config: health, action set (array of simple actions), status effect list.
- Status effect descriptors: id, duration, stackable, application rules.

Testing Strategy
----------------
- Unit test: StatusEffect_ApplyAndDecay
- Integration test: CombatTurnCycle_NoExceptions
- Edge cases: zero-damage actions, infinite-loop prevention, large number of status stacks

Acceptance Criteria
-------------------
1. CombatPlayground completes automated N-turn run without exceptions.
2. Status effects apply, stack, and decay according to definitions.
3. CombatLogger outputs readable sequence for debugging.

Telemetry / Observability Hooks
------------------------------
- Emit `combatTurnStart`, `combatTurnEnd`, `statusApplied` events with minimal payloads.

Performance Considerations
--------------------------
- Limit per-frame processing; run resolution on discrete phases.

Error Handling & Validation
---------------------------
- Validate actor data; fallback to default stats if missing.

Dependencies & Ordering
-----------------------
- Depends on Step-001 (Card System) for full action testing; can run in isolation with stub actions.

Risks & Mitigations
-------------------
- Risk: Test actor behaviors differ from production AI. Mitigation: keep AI pattern simple and clearly documented.

Rollback / Disable Plan
------------------------
- Mark dev-only components with `#if UNITY_EDITOR` and feature toggles.

Completion Checklist
--------------------
- [ ] Create `CombatPlayground` scene
- [ ] Implement `CombatLogger`
- [ ] Add play-mode automated scenario

Changelog Stub
-------------
Add date/author/files when implemented.
