---
description: "Audit roadmap & requirements for structural & semantic gaps; generate prioritized improvement actions; optionally integrate new requested features"
mode: "agent"
model: "gpt-5"
tools:
 - "file-system"
 - "editor"
---

# Prompt: Audit & Evolve Roadmap and Requirements

## Purpose
Analyze current `requirements.md/.json` and `roadmap.md/.json` plus entity/data schemas to identify missing tasks, inconsistencies, sequencing issues, or enhancement opportunities. Output a rigorous, prioritized improvement list. Optionally integrate user-provided feature requests into the roadmap & requirements while preserving existing task IDs and status history.

## Input Variables
Optional:
- `${input:newFeatures}` (string; may be empty) — newline or bullet-separated feature proposals (e.g., "Add Achievement System", "Introduce status effect: Bleed").
Internal/Implicit (must read):
- `Assets/docs/requirements.md` / `requirements.json`
- `Assets/docs/roadmap.md` / `roadmap.json`
- `Assets/docs/schemas/*.schema.json`
- Existing plan folders (`Assets/docs/plans/**`) for cross-check
- Any status or lifecycle docs (e.g., `PlanStatusLifecycle.md`) to interpret statuses.

## Assumptions & Constraints
If a JSON variant of a doc is missing, fall back to Markdown only. Never invent data; mark unavailable artifacts explicitly in the audit report. Preserve formatting of unchanged sections. Abort gracefully only if write permissions fail.

## Audit Responsibilities
Perform the following steps explicitly:
1. Extract canonical list of major systems, entities, data models, mechanics, and cross-cutting concerns (e.g., persistence, telemetry) from requirements.
2. Extract roadmap phases & tasks; normalize each task record: `{phase, id, title, status, planFolder?, dependencies?, tags?}` (add inferred fields only in report, do not write back unless new tasks added).
3. Build coverage matrix: system/entity -> list of task IDs touching it. Include counts & phase distribution.
4. Gap detection (flag with severity):
  - Systems/entities in requirements with zero tasks (Critical).
  - Systems/entities with only future phases but missing foundational earlier prerequisites (High).
  - Tasks referencing plan folders that are missing (Medium).
  - Tasks missing any associated plan while similar peers have one (Medium).
  - Stalled statuses (e.g., `error`, `blocked`, `stuck`) older than threshold if timestamp data exists; if not, just list by status (Info).
  - Schema-defined properties not referenced by any task (Low) and requirement-mentioned entities lacking a schema file (High).
5. Consistency checks:
  - Roadmap phase ordering vs dependency declarations (if a task depends on a later-phase task, flag).
  - Duplicate or near-duplicate task titles (fuzzy match).
  - Ambiguous verbs ("improve", "optimize") without measurable criteria.
6. Enhancement recommendation categories: Testing, Tooling & Automation, Monitoring/Telemetry, Performance & Profiling, Data Validation & Integrity, UX Polish, Documentation, Risk Mitigation.
7. Prioritize all gaps & recommendations using scoring: `priorityScore = severityWeight + coverageImpact + dependencyUnblock + riskMitigationBonus`. Provide weights used.
8. Produce ordered action list with rationale and suggested next concrete task stubs.
9. Summarize current progress metrics: total tasks, % done, % in-progress, % blocked, system coverage percentages.

## Integration of New Features (if `${input:newFeatures}` provided)
Steps:
1. Parse lines; discard empty & duplicate after normalization. Assign slug: kebab-case alphanumerics only.
2. Classify by keyword heuristics (e.g., "schema", "data" -> early; "system", "engine" -> mid; "refactor", "optimize" -> late; security/test -> late mid). Allow override if user supplies `[PhaseX]` tag inline.
3. Determine next incremental task ID within chosen phase (format `PhaseN next id N.x` where x increments).
4. Append new tasks to `roadmap.json` with fields: `{id, title, status:"pending", slug, createdDate: ISO8601, tags:["feature-request"], planFolder:null}`.
5. Insert markdown checklist in `roadmap.md` under phase header: `- [ ] (NEW) <title>`.
6. If feature implies requirements changes (new entity/system), update or create "Pending Additions" section in `requirements.md` with bullet: `- <Entity/System>: rationale sentence`.
7. Log all added tasks in audit report.
8. Do not modify existing done tasks except to add cross-reference note (non-invasive comment) if directly related.

## Safety & Consistency Rules
- Never renumber existing tasks.
- New task IDs must not collide; validate uniqueness before write.
- Preserve task `status` values; if unknown pattern encountered, list under "Unrecognized Status Values" in report.
- Do not alter plan folder references or delete content.
- All writes must be idempotent regarding previously added tasks (detect if a slug already exists, skip duplicate addition and report skipped).
- Fallback logic: if JSON file missing, only update Markdown and include "JSON missing" note in report.
- If write fails, abort early with explicit error section.

## Output Artifacts
Must produce or update:
1. `roadmap.json` (only if new tasks) & `roadmap.md` (phase checklist entries).
2. `requirements.md` (only if new "Pending Additions" needed).
3. `Assets/docs/audit-report.md` containing:
  - Executive summary (1-2 paragraphs).
  - Coverage matrix (tabular markdown) with counts & % coverage.
  - Progress metrics.
  - Gap list with severity & priorityScore sorted descending.
  - Enhancement recommendations (grouped by category).
  - Newly added tasks (if any) with IDs.
  - Skipped duplicate feature requests (if any).
  - Schema discrepancies (missing schemas, orphan properties, unused properties).
  - Unrecognized status values.
  - Next 5 concrete task suggestions (well-scoped, testable).
  - Methodology & weighting explanation.
4. Final summary line: `Summary: tasks=<total> gaps=<count> added=<count> pending=<pendingCount>`.

### Companion JSON Output (Optional)
Additionally create/update `Assets/docs/audit-report.json` mirroring key structured data for automation. Schema:
```
{
  "summary": {
    "totalTasks": number,
    "gaps": number,
    "tasksAdded": number,
    "pendingCount": number,
    "coveragePercent": number,
    "donePercent": number,
    "blockedPercent": number
  },
  "systems": [
    {
      "name": string,
      "tasks": [string],
      "coveragePhaseDistribution": { "phase": string, "count": number }[]
    }
  ],
  "gaps": [
    {
      "id": string,               // synthetic identifier e.g. GAP-001
      "system": string|null,
      "description": string,
      "severity": "Critical"|"High"|"Medium"|"Low"|"Info",
      "priorityScore": number
    }
  ],
  "recommendations": [
    {
      "category": string,
      "title": string,
      "rationale": string,
      "suggestedTaskStub": string,
      "priorityScore": number
    }
  ],
  "newTasks": [
    {
      "id": string,
      "phase": string,
      "title": string,
      "slug": string,
      "status": string
    }
  ],
  "schemaDiscrepancies": {
    "missingSchemas": string[],
    "unusedProperties": [ { "schema": string, "property": string } ],
    "orphanEntities": string[]
  },
  "unrecognizedStatuses": string[],
  "nextTasks": [
    { "title": string, "objective": string, "acceptance": string }
  ],
  "weights": {
    "severity": { "Critical": number, "High": number, "Medium": number, "Low": number, "Info": number },
    "formula": string
  }
}
```
Rules:
- Omit arrays that would be empty OR include them as empty arrays (choose one strategy and document; prefer empty arrays for stable shape).
- Ensure numeric fields are numbers (no strings).
- Keep ordering of top-level keys as listed for readability.
- If any section omitted due to missing source data (e.g., schemas), include a `"_warnings": ["Schemas unavailable"]` field at root.

## Edge Cases & Handling
- Empty requirements: produce critical gap for "All systems undefined" and recommend creating baseline.
- No schemas present: one high-severity discrepancy; skip property cross-ref.
- All tasks already covered: still produce enhancement recommendations.
- Large number of new features (>25): batch additions, cap at 25, rest queued (report overflow list).
- Circular dependencies among tasks: detect and list cycle chain.
- Inconsistent phase naming (e.g., "Phase 1" vs "Phase1"): standardize in report, do not modify originals.
- Timestamp absent: skip age-based stall detection gracefully.

## Abort Conditions
- Write-protected documents (cannot update) — abort with explanation.
- JSON parse failure in existing docs — continue using Markdown fallback, list parse error.

## Finish
Return final summary line and ensure audit report contains all sections. If no changes applied, explicitly state "No modifications made; informational audit only.".

## Quality & Verification
Before finishing, validate:
- New task IDs unique.
- Markdown phase insertion occurred under correct header.
- "Pending Additions" section created only once.
- Audit report includes required sections; if a section has no content, include placeholder note.

## Priority Weights (Reference)
Define severityWeight mapping: Critical=5, High=4, Medium=3, Low=2, Info=1. coverageImpact = ceil(uncoveredSystems/3). dependencyUnblock = 2 if directly unblocks blocked tasks else 0. riskMitigationBonus = 1 if reduces high-risk area. Document formula.

## Success Criteria
An effective run produces a comprehensive audit report with actionable, prioritized list, integrates new features (if provided) without corrupting existing roadmap, and summarizes metrics clearly.

---
Version: 1.0