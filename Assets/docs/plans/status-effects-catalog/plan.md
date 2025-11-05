# Task: Status Effects Catalog

## Objective
Create a comprehensive catalog and data model for status effects used in combat. This includes enumerating effects, defining data schemas, implementing machine-readable schema and reference implementations, writing unit tests for core behavior, and integrating the catalog into the combat system and card/equipment effect resolution pipeline.

## Prerequisites
- Review `Assets/docs/requirements.md` for status effect semantics and stacking rules.
- Reference schema files in `Assets/docs/schemas/` (particularly `skilltree.schema.json`, `cards.schema.json`, `blessings.schema.json`, and `changes-log.schema.json` where relevant).
- Existing combat and card systems under `Assets/Scripts/` (scan for effect resolution pipeline).

## Step-by-Step Instructions
1. Enumerate Effects
   - Create a master list of status effects (Burn, Freeze, Poison, Shield, Stun, Vulnerable, Weak, etc.).
   - For each effect record: id, displayName, description, stacking rules, duration semantics, tags (e.g., damage-over-time, crowd-control), and canonical unity data type (ScriptableObject vs JSON).

2. Define Data Schema
   - Create or extend `Assets/docs/schemas/status-effects.schema.json` describing fields: id (string), name (string), stacking: { mode: "stack"|"refresh"|"override", maxStacks: number }, duration: { type: "turns"|"seconds", value: number }, effects: [ { type: "damage"|"heal"|"modifyStat"|"applyTag", params: {} } ], sourceWhitelist: ["card","equipment","blessing","ability"]
   - Add examples and JSON schema tests.

3. Implement C# Data Models (Editor-friendly)
   - Implement a ScriptableObject `StatusEffectDefinition` under `Assets/Scripts/DataModels/` with serializable fields matching the schema.
   - Provide CreateAssetMenu attribute and editor inspector notes.

4. Integrate with Combat Pipeline
   - Wire application/removal hooks into the combat status manager.
   - Ensure stacking and duration semantics are enforced by central StatusEffectManager.
   - Add event hooks for telemetry (statusApplied, statusExpired, stacksChanged).

5. Unit Tests
   - Write tests using Unity Test Framework for stacking behavior, duration ticks, expiry, and interaction with damage/heal calculation.

6. Data & Migration
   - Create a sample `Assets/docs/plans/status-effects-catalog/examples/` with sample JSON files and ScriptableObject assets.
   - Provide migration notes if schemas change (versioning field in schema).

## Deliverables
- `Assets/docs/plans/status-effects-catalog/plan.md` (this file)
- `Assets/docs/plans/status-effects-catalog/plan.json` (machine-readable plan)
- `Assets/docs/schemas/status-effects.schema.json` (new schema file)
- C# ScriptableObject: `StatusEffectDefinition.cs` under `Assets/Scripts/DataModels/`
- Unit tests under `Assets/Tests/StatusEffectsTests.cs`
- Example JSON files under `Assets/docs/plans/status-effects-catalog/examples/`

## Notes
- Use relative paths when referencing repository files.
- Keep data-driven design in mind; prefer adding event-driven hooks rather than hard-coding behavior.
- For Unity-specific types, prefer simple serializable structs for parameters to keep inspector usable.
- Tag this plan with `tags: ["combat","data","testing"]` in `plan.json`.

