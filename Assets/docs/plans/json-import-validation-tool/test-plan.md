# Test Plan - JSON Import & Validation Tool (task 2.3)

This plan summarizes the tests generated for the JsonImportValidator Editor tool.

Generated tests:

- Assets/Tests/Editor/EditorTools/JsonImportValidatorTests_Extended.cs

Targeted behaviors:

- RefreshFileList: verifies folder scanning, handling of non-existing folders, and message field updates.
- DoDryRunValidation: verifies basic JSON structural checks (object/array vs invalid), and message reporting of valid/invalid counts.

Notes:

- Tests use reflection to invoke internal EditorWindow methods and avoid launching UI.
- Tests are Editor-only and do not modify production source files.
