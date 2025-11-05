---
description: "Generates a scoped, actionable Unity implementation task list from requirements and roadmap artifacts; outputs to Assets/docs/unity/Unity-tasks.md"
mode: "agent"
# model/tool declarations removed to satisfy linter (original pattern mirrored from implement-task prompt).
---

# Prompt: Unity Implementation Tasks Generator

## Objective
Produce a refined, prioritized set of Unity-facing implementation tasks translating high-level design (`requirements.*`) and lifecycle progress (`roadmap.*`) into concrete engine work. Output is a markdown file: `Assets/docs/unity/Unity-tasks.md`.

## Inputs (Implicit – do NOT request user input)
- Requirements (human): `Assets/docs/requirements.md`
- Requirements (machine): `Assets/docs/requirements.json`
- Roadmap (machine): `Assets/docs/roadmap.json`
- Roadmap (human): `Assets/docs/roadmap.md` (optional if missing)

## Output
Create (or overwrite) `Assets/docs/unity/Unity-tasks.md` containing:
1. Header metadata block (title, generated timestamp, source versions if discoverable)
2. Overview (goal of this batch of tasks / current phase alignment)
3. Environment Assumptions (Unity version unknown → mark as TODO if not inferable; platform targets from requirements)
4. Task Catalog (grouped by category; each task fully defined per schema below)
5. Dependency Graph (inline adjacency list or simple table)
6. Priority Matrix (rows: tasks; columns: impact, effort, risk, status)
7. Immediate Sprint Picks (top 5–10 highest value tasks not yet started)
8. Validation & Gaps Section (uncovered requirements + roadmap pending items → proposed tasks)
9. Future Considerations (deferred / advanced features)
10. Changelog Note (explain generation rules; diff strategy for future runs)

## Task Definition Schema
Each task MUST render as a markdown block starting with `### [TaskId] Task Name` and include a machine-parsable fenced JSON snippet following this shape:
```json
{
  "id": "<phase.task or generated UID>",
  "name": "<concise name>",
  "category": "<enum: DataModel | ScriptableObject | System | EditorTool | UI | Persistence | Performance | Validation | Telemetry | Testing | Infrastructure | Documentation>",
  "objective": "<1–2 sentence outcome>",
  "rationale": "<tie back to requirement or roadmap gap; cite ids>",
  "deliverables": ["<file or folder path>", "..."],
  "codePlacement": ["Assets/Scripts/...", "Assets/Editor/..."],
  "artifacts": {
    "runtime": ["<MonoBehaviour/Class names>", "..."],
    "data": ["<ScriptableObject/DataModel names>"],
    "tests": ["<Test file suggestions>"]
  },
  "dependencies": ["<task id>", "..."],
  "acceptanceCriteria": ["<verifiable criteria>", "..."],
  "testStrategy": "<unit/integration/perf; outline>",
  "risk": "<Low|Medium|High>",
  "effort": "<XS|S|M|L|XL>",
  "status": "pending",
  "priority": "<P1|P2|P3>",
  "tags": ["<keyword>", "..."],
  "notes": "<extra clarifications>",
  "origin": {
    "requirements": ["<section or concept>", "..."],
    "roadmap": ["<roadmap task id>", "..."],
    "gaps": ["<gap id or inferred>"]
  }
}
```

## Generation Rules
1. Parse existing roadmap tasks; exclude those with `status == done` unless follow-ups are required (e.g., missing tests, extended integration). Mark derived follow-up tasks with suffix `-FUP`.
2. Map each major system in requirements to at least one actionable Unity task if not already represented (Character, Equipment, Blessing evolution, StatusEffect integration tests, RNG seed harness, Telemetry logger, Data integrity checks, Achievements evaluation loop, Save migration tests, Schema doc generation).
3. Infer missing glue tasks (e.g., central `GameContext` initializer, object pooling manager integration, deterministic seed injection in run start).
4. Do NOT invent broad refactors; keep tasks atomic and additive.
5. Every task must have ≥1 deliverable path restricted to allowed roots: `Assets/Scripts/`, `Assets/Editor/`, `Assets/Resources/`, `Assets/Tests/`, `Assets/docs/`.
6. If a requirement concept lacks a plan or roadmap entry, add a task and list it under Validation & Gaps.
7. Use consistent ID conventions: reuse roadmap ids where plausible; new tasks adopt pattern `<phase>.<sequence>` or `NX.<sequence>` if phase unknown. Follow-ups: `<id>-FUP`.
8. Prioritize by gameplay visibility and core loop enablement (Combat + Card + SkillTree synergy > meta progression > telemetry > docs).
9. Provide acceptance criteria that are testable (e.g., "Playing an Attack card reduces enemy HP by card effect value and logs a combat event").
10. Align testStrategy with artifacts: runtime logic → unit tests; loops/state transitions → integration; performance baseline → benchmark harness.

## Dependency Heuristics
- Data models precede ScriptableObjects; ScriptableObjects precede runtime Systems; Systems precede UI; Persistence follows Systems; Telemetry/Performance after baseline Systems; Documentation last.

## Validation Phase (Pre-write Unity-tasks.md)
Perform consistency checks before finalizing:
- Confirm all mandatory categories from requirements have at least one task.
- Ensure no duplicate IDs.
- Each task includes ≥3 acceptanceCriteria (except XS tasks which may have ≥2).
- Each deliverable path unique across tasks OR rationale if shared.
- All referenced dependencies exist.

## Post-Generation Summary (placed at end of Unity-tasks.md)
Include counts:
- totalTasks
- byCategory
- byPriority
- followUpsAdded
- gapsAddressed

## Error Handling / Abort
Abort (do not write file) and emit concise reason if:
- Any input file missing (except optional `roadmap.md`) → specify missing paths.
- Requirements parsing yields zero systems.
- Roadmap JSON malformed.

## Example Task Block (Illustrative)
```md
### [5.4] Run Telemetry Logger
Core event logging for combat/card actions with JSONL output.
```json
{
  "id": "5.4",
  "name": "Run Telemetry Logger",
  "category": "Telemetry",
  "objective": "Log structured run events (cardPlayed, damageDealt, seed, runEnd).",
  "rationale": "Requirements: Telemetry & Logging; Roadmap gap: GAP-002",
  "deliverables": ["Assets/Scripts/Systems/Telemetry/RunTelemetryLogger.cs", "Assets/Tests/Telemetry/RunTelemetryLoggerTests.cs"],
  "codePlacement": ["Assets/Scripts/Systems/Telemetry/"],
  "artifacts": {"runtime": ["RunTelemetryLogger"], "data": [], "tests": ["RunTelemetryLoggerTests"]},
  "dependencies": ["3.2", "3.1"],
  "acceptanceCriteria": ["Logger initializes with config toggle", "Events serialized as JSON lines", "Disabled toggle prevents file creation"],
  "testStrategy": "Unit tests validating serialization + integration test hooking combat event.",
  "risk": "Medium",
  "effort": "M",
  "status": "pending",
  "priority": "P2",
  "tags": ["logging","monitoring"],
  "notes": "Retention policy deferred.",
  "origin": {"requirements": ["Telemetry & Logging"], "roadmap": ["5.4"], "gaps": ["GAP-002"]}
}
```

## Implementation Steps (FOR THIS PROMPT EXECUTION ITSELF)
1. Load input artifacts.
2. Extract systems & gaps.
3. Build task candidate list (existing + inferred + follow-ups).
4. Assign priorities & effort.
5. Generate dependency graph.
6. Validate per rules; abort if any fatal violation.
7. Write `Assets/docs/unity/Unity-tasks.md`.
8. Emit summary section.

## Scope Focus: Enabling Usage of Existing Implementation
The generated tasks MUST emphasize making previously implemented systems (cards, combat, equipment, blessings, skill tree, save system, achievements, status effects, telemetry placeholders) usable inside Unity WITHOUT creating new runtime code during this prompt run. Task types should center on:
- Light integration glue (e.g., initialization ordering, scene bootstrap orchestrator).
- Minimal UI/UX scaffolds to expose already implemented logic (e.g., card draw test panel, combat turn HUD prototype).
- Testing / validation harness tasks for existing systems (e.g., play mode test grouping, status effect verification matrix).
- Configuration & data population tasks (e.g., sample ScriptableObject sets, seed manager configuration asset).
- Observability tasks (telemetry logger hook insertion points, achievement evaluation trigger mapping) that describe needed work but do not perform it.

## Prohibited Actions (Hard Constraints for Prompt Execution)
- Do NOT invoke or trigger a Unity build, compilation pipeline, or test runner.
- Do NOT modify or create C# source files, ScriptableObjects, scenes, or assets.
- Do NOT alter `roadmap.json`, task statuses, or create plan folders.
- Do NOT delete or rename existing artifacts.
- Do NOT generate code snippets outside the JSON task definitions.
- Do NOT perform dependency installation or environment mutation.

## Task Focus Guidelines
When generating tasks, prefer those that accelerate player-visible usage of existing code over deep new feature expansion. Examples of acceptable task objectives:
- "Provide a temporary scene with a Card Hand display bound to existing CardSystem APIs."
- "Add a Turn Cycle overlay UI hooking CombatSystem events (design stub only)."
- "Create Achievement evaluation trigger mapping doc referencing implemented telemetry points."
- "Design validation checklist for Blessing evolution states using current data definitions."
- "Outline pooling verification harness referencing Performance Baseline metrics." 
All examples should be converted into properly structured tasks following the schema—without producing the described artifacts in this prompt run.

## Non-Deviation Rules
- Do not modify roadmap or requirements; this prompt is read-only for those.
- Do not create any code files; only generate the tasks markdown.
- Keep output deterministic given identical inputs.
- No refactors; tasks must be additive suggestions.
 - No build, compile, test, or asset creation actions executed by this prompt run.

## Future Extension Hooks (Document, do NOT implement now)
- Optional JSON mirror: `Unity-tasks.json` for automation.
- Automatic plan folder scaffolder prompt trigger.

## Completion Criteria
Prompt run considered successful if file is written and passes all validation rules.

## Final Output Acknowledgement
Return a brief confirmation:
`Unity-tasks.md generated: <taskCount> tasks; <followUps> follow-ups; <gaps> gaps addressed.`

---
Version: 1.0
