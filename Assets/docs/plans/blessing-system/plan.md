# Task: Blessing System

## Objective

Design and implement the Blessing System: data definitions, in-editor content workflow, runtime acquisition/evolution logic, and integration points with combat and progression systems.

## Prerequisites

- `Assets/docs/requirements.md` (requirements and design principles)
- `Assets/docs/schemas/blessings.schema.json` (schema for blessings)
- Existing ScriptableObject base classes under `Assets/Scripts/DataModels/` (recommended)
- JSON import & validation editor tool (optional but helpful): `Assets/docs/plans/json-import-validation-tool`

## Step-by-Step Instructions

1. Analyze schema
   - Review `Assets/docs/schemas/blessings.schema.json` to confirm fields and evolution model.
   - Identify enum candidates (rarity, trigger types).

2. Author C# data models
   - Create POCO C# classes mirroring the schema under `Assets/Scripts/DataModels/`.
   - Mark runtime-facing classes with `[Serializable]` and Unity-specific ScriptableObject variants where appropriate.

3. Create ScriptableObject definitions
   - Add `BlessingDefinition : ScriptableObject` with serialized fields matching schema.
   - Add `[CreateAssetMenu(menuName = "Deckbuilder/Blessing Definition")]`.

4. Editor tooling
   - Add an editor importer using existing JSON import tool patterns to load blessing JSON and validate against `Assets/docs/schemas/blessings.schema.json`.
   - Provide a lightweight inspector UI to preview evolution stages and prerequisites.

5. Runtime systems
   - Implement BlessingManager service to register available blessings, handle acquisition events, and evaluate evolution conditions.
   - Define BlessingInstance data (references to definition id, current stage, applied modifiers).
   - Integrate with run state saving for persistent in-run tracking.

6. Evolution mechanics
   - Implement deterministic evaluation of evolution triggers using run state conditions (e.g., counters, flags).
   - Provide hooks for adding/removing blessing stages and for displaying evolution options to the player.

7. Integration and testing
   - Integrate Blessing effects with CombatSystem and CardSystem effect pipeline.
   - Add unit tests for evolution evaluation and stacking rules.
   - Add schema compliance tests for blessing JSON assets.

## Deliverables

- `Assets/Scripts/DataModels/BlessingDefinition.cs` (ScriptableObject definition)
- `Assets/Scripts/Systems/BlessingManager.cs` (runtime manager/service)
- `Assets/Editor/Importers/BlessingJsonImporter.cs` (editor importer/validator)
- `Assets/docs/plans/blessing-system/plan.json` (machine-readable plan)
- Unit tests under `Assets/Tests/` covering evolution and stacking rules

## Notes

- Use relative paths when referencing other docs, e.g., `Assets/docs/schemas/blessings.schema.json`.
- Follow existing project conventions for folders and namespaces.
- Prefer data-driven behaviors; avoid hard-coding evolution logic in systems.
