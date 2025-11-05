# Test Plan for Task 2.1 — ScriptableObject Base Classes

Objective: Provide EditMode tests validating ScriptableObject scaffolding created in Task 2.1. Focus areas:
- Presence of `CreateAssetMenu` attributes so assets are creatable via the Unity menu.
- Default initialization of serialized list fields.
- Basic validation logic in `CollectValidationIssues` for expected missing fields or malformed data.

Generated tests:
- `Assets/Tests/ScriptableObjects/CardDefinitionTests.cs`
- `Assets/Tests/ScriptableObjects/BlessingDefinitionTests.cs`
- `Assets/Tests/ScriptableObjects/EquipmentDefinitionTests.cs`
- `Assets/Tests/ScriptableObjects/SkillTreeDefinitionTests.cs`

Notes:
- Tests use reflection to set private serialized backing fields where necessary to simulate valid content.
- Tests are EditMode (do not require runtime GameObjects) and safe to run in CI.

Timestamp: 2025-11-01T12:40:00Z
