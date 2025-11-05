---
description: "Generates per-task detailed Unity implementation step guides from Unity-tasks.md into Assets/docs/unity/tasks/ as sequential Step-XXX-<slug>.md files."
mode: "agent"
---

# Prompt: Unity Task Implementation Planner

## Objective
Transform each task defined in `Assets/docs/unity/Unity-tasks.md` into a standalone, deeply detailed implementation plan document placed under `Assets/docs/unity/tasks/`. Each plan clarifies how to execute the task inside the Unity project (without actually performing the implementation during this prompt run).

## Inputs (Implicit)
- Source task list: `Assets/docs/unity/Unity-tasks.md`
- Requirements (for cross-reference, read-only): `Assets/docs/requirements.md`, `Assets/docs/requirements.json`
- Roadmap (for status & original IDs): `Assets/docs/roadmap.json`

## Output Artifacts (Created by this Prompt Execution)
- One markdown file per task: `Assets/docs/unity/tasks/Step-<NNN>-<task-slug>.md` (NNN zero-padded sequence starting at 001 in the order tasks appear in `Unity-tasks.md`).
- Each file is self-contained and does NOT create or modify code/assets.

## File Naming Rules
1. Extract task name from heading pattern: `### [<id>] <Task Name>`.
2. Create slug: lowercase, replace spaces and invalid characters with `-`, collapse repeats, strip trailing `-`.
3. Sequence index increments per encountered task (including follow-ups); start at 1.
4. Filename pattern: `Step-<NNN>-<slug>.md` e.g., `Step-007-run-telemetry-logger.md`.

## Task Plan Document Structure
Each generated plan MUST follow this section order:
1. Title: `# Step <NNN>: <Task Name>`
2. Metadata Block (JSON fenced for machine parsing) containing:
```json
{
  "stepId": "Step-<NNN>",
  "taskId": "<original id>",
  "name": "<Task Name>",
  "category": "<copied>",
  "priority": "<P1|P2|P3>",
  "effort": "<XS|S|M|L|XL>",
  "risk": "<Low|Medium|High>",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": {
    "requirements": ["..."],
    "roadmap": ["..."],
    "gaps": ["..."]
  }
}
```
3. Overview (1–2 paragraphs summarizing objective & user-facing impact).
4. Prerequisites
   - Related systems/classes already implemented (list from rationale/artifacts).
   - External assumptions (Unity version if known; otherwise mark TODO).
5. Deliverables & Placement
   - Enumerate proposed files/folders (from task JSON: deliverables) with explanation per path (purpose, type).
   - Explicitly state: "This planning step does not create these files; they are targets for implementation." 
6. Detailed Implementation Steps (ordered list) – MUST be atomic, each step ≤ 1 clear action:
   - Include code intent (e.g., "Create MonoBehaviour CardHandPanel.cs handling draw/discard events") WITHOUT actual code blocks except short signatures.
   - Reference Unity APIs where relevant (e.g., `ScriptableObject.CreateInstance`, `Addressables`, `UnityEvent`, `Update`, `OnEnable`).
7. Data & Configuration Plan
   - If ScriptableObjects involved: outline fields & validation rules.
   - If JSON ingestion: specify schema validation call points.
8. Testing Strategy
   - Unit test cases (list) – inputs, expected outcomes.
   - Integration / play mode tests – scenario flows.
   - Edge cases (≥3) – empty data, invalid enum, performance stress.
9. Acceptance Criteria (copy & expand from task JSON; make each criteria verifiable with a named test or validation step).
10. Telemetry / Observability Hooks (if applicable) – events to emit & structure.
11. Performance Considerations – pooling, GC, update frequency guidelines.
12. Error Handling & Validation – fallback behaviors, logged warnings vs exceptions.
13. Dependencies & Ordering – tasks that must be complete beforehand; tasks unblocked after completion.
14. Risks & Mitigations – bullet list mapping risk→mitigation.
15. Rollback / Disable Plan – how to back out feature or guard with feature flag.
16. Extension Points – safe future enhancements (DO NOT implement now).
17. Glossary (optional) – define domain terms referenced.
18. Completion Checklist – condensed list for implementer sign-off.
19. Changelog Stub – instruct implementer to add entry when done.

## Implementation Step Detailing Rules
- Use imperative voice ("Add", "Create", "Inject", "Register").
- Keep steps granular: avoid multi-action sentences.
- Cross-reference source systems by name (e.g., `CombatSystem`, `CardSystem`).
- Never include large code examples; only brief signatures or pseudo-code (≤ 2 lines) when critical.

## Parsing Logic
1. Read `Unity-tasks.md`.
2. Identify all headings matching regex: `^### \[(.+?)\]\s+(.+)$`.
3. For each, capture JSON block immediately following fenced `json` snippet.
4. Parse JSON; if malformed → record error and skip file generation for that task (log summary at end).
5. Build ordered list; assign sequence numbers.

## Abort Conditions (No files written)
- `Unity-tasks.md` missing.
- Zero valid tasks parsed.
- >10% of task JSON blocks malformed.

## Summary Report
After generating all plan files, create (or update) `Assets/docs/unity/tasks/summary.index.md` containing:
- generationTimestamp (ISO)
- totalSteps
- skippedTasks (array of taskId with reason)
- categoriesCount (map category→count)
- priorityBreakdown
- effortDistribution
- fileList (array of generated filenames)

## Non-Deviation Rules
- Do NOT implement, compile, or test code; planning only.
- Do NOT modify roadmap or requirements.
- Do NOT rename or alter existing source artifacts.
- Deterministic output given identical `Unity-tasks.md`.

## Future Extension Hooks
- Optional autogen of `Unity-tasks.plans.json` combined index.
- Optional diff mode comparing previous run to detect newly added tasks.

## Completion Acknowledgement
Return: `Unity task plans generated: <totalSteps> steps; <skipped> skipped.`

---
Version: 1.0
