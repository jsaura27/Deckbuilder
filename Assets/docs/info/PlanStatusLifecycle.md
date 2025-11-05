# Plan & Roadmap Status Lifecycle

## Goal
Enable fully automated selection and execution of tasks without manual inputs by introducing explicit status fields and folder references in the roadmap JSON. Prompts can then:
1. Auto-discover next task.
2. Generate a plan folder and mark task as `planned`.
3. Implementation agent picks the earliest `planned` task, marks it `in-progress`.
4. On success marks it `done`; on failure marks it `error` with a note.

## Status Values
| Status | Meaning | Transition Actor |
|--------|---------|------------------|
| pending | Task defined in roadmap, no plan folder yet | Initial state in `roadmap.json` |
| planned | Plan folder created (`plan.md` + `plan.json`) | Planning prompt (`next-task`) |
| in-progress | Implementation started | Implementation prompt |
| done | Implementation finished and build passed | Implementation prompt |
| error | Implementation attempt failed (mismatch/build errors) | Implementation prompt |

## Roadmap JSON Extensions (Per Task)
Add fields:
```jsonc
{
  "id": "1.3",
  "name": "Generate C# Data Models",
  "status": "pending",            // lifecycle status
  "planFolder": null,               // path like Assets/docs/plans/generate-csharp-data-models
  "startedAt": null,                // ISO timestamp when moved to in-progress
  "completedAt": null,              // ISO timestamp when moved to done
  "error": null                     // error message if status == error
}
```

## Automation Rules
### Planning Prompt (Zero Input)
1. Load `roadmap.json`.
2. Find first task with `status == pending`.
3. Derive folder name (kebab-case of `name`).
4. Create plan folder + plan pair.
5. Update task:
   - `status = "planned"`
   - `planFolder = "Assets/docs/plans/<folder>"`
   - Persist updated `roadmap.json`.
6. Return summary.

### Implementation Prompt (Zero Input)
1. Load `roadmap.json`.
2. Select oldest task with `status == planned` (by order in file or earliest planned timestamp if tracked).
3. Set `status = "in-progress"`, `startedAt = now` and persist.
4. Validate plan folder contents.
5. Execute steps; if success:
   - `status = "done"`; `completedAt = now`; clear `error`.
   Else:
   - `status = "error"`; set `error` message.
6. Persist `roadmap.json`.
7. Produce change log + summary.

## Error Handling
- If no `pending` tasks exist: planning prompt returns "All tasks planned".
- If no `planned` tasks exist: implementation prompt returns "No tasks ready for implementation".
- If plan folder incomplete: mark task `error` with reason.

## Concurrency Considerations
- Prompts should lock roadmap file (single write) or perform read-modify-write quickly to avoid race conditions.
- If another process modifies statuses mid-run, re-validate before final write.

## Future Enhancements
- Add `priority` or `blockedBy` fields for more complex sequencing.
- Introduce a checksum of plan pair to detect manual edits.
- Provide a separate audit log `roadmap-history.json` for status transitions.

## Implementation Order
1. Patch `roadmap.json` with new fields (initially `status: "pending"`).
2. Add `roadmap.schema.json` describing the extended task shape.
3. Update both prompts to remove input reliance and implement lifecycle transitions.
4. Test by running planning prompt twice (second should pick next `pending`).
5. Run implementation prompt to execute first `planned`.

---
Version: 1.0
Maintainer: Docs/Engineering