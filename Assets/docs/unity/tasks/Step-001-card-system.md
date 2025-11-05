# Step 001: Card System

```json
{
  "stepId": "Step-001",
  "taskId": "3.1",
  "name": "Card System",
  "category": "System",
  "priority": "P1",
  "effort": "L",
  "risk": "Medium",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["cardSystem"], "roadmap": ["3.1"], "gaps": [] }
}
```

Overview
--------
Expose and validate the existing CardSystem in Unity by providing designer-facing fixtures: a small test scene that allows drawing/discarding, simple CardDefinition ScriptableObject samples, and unit tests for effect resolution.

Prerequisites
-------------
- `CardSystem`, `Deck`, and `Card` runtime classes exist (as per project codebase).
- Sample `CardDefinition` data exists or will be created by implementer as fixtures.
- Unity editor version unspecified (mark TODO if needed).

Deliverables & Placement
------------------------
- `Assets/Scenes/Dev/CardPlayground.unity` — Dev scene for manual testing.
- `Assets/Scripts/Systems/CardSystem/` — location for any small scaffolding helpers (planned, not created here).
- `Assets/Tests/CardSystem/CardSystemTests.cs` — test suggestions.
This planning step does not create these files; they are targets for implementation.

Detailed Implementation Steps
----------------------------
1. Create a dev scene `CardPlayground.unity` with a root `CardPlayground` GameObject.
2. Add a lightweight UI panel named `CardHandPanel` (placeholder) containing elements for drawn cards.
3. Hook the `CardSystem` to `CardHandPanel` via a short-lived designer script (register to draw events).
4. Provide 3 sample `CardDefinition` ScriptableObjects: `attack_common`, `defense_common`, `curse_common` with minimal fields.
5. Add a play-mode checklist that records draw/discard operations to the console for quick validation.
6. Write unit tests for effect resolution using plain C# test fixtures (card damage, heal, status applying).

Data & Configuration Plan
-------------------------
- `CardDefinition` fields: id (string), name (string), cost (int), type (enum), rarity (enum), effects (array of effect descriptors).
- Validate sample SOs against minimal expectations: non-empty id, valid type enum.

Testing Strategy
----------------
- Unit tests:
  - EffectResolution_HappyPath: instantiate card data, call resolution pipeline, assert HP deltas.
  - DrawFromEmptyDeck_Reshuffle: simulate draws until empty, assert reshuffle behavior.
- Play-mode tests:
  - CardPlayground_DrawVisual: start scene, simulate draw input, assert UI updated.
- Edge cases:
  - Empty effect list, malformed effect parameters, extremely large cost values.

Acceptance Criteria
-------------------
1. Designer can open `CardPlayground.unity` and perform a draw that results in visible card representations.
2. Effect resolution unit tests pass locally (no exceptions; expected numeric outcomes).
3. Deck shuffle and discard logic do not duplicate cards or lose cards.

Telemetry / Observability Hooks
------------------------------
- Emit `cardPlayed` event (non-blocking) with fields: cardId, playerId, timestamp, runSeed.

Performance Considerations
--------------------------
- Use pooling for visual card instances to avoid frequent allocations.
- Keep update loops lightweight; offload heavy calculations to discrete resolution calls.

Error Handling & Validation
---------------------------
- Validate card data on load; log warnings for missing fields and fail unit tests if critical.

Dependencies & Ordering
-----------------------
- No hard dependencies beyond basic runtime classes. Prefer to run after data integrity checks (Step-010).

Risks & Mitigations
-------------------
- Risk: Designer scene diverges from runtime expectations. Mitigation: keep data fixtures minimal and mirror runtime types.

Rollback / Disable Plan
------------------------
- Guard dev scene scripts with a `#if UNITY_EDITOR` or feature toggle to disable in production builds.

Extension Points
----------------
- Add card preview inspector for designers (deferred).

Completion Checklist
--------------------
- [ ] Create `CardPlayground.unity` (dev scene)
- [ ] Add 3 sample `CardDefinition` SOs
- [ ] Implement CardHandPanel scaffolding
- [ ] Add unit tests for effect resolution
- [ ] Validate acceptance criteria

Changelog Stub
-------------
When implemented, add a brief note: date, author, files created.
