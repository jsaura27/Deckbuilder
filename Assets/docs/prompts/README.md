# Prompts Folder

Operational AI prompt agents orchestrating the feature lifecycle: plan → implement → review → audit/evolve → refine.

## Current Prompt Agents
| File | Role | Trigger Style | Key Outputs |
|------|------|---------------|-------------|
| `next-task.prompt.md` | Planning agent: creates next task plan folder | Zero-input | `plans/<task>/plan.md` + `plan.json` |
| `implement-task.prompt.md` | Implementation agent: executes one planned task | Zero-input | Code changes, `changes-log.json`, `summary.md` |
| `review-implementation.prompt.md` | Review agent: audits latest completed implementation | Zero-input | `review-report.md` |
| `audit-roadmap.prompt.md` | Audit/evolve agent: analyzes roadmap & requirements gaps | Optional input: `newFeatures` | `audit-report.md`, roadmap amendments |
| `implement-roadmap-changes.prompt.md` | Roadmap maintenance agent: applies selected audit recommendations | Inputs: `applyList`, `applyAll`, `enableCI`, `dryRun` | Updated `roadmap.md/.json`, `requirements.md` (conditional), implementation log |
| `testing-implementation.prompt.md` | Testing agent: generates or validates tests for specified completed task | Required input: `taskId` | Test files, `test-plan.md`, `test-report.json`, updates `summary.md` & `changes-log.json` |

> Meta (`*.meta`) files are Unity asset metadata and not part of prompt logic.

## Lifecycle Summary
1. Plan: `next-task.prompt.md` scaffolds a plan folder for the first `pending` roadmap task.
2. Implement: `implement-task.prompt.md` executes deliverables, producing audit trail & summary.
3. Test (optional): `testing-implementation.prompt.md` (requires `taskId`) adds/validates automated tests & coverage artifacts.
4. Review: `review-implementation.prompt.md` validates scope & quality, producing a review report.
5. Audit/Evolve: `audit-roadmap.prompt.md` suggests new tasks/features & coverage fixes.
6. Roadmap Maintenance: `implement-roadmap-changes.prompt.md` applies curated audit changes (supports `applyList` / `applyAll`, `dryRun`, optional `enableCI`).

## Conventions
Plan Folder Naming: kebab-case of roadmap task title (`add-combat-system`).
Prompt Inputs: Prefer reading `.json` artifacts when structure matters (roadmap, requirements, schemas).
Status Lifecycle: `pending → planned → in-progress → done/error` (review does not change status).

## Maintenance Tips
- Keep each prompt’s expected artifacts listed in `PromptArchitecture.md` updated when adding new agents.
- When adding a new agent, define: selection rule, required inputs, outputs, success criteria.
- Validate new plan folders contain consistent `plan.md` / `plan.json` before running implementation.
- Ensure `changes-log.json` schema (if present) stays aligned with any new audit fields (e.g., `testFilesCreated`).
- Bump both this README and architecture guide version numbers together.

## Versioning
Version: 1.2
Changelog:
- 1.0 Initial (planning + implementation).
- 1.1 Added review & audit prompts (see architecture guide).
- 1.2 Added testing & roadmap change implementation prompts; expanded lifecycle & maintenance section.

For full architecture, see `Assets/docs/PromptArchitecture.md`.