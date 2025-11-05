# Validation Report: Load & Validate Schemas

Generated: 2025-10-31T00:00:00Z

Summary
-------

- Schemas found: `blessings.schema.json`, `cards.schema.json`, `changes-log.schema.json`, `equipment.schema.json`, `roadmap.schema.json`, `skilltree.schema.json` (all under `Assets/docs/schemas/`).
- Syntax check: All listed schema files parsed successfully as JSON.
- $ref inspection: no `$ref` occurrences were found in these schema files.
- Schema drafts: Mix of Draft-07 and 2020-12 (see per-file metadata below).

Per-file notes
--------------

- `blessings.schema.json` — $schema: draft-07 — parsed OK.
- `cards.schema.json` — $schema: draft-07 — parsed OK.
- `changes-log.schema.json` — $schema: 2020-12 — parsed OK.
- `equipment.schema.json` — $schema: draft-07 — parsed OK.
- `roadmap.schema.json` — $schema: 2020-12 — parsed OK.
- `skilltree.schema.json` — $schema: draft-07 — parsed OK.

Recommendations
---------------

1. If you plan to use automated schema validators, pick one that supports both Draft-07 and 2020-12, or validate files using the appropriate validator per draft.
2. If any schemas will reference other schemas in the future, adopt a consistent $id and $ref pattern and consider mirroring external refs under `Assets/docs/schemas/external/` for offline validation.
3. Optionally add an automated validation script in the plan deliverables (Node.js + AJV or Python + jsonschema). See plan.md step 7.
