---
description: "Audit the latest completed task's implementation against its plan and log discrepancies"
mode: "agent"
model: "gpt-5 mini"
tools:
 - "file-system"
 - "editor"
 - "build"
---

# Prompt: Review & Fix Latest Implementation

## Purpose
Automatically inspects the most recently completed roadmap task (`status == "done"`) and validates its delivered artifacts against the corresponding plan folder. If discrepancies exist, it actively fixes them: creates missing deliverables, repairs code/content mismatches, adds/expands tests, and re-runs builds until the implementation matches the plan or a terminal error condition is reached. Produces a comprehensive review report of original issues and applied fixes.

## Implicit Inputs
- Roadmap: `Assets/docs/roadmap.json`
- Plan folders: `Assets/docs/plans/*/`
- Requirements & schemas for contextual validation.

## Selection Logic
1. Load roadmap tasks with `status == "done"`.
2. Choose the one with latest `completedAt` timestamp.
3. Resolve `planFolder` path; assert presence of `plan.md`, `plan.json`, `changes-log.json`, `summary.md`.
4. Abort with message if no done tasks exist.

## Validation & Fix Steps
1. Parse `plan.json` steps & deliverables.
2. Cross-check `changes-log.json` filesCreated/filesEdited against deliverables list.
3. Ensure each deliverable path exists.
4. Confirm `summary.md` includes test instructions (search for 'How to' or 'Test').
5. Run an initial build (mandatory); capture current compile errors involving deliverable and changed files.
6. Compare timestamps (startedAt < completedAt).
7. For each step, attempt heuristic verification (e.g., if step mentions 'Create ScriptableObject Base Classes' verify existence of corresponding `.cs` files).
8. Inspect `changes-log.json.tests` ensuring `executed == true`, `failures.length == 0`; otherwise record discrepancy.
9. Verify every plan step has trace: file created/edited or explicit decision entry; missing traces become UNFULFILLED_STEP discrepancies.
10. Begin Fix Phase if any discrepancies were found.

### Fix Phase Workflow
Perform targeted corrections in controlled passes (max 3 full cycles):
1. Missing deliverables: create required files/assets exactly as specified (scripts with correct class names, placeholder data assets) under proper paths.
2. Content mismatches: edit existing files to align class names, namespaces, serialized fields, or method signatures with plan.
3. Tests:
	 - If NO_TESTS: generate minimal tests mirroring rules from implement prompt; then add at least one behavior assertion per public method returning a primitive or bool.
	 - If TEST_FAILURE: adjust code or tests minimally to achieve green; prefer fixing implementation over muting test unless test expectation conflicts with plan.
	 - Follow canonical Unity test folder placement rules as defined in Assets/Tests/README.md
4. Build errors (BUILD_ERROR): apply minimal fixes (missing `using`, type name corrections, add stub methods) while avoiding speculative large refactors.
5. Unfulfilled steps: create remaining artifacts or log explicit rationale if truly non-actionable; attempt code addition if plan implies functionality (e.g., method stub for described behavior).
6. Summary gaps: update `summary.md` adding missing sections.
7. Timestamps anomaly: do not alter historical values; log but continue.
8. After each pass, re-run build; if still failing and not improved, proceed to next pass. Abort after 3 passes and classify severity as `critical`.

All modifications must be logged in a new `review-fixes-log.json` placed in the plan folder capturing:
```
{
	"task": "<task-id>",
	"timestamp": "<ISO>",
	"passes": [
		{
			"index": 1,
			"discrepanciesFound": ["MISSING_FILE: Assets/Scripts/X.cs"],
			"actions": [
				{"type": "create", "path": "Assets/Scripts/X.cs", "reason": "Plan deliverable missing"},
				{"type": "edit", "path": "Assets/Scripts/Y.cs", "reason": "Align class name with plan"}
			],
			"buildStatus": "success",
			"testSummary": "Generated 3 new tests, all passing"
		}
	],
	"finalStatus": "success|partial|critical",
	"remainingDiscrepancies": [],
	"notes": "<extra-context>"
}
```

If `review-fixes-log.json.finalStatus == "success"` and roadmap task previously `done` but had hidden gaps, no roadmap status change is required; optionally append a `reviewedAt` timestamp inside roadmap task object if schema permits (only if field already exists—do not extend schema otherwise). If finalStatus is `critical`, create a follow-up roadmap task recommendation in report.

### Discrepancy Categories
Use structured categories for clarity:
- MISSING_FILE: Deliverable path listed but not present.
- CONTENT_MISMATCH: File exists but lacks expected class/asset name or semantics inferred from step.
- NO_TESTS: Tests block absent or not executed.
- TEST_FAILURE: Any test failure listed.
- BUILD_ERROR: Compile error referencing a deliverable/changed file.
- TIMESTAMP_ANOMALY: Invalid or out-of-order started/completed times.
- UNFULFILLED_STEP: Plan step lacking artifact or decision rationale.
- SUMMARY_GAP: `summary.md` missing required sections (e.g., Test Execution).

### Remediation & Follow-ups
If after Fix Phase any discrepancies remain, generate `followups.json` describing unresolved gaps and suggested roadmap tasks.
Structure:
```
{
	"task": "<task-id>",
	"missingDeliverables": ["Assets/Scripts/..."],
	"unfulfilledSteps": ["Step description"],
	"testGaps": ["ClassNameTests.cs not present"],
	"suggestedTasks": [
		{
			"id": "<task-id>-tests",
			"title": "Add comprehensive tests for <ClassName>",
			"rationale": "Auto-generated tests were minimal; need coverage for edge cases.",
			"estimatedEffort": "S"
		}
	]
}
```
Include optional `buildFixHints` for remaining errors. If ALL discrepancies resolved, `followups.json` may be omitted.

### Severity & Outcome
Severity determined AFTER fix passes:
- success: All discrepancies resolved; tests passing; build clean.
- partial: Improvements made but some non-critical discrepancies remain (e.g., timestamp anomaly, minor naming issues) without blocking build/tests.
- critical: Blocking issues remain (build failures, missing deliverables, failing tests) after 3 passes.
If outcome is partial or critical, recommendations must list explicit actionable next steps.

## Reporting
Create or update `review-report.md` inside the plan folder containing:
- Task id & name
- Original discrepancy summary (before fixes)
- Fix passes overview (table-like list with actions and results)
- Final deliverables checklist (all present?)
- Remaining discrepancies (if any) with categories
- Build and test final status
- Outcome severity (success | partial | critical)
- Recommendations & suggested follow-up tasks
- Paths of newly created or edited files during review
If `review-fixes-log.json` exists, reference it.

## Output Summary
Return confirmation with:
- Task id
- Discrepancies found initially
- Discrepancies remaining
- Passes executed
- Final build & test status
- Outcome severity
- Follow-up task count (if any)

## Abort Conditions
- No completed tasks.
- Missing plan folder artifacts.

## Modification Rules
The reviewer MAY modify or create source files strictly to fulfill plan deliverables or fix introduced errors. No broad refactors; changes must be minimal and goal-aligned. Log every change in `review-fixes-log.json`.

---
Version: 2.0 (active fixer)