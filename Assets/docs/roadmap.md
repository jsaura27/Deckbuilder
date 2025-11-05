# Project Roadmap � Rogue-lite Deckbuilder

This roadmap breaks the implementation into phased tasks, mapping requirements to Unity modules. Use the checkboxes to track progress.

## Phase1: Requirements Parsing & Schema Integration
- [x] Task1.1: Parse Requirements (DONE)
 - [x] Read `Assets/docs/requirements.md` and `Assets/docs/requirements.json`
 - [x] Extract gameplay systems, entities, and relationships
 - [x] Map systems to Unity modules (examples below)
 - Cards → `ScriptableObject` definitions + `CardSystem` service (MonoBehaviour)
 - Combat → `CombatManager` (MonoBehaviour), `TurnManager`, `StatusEffectManager`
 - Equipment → `EquipmentDefinition` (ScriptableObject), `EquipmentManager`
 - Blessings → `BlessingDefinition` (ScriptableObject), `BlessingManager`
 - Skill Tree → `SkillTreeDefinition` (ScriptableObject), `SkillTreeService`
- [ ] Task1.2: Load & Validate Schemas (PENDING)
 - [ ] Load schemas from `Assets/docs/schemas/`
 - [ ] Validate any sample JSON data if present
 - [ ] Produce validation report and flag missing/invalid fields
- [ ] Task1.3: Generate C# Data Models (PENDING)
 - [ ] Auto-generate POCO classes from schemas
 - [ ] Add `[Serializable]` and Unity-friendly types
 - [ ] Place generated classes under `Assets/Scripts/DataModels/`

## Phase2: ScriptableObject Scaffolding
 - Status: PENDING
## Phase2: ScriptableObject Scaffolding
- [ ] Task2.1: Create ScriptableObject Base Classes (PENDING)
 - [ ] `CardDefinition : ScriptableObject`
 - [ ] `BlessingDefinition : ScriptableObject`
 - [ ] `EquipmentDefinition : ScriptableObject`
 - [ ] `SkillTreeDefinition : ScriptableObject`
- [ ] Task2.2: Add Schema-Driven Fields
 - [ ] Match public fields to schema properties
 - [ ] Use enums for `Rarity`, `CardType`, `SlotType`
 - [ ] Add `[CreateAssetMenu]` for editor creation
- [ ] Task2.3: JSON Import & Validation Tool (Editor)
 - [ ] Editor tool to load JSON files and validate against schemas
 - [ ] Convert validated JSON into ScriptableObjects
 - [ ] Place tool under `Assets/Editor/ImportTools/`

## Phase3: Core Systems Implementation
 - Status: PENDING
## Phase3: Core Systems Implementation
- [ ] Task3.1: Card System (PENDING)
 - [ ] Deck builder UI and data model
 - [ ] Draw/discard/pile logic with pooling
 - [ ] Effect resolution pipeline (damage, block, statuses)
- [ ] Task3.2: Combat System
 - [ ] Turn-based loop: Draw ? Action ? Enemy ? Resolve
 - [ ] `EnemyAI` simple state machine with data-driven behaviors
 - [ ] `StatusEffectManager` with stacking and durations
- [ ] Task3.3: Equipment System
 - [ ] Equip slots and rules (Weapon, Armor, Trinket)
 - [ ] Passive stat modifiers and card modifiers
 - [ ] Integration with card effects and skill tree
- [ ] Task3.4: Blessing System
 - [ ] Blessing acquisition and altar interaction
 - [ ] Evolution condition tracking and resolution
 - [ ] Blessing effect manager and stacking rules
- [ ] Task3.5: Skill Tree System
 - [ ] Branch selection UI and enforcement (choose1 of4)
 - [ ] Node unlocking, prerequisites and conditional unlocks
 - [ ] Class evolution triggers

- [ ] Task3.6: Performance Baseline & Pooling (PENDING)
 - [ ] Identify allocations & hotspots
 - [ ] Implement object pools
 - [ ] Baseline frame & GC metrics doc
 - [ ] Benchmark harness & report
- [ ] Task3.7: Status Effects Catalog (PENDING)
 - [ ] Enumerate baseline effects & stacking
 - [ ] Data representation (SO/JSON)
 - [ ] Unit tests for effect resolution
 - [ ] Integrate with Combat System

## Phase4: Run Management & Progression
 - Status: PENDING
## Phase4: Run Management & Progression
- [ ] Task4.1: Run State Manager (PENDING)
 - [ ] Track run state: player level, deck, skill tree, blessings, RNG seed
 - [ ] Reset on new run, optional save for debugging
- [ ] Task4.2: Meta Progression (Optional)
 - [ ] Persist unlocks (new classes/cards/blessings/equipment)
 - [ ] Save format and migration plan (versioned JSON)

- [ ] Task4.3: Save System & Migration (PENDING)
 - [ ] Define save schema (meta + optional run snapshot)
 - [ ] Implement serialization + version tag
 - [ ] Migration stub
 - [ ] Load/save tests
- [ ] Task4.4: Achievements Framework (PENDING)
 - [ ] Achievement schema & examples
 - [ ] Evaluation dispatcher
 - [ ] Persistence integration
 - [ ] Sample achievements

## Phase5: Testing & Validation
 - Status: PENDING
## Phase5: Testing & Validation
- [ ] Task5.1: Unit Tests (PENDING)
 - [ ] Use Unity Test Framework or NUnit for core logic
 - [ ] Tests for card effects, combat flow, skill tree resolution
- [ ] Task5.2: Schema Compliance Tests
 - [ ] Run schema validation on all JSON content
 - [ ] Fail build or emit warnings on invalid content
- [ ] Task5.3: Editor Tool QA
 - [ ] Test JSON import, ScriptableObject creation, and editing

- [ ] Task5.4: Run Telemetry Logger (PENDING)
 - [ ] Define event model
 - [ ] Implement JSONL logger
 - [ ] Config toggle & retention
 - [ ] Sample log test
- [ ] Task5.5: Deterministic RNG Seed Harness (PENDING)
 - [ ] Central seed manager
 - [ ] Repeat-run equivalence test
 - [ ] Override mechanism (debug)
 - [ ] Documentation
- [ ] Task5.6: Data Integrity Checks (PENDING)
 - [ ] ID uniqueness scanner
 - [ ] Cross-reference resolver
 - [ ] Report generation
 - [ ] Build-time validation gate

## Phase6: Packaging & CI Integration
 - Status: PENDING
## Phase6: Packaging & CI Integration
- [ ] Task6.1: Folder Structure Enforcement (PENDING)
 - [ ] Validate asset placement and auto-fix misplaced files
- [ ] Task6.2: CI Pipeline Hooks
 - [ ] Add schema validation step to pipeline
 - [ ] Run unit tests in CI
 - [ ] Optional: auto-generate changelog from `Assets/docs/requirements.md`

- [ ] Task6.3: Schema Doc Generation (PENDING)
 - [ ] Parse schemas
 - [ ] Generate markdown docs
 - [ ] Link from requirements

---

## Notes & Implementation Tips
- Use data-driven, versioned JSON manifests for content packs.
- Provide deterministic RNG seed support for reproducible runs.
- Prefer `ScriptableObject` for in-editor content; provide JSON import/export for pipelines.
- Keep systems modular: create services and interfaces so systems are swappable.

Files to create (suggested)
- `Assets/Scripts/DataModels/` (generated C# classes)
- `Assets/Scripts/Systems/CardSystem.cs`, `CombatManager.cs`, `EquipmentManager.cs`, `BlessingManager.cs`, `SkillTreeService.cs`
- `Assets/Editor/ImportTools/JsonSchemaImporter.cs`

---

## Status Lifecycle (Summary)
Each task now moves through statuses: `pending → planned → in-progress → done` (or `error`).

- pending: Defined, no plan folder yet.
- planned: Plan folder with `plan.md` and `plan.json` exists.
- in-progress: Implementation underway.
- done: Implementation completed; build validated.
- error: Implementation failed; requires review.

Automation prompts update these statuses in `roadmap.json`.

Version:1.1 – Lifecycle fields added; Parse Requirements marked done.
