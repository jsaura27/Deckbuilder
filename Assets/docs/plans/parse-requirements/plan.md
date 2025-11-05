# Task: Parse Requirements

## Objective
Extract and formalize gameplay systems, entities, and relationships from the existing requirements documents (`Assets/docs/requirements.md` and `Assets/docs/requirements.json`) and map each system to Unity module candidates. This task prepares the project for schema-driven data modeling and ScriptableObject scaffolding.

## Prerequisites
- Access to `Assets/docs/requirements.md` and `Assets/docs/requirements.json`
- Access to JSON schemas in `Assets/docs/schemas/` (cards, blessings, equipment, skilltree)
- Unity project opened in editor (for verifying ScriptableObject mapping decisions)
- Basic knowledge of Unity `ScriptableObject`, MonoBehaviour, and data-driven design

## Step-by-Step Instructions
1. Review requirements files
 - Open `Assets/docs/requirements.md` and `Assets/docs/requirements.json`.
 - Identify and list all top-level systems: Character, Equipment, Blessings, Cards, Combat, Progression, Save/Performance/Testing constraints.
 - Why: Ensures full coverage and prevents missing mappings later.

2. Extract entities and relationships
 - For each system, extract entity types (e.g., Card, Blessing, Equipment, SkillNode, SkillBranch, Class, Enemy) and key properties described in requirements and in schemas.
 - Produce a tabular mapping (local notes file) of entity -> key fields -> relationships (e.g., Card.effect -> StatusEffect definitions; Equipment.onEquipEffects -> SkillTree modifications).
 - Why: Clarifies data dependencies and cross references required for schema generation.

3. Map systems to Unity modules
 - For each extracted system map to a recommended Unity implementation:
 - Data definitions -> `ScriptableObject` (e.g., `CardDefinition`, `EquipmentDefinition`, `BlessingDefinition`, `SkillTreeDefinition`)
 - Runtime managers -> MonoBehaviours/Services (e.g., `CombatManager`, `CardSystem`, `EquipmentManager`, `BlessingManager`, `SkillTreeService`, `RunStateManager`)
 - Persistent meta-data -> JSON save format + small persistent manager (`MetaProgressionManager`)
 - Record mapping in the notes file with rationale (editor usability, referencing, runtime performance).

4. Cross-check with JSON schemas
 - Open `Assets/docs/schemas/cards.schema.json`, `blessings.schema.json`, `equipment.schema.json`, and `skilltree.schema.json`.
 - Verify that schema properties correspond to the extracted entity fields from step2. Note missing fields or mismatches (e.g., schema lacks an explicit `rarity` in a node, or skill node `type` unclear).
 - If mismatches exist, record proposed schema amendments (e.g., add `targets`, `duration`, `stacking` to status effect objects).

5. Produce a mapping artifact
 - Create a small mapping document (can be appended to this plan or saved as `Assets/docs/plans/parse-requirements/mapping.json`) listing:
 - System name
 - Suggested ScriptableObject name and path
 - Runtime manager name and path
 - Key referenced schemas
 - Why: Acts as the single source of truth for later code generation.

6. Determine generation strategy for C# models
 - Decide whether to auto-generate POCOs from schemas (preferred) or hand-craft ScriptableObject fields.
 - Document the chosen generator tool or approach (e.g., use a custom JSON->C# generator or an external tool that outputs Unity-friendly classes).

7. Validation and sign-off
 - Walk through the mapping with a teammate or record a short checklist verifying:
 - All systems from requirements accounted for
 - All schema fields are mapped or noted for amendment
 - All ScriptableObject and manager names are specified
 - Mark the task complete when the mapping file and `mapping.json` are present in the task folder.

## Deliverables
- `Assets/docs/plans/parse-requirements/plan.md` (this document)
- `Assets/docs/plans/parse-requirements/plan.json` (machine-readable plan)
- `Assets/docs/plans/parse-requirements/mapping.json` — entity-to-module mapping artifact (created in step5)

## Notes
- Keep mappings conservative: prefer simpler ScriptableObject shapes first and extend with composition.
- Use `id` fields for cross-references and prefer human-readable `slug` plus GUID when generating assets.
- When schemas lack detail, annotate the mapping with an explicit TODO so the schema can be extended before generation.
- This task does not modify game runtime code; it prepares artifacts for Phase1.3 and Phase2.
