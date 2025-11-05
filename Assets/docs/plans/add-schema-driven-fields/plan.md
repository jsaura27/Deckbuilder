# Task: Add Schema-Driven Fields

## Objective

Add schema-driven fields to the previously-created ScriptableObject base classes so their serialized fields match the JSON schemas for cards, equipment, blessings, and skill trees. This makes ScriptableObject assets first-class serialized representations of the game's data models and ensures parity between JSON data and Unity assets.

## Prerequisites

- Roadmap task 2.1 "Create ScriptableObject Base Classes" completed.
- Schemas available at `Assets/docs/schemas/` (cards, equipment, blessings, skilltree).
- Destination folder: `Assets/Scripts/DataModels/` or `Assets/Scripts/ScriptableObjects/` (confirm project convention).
- Unity knowledge: use of `[Serializable]`, `ScriptableObject`, `CreateAssetMenu`, and serializable collections.

## Step-by-Step Instructions

1. Inspect JSON schemas in `Assets/docs/schemas/` and extract properties for each domain: Card, Equipment, Blessing, SkillTree.
2. For each domain, derive a Unity-friendly C# class/ScriptableObject:
   - Prefer `ScriptableObject` for authoring game content in the inspector.
   - Use C# enums for fields with `enum` in schemas (e.g., Rarity, CardType, SlotType).
   - Mark classes with `[CreateAssetMenu(fileName = "New<Card|Equipment|Blessing>", menuName = "Game/<Domain>")]`.
   - Use serializable lists for arrays and plain types for scalars.
3. Create a small helper attribute/serializer if required for polymorphic `effects` objects (e.g., use a discriminated union pattern or a small EffectType enum + generic payload container).
4. Place files under `Assets/Scripts/DataModels/` and update namespaces to match project convention (e.g., `Deckbuilder.DataModels`).
5. Add comments linking each C# field back to the corresponding schema property and the schema file path.
6. Add unit tests (Unity Test Framework) to validate that ScriptableObject instances can be created and their serialized JSON matches schema expectations (optional minimal smoke test).

## Deliverables

- `Assets/docs/plans/add-schema-driven-fields/plan.md` (this file)
- `Assets/docs/plans/add-schema-driven-fields/plan.json` (machine-readable plan)
- New C# files under `Assets/Scripts/DataModels/` matching schemas (suggested: `CardDefinition.cs`, `EquipmentDefinition.cs`, `BlessingDefinition.cs`, `SkillTreeDefinition.cs`, plus enums).
- Optional tests under `Assets/Tests/`.

## Notes

- For polymorphic/complex `effects`, start with a simple JSON string or serialized `ScriptableObject` reference to an `EffectDefinition` ScriptableObject; expand to typed effect classes later.
- Keep data loading and runtime conversion (to runtime POCOs) separate from ScriptableObject definitions to maintain editor ergonomics.
- Use stable `id` strings for cross-references and prefer GUID-like ids for content authored outside the editor.
- Use relative paths when referencing docs: `Assets/docs/schemas/cards.schema.json`.

