# Task: Save System & Migration

## Objective

Design and implement a versioned save system for the project that persists meta-progression and provides an extensible migration registry for future save schema changes. Include a migration stub for in-run snapshotting (optional), and provide test and validation steps.

## Prerequisites

- Reference: `Assets/docs/requirements.md`
- Reference: `Assets/docs/requirements.json`
- Schemas: `Assets/docs/schemas/meta-progression.schema.json`, `Assets/docs/schemas/roadmap.schema.json`
- Existing plans: `Assets/docs/plans/` (for patterns)

## Step-by-Step Instructions

1. Define save schemas
   - Create a top-level save schema `schemas/save.schema.json` that references `meta-progression.schema.json` and defines envelope fields: version, createdAt, contentVersion, seed, payload.
   - Add an optional snapshot sub-schema for in-run debug snapshots.

2. Implement SaveData C# model
   - Add a C# POCO under `Assets/Scripts/Save/SaveData.cs` matching the schema. Mark serializable and add Unity-friendly types (DateTime as string, enums as string/int as appropriate).
   - Provide methods to serialize/deserialize to JSON using Unity's JsonUtility or Newtonsoft.Json (package may be added).

3. Migration registry
   - Implement a registry `ISaveMigration` and concrete migration classes that map an older version object to the new schema.
   - Add a MigrationRunner that selects migrations based on semantic version ordering and applies them during load.

4. Persistence layer
   - Implement `SaveManager` with methods: SaveMetaProgression(SaveData), LoadMetaProgression(), BackupSave(), ValidateSaveSchema(json).
   - Use atomic file operations: write to temp file then move/replace to avoid partial saves.

5. Tests
   - Unit tests: serialization roundtrip, migration application, invalid schema detection.
   - Integration test: save -> backup -> migrate -> load reproduces expected state.

6. Documentation & tooling
   - Add `Assets/docs/plans/save-system-migration/plan.json` (machine-readable mirror).
   - Document migration steps in README under `Assets/docs/` and add examples for manual migration.

## Deliverables

- `Assets/docs/plans/save-system-migration/plan.md`
- `Assets/docs/plans/save-system-migration/plan.json`
- Schema: `Assets/docs/schemas/save.schema.json` (if not present)
- C# models under `Assets/Scripts/Save/`
- `Assets/Scripts/Save/SaveManager.cs` and migration classes
- Unit and integration tests under `Assets/Tests/Save/`

## Notes

- Use relative workspace variables in docs: `${workspaceFolder}`.
- If Newtonsoft.Json is preferred, add it to the project manifest or use the Unity package manager.
- Keep migrations idempotent and well-versioned.
