# Task: Achievements Framework

## Objective

Design and deliver an Achievements Framework for the game that defines achievement schemas, an evaluation dispatcher, persistence integration, and sample achievements so designers can author and ship achievements reliably.

## Prerequisites

- `Assets/docs/requirements.md` (gameplay and progression requirements)
- `Assets/docs/schemas/` (to reference or extend existing schemas)
- Existing persistence/save system (see `Assets/docs/plans/save-system-migration`)
- Access to Unity Editor for wiring ScriptableObjects and testing

## Step-by-Step Instructions

1. Discovery & Schema
   - Inspect `Assets/docs/schemas/` and `Assets/docs/requirements.md` to identify existing event and progression models.
   - Define an `achievement.schema.json` that captures: id, title, description, criteria (event filters), thresholds, rewards, category, visibility, and version.
   - Map reward types (unlock, currency, cosmetic, meta-progression) to existing game systems.

2. Data Representation
   - Create a C# data model (POCO) for achievements under `Assets/Scripts/DataModels/` (serializable for runtime and tooling).
   - Add a ScriptableObject authoring type at `Assets/Scripts/ScriptableObjects/AchievementDefinition.cs` with CreateAssetMenu for designers.

3. Evaluation Dispatcher
   - Implement an `AchievementEvaluator` module that:
     - Subscribes to in-game events (use existing event bus or add a lightweight dispatcher).
     - Tracks progress per-run and persistent progress where required.
     - Evaluates criteria (simple comparisons, counters, composite conditions).

4. Persistence & Integration
   - Extend the save system to store unlocked achievements and persistent progress. Reference `Assets/docs/plans/save-system-migration` for schema and migration patterns.
   - Provide migration stubs for future schema changes.

5. Editor Tools
   - Provide an editor window to bulk-import/validate achievement JSON against `achievement.schema.json` and to preview rewards.
   - Add unit/editor tests to validate evaluation logic for common conditions.

6. Sample Content
   - Create 5 sample achievements (tutorial, first win, deck collector, combo master, long run) as ScriptableObjects in `Assets/Content/Achievements/`.

7. QA & Docs
   - Write usage documentation in `Assets/docs/plans/achievements-framework/README.md` (short) and link to `Assets/docs/requirements.md`.
   - Run unit tests and a quick Unity playtest to validate unlock flow.

## Deliverables

- `Assets/docs/plans/achievements-framework/plan.json` (machine-readable plan)
- `Assets/docs/plans/achievements-framework/plan.md` (this human plan)
- `Assets/docs/schemas/achievement.schema.json` (schema file)
- `Assets/Scripts/DataModels/AchievementModel.cs` (POCO)
- `Assets/Scripts/ScriptableObjects/AchievementDefinition.cs` (SO authoring asset)
- `Assets/Scripts/Systems/AchievementEvaluator.cs` (runtime evaluator)
- `Assets/Content/Achievements/` (sample achievements)
- Editor import/validation window and unit tests

## Notes

- Use relative paths when referencing project docs (e.g., `Assets/docs/requirements.md`).
- For Unity compatibility, decorate runtime data classes with `[Serializable]` and use ScriptableObjects for designer-facing content.
- Keep evaluation logic deterministic and testable — separate pure logic from Unity-specific subsystems.
