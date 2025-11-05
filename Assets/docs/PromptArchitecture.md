# Prompt Architecture & Usage Guide

## Overview
This Unity project uses a lightweight prompt-driven workflow to plan, implement, test, review, audit, and evolve features. Prompts live under `Assets/docs/Prompts/` and coordinate with roadmap and plan artifacts under `Assets/docs/`. Core prompt roles:

1. Planning Prompt (`Next-task.prompt.md`): Zero-input; generates plan folder for first `pending` task and sets status to `planned`.
2. Implementation Prompt (`implement-task.prompt.md`): Zero-input; implements first `planned` task and transitions status `in-progress` → `done`/`error`.
3. Testing Prompt (`testing-implementation.prompt.md`): Input `taskId` required; generates or validates automated tests for a specified completed task (appends test artifacts, updates `summary.md`).
4. Review Prompt (`review-implementation.prompt.md`): Audits most recent `done` task against its plan and produces `review-report.md`.
5. Audit/Evolve Prompt (`audit-roadmap.prompt.md`): Audits coverage/gaps; optionally integrates new features provided via `${input:newFeatures}` and updates roadmap/requirements.
6. Roadmap Maintenance Prompt (`implement-roadmap-changes.prompt.md`): Applies approved audit recommendations to the roadmap (add/remove/update tasks) while preserving status history.

Supporting documents (requirements, roadmap, plans, schemas) provide structured inputs ensuring consistency and traceability.

## Directory Structure (Docs Layer)
```
Assets/docs/
  Prompts/                  # Operational prompts (agents)
    Next-task.prompt.md     # Planning agent
  implement-task.prompt.md# Implementation agent
  testing-implementation.prompt.md# Testing agent
  review-implementation.prompt.md # Review agent
  audit-roadmap.prompt.md         # Audit/evolve agent
  implement-roadmap-changes.prompt.md # Roadmap maintenance agent
  requirements.md/.json     # Functional requirements (source-of-truth design)
  roadmap.md/.json          # Phased task breakdown with completion state
  schemas/                  # JSON Schemas for data-driven content types
  plans/                    # One subfolder per generated task plan
    <task-folder>/
      plan.md               # Human readable implementation steps
      plan.json             # Machine readable representation
      mapping.json?         # (Optional) Entity->module mapping artifact
      changes-log.json      # Implementation audit trail (created by implement prompt)
      summary.md            # High-level summary and test instructions
  PromptArchitecture.md     # (This guide)
```

## Prompt Roles & Contracts
### 1. `Next-task.prompt.md`
Purpose: Inspect the roadmap, select the next unchecked task, and scaffold its plan folder.

Inputs:
- `roadmap.md` / `roadmap.json`
- `requirements.md` / `requirements.json`
- `schemas/*.schema.json`
- Existing `plans/` subfolders to avoid duplicates.

Outputs (under `Assets/docs/plans/<kebab-task-name>/`):
- `plan.md` (Human tutorial-style document)
- `plan.json` (Structured mirror of `plan.md`)
- Optional: `mapping.json` or other supplemental artifacts.

Key Constraints:
- Must not mark tasks complete—only produce planning assets.
- `plan.md` and `plan.json` must have identical task name, steps, and deliverables.
- Folder name is kebab-case transformation of the roadmap task title.

Success Criteria:
- New folder created with consistent plan pair.
- Steps are actionable, atomic, and reference concrete file paths.

### 2. `implement-task.prompt.md`
Purpose: Zero-input implementation of the next `planned` task, enforcing strict scope.

Implicit Inputs:
- Roadmap JSON for task selection & status updates.
- Selected plan folder's `plan.md` / `plan.json` pair.

Outputs:
- Implemented deliverables + `changes-log.json` + `summary.md`.
- Roadmap task status updated (`in-progress` → `done`/`error`).

Success Criteria:
- Plan pair matches; build passes without new errors; all deliverables present.

### 3. `testing-implementation.prompt.md`
Purpose: Add or validate tests for a specified completed task (by `taskId`) to improve quality coverage.

Inputs:
- Required explicit: `taskId` (string roadmap task identifier).
- Implicit: latest plan folder (`plan.md` / `plan.json`), `changes-log.json`, codebase test framework folders.

Outputs:
- New/updated test files under appropriate test directory.
- `test-plan.md` and `test-report.json` artifacts.
- Augmented `summary.md` with test execution instructions & coverage notes.

Success Criteria:
- Tests align with deliverables steps.
- >= target coverage threshold (e.g., 80%) or partial status explained.
- At least one happy path and one failure/edge case per new feature.
- No unrelated files modified.

### 4. `review-implementation.prompt.md`
Purpose: Audit latest `done` task; verify deliverables and produce `review-report.md`.

### 5. `audit-roadmap.prompt.md`
Purpose: Identify roadmap/requirements gaps, suggest or add tasks, integrate optional new features list.

### 6. `implement-roadmap-changes.prompt.md`
Purpose: Normalize and apply curated changes output by an audit run (e.g., new tasks, status adjustments, priority tags).

Inputs:
- Implicit: Current `roadmap.md` / `roadmap.json`, `audit-report.md` / `audit-report.json` if present, `requirements.md` / `requirements.json`.
- Optional explicit runtime inputs:
  - `applyList` (selection tokens)
  - `applyAll` (boolean-like)
  - `enableCI` (boolean-like)
  - `dryRun` (boolean-like)

Outputs:
- Updated roadmap artifacts with consistent formatting & preserved historical fields (timestamps, previous statuses where needed).
- Updated requirements artifacts (conditional on changes requiring new sections).
- Implementation Log appended to audit report.
- `implementation-summary.json` and optionally `implementation-dryrun.md`.

Success Criteria:
- Only intended selected changes applied.
- Removed tasks retained historically if configured.
- Roadmap & requirements remain valid JSON/markdown pairing.
- Summary & log accurately reflect counts & decisions.

## Lifecycle Flow (Automated)
1. Author/evolve roadmap & requirements (manual or via audit prompt).
2. Planning prompt picks first `pending`, creates plan, sets `planned`.
3. Implementation prompt picks first `planned`, executes, sets `in-progress` then `done`/`error`.
4. Testing prompt (optional) adds/validates tests for latest task.
5. Review prompt (optional) audits latest `done` task.
6. Audit prompt (optional) adds missing tasks or features; updates docs.
7. Roadmap maintenance prompt applies accepted audit changes.
8. Repeat until all tasks `done`.

## Artifact Relationships
| Artifact | Produced By | Consumed By | Purpose |
|----------|-------------|------------|---------|
| requirements.md/.json | Human + audit prompt | All prompts | Global feature scope |
| roadmap.md/.json | Human + planning/audit/roadmap-change prompts | Planning/Implementation/Review/Testing | Task sequencing & status lifecycle |
| schemas/*.schema.json | Human/Tool | All prompts | Data model definitions |
| plans/<task>/plan.md | Planning prompt | Implementation + Review + Testing | Human instruction set |
| plans/<task>/plan.json | Planning prompt | Implementation + Review + Testing | Machine-executable structure |
| plans/<task>/mapping.json | Planning prompt/manual | Implementation + Review + Testing | Mapping reference |
| changes-log.json | Implementation prompt (+ appended by Testing prompt) | Review + Audit + Testing | Implementation & test audit trail |
| summary.md | Implementation + Testing prompt | Review + QA | Verification & test instructions |
| test files (e.g., *.Tests.cs) | Testing prompt | Review + CI | Automated validation suite |
| review-report.md | Review prompt | Audit prompt | Compliance snapshot |
| audit-report.md | Audit prompt | Roadmap-change prompt | Coverage & recommendations |
| roadmap-change-log.md/json | Roadmap-change prompt | Audit + Maintainers | Traceability of modifications |

## Usage Patterns & Tips
### Generating a New Plan
- Ensure roadmap task remains unchecked.
- Provide clear task names—avoid ambiguous numbering only.
- After generation, manually inspect `plan.md` for completeness (add examples if needed). Keep `plan.json` in sync.

### Implementing a Plan
- Provide the full relative path to the plan folder as input (`Assets/docs/plans/<task>`).
- Validate that no unrelated edits occurred (compare `filesCreated` & `filesEdited` in `changes-log.json`).
- If ambiguity surfaces mid-implementation, prefer minimal placeholders and log them.

### Extending Schemas
- Extend JSON schemas before generating data-model tasks to avoid churn.
- Keep schema changes versioned; consider adding a `schema-version` property.

### Naming Conventions
- Plan folders: kebab-case (lowercase, hyphen-separated).
- ScriptableObject classes: `PascalCaseDefinition` (`CardDefinition`).
- Runtime managers/services: `PascalCaseManager` or `PascalCaseSystem` (`CombatManager`, `CardSystem`).

## Quality & Governance
| Check | When | Responsible |
|-------|------|-------------|
| Plan pair consistency | After planning generation | Reviewer |
| Implementation scope adherence | After implementation | Reviewer/CI |
| Test coverage added/validated | After testing prompt run | Testing prompt/CI |
| Build passes (no new errors) | After implementation & testing | Implementation + Testing prompts |
| Roadmap changes applied correctly | After roadmap-change prompt | Maintainer/Reviewer |
| Roadmap completion updated | After review | Maintainer |

## Improving Prompts (Enhancements So Far)
Implemented:
- Zero-input planning & implementation lifecycle.
- Roadmap status fields with timestamps.
- Review & audit prompts.
- Testing & roadmap maintenance prompts with explicit inputs.
- Expanded artifact relationship table.

Future Improvements:
- Validate `changes-log.json` against `changes-log.schema.json` (now added).
- Add plan folder `status.json` with hash of plan pair.
- CI integration prompt for automated nightly review.
- Rollback prompt to revert last implementation.
- Coverage analysis prompt to measure test depth per feature.
- Dependency impact prompt to forecast changes before applying roadmap modifications.

## FAQ
Q: Can I edit a generated plan before implementation?
A: Yes—update both `plan.md` and `plan.json` together. If structure changes, re-run the implementation prompt only after alignment.

Q: How do I rollback an implementation?
A: Use VCS (git) to revert commits; consult `changes-log.json` for impacted file list.

Q: Can multiple plans be implemented in one run?
A: Not with the current prompts. Run the implementation prompt separately per plan folder to maintain granular audit trails.

## Glossary
- Plan Folder: Directory under `Assets/docs/plans/` containing the plan pair and related artifacts.
- Plan Pair: The coupled `plan.md` + `plan.json` describing one task.
- Implementation Prompt: Agent executing exactly one plan folder.

---
Version: 1.2
Maintainer: Docs/Engineering
Changelog:
- 1.0 Initial prompt architecture guide created.
- 1.1 Added zero-input lifecycle plus review & audit prompts.
 - 1.2 Added testing & roadmap maintenance prompts; expanded artifact table & lifecycle.
