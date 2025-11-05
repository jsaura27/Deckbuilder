---
description: "Auto-generates unit & integration tests for a completed task's implementation to reach >=80% coverage"
mode: "agent"
---

# Prompt: Generate Tests for Completed Task Implementation

## Overview
Automates creation of unit and integration tests for a specified completed roadmap task to achieve at least 80% code coverage across the files introduced or edited by that task. It locates the plan folder via `roadmap.json`, inspects implementation artifacts (`plan.json`, `plan.md`, `changes-log.json`), derives test cases, creates test files, runs a build & coverage instrumentation, and updates reporting artifacts. Designed to be invoked with a single input: the target task id (e.g., `3.2`).

## Inputs
- Required: `taskId` (string) — roadmap task id (e.g., `3.2`).

## Implicit Inputs
- Roadmap: `Assets/docs/roadmap.json`
- Plan folder: resolved from `planFolder` in roadmap task
- Plan artifacts: `plan.json`, `plan.md`
- Change log: `changes-log.json`
- Existing tests folder(s): `Assets/Tests/`
- Requirements: `Assets/docs/requirements.md`

## Selection Logic
1. Load `roadmap.json`.
2. Find task with `id == taskId` and `status == "done"` (abort if not done).
3. Extract `planFolder`. Abort if missing.
4. Verify presence of `plan.json`, `plan.md`, `changes-log.json`.
5. Collect file lists from `changes-log.json.filesCreated` and `filesEdited`.

## Test Generation Strategy
1. Classify target files: MonoBehaviours, ScriptableObjects, Interfaces, Plain C# classes.
2. For each file:
   - Parse public methods, properties, events and identify behaviors to assert.
   - For ScriptableObjects: instantiate and validate default field values.
   - For MonoBehaviours: simulate lifecycle where feasible (e.g., invoke public methods).
````prompt
---
description: "Auto-generates unit & integration tests for a completed task's implementation to reach >=80% coverage"
mode: "agent"
---

# Prompt: Generate Tests for Completed Task Implementation

## Overview
Automates creation of unit and integration tests for a specified completed roadmap task. The agent uses plan artifacts to identify implementation files, derives test cases, updates or creates per-class test files (not task-scoped folders), and writes reporting artifacts. Designed to be invoked with a single input: the target task id (e.g., `3.2`).

## Inputs
- Required: `taskId` (string) — roadmap task id (e.g., `3.2`).

## Implicit Inputs
- Roadmap: `Assets/docs/roadmap.json`
- Plan folder: resolved from `planFolder` in roadmap task
- Plan artifacts: `plan.json`, `plan.md`
- Change log: `changes-log.json`
- Existing tests folder(s): `Assets/Tests/`
- Requirements: `Assets/docs/requirements.md`

## Selection Logic
1. Load `roadmap.json`.
2. Find task with `id == taskId` and `status == "done"` (abort if not done).
3. Extract `planFolder`. Abort if missing.
4. Verify presence of `plan.json`, `plan.md`, `changes-log.json`.
5. Collect file lists from `changes-log.json.filesCreated` and `filesEdited`.

## Test Generation Strategy
1. Classify target files: MonoBehaviours, ScriptableObjects, Interfaces, Plain C# classes.
2. For each file:
  - Parse public methods, properties, events and identify behaviors to assert.
  - For ScriptableObjects: instantiate and validate default field values.
  - For MonoBehaviours: simulate lifecycle where feasible (e.g., invoke public methods).
3. Derive unit tests per component:
  - Happy path test(s) per public method.
  - Edge cases: null/invalid inputs where safe, boundary numeric values.
  - Event verification (e.g., `OnTurnStarted` invoked).
4. Integration tests:
  - Combine multiple components to simulate a minimal action sequence.
  - Seed-based deterministic behavior where applicable.
5. Coverage Goal: generate sufficient tests to achieve >=80% line coverage for files created/edited when possible.

## Coverage Calculation
- After test method creation, the agent should prefer to run instrumentation (Unity Test Runner / Code Coverage) in the environment or CI if available.
- If instrumentation is unavailable, compute a heuristic: compare tested public methods vs total public methods and estimate line coverage; record this heuristic in `test-report.json.methodCoverageHeuristic`.
- If coverage < 80%, iterate up to 3 additional loops to add targeted tests for uncovered public methods.

## Artifacts Generated (test files placement policy)

The agent MUST NOT create test folders named after the roadmap task id (for example `Assets/Tests/2.3/`). Creating task-scoped test folders leads to duplicated test classes and fragmentation.

Placement policy (required):
- Unit tests: place in `Assets/Tests/Editor/<Area>/` (for example `Assets/Tests/Editor/DataModels/`), grouped by the production class or small related namespace.
- Integration tests: place in `Assets/Tests/Integration/<Area>/` (for example `Assets/Tests/Integration/DataModels/`).

Test placement algorithm (required):
1. For each production type (class) being tested, search the workspace for an existing test file named `<TypeName>Tests*.cs` under `Assets/Tests/Editor/**` (for unit tests) or `Assets/Tests/Integration/**` (for integration tests).
2. If such a test file exists, append the new test methods into that file (preserving the file's namespace, existing using directives, and class naming conventions). Do NOT create another test class with the same production type name.
3. If the test file does not exist, create `<TypeName>Tests_Extended.cs` in the appropriate area folder with a namespace consistent with the project's test layout (examples: `Deckbuilder.Tests.Editor.DataModels`, `Deckbuilder.Tests.Integration.DataModels`).
4. If multiple production types are small, documented grouping into a single test file is permitted, but the default is one test file per production type.

## Edge Cases & Deterministic Behavior
The agent must handle ambiguous or conflicting workspace states deterministically. Implement the following rules when placing or updating test files:

- Multiple matching test files found for the same production type:
  1. Prefer files located under `Assets/Tests/Editor/` (unit) or `Assets/Tests/Integration/` (integration) matching the same area namespace as the production code (e.g., `DataModels`, `Combat`).
  2. If several matches remain, choose the file whose filename most closely matches `<TypeName>Tests` (use Levenshtein or simple substring match). If still ambiguous, choose the file with the most recent modification time.
  3. If ambiguity remains, append a short diagnostic comment at the top of the chosen file indicating the agent made a deterministic selection and log the alternative paths in `changes-log.json.decisions`.

- Namespace mismatch between production type and test file:
  - Preserve the test file's existing namespace. Do not rewrite namespaces in existing test files. If creating a new file, use a namespace derived from production type's folder and `Deckbuilder.Tests.Editor.<Area>` or `Deckbuilder.Tests.Integration.<Area>`.

- Idempotency and merging:
  - If appending tests to an existing file, check for existing test method names or identical test bodies to avoid duplicates. If a logically equivalent test exists, do not add a duplicate; instead, log the skip in `changes-log.json.decisions`.
  - When adding a new method, follow the project's existing formatting (using directives at top, single namespace block). If the file uses `partial` test classes or different structure, respect that layout.

- Formatting and style:
  - Preserve existing file formatting (indentation, using groups). When creating new files, use the repository's predominant style (match existing test files: namespace then class then methods).

- Conflict resolution (concurrent edits or repeated agent runs):
  - If the target test file was modified since it was scanned, the agent should abort the append operation and retry scanning up to 2 times with a short backoff; on persistent race, write a diagnostic entry in `changes-log.json.failures` and skip that file.

- Selection priority summary:
  1. Exact filename match under the correct area folder.
  2. Best substring/closest filename match under area folder.
  3. Most recently modified among equals.
  4. Create new `<TypeName>Tests_Extended.cs` if no acceptable match.

- Logging and traceability:
  - For every file created or edited, append an entry to `changes-log.json` (under `filesCreated` or `filesEdited`). For any deterministic choice or fallback, add a short note to `changes-log.json.decisions` describing why that file was chosen.


Other artifacts generated by the agent (placed in the plan folder):
- `test-plan.md` — summary of generated test cases mapped to code elements
- `test-report.json` — machine-readable results (must conform to `Assets/docs/schemas/test-report.schema.json`)
- Update `changes-log.json` — append created test files under `filesCreated` using the new per-class paths

Example `test-report.json` (format to follow):
```json
{
  "task": "3.2",
  "coverage": 0.85,
  "files": [
   { "path": "Assets/Scripts/Combat/TurnManager.cs", "coverage": 0.9 },
   { "path": "Assets/Scripts/Combat/CombatState.cs", "coverage": 0.75 }
  ],
  "generatedTests": ["Assets/Tests/Editor/Combat/TurnManagerTests_Extended.cs"],
  "integrationTests": ["Assets/Tests/Integration/Combat/CombatFlowTests.cs"],
  "iterations": 2,
  "status": "target-met"
}
```

Must conform to `Assets/docs/schemas/test-report.schema.json`.

## Validation Steps
1. Ensure all referenced original implementation files still exist.
2. Confirm new/updated test files compile (static error check).
3. Run tests; capture pass/fail counts.
4. If any new test fails unexpectedly (logic or compilation), attempt fix or mark in `test-report.json.failures`.
5. Abort if plan artifacts missing or if task status != done.

## Reporting & Status Updates
- Do not change the task `status` (remains `done`).
- Append or create `test-report.json` and `test-plan.md` in the plan folder.
- Optionally add decisions to `changes-log.json` relating to coverage gaps or skipped tests.

## Changes to Existing Files
- Only modify:
  - `changes-log.json` (append created tests under `filesCreated`)
  - `summary.md` (add section)

No other existing source modifications unless required for testability (if so, must log as `filesEdited` with rationale and keep changes minimal).

## Non-Deviation Rules
- Do not refactor production code for coverage unless absolutely required; prefer test-only helpers.
- Maintain isolation: each test file targets a small surface area.
- Avoid flaky timing-based tests.

## Failure Handling
- If coverage >=70% but cannot reach 80% due to limited logic surface, mark `status: "partial"` inside `test-report.json` with `reason`.
- If instrumentation unavailable, fallback to heuristic coverage estimation and note method count vs tested count.

## Test File Naming Conventions
- Unit tests: `<TypeName>Tests_Extended.cs` (placed under `Assets/Tests/Editor/<Area>/`)
- Integration tests: descriptive names placed under `Assets/Tests/Integration/<Area>/` (examples: `CombatFlowTests.cs`, `StatusEffectPipelineTests.cs`)

## Output Summary (console/message)
Return: taskId, totalTestFilesModifiedOrCreated, coveragePercent, status (target-met|partial|error).

## Abort Conditions
- Task not found or not done.
- Missing plan artifacts.
- No files to test (empty implementation lists).

## Example Invocation Decision Flow
1. Input taskId=3.2
2. Resolve plan folder: `Assets/docs/plans/combat-system`
3. Collect files from `changes-log.json` for that plan and classify target types.
4. For each type, find or create the corresponding per-class test file and add tests there.
5. Run tests/instrumentation; write artifacts.

---
Version: 1.1
Lifecycle: Post-implementation test augmentation
````
## Abort Conditions
- Task not found or not done.
- Missing plan artifacts.
- No files to test (empty implementation lists).

## Example Invocation Decision Flow
1. Input taskId=3.2
2. Resolve plan folder: `Assets/docs/plans/combat-system`
3. Generate extended tests for: TurnManager, CombatState, StatusEffect, CombatAction, IEnemyAI.
4. Produce integration test combining TurnManager + PatternAI.
5. Achieve >=80% coverage, write artifacts, update logs.

---
Version: 1.0
Lifecycle: Post-implementation test augmentation
