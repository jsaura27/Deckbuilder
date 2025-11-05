# Step 004: Equipment System

```json
{
  "stepId": "Step-004",
  "taskId": "3.3",
  "name": "Equipment System",
  "category": "System",
  "priority": "P2",
  "effort": "M",
  "risk": "Low",
  "status": "pending",
  "sourceFile": "Assets/docs/unity/Unity-tasks.md",
  "origin": { "requirements": ["equipmentSystem"], "roadmap": ["3.3"], "gaps": [] }
}
```

Overview
--------
Create a small equip panel and data fixtures to validate slot rules, passive stat modifiers, and card modifiers.

Prerequisites
-------------
- `EquipmentManager` and `EquipmentDefinition` must be present in code.

Deliverables & Placement
------------------------
- `Assets/Editor/Tools/EquipPanelPrototype.cs` (implementation target)
- `Assets/Tests/Equipment/EquipmentTests.cs` — test file suggestion.
This planning step does not create these files; they are targets for implementation.

Detailed Implementation Steps
----------------------------
1. Create `EquipPanelPrototype` editor or dev UI allowing equip/unequip actions.
2. Provide sample `EquipmentDefinition` SOs for Weapon/Armor/Trinket with modifiers.
3. Hook `EquipmentManager` to apply modifiers to Player stats and ensure changes are reflected in combat simulations.
4. Write unit tests validating modifier stacking and slot enforcement.

Data & Configuration Plan
-------------------------
- EquipmentDefinition fields: id, name, slotType, rarity, statModifiers (list), cardModifiers (optional).

Testing Strategy
----------------
- Unit tests: ModifierApplication_CorrectValues, SlotEnforcement_SingleItemPerSlot

Acceptance Criteria
-------------------
1. Equip panel allows equipping items and results in expected stat changes.
2. Card modifiers (like +1 draw) affect subsequent draws as expected.

Completion Checklist
--------------------
- [ ] Create sample equipment SOs
- [ ] Implement equip panel prototype
- [ ] Add unit tests for equipment logic

Changelog Stub
-------------
Record implementation notes on completion.
