 # Task: Meta Progression (Optional)
 
 ## Objective
 Define and implement a meta-progression system to persist player unlocks, achievements, and optional progression data across runs. Provide a clear save format, migration strategy, and integration points for UI and gameplay systems.
 
 ## Prerequisites
 - Read `Assets/docs/requirements.md` and `Assets/docs/requirements.json` for progression and persistence requirements.
 - Review existing save-related tasks and schemas (if present) under `Assets/docs/schemas/`.
 - Confirm target storage: local file (JSON) with versioning; consider Unity PlayerPrefs only for small flags (not recommended).
 
 ## Step-by-Step Instructions
 1. Design save schema
    - Create `Assets/docs/schemas/meta-progression.schema.json` describing persisted fields: unlockedClasses, unlockedCards, unlockedBlessings, unlockedEquipment, achievements, version, createdAt, modifiedAt.
    - Reference: `Assets/docs/requirements.json` -> `technical.saveMetaProgression`.

 2. Implement data model
    - Add C# POCOs under `Assets/Scripts/DataModels/Meta/` (e.g., MetaProgressionData.cs) matching schema fields.
    - Mark serializable types with `[Serializable]` and provide Unity-friendly containers (ScriptableObject-based manifests optionally for editor tooling).

 3. Persistence API
    - Implement a `MetaProgressionService` (singleton / DI service) in `Assets/Scripts/Services/Save/` with methods: Load(), Save(), Reset(), MigrateIfNeeded()
    - Use file-based JSON serialization in `Application.persistentDataPath` with a manifest file name like `meta_progression_v{version}.json`.

 4. Versioning & Migration
    - Introduce `version` field in schema. Implement a simple migration registry mapping from old versions to upgrade functions.
    - Add migration unit tests under `Assets/Tests/` that exercise migrating v1 -> v2 shapes.

 5. Integration points
    - Hook Save() to achievement unlocks, shop purchases, and any UI toggles that change meta state.
    - Provide editor tools to import/export meta progression for testing (`Assets/Editor/SaveTools/MetaProgressionTool.cs`).

 6. UI & UX
    - Add a minimal UI screen under `Assets/Scenes/UI/` for viewing unlocked items and resetting progression.
    - Add a confirmation flow for destructive Reset.

 7. Tests & Validation
    - Unit tests for serialization round-trips, migration functions, and Load/Save error handling.
    - Add a build-time validation step (editor script) to ensure schema compliance when deploying content packs.

 ## Deliverables
 - `Assets/docs/plans/meta-progression-optional/plan.md` (this file)
 - `Assets/docs/plans/meta-progression-optional/plan.json`
 - `Assets/docs/schemas/meta-progression.schema.json` (new schema)
 - C# data models: `Assets/Scripts/DataModels/Meta/MetaProgressionData.cs`
 - Service: `Assets/Scripts/Services/Save/MetaProgressionService.cs`
 - Editor tools: `Assets/Editor/SaveTools/MetaProgressionTool.cs` (optional)
 - Unit tests: `Assets/Tests/MetaProgressionTests.cs`

 ## Notes
 - Use JSON file storage for portability and testability; keep version field at top-level to simplify migrations.
 - Keep schema and C# models in sync; small utility script can assert shape parity during CI.
 - For optional features (save current run), split into a follow-up task to avoid scope creep.
