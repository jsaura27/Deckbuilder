# Task: Create ScriptableObject Base Classes

## Objective
Establish foundational ScriptableObject definitions for core gameplay data (CardDefinition, BlessingDefinition, EquipmentDefinition, SkillTreeDefinition) to enable data-driven content creation and downstream tooling (JSON import, validation, editors).

## Prerequisites
- Roadmap Phase2 kickoff (Phase1 data models & schemas completed)
- Generated C# POCO data models under `Assets/Scripts/DataModels/` (from task 1.3)
- Schemas for reference: `Assets/docs/schemas/cards.schema.json`, `Assets/docs/schemas/blessings.schema.json`, `Assets/docs/schemas/equipment.schema.json`, `Assets/docs/schemas/skilltree.schema.json`
- Requirements context: `Assets/docs/requirements.md`

## Step-by-Step Instructions
1. Folder Structure
   - Create `Assets/Scripts/ScriptableObjects/Base/` for abstract/base classes.
   - Create `Assets/Scripts/ScriptableObjects/Definitions/` for concrete assets.
   - Ensure editor-friendly organization: optionally add assembly definition later.
2. Enumerations & Shared Types
   - Add enums: `CardType { Attack, Defense, Utility, Curse }`, `Rarity { Common, Rare, Epic, Legendary }`, `EquipmentSlotType { Weapon, Armor, Trinket }`.
   - Place in `Assets/Scripts/ScriptableObjects/Base/Enums.cs`.
3. Base Class Design Principles
   - Immutable identifiers: public string `Id` (serialized, read-only in inspector via custom editor later).
   - Use `[CreateAssetMenu]` only on concrete leaf classes (avoid clutter for abstract bases).
   - Provide virtual validation method `Validate()` returning list of issues (future use by tooling).
4. CardDefinition
   - Fields (map schema): id, name, type (enum), cost (int >=0), rarity, description (TextArea), effects (list of EffectData), tags (list<string>), upgradePath (nested struct with upgradedId, cost override?).
   - Consider nested serializable `CardEffectData { string effectType; string target; float value; string jsonPayload; }` to remain flexible pre-effect system.
   - Add `[CreateAssetMenu(menuName="Game/Cards/Card")]`.
5. BlessingDefinition
   - Fields: id, name, rarity, description, baseEffect (generic serializable object placeholder or JSON text), triggers (list<string>), evolution stages (list of BlessingEvolutionStage { int stage; string condition; ScriptableObject resultingEffectRef? or placeholder object }).
   - `[CreateAssetMenu(menuName="Game/Blessings/Blessing")]`.
6. EquipmentDefinition
   - Fields: id, name, slotType, rarity, statModifiers (list<StatModifier>), cardModifiers (list<CardModifier>), onEquipEffects (list<EffectData>).
   - Define `StatModifier { string stat; float flat; float percent; }`, `CardModifier { string filterTag; string modification; float value; }`.
   - `[CreateAssetMenu(menuName="Game/Equipment/Equipment")]`.
7. SkillTreeDefinition
   - Fields: id, classId, branches (list<SkillBranch> with name + nodes).
   - Node: `SkillNode { string id; string type; int cost; List<string> prerequisites; string conditionalUnlock; EffectData effect; }`.
   - `[CreateAssetMenu(menuName="Game/SkillTrees/SkillTree")]`.
8. Validation Hooks
   - For each definition implement `public override void OnValidate()` (Unity) to call internal `Validate()` and optionally log warnings in editor (gated by a static flag for noise control).
   - Basic rules: non-empty Id, uniqueness within loaded assets (future: global registry), valid enum values, cost >=0, evolution stages ordered, prerequisite ids exist within branch.
9. ID Strategy
   - Add comment: IDs authored manually now; future automation may generate GUID-style IDs. Provide `string Id` not `Guid` for inspector convenience.
10. Namespaces & Code Style
    - Namespace: `Game.Data` (or `Game.Data.ScriptableObjects`).
    - Mark data containers `[Serializable]` and keep fields `private` with `[SerializeField]` + public getters; mutability only where needed.
11. Future Extensibility Notes
    - JSON import tool (task 2.3) will map JSON schema instances into these ScriptableObjects.
    - Validation service will iterate all `ScriptableObject` assets implementing an `IValidatable` interface (add later).
12. Deliverables Placement
    - Commit new scripts under `Assets/Scripts/ScriptableObjects/`.
    - (Optional) Add `README_ScriptableObjects.md` summarizing conventions.

## Deliverables
- Enums & shared types script (`Enums.cs`)
- `CardDefinition.cs`
- `BlessingDefinition.cs`
- `EquipmentDefinition.cs`
- `SkillTreeDefinition.cs`
- Supporting serializable structs: EffectData, CardEffectData, StatModifier, CardModifier, BlessingEvolutionStage, SkillBranch, SkillNode, UpgradePath
- Validation methods stubs

## Notes
- Keep implementations lean; do not implement gameplay logic—only data holders.
- Prefer composition-friendly design (lists of structs) over inheritance for effect payloads.
- Defer complex effect serialization strategy until effect resolution pipeline is designed (Phase3).
- Consider adding `Addressable` labels later for content packaging.
