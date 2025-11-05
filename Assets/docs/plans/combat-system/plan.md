# Task: Combat System

## Objective

Design and implement the Combat System: a turn-based combat manager that coordinates the player and enemy turns, resolves card-driven actions and status effects, and exposes integration points for AI, equipment, and blessings.

## Prerequisites

- `Assets/docs/requirements.md`
- Existing Card System (Assets/docs/plans/card-system)
- Data schemas: `Assets/docs/schemas/cards.schema.json`, `Assets/docs/schemas/blessings.schema.json`, `Assets/docs/schemas/skilltree.schema.json`
- Unity Editor knowledge for ScriptableObject creation

## Step-by-Step Instructions

1. Analysis & Design
   - Review `requirements.md` Combat System section and relevant schemas.
   - Define core responsibilities: Turn manager, Action queue, Resolution pipeline, Status effect manager, Enemy AI interface.
   - Produce a small sequence diagram in `notes/` (optional) showing Draw -> Action -> Enemy phases.

2. Data & Models
   - Define runtime data models (C# classes) under `Assets/Scripts/Combat/`:
     - `TurnManager` (controls phase transitions)
     - `CombatState` (tracks participants, turn order, seed)
     - `ICombatant` interface and `PlayerCombatant` / `EnemyCombatant` implementations
   - Ensure data-driven definitions reference schema fields (card effects, status effects).

3. Status Effects Manager
   - Implement `StatusEffect` base and concrete types (Burn, Stun, Poison, Shield, Vulnerable).
   - Define stacking rules and duration semantics in data and tests.

4. Action & Resolution Pipeline
   - Implement an `Action` abstraction with prioritized resolution.
   - Resolve card effects as pure functions where possible; use dependency injection for side-effects (e.g., spawning VFX)

5. Enemy AI Interface
   - Define `IEnemyAI` with a method to select actions given `CombatState`.
   - Provide a simple `PatternAI` and `ConditionalAI` implementations for testing.

6. Integration Points
   - Hook equipment modifiers, blessings, and skill nodes into the resolution pipeline (via effect modifiers).
   - Expose editor utilities for simulating combat scenarios.

7. Tests & Validation
   - Add unit tests for turn transitions, damage resolution, status stacking, and deterministic seed replay.
   - Add simple integration test that runs a mock combat to completion.

## Deliverables

- `Assets/docs/plans/combat-system/plan.md` (this file)
- `Assets/docs/plans/combat-system/plan.json` (machine-readable mirror)
- `Assets/Scripts/Combat/` with skeleton classes and interfaces
- Unit tests under `Assets/Tests/CombatTests/`
- A small example scene or editor test harness to simulate a fight (optional)

## Notes

- Use relative paths like `${workspaceFolder}/Assets/Scripts/Combat/` in any scripts or editor commands.
- Follow Unity patterns: `[Serializable]` for data containers and `ScriptableObject` for persistent definitions where appropriate.
- Keep the resolution pipeline testable and free of direct UnityEngine dependencies where possible.
