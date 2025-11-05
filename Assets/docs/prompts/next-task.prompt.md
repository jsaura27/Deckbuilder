---
description: "Generate an implementation plan for the next uncompleted roadmap task"
mode: "agent"
model: "gpt-5 mini"
tools:
 - "file-system"
 - "editor"
---

# Prompt: Generate Implementation Plan for Next Roadmap Task

Description

Acts as a zero-input planning agent. It auto-selects the next task from `Assets/docs/roadmap.json` needing a plan:

- Load roadmap JSON and pick the first task with `status == "pending"` (fallback: first with `done == false` if `status` absent).
- Analyze context (phase, dependencies, schemas, requirements).
- Generate a `plan.md` + `plan.json` pair in a new plan folder.
- Update roadmap task to `status = "planned"` and attach `planFolder` path.

Implicit Inputs (auto-discovered)

- Roadmap: `Assets/docs/roadmap.json` (+ markdown for human context)
- Requirements: `Assets/docs/requirements.md` / `Assets/docs/requirements.json`
- Schemas: `Assets/docs/schemas/*.schema.json`
- Existing plans: enumerate `Assets/docs/plans/*/plan.json`

Responsibilities

1. Task Selection
- Parse roadmap JSON; select first `pending` (or legacy `done == false`).
- Extract id, name, phase; ensure no duplicate plan folder.

2. Folder Creation
- Derive kebab-case folder name.
- Create folder under `Assets/docs/plans/`.
- Store folder path for roadmap update.

3. Output Files (place under the created task folder)

- `plan.md` (Human-readable)
	Structure:
	# Task: [Task Name]
	## Objective
	## Prerequisites
	## Step-by-Step Instructions
	## Deliverables
	## Notes

- `plan.json` (Machine-readable mirror)
	{
		"task": "[Task Name]",
		"objective": "[Short description]",
		"prerequisites": [ ... ],
		"steps": [ { "step": 1, "title": "...", "description": "..." } ],
		"deliverables": [ ... ],
		"notes": [ ... ]
	}

- Additional mapping file(s) if relevant (e.g., `mapping.json`).

Formatting and references

- Use relative paths (`Assets/docs/requirements.md`).
- Variable syntax: `${workspaceFolder}`, `${workspaceFolderBasename}`, `${file}`, `${fileBasename}`, `${fileDirname}`, `${input:variableName}`.

Behavior and constraints

- Use requirements for ambiguity resolution.
- Reference schemas for data modeling tasks.
- Guarantee `plan.md` and `plan.json` mirror task structure.
- After generation, patch roadmap task: add `planFolder`, set `status` to `planned`.
- Abort with message if no pending tasks remain.

Examples & Hints

- Example folder naming: `Task1.3: Generate C# Data Models` => `generate-csharp-data-models`.
- Unity tips: include destination folders (Assets/Scripts/DataModels/), mention [Serializable] and ScriptableObject patterns.

Finish

- Write plan files.
- Update roadmap JSON task status.
- Return confirmation: task id, plan folder path, remaining pending count.
