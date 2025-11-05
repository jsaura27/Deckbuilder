# Test Plan for Task 1.3 — Generate C# Data Models

Objective: Provide unit tests for generated POCO data model classes to validate basic construction, field assignment, and JSON serialization round-trips.

Generated tests (unit):
- `CardTests_Extended.cs` — validates Card, Effect, UpgradePath field assignment and JsonUtility roundtrip.
- `BlessingTests_Extended.cs` — validates Blessing and EvolutionStage lists and roundtrip.
- `EquipmentTests_Extended.cs` — validates Equipment fields and list assignment.
- `SkillTreeTests_Extended.cs` — validates nested Branch/Node lists and JsonUtility roundtrip.

Strategy and rationale:
- These models are plain data holders (fields only). Tests focus on serialization/deserialization and basic structural integrity.
- Use UnityEngine.JsonUtility to ensure JSON roundtrip behaves correctly in Unity edit mode.

Next steps:
- Run Unity Test Runner in EditMode or use CI to run tests. These tests are deterministic and should not be flaky.
- If future logic is added (validation methods, constructors, or behavior), add targeted unit tests for those methods.

Timestamp: 2025-11-01T12:10:00Z
