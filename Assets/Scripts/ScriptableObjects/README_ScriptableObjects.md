# ScriptableObject Definitions (Task 2.1)

This folder contains foundational data holder assets for core gameplay content.

## Structure
- `Base/` shared enums, serializable structs, abstract base classes (`DefinitionBase`, `IValidatable`).
- `Definitions/` concrete ScriptableObject types (`CardDefinition`, `BlessingDefinition`, `EquipmentDefinition`, `SkillTreeDefinition`).

## Design Principles
- Data only: no runtime game logic or side effects.
- Private serialized fields + public getters for encapsulation.
- Validation pattern via `IValidatable` and `DefinitionBase.CollectValidationIssues`.
- Flexible `EffectData` and related structs for early phases; to be specialized in Phase3 effect system task.

## Future Extensions
- Add assembly definition file to isolate compile domain (e.g., `Game.ScriptableObjects.asmdef`).
- Introduce richer effect polymorphism or typed payloads.
- Integrate automated validation pass during editor build pipeline.

## Related Plan
See `Assets/docs/plans/create-scriptableobject-base-classes/plan.md` for original implementation plan.
