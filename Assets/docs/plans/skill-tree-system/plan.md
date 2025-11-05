# Task: Skill Tree System

## Objective

Design and implement the Skill Tree System for the game: branch selection UI, node unlock mechanics, prerequisite validation, conditional unlock evaluation, and class evolution triggers integrated into the run lifecycle.

## Prerequisites

- Assets/docs/requirements.md
- Assets/docs/requirements.json
- Assets/docs/schemas/skilltree.schema.json
- Existing ScriptableObject base class: Assets/Scripts/ScriptableObjects/Definitions/SkillTreeDefinition.cs
- Existing data model: Assets/Scripts/DataModels/SkillTreeDefinition.cs
- Character/class system foundation (for classId and class evolution)
- Run state manager (for per-run persistence and reset)

## Step-by-Step Instructions

1. Review and extend data model
   - Audit `Assets/docs/schemas/skilltree.schema.json` for completeness.
   - Ensure SkillNode includes: id, type, cost (skill points or currency), prerequisites, conditionalUnlock expression, effect definition.
   - Define branch structure: Offense, Defense, Utility, Chaos (4 branches per class).
   - Validate that the existing SkillTreeDefinition ScriptableObject matches schema requirements.

2. Implement runtime data structures
   - Create `SkillTreeRuntimeState` class under `Assets/Scripts/Systems/SkillTree/` to track:
     - Selected branch (locked at run start)
     - Unlocked nodes (list of node ids)
     - Available skill points
     - Current class evolution stage
   - Add serialization support for run snapshot persistence (optional).

3. Create SkillTreeService interface and implementation
   - Define `ISkillTreeService` interface under `Assets/Scripts/Services/`:
     - `SelectBranch(string branchName)` - locks branch choice at run start
     - `UnlockNode(string nodeId)` - validates prerequisites and cost, applies effect
     - `CanUnlockNode(string nodeId)` - checks prerequisites, cost, and conditional unlocks
     - `GetUnlockedNodes()` - returns current unlocked node list
     - `GetAvailableNodes()` - returns nodes that can be unlocked now
     - `ResetTree()` - clears state for new run
   - Implement `SkillTreeManager : MonoBehaviour, ISkillTreeService` under `Assets/Scripts/Systems/SkillTree/`.
   - Use dependency injection pattern to allow other systems to access ISkillTreeService.

4. Implement prerequisite validation
   - For each node, check that all prerequisite node ids are in the unlocked set.
   - Validate prerequisites graph at load time to detect circular dependencies or missing nodes.
   - Log validation warnings in editor mode.

5. Implement conditional unlock system
   - Define a simple expression evaluator for conditionalUnlock strings (e.g., "no-damage", "three-kills-without-card-play", "win-X-battles-without-damage").
   - Create `ConditionalUnlockEvaluator` class under `Assets/Scripts/Systems/SkillTree/Conditions/`.
   - Hook into run event system to track conditions (combat events, card plays, damage taken, etc.).
   - Store condition progress in SkillTreeRuntimeState.

6. Class evolution integration
   - Define class evolution rules in skill tree data (e.g., Warrior → Berserker when specific node unlocked).
   - Add `classEvolution` field to SkillNode schema and data model if needed.
   - Trigger class evolution through SkillTreeService when evolution node is unlocked.
   - Integrate with character system to apply class evolution (stat changes, ability unlocks).

7. Branch selection UI
   - Create UI prefab under `Assets/Prefabs/UI/SkillTree/`:
     - Branch selection screen (shown at run start)
     - Skill tree panel (in-run progression UI)
   - Implement `SkillTreeUI` MonoBehaviour under `Assets/Scripts/UI/`:
     - Display 4 branches with descriptions
     - Highlight available nodes
     - Show prerequisites and costs
     - Handle node unlock button clicks
   - Wire UI to SkillTreeService.

8. Integration with run lifecycle
   - Hook SkillTreeService.ResetTree() into run start event.
   - Display branch selection UI after run initialization.
   - Lock branch choice after selection.
   - Trigger skill tree UI from pause menu or level-up event.
   - Save selected branch and unlocked nodes in run state snapshot.

9. Effect application system
   - Define effect types for skill nodes (stat modifiers, ability unlocks, passive effects).
   - Create `SkillNodeEffect` base class under `Assets/Scripts/Systems/SkillTree/Effects/`.
   - Implement concrete effect classes: `StatModifierEffect`, `AbilityUnlockEffect`, `PassiveEffect`.
   - Apply effects when node is unlocked; remove effects on run reset.
   - Integrate with combat system, card system, and character stats.

10. Tests and validation
    - Unit tests for prerequisite validation logic.
    - Unit tests for conditional unlock evaluation.
    - Unit tests for branch selection and node unlock flow.
    - Integration test for full run lifecycle with skill tree progression.
    - Schema compliance test for skill tree JSON files.

## Deliverables

- `Assets/Scripts/Systems/SkillTree/SkillTreeRuntimeState.cs` (runtime state tracking)
- `Assets/Scripts/Services/ISkillTreeService.cs` (service interface)
- `Assets/Scripts/Systems/SkillTree/SkillTreeManager.cs` (service implementation)
- `Assets/Scripts/Systems/SkillTree/Conditions/ConditionalUnlockEvaluator.cs` (unlock conditions)
- `Assets/Scripts/Systems/SkillTree/Effects/` (effect classes)
- `Assets/Scripts/UI/SkillTreeUI.cs` (UI controller)
- `Assets/Prefabs/UI/SkillTree/` (UI prefabs)
- Unit tests under `Assets/Tests/SkillTreeTests/`
- Integration tests under `Assets/Tests/Integration/SkillTreeIntegrationTests/`
- `plan.json` (machine-readable mirror)

## Notes

- Skill tree resets every run; branch selection is a one-time choice at run start.
- Prerequisites should form a directed acyclic graph (DAG); validate at load time.
- Conditional unlocks require event tracking integration; design extensible condition system.
- Class evolution is triggered by reaching specific nodes; document evolution paths per class.
- Use ScriptableObject assets for skill tree definitions; JSON import tool can generate these.
- Skill points may be awarded on level-up or through other progression events; define acquisition rules.
- Effect application should be reversible for run reset; use additive stat modifiers where possible.
- Consider caching available nodes to avoid repeated prerequisite checks during UI updates.

