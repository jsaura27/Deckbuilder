# Task: JSON Import & Validation Tool

## Objective
Create an Editor utility that imports gameplay JSON content, validates it against the project's JSON schemas, and converts valid data into Unity ScriptableObjects.

## Prerequisites
- `Assets/docs/schemas/` exists with relevant schema files (cards, blessings, equipment, skilltree).
- `Assets/docs/requirements.md` and `requirements.json` for domain guidance.
- Destination folders for generated ScriptableObjects (e.g., `Assets/Data/ScriptableObjects/` or `Assets/Scripts/DataModels/`).

## Step-by-Step Instructions
1. Discover and load JSON files from a user-selected folder in the Unity Editor.
2. For each JSON file, determine which schema it targets (use a manifest or filename conventions).
3. Validate the JSON content against the matching schema using a JSON Schema validator (Newtonsoft.Json.Schema or built-in lightweight validator).
4. On validation failure, collect and display errors in an Editor window with file and error locations.
5. On success, convert the JSON object into a Unity ScriptableObject instance:
   - Map fields to ScriptableObject properties according to schema.
   - Use [CreateAssetMenu] attributes and place assets under a configurable destination folder.
   - Set asset names and ids consistently (e.g., `{id}_{name}`).
6. Provide import options (dry-run, overwrite existing, skip duplicates).
7. Add a validation report export (markdown or JSON) saved under `Assets/docs/plans/json-import-validation-tool/`.

## Deliverables
- `Assets/docs/plans/json-import-validation-tool/plan.md` (this file)
- `Assets/docs/plans/json-import-validation-tool/plan.json`
- `Assets/Editor/Tools/JsonImportValidator.cs` (Editor window + import logic)
- Validation report files under the plan folder

## Notes
- Use relative paths (`Assets/docs/schemas/`) in messages and configs.
- Prefer Newtonsoft.Json for parsing; for schema validation consider `Newtonsoft.Json.Schema` but note licensing when used in closed-source builds.
- Keep conversions idempotent: importing twice without changes should not create duplicates.
- Unit tests: validate conversion routines with small JSON fixtures.
