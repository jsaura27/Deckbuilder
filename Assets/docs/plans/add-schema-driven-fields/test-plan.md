# Test Plan for Task 2.2 - Add Schema-Driven Fields

This plan documents the generated tests for the ScriptableObject data models created by task 2.2.

## Scope
- Files: Assets/Scripts/DataModels/Enums.cs, EffectDefinition.cs, CardDefinition.cs, EquipmentDefinition.cs, BlessingDefinition.cs, SkillTreeDefinition.cs

## Unit Tests (per-class locations)
- `Assets/Tests/Editor/DataModels/CardDefinitionTests_Extended.cs`
  - Create instance of `CardDefinition`
  - Validate default collections and assignable fields
  - Verify `UpgradePath` serialization-friendly class

- `Assets/Tests/Editor/DataModels/EffectDefinitionTests_Extended.cs`
  - Create instance of `EffectDefinition`
  - Validate `effectType` and `payloadJson` assignment

## Integration Tests (grouped by area)
- `Assets/Tests/Integration/DataModels/ScriptableObjectCreationIntegrationTests.cs`
  - Create `EffectDefinition` and `CardDefinition`
  - Attach effect to card and verify composition

## Coverage Strategy
- These tests are smoke and composition tests intended to validate editor-time ScriptableObject creation and basic field access.
- If line-coverage instrumentation is available in the environment, aim to extend tests for Equipment/Blessing/SkillTree definitions.

## Notes
- Tests use Unity's ScriptableObject.CreateInstance and NUnit assertions compatible with Unity Test Framework.
- These tests avoid editor-only APIs (AssetDatabase) so they can run in Edit Mode with Unity Test Runner.
