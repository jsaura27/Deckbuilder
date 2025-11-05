# Task: Generate C# Data Models

## Objective

Auto-generate C# Plain Old CLR Objects (POCOs) from JSON schemas located in `Assets/docs/schemas/`, add Unity-friendly annotations (e.g., `[Serializable]`), and place generated classes under `Assets/Scripts/DataModels/` with clear namespace and partial class support.

## Prerequisites

- `Assets/docs/schemas/` present and schemas validated (see `Assets/docs/plans/load-validate-schemas`).
- Destination folder: `Assets/Scripts/DataModels/` (create if missing).
- Decide on a mapping strategy for JSON Schema types to C# types (e.g., string -> string, integer -> int, number -> float/double, object -> class/dictionary, array -> List<T>). Document in the plan.

## Step-by-Step Instructions

1. Inventory schemas
   - List JSON schema files and choose which ones to generate models for (e.g., `cards.schema.json`, `blessings.schema.json`, `equipment.schema.json`, `skilltree.schema.json`).
2. Define type mappings
   - Map JSON types to C# types, include nullable handling and enums for `enum` arrays.
3. Generate class per schema
   - Generate a class per top-level schema object with properties matching schema properties.
   - Add `[Serializable]` attribute to each class.
   - Use `System.Collections.Generic.List<T>` for arrays.
   - For nested objects, generate nested or separate classes (use separate classes under the same namespace for clarity).
4. Unity-friendly adjustments
   - Use types Unity serializes (int, float, string, bool, List<T>, custom ScriptableObject/Serializable classes).
   - Add comments referencing the source schema file and `$schema` draft.
5. Place files
   - Write generated `.cs` files to `Assets/Scripts/DataModels/` with filenames matching class names (kebab/case -> PascalCase).
6. Optional: Add editor utility
   - Add `Assets/Editor/GenerateDataModelsEditor.cs` with a menu item to re-run generation from within Unity.

## Deliverables

- `Assets/Scripts/DataModels/*.cs` (generated POCO classes)
- `Assets/docs/plans/generate-csharp-data-models/plan.md`
- `Assets/docs/plans/generate-csharp-data-models/plan.json`
- Optional: `Assets/Editor/GenerateDataModelsEditor.cs`

## Notes

- Use partial classes if future generation runs should preserve manual edits: generate `*.generated.cs` and allow `*.cs` for manual extensions.
- If schemas include enums, generate C# enums under `Assets/Scripts/DataModels/Enums/` and refer to them in generated classes.
- Keep generation idempotent: re-running should overwrite only generated files and not manual files.
