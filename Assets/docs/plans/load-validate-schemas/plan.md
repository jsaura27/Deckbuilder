# Task: Load & Validate Schemas

## Objective

Load all JSON schemas from `Assets/docs/schemas/`, verify they are syntactically valid, validate sample JSON data where present, and produce a short validation report and remediation guidance.

## Prerequisites

- Access to `Assets/docs/schemas/` and any sample JSON files.
- `Assets/docs/requirements.md` and `Assets/docs/requirements.json` for resolving ambiguities.
- Node.js or Python available for running schema validation scripts (optional but recommended).

## Step-by-Step Instructions

1. Inventory schemas
   - List files under `Assets/docs/schemas/` and note filenames and relative paths.
2. Syntactic validation
   - Parse each `.schema.json` with a JSON parser to catch syntax errors.
3. Semantic/Schema validation
   - Use a JSON Schema validator (AJV for Node.js or jsonschema/fastjsonschema for Python) to ensure each schema itself is a valid draft (e.g., Draft 07/2019-09) and that referenced $ref paths are resolvable.
4. Validate sample data
   - For each schema, locate sample data files (e.g., `Assets/docs/samples/` or inline examples). Validate samples against their schema and record pass/fail with error messages.
5. Produce report
   - Create `validation-report.md` in this folder summarizing findings: valid schemas, schemas with syntax/semantic issues, missing references, and failing samples with error excerpts.
6. Remediation guidance
   - For each failing schema/sample, provide actionable fixes (e.g., replace $ref relative paths, add missing definitions, adjust types or required lists).
7. Optional: Add automated scripts
   - Add `scripts/validate-schemas.js` (Node + AJV) or `scripts/validate_schemas.py` to automate steps 2-4.

## Deliverables

- `plan.md` (this file)
- `plan.json` (machine-readable plan)
- `validation-report.md` (created after running validations)
- Optional: `scripts/validate-schemas.js` or `scripts/validate_schemas.py`

## Notes

- Use relative paths like `Assets/docs/schemas/` when referencing files.
- If schemas reference external URIs, download or mirror them under `Assets/docs/schemas/external/` for offline validation.
- When choosing a validator, prefer one that supports the schema draft used in the repo. If draft is unknown, try Draft 07 and Draft 2019-09.
