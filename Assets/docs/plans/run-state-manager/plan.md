# Task: Run State Manager

## Objective
Design and implement a robust Run State Manager to track the lifecycle and state of a single playthrough (a "run"). The manager will encapsulate run-scoped data (player progression, deck, RNG seed, events, skill tree choices) and provide APIs for starting, ending, serializing, and resetting runs.

## Prerequisites
- Review `Assets/docs/requirements.md` and `Assets/docs/requirements.json` for definitions of run lifecycle and persistence needs.
- Reference `Assets/docs/schemas/` for data models (especially `roadmap.schema.json` and related schemas).
- Existing data models under `Assets/Scripts/DataModels/` (or plan to generate POCOs from schema if missing).
- Unity patterns knowledge: use of ScriptableObject for configurable data, and [Serializable] POCOs for runtime state.

## Step-by-Step Instructions
1. Design the RunState data model
   - Define a serializable C# class `RunState` containing:
     - string runId (GUID)
     - long seed
     - DateTime startedAt
     - DateTime? endedAt
     - PlayerProgress playerProgress (level, XP, chosen class)
     - DeckState deckState (draw pile, hand, discard, extra zones)
     - List<string> acquiredBlessings (ids)
     - List<string> acquiredEquipment (ids)
     - SkillTreeSelection skillTreeSelection
     - Dictionary<string, object> metadata
   - Add JSON schema or match existing schema if available.

2. Implement RunStateManager service
   - Create `Assets/Scripts/Runtime/RunState/RunState.cs` and `RunStateManager.cs`.
   - Responsibilities:
     - CreateNewRun(seed?, metadata?) -> RunState
     - LoadRun(serializedData) -> RunState
     - SaveRun(runState) -> serialized JSON
     - EndRun(result) -> sets endedAt, writes telemetry event
     - ResetRun() -> clears in-memory run state and notifies systems
   - Expose events: OnRunStarted, OnRunEnded, OnRunReset
   - Make service injectable (singleton via Game bootstrap or DI container)

3. Serialization & persistence
   - Provide JSON serialization using Unity's `JsonUtility` for lightweight use, and a fallback to `Newtonsoft.Json` (if available) for richer types.
   - Store meta progression separately from RunState (persist under `Application.persistentDataPath` with version tag).
   - Implement migration hooks keyed by save version.

4. Editor tooling (optional)
   - Add a small EditorWindow to view and load saved RunState snapshots for debugging.

5. Tests
   - Unit tests for: creating runs, deterministic RNG seed behavior, saving/loading roundtrip, reset semantics.
   - Integration test to ensure RunStateManager triggers OnRunStarted and OnRunEnded flows.

## Deliverables
- `Assets/docs/plans/run-state-manager/plan.md` (this file)
- `Assets/docs/plans/run-state-manager/plan.json` (machine-readable mirror)
- New runtime scripts under `Assets/Scripts/Runtime/RunState/` (RunState.cs, RunStateManager.cs)
- Unit tests under `Assets/Tests/RunState/`

## Notes
- Use `${workspaceFolder}` variables when referencing file locations in scripts or editor tools.
- Ensure run serialization includes a version tag and schema validation for future-proofing.
- Keep RunState compact; large runtime logs or telemetry should be stored separately to avoid bloated save files.