# Roadmap & Requirements Audit Report (2025-11-04)

## Executive Summary
The current roadmap (version 1.1) covers foundational data parsing, ScriptableObject scaffolding, and core gameplay systems (cards, combat, equipment, blessings, skill tree), plus early progression and pending validation/CI work. Newly added feature-request tasks expand scope into economy, encounter flow, event-driven narrative, map navigation, telemetry enhancement, and analytical tooling. Coverage of emergent systems (encounters, map, economy, synergy analysis) was previously absent, representing critical progression pathway and UX depth gaps. Testing and validation phases remain unimplemented (Phase5+), leaving several higher-risk systems (data integrity, telemetry, deterministic RNG) incomplete.

The audit identifies structural gaps for unrepresented gameplay flows (encounter sequencing, reward distribution, currency economy, map traversal) and recommends prioritizing enabling infrastructure (encounter + map + currency) before analytical layers (synergy engine). A Pending Additions section has been inserted into `requirements.md` to formalize newly introduced systems.

## Coverage Matrix
| System / Concern | Task IDs | Count | Phases |
|------------------|----------|-------|--------|
| Card System | 3.1, 3.6 (perf), 3.7 (effects) | 3 | Phase3 |
| Combat System | 3.2, 3.7, 3.10 (char/enemy consolidation) | 3 | Phase3 |
| Equipment System | 3.3 | 1 | Phase3 |
| Blessing System | 3.4 | 1 | Phase3 |
| Skill Tree System | 3.5 | 1 | Phase3 |
| Performance / Pooling | 3.6 | 1 | Phase3 |
| Status Effects | 3.7 | 1 | Phase3 |
| Run State / Save | 4.1, 4.3 | 2 | Phase4 |
| Achievements / Meta | 4.2, 4.4 | 2 | Phase4 |
| Testing Framework | 5.1 | 1 | Phase5 |
| Schema Compliance | 5.2 | 1 | Phase5 |
| Editor QA | 5.3 | 1 | Phase5 |
| Telemetry / Logging | 5.4, 5.7 | 2 | Phase5 |
| Deterministic RNG | 5.5 | 1 | Phase5 |
| Data Integrity | 5.6 | 1 | Phase5 |
| Packaging / CI | 6.1, 6.2 | 2 | Phase6 |
| Documentation Generation | 6.3 | 1 | Phase6 |
| Encounter System | 3.8 | 1 | Phase3 |
| Reward System | 3.9 | 1 | Phase3 |
| Character & Enemy Consolidation | 3.10 | 1 | Phase3 |
| Shop System | 4.5 | 1 | Phase4 |
| Currency System | 4.6 | 1 | Phase4 |
| Event System | 4.7 | 1 | Phase4 |
| Map & Node Graph | 4.8 | 1 | Phase4 |
| Run Summary | 5.7 | 1 | Phase5 |
| Synergy Detection | 5.8 | 1 | Phase5 |

Total distinct systems referenced: 22.

## Progress Metrics
- Total tasks: 40
- Done: 22 (55%)
- Pending: 18 (45%)
- Blocked/Stalled (error field non-null): 2 (3.4, 3.7) – require environment build/test execution.
- Phase coverage: Core systems (Phase3) largely complete except new additions; progression (Phase4) mid-level; validation/automation (Phase5/6) untouched.

## Gap Analysis (Sorted by Severity & Priority Score)
Severity weights: Critical=5, High=4, Medium=3, Low=2, Info=1. Formula: priorityScore = severityWeight + coverageImpact + dependencyUnblock + riskMitigationBonus.

| Gap ID | Description | Severity | Priority Score | Rationale |
|--------|-------------|----------|----------------|-----------|
| GAP-A1 | Lack of implemented encounter sequencing logic before reward/shop/map tasks | Critical | 8 | Encounter flow underpins progression pacing |
| GAP-A2 | Reward distribution rules absent; impacts player progression balance | High | 7 | Blocks meaningful post-combat incentives |
| GAP-A3 | Currency model unspecified (format, accumulation rules) | High | 7 | Needed for shop & event economy decisions |
| GAP-A4 | Map & Node Graph lacks schema & traversal rules | High | 7 | Required for run structure variety |
| GAP-A5 | Event System lacks schema for branching outcomes | High | 6 | Narrative dynamism dependent on this |
| GAP-A6 | Synergy detection heuristics undefined | Medium | 5 | Enhances deck-building but not blocking core loop |
| GAP-A7 | Run Summary aggregation spec unspecified | Medium | 5 | Telemetry usefulness reduced without summarization |
| GAP-A8 | Character & Enemy shared stat framework not formalized | Medium | 5 | AI tuning & combat scaling depend on unified model |
| GAP-A9 | Missing plan folders for all newly added tasks | Medium | 4 | Consistency & onboarding impact |
| GAP-A10 | No schema for currency, encounter, map, reward, synergy models | High | 7 | Data-driven extension blocked |

## Consistency Checks
- Phase ordering: New tasks correctly placed (encounter before map & reward interactions). Telemetry extension (5.7) depends on 5.4 (valid earlier task).
- Duplicate Titles: None detected (string match + manual inspection).
- Ambiguous verbs: "Performance Baseline & Pooling" acceptable; new tasks clarified with action-oriented subtasks.
- Unrecognized statuses: None (statuses: done, pending).

## Schema Discrepancies
- Missing Schemas: encounter, reward, shop, currency, event, map-node-graph, synergy, run-summary.
- Unused properties (sample heuristic): `cards.schema.json`: `tags` referenced only implicitly; add explicit task for tag-based filtering. `equipment.schema.json`: `onEquipEffects` not referenced by any task.
- Orphan Entities: None in existing requirements; new entities formalized via Pending Additions.

## Enhancement Recommendations
| Category | Recommendation | Rationale | Suggested Task Stub | Priority |
|----------|----------------|-----------|---------------------|----------|
| Data Validation & Integrity | Add Encounter / Map / Currency schema definitions | Enables consistent authoring & testing | "Define schemas for encounter.json, map.json, currency.json" | High |
| Testing | Introduce automated simulation tests for encounter + reward flow | Catch balance regressions early | "Encounter simulation test harness" | High |
| Monitoring / Telemetry | Extend telemetry events for map path & reward choices | Improves analytic insight | "Add telemetry events: mapNodeVisited, rewardClaimed" | Medium |
| Performance & Profiling | Batch evaluate synergy heuristics asynchronously | Prevent frame spikes during deck evaluation | "Synergy engine async evaluation" | Medium |
| Tooling & Automation | Editor tool for reward table preview & probability visualization | Aids balancing & iteration | "Reward table preview window" | Medium |
| UX Polish | Add run-end summary UI panel | Improves closure & retention | "Run Summary UI" | Medium |
| Documentation | Generate economy glossary (currency sources/sinks) | Helps tuning & onboarding | "Economy glossary doc generation" | Low |
| Risk Mitigation | Add fallback for missing schema definitions (soft warnings) | Prevent runtime hard failures | "Schema fallback loader" | Medium |

## New Tasks Added
- 3.8 Encounter System
- 3.9 Reward System
- 3.10 Character & Enemy System Consolidation
- 4.5 Shop System
- 4.6 Currency System
- 4.7 Event System (Narrative / Random)
- 4.8 Map & Node Graph System
- 5.7 Run Summary & Telemetry Integration
- 5.8 Synergy Detection Engine

## Next 5 Concrete Task Suggestions
1. Create `Assets/docs/schemas/encounter.schema.json` (entities: encounterId, enemyGroupRefs, preCombatModifiers, rewardProfileRef).
2. Create `Assets/docs/schemas/map-node.schema.json` (fields: nodeId, nodeType, edges[], encounterRef, eventRef, shopRef).
3. Implement `EncounterManager` service with load + resolve pipeline and stub simulation tests.
4. Implement `RewardTable` ScriptableObject + editor preview tool.
5. Implement `CurrencyService` (earn/spend events + telemetry emission).

## Methodology & Weighting Explanation
Severity weights: Critical=5, High=4, Medium=3, Low=2, Info=1. CoverageImpact scored as +1 where system had zero prior tasks. DependencyUnblock +2 if enabling downstream (encounter before map/reward). RiskMitigationBonus +1 where failure could degrade stability or analytics. PriorityScore = severityWeight + coverageImpact + dependencyUnblock + riskMitigationBonus.

## Unrecognized Status Values
None.

## Skipped Feature Requests
None (no duplicates; slugs unique).

## Summary Line
Summary: tasks=40 gaps=10 added=9 pending=18
