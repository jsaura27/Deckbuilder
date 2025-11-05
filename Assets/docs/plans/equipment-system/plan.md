# Task: Equipment System

## Objective

Design and implement the Equipment System for the game: data definitions, runtime representations, equip rules, and integration points with combat, cards, and progression systems.

## Prerequisites

- Assets/docs/requirements.md
- Assets/docs/requirements.json
- Assets/docs/schemas/equipment.schema.json
- Existing data model patterns under Assets/Scripts/DataModels/ (if present)
- Existing ScriptableObject base classes created by Phase2

## Step-by-Step Instructions

1. Define data model
   - Review `Assets/docs/schemas/equipment.schema.json` and expand where necessary (effects, cardModifiers shape).
   - Decide on stable `id` format (GUID or namespaced slug).

2. Create C# data models
   - Implement POCO classes matching schema under `Assets/Scripts/DataModels/Equipment/`.
   - Add [Serializable] and Unity-friendly types (enums for Rarity and SlotType).

3. Implement ScriptableObject representations
   - Create `EquipmentDefinition : ScriptableObject` with CreateAssetMenu attribute and fields matching the data model.
   - Place assets under `Assets/ScriptableObjects/Equipment/`.

4. Equip rules and runtime system
   - Implement an `EquipmentManager` service responsible for equip/unequip, slot validation, and stat modifier aggregation.
   - Use dependency inversion so systems can request IEquipmentService.
   - Implement stacking and exclusivity flags and expose configuration in the definition.

5. Card modifiers and effect resolution
   - Define a clear contract for cardModifiers (e.g., {"target":"Attack","modifier":{"draw":1}}).
   - Integrate modifier application into the card effect resolution pipeline.

6. Acquisition & Persistence
   - Hook into loot drop and merchant systems (stubs if not implemented yet).
   - Ensure equipment state is represented in run snapshots (optional) and meta progression saving.

7. Tests and validation
   - Add unit tests for stat aggregation, slot enforcement, and modifier application.
   - Add schema compliance test for equipment JSON files.

## Deliverables

- `Assets/Scripts/DataModels/Equipment/` (POCO classes)
- `Assets/ScriptableObjects/Equipment/` (ScriptableObject assets)
- `Assets/Scripts/Services/EquipmentManager.cs` (runtime manager)
- Unit tests under `Assets/Tests/EquipmentTests/`
- `plan.json` (machine-readable mirror)

## Notes

- Use enums for `Rarity` and `SlotType` to match schema enums.
- For now prefer ScriptableObject assets for editor workflow; JSON import tooling can produce these assets.
- Keep cardModifiers expressive but constrained; prefer small typed modifier objects rather than freeform JSON where possible.
