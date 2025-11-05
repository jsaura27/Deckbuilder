---
title: "Unity Implementation Tasks - Generated"
generated: "2025-11-04T12:00:00Z"
source:
  requirements: "Assets/docs/requirements.md"
  requirements_json: "Assets/docs/requirements.json"
  roadmap: "Assets/docs/roadmap.json"
---

# Unity Implementation Tasks

Overview
-------
This file translates the project's requirements and roadmap into prioritized, actionable Unity-facing tasks designed to make implemented systems usable in the Unity editor and runtime. It is read-only: it prescribes work but does not modify the repository.

Environment assumptions
-----------------------
- Target platforms: Windows, macOS (from requirements)
- Unity version: TODO (infer or specify by implementer)

Task Catalog
------------

### [3.1] Card System
Card runtime, deck management, drawing/discard logic and effect resolution.
```json
{
  "id": "3.1",
  "name": "Card System",
  "category": "System",
  "objective": "Expose the CardSystem runtime to Unity via test scenes and data fixtures so designers can validate draw/discard and effect resolution.",
  "rationale": "Core gameplay system (requirements.cardSystem) and roadmap task 3.1; high gameplay visibility.",
  "deliverables": ["Assets/Scripts/Systems/CardSystem/","Assets/docs/unity/tasks/Step-001-card-system.md"],
  "codePlacement": ["Assets/Scripts/Systems/CardSystem/"],
  "artifacts": {"runtime": ["CardSystem","Deck","Card"], "data": ["CardDefinition (ScriptableObject)"] , "tests": ["Assets/Tests/CardSystem/CardSystemTests.cs"]},
  "dependencies": [],
  "acceptanceCriteria": ["A designer can open a test scene and manually perform a draw to observe card instances populated in a CardHand view","Effect resolution produces expected HP/delta given a sample CardDefinition","Deck shuffle and discard behave per rules (no duplicates introduced)"] ,
  "testStrategy": "Create play-mode test scene + unit tests for effect resolution; use designer fixture SOs for cards.",
  "risk": "Medium",
  "effort": "L",
  "status": "pending",
  "priority": "P1",
  "tags": ["core","cards"],
  "notes": "Prefer minimal designer-facing scene rather than full UI; focus on data fixtures and logging.",
  "origin": {"requirements": ["cardSystem"], "roadmap": ["3.1"], "gaps": []}
}
```

### [3.2] Combat System
Turn manager, enemy AI hook, status effect integration.
```json
{
  "id": "3.2",
  "name": "Combat System",
  "category": "System",
  "objective": "Provide a minimal combat playground scene that exercises turn flow, status effects, and simple AI behaviors to validate runtime integration.",
  "rationale": "Core loop (requirements.combatSystem) and roadmap task 3.2; enables playtesting.",
  "deliverables": ["Assets/Scenes/Dev/CombatPlayground.unity","Assets/docs/unity/tasks/Step-002-combat-system.md"],
  "codePlacement": ["Assets/Scripts/Systems/Combat/","Assets/Scripts/AI/"],
  "artifacts": {"runtime": ["CombatManager","TurnManager","StatusEffectManager"], "data": [], "tests": ["Assets/Tests/Combat/CombatFlowTests.cs"]},
  "dependencies": ["3.1"],
  "acceptanceCriteria": ["Combat playground runs a full turn cycle without exceptions","Status effects apply and decay per duration rules","Enemy AI performs at least one defined action pattern"],
  "testStrategy": "Play-mode scenario that runs automated turn cycles; unit tests for status effect resolution.",
  "risk": "Medium",
  "effort": "L",
  "status": "pending",
  "priority": "P1",
  "tags": ["core","combat"],
  "notes": "Keep enemy AI minimal (patterned) for initial validation.",
  "origin": {"requirements": ["combatSystem","status effects"], "roadmap": ["3.2","3.7"], "gaps": []}
}
```

### [3.4] Blessing System
Altar interactions, blessing evolution, stacking rules.
```json
{
  "id": "3.4",
  "name": "Blessing System",
  "category": "System",
  "objective": "Create validation tasks and data fixtures to exercise Blessing acquisition and evolution logic within a designer scene.",
  "rationale": "Requirements define blessing evolution; roadmap 3.4 shows partial completion requiring Unity-side validation.",
  "deliverables": ["Assets/Scripts/Systems/Blessings/","Assets/docs/unity/tasks/Step-003-blessing-system.md"],
  "codePlacement": ["Assets/Scripts/Systems/Blessings/"],
  "artifacts": {"runtime": ["BlessingManager","BlessingDefinition"], "data": ["BlessingDefinition (SO samples)"], "tests": ["Assets/Tests/Blessings/BlessingEvolutionTests.cs"]},
  "dependencies": [],
  "acceptanceCriteria": ["Blessing evolution conditions evaluate and progress stages","Stacking and exclusivity rules respected in simulations","Designer can trigger evolution scenarios via dev console or scene UI"],
  "testStrategy": "Unit tests for evaluation logic; play-mode scene to walk evolution scenarios.",
  "risk": "Medium",
  "effort": "M",
  "status": "pending",
  "priority": "P2",
  "tags": ["blessings","data"],
  "notes": "Create small sample SOs for common/rare/epic chains as fixtures.",
  "origin": {"requirements": ["blessingsSystem"], "roadmap": ["3.4"], "gaps": []}
}
```

### [3.3] Equipment System
Equip slots, passive modifiers, card modifiers.
```json
{
  "id": "3.3",
  "name": "Equipment System",
  "category": "System",
  "objective": "Provide data fixtures and a simple equip UI panel to validate slot rules and item effects against Card/Combat systems.",
  "rationale": "Requirements for equipment interactions with combat and cards; roadmap 3.3.",
  "deliverables": ["Assets/Scripts/Systems/Equipment/","Assets/docs/unity/tasks/Step-004-equipment-system.md"],
  "codePlacement": ["Assets/Scripts/Systems/Equipment/","Assets/Editor/Tools/"],
  "artifacts": {"runtime": ["EquipmentManager","EquipmentDefinition"], "data": ["EquipmentDefinition (SO samples)"], "tests": ["Assets/Tests/Equipment/EquipmentTests.cs"]},
  "dependencies": ["3.1","3.2"],
  "acceptanceCriteria": ["Equipping an item applies passive stat modifiers","Card modifiers (e.g., +1 draw) affect subsequent draws","Slot rules (one per slot) enforced"],
  "testStrategy": "Unit tests for modifier application + play-mode equip panel test.",
  "risk": "Low",
  "effort": "M",
  "status": "pending",
  "priority": "P2",
  "tags": ["equipment","integration"],
  "notes": "Focus on data-driven definitions and clear mapping to CardSystem hooks.",
  "origin": {"requirements": ["equipmentSystem"], "roadmap": ["3.3"], "gaps": []}
}
```

### [3.5] Skill Tree System
Branch selection UI, node unlocking, class evolution triggers.
```json
{
  "id": "3.5",
  "name": "Skill Tree System",
  "category": "System",
  "objective": "Produce a designer-facing skill tree selection scene and data fixtures to validate branch selection, node prerequisites, and evolution triggers.",
  "rationale": "High impact on player progression and run design (requirements.skillTree); roadmap 3.5.",
  "deliverables": ["Assets/Scripts/Systems/SkillTree/","Assets/docs/unity/tasks/Step-005-skill-tree-system.md"],
  "codePlacement": ["Assets/Scripts/Systems/SkillTree/","Assets/Editor/"],
  "artifacts": {"runtime": ["SkillTreeService","SkillNode"], "data": ["SkillTreeDefinition (SO samples)"], "tests": ["Assets/Tests/SkillTree/SkillTreeTests.cs"]},
  "dependencies": [],
  "acceptanceCriteria": ["Player can select exactly one branch at run start in the designer scene","Node prerequisites prevent selection until met","Evolution triggers fire when nodes' conditions satisfied"],
  "testStrategy": "Unit tests for node logic; play-mode branch selection scenario.",
  "risk": "Medium",
  "effort": "M",
  "status": "pending",
  "priority": "P1",
  "tags": ["skilltree","progression"],
  "notes": "Keep UI scaffold minimal; data validation is primary.",
  "origin": {"requirements": ["skillTree"], "roadmap": ["3.5"], "gaps": []}
}
```

### [4.3] Save System & Migration
Versioned meta-save and migration tests.
```json
{
  "id": "4.3",
  "name": "Save System & Migration",
  "category": "Persistence",
  "objective": "Provide test cases and migration verification tasks for meta progression saves and optional run snapshots.",
  "rationale": "Requirements: persistence & roadmap 4.3.",
  "deliverables": ["Assets/Scripts/Save/","Assets/docs/unity/tasks/Step-006-save-system.md"],
  "codePlacement": ["Assets/Scripts/Save/"],
  "artifacts": {"runtime": ["SaveManager","MigrationRegistry"], "data": [], "tests": ["Assets/Tests/Save/SaveTests.cs"]},
  "dependencies": [],
  "acceptanceCriteria": ["Meta progression saves and loads across versions","Migration stubs apply transformations without data loss","Save serialization is deterministic with seed metadata"],
  "testStrategy": "Unit tests for serialization + migration paths; round-trip tests with sample data.",
  "risk": "Medium",
  "effort": "M",
  "status": "pending",
  "priority": "P2",
  "tags": ["persistence","save"],
  "notes": "Start with meta progression; run snapshot optional.",
  "origin": {"requirements": ["save system"], "roadmap": ["4.3"], "gaps": []}
}
```

### [4.4] Achievements Framework
Schema, evaluation dispatcher, persistence hooks.
```json
{
  "id": "4.4",
  "name": "Achievements Framework",
  "category": "System",
  "objective": "Define evaluation triggers and provide a mapping doc that links telemetry events to achievement criteria; include sample achievement fixtures.",
  "rationale": "Requirements: achievements; roadmap 4.4 completed in code but needs Unity-side mapping.",
  "deliverables": ["Assets/Docs/Achievements/","Assets/docs/unity/tasks/Step-007-achievements-framework.md"],
  "codePlacement": ["Assets/Scripts/Systems/Achievements/"],
  "artifacts": {"runtime": ["AchievementManager","AchievementDefinition"], "data": ["AchievementDefinition (fixtures)"], "tests": ["Assets/Tests/Achievements/AchievementTests.cs"]},
  "dependencies": ["5.4"],
  "acceptanceCriteria": ["Achievement evaluation triggers can be exercised via designer scenes","Sample achievements persist to meta-save"],
  "testStrategy": "Unit tests for evaluation dispatcher; integration test triggering via telemetry stubs.",
  "risk": "Low",
  "effort": "S",
  "status": "pending",
  "priority": "P3",
  "tags": ["achievements","meta"],
  "notes": "Provide wiring doc rather than full editor UI initially.",
  "origin": {"requirements": ["achievements"], "roadmap": ["4.4"], "gaps": []}
}
```

### [5.4] Run Telemetry Logger
Structured JSONL event logging for runs.
```json
{
  "id": "5.4",
  "name": "Run Telemetry Logger",
  "category": "Telemetry",
  "objective": "Document where telemetry hooks should be placed and provide sample event schemas; include validation tasks to ensure events contain seed and version metadata.",
  "rationale": "Telemetry & logging in requirements; roadmap 5.4 pending.",
  "deliverables": ["Assets/Docs/Telemetry/","Assets/docs/unity/tasks/Step-008-run-telemetry-logger.md"],
  "codePlacement": ["Assets/Scripts/Systems/Telemetry/"],
  "artifacts": {"runtime": ["TelemetryLogger"], "data": [], "tests": ["Assets/Tests/Telemetry/TelemetryTests.cs"]},
  "dependencies": [],
  "acceptanceCriteria": ["Event schemas defined and validated against sample payloads","Events include seed, run id, and version"],
  "testStrategy": "Schema validation unit tests; integration smoke with combat/card events.",
  "risk": "Low",
  "effort": "S",
  "status": "pending",
  "priority": "P2",
  "tags": ["telemetry","logging"],
  "notes": "Prefer non-blocking hooks and toggleable logging.",
  "origin": {"requirements": ["telemetry & logging"], "roadmap": ["5.4"], "gaps": ["GAP-002"]}
}
```

### [5.5] Deterministic RNG Seed Harness
Central seed manager & repeat-run tests.
```json
{
  "id": "5.5",
  "name": "Deterministic RNG Seed Harness",
  "category": "Testing",
  "objective": "Provide tasks to integrate a central RNG seed manager and create repeatable run validation scenes to ensure deterministic behavior given seed+content version.",
  "rationale": "Deterministic RNG is required for reproducible runs and debugging (requirements).",
  "deliverables": ["Assets/Scripts/Systems/RNG/","Assets/docs/unity/tasks/Step-009-rng-seed-harness.md"],
  "codePlacement": ["Assets/Scripts/Systems/RNG/"],
  "artifacts": {"runtime": ["SeedManager"], "data": [], "tests": ["Assets/Tests/RNG/SeedTests.cs"]},
  "dependencies": [],
  "acceptanceCriteria": ["Given seed X and content version Y, a deterministic sequence of RNG draws reproduces prior run events","Seed recorded in telemetry and saves"],
  "testStrategy": "Round-trip integration tests generating deterministic runs; unit tests for SeedManager behavior.",
  "risk": "Low",
  "effort": "S",
  "status": "pending",
  "priority": "P2",
  "tags": ["determinism","testing"],
  "notes": "Design for pluggable RNG implementations.",
  "origin": {"requirements": ["deterministic RNG"], "roadmap": ["5.5"], "gaps": ["GAP-004"]}
}
```

### [5.6] Data Integrity Checks
ID uniqueness, cross-reference validation and build-time gating.
```json
{
  "id": "5.6",
  "name": "Data Integrity Checks",
  "category": "Validation",
  "objective": "Create tasks to implement data validation tools or checklists that verify unique IDs, cross-references, and schema compliance for all content assets.",
  "rationale": "Ensure content quality and prevent runtime errors caused by invalid references (requirements).",
  "deliverables": ["Assets/Editor/Validation/","Assets/docs/unity/tasks/Step-010-data-integrity-checks.md"],
  "codePlacement": ["Assets/Editor/Validation/"],
  "artifacts": {"runtime": [], "data": [], "tests": ["Assets/Tests/Validation/ContentValidationTests.cs"]},
  "dependencies": [],
  "acceptanceCriteria": ["Scanner identifies duplicate IDs and missing references","Validation report generated and human-readable"],
  "testStrategy": "Unit tests for validation rules; sample broken fixtures to assert detection.",
  "risk": "Low",
  "effort": "M",
  "status": "pending",
  "priority": "P1",
  "tags": ["validation","editor"],
  "notes": "Prefer editor-only validation under UNITY_EDITOR.",
  "origin": {"requirements": ["data validation"], "roadmap": ["5.6"], "gaps": ["GAP-007"]}
}
```

Dependency Graph
----------------
- 3.1 (Card System) → 3.2 (Combat System), 3.3 (Equipment)
- 3.2 → 3.3 (Equipment) optional
- 5.6 (Data Integrity) is independent but recommended early

Priority Matrix (summary)
- P1: 3.1 (Card System), 3.2 (Combat System), 3.5 (Skill Tree), 5.6 (Data Integrity)
- P2: 3.3 (Equipment), 3.4 (Blessings), 4.3 (Save System), 5.4 (Telemetry), 5.5 (RNG)
- P3: 4.4 (Achievements Framework)

Immediate Sprint Picks (top 6)
1. 3.1 — Card System (P1)
2. 3.2 — Combat System (P1)
3. 3.5 — Skill Tree System (P1)
4. 5.6 — Data Integrity Checks (P1)
5. 3.3 — Equipment System (P2)
6. 3.4 — Blessing System (P2)

Validation & Gaps
------------------
- Schemas: Ensure `Assets/docs/schemas/` contains cards/blessings/equipment/skilltree schemas (requirements expect them). If missing, add a validation task.
- Roadmap-pending tasks (phase 5/6): create follow-up tasks for Unit Tests, Schema Compliance, Editor QA when ready for Unity test runner.

Future Considerations
- Editor import tools for JSON → ScriptableObject conversion (Phase2.3)
- CI gating for schema validation and unit tests (Phase6.2)

Counts & Summary
- totalTasks: 11
- byCategory: System:7, Persistence:1, Telemetry:1, Testing:1, Validation:1
- byPriority: P1:4, P2:6, P3:1
- followUpsAdded: 0
- gapsAddressed: ["schemas presence check", "unit tests planning"]

Changelog Note
------------
Generated following `Assets/docs/Prompts/Unity-tasks-creator.prompt.md` rules. This file is intended to be read-only; implementers should create per-task step plans using `Assets/docs/Prompts/tasks-planner.prompt.md`.

---
Generated by automation on 2025-11-04T12:00:00Z
