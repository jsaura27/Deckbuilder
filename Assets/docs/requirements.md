Rogue-lite Deckbuilder � Requirements

Game Overview
- Title: (Working) Rogue-lite Deckbuilder
- Genre: Rogue-lite deckbuilder
- Platform: Desktop only (Windows, macOS)
- Perspective:2D or isometric (configurable per project art pipeline)
- Core loop: Explore ? Battle ? Acquire ? Evolve ? Reset

Design Principles
- Modular: subsystems are data-driven and injectable
- Atomic: systems composed of small units (cards, blessings, equipment, skill nodes)
- Extensible: new classes, cards, blessings, equipment, and branches added via data/config
- Deterministic run logic with controlled randomness for replayability

Systems & Features

1) Character System
- Playable classes: Warrior, Rogue, Mage
- Extensible classes: classes declared via data files (JSON / ScriptableObjects)
- Leveling
 - Characters gain XP during a run and level up
 - Levels reset at the beginning of each new run
 - Level metadata: level, XP, XPToNext
- Skill tree
 - Resets each run
 - Skill tree split into4 branches: Offense, Defense, Utility, Chaos
 - On run start player chooses exactly one branch; other branches remain locked for that run
 - Branch progression may evolve class (e.g., Warrior ? Berserker) by reaching specific node(s)
 - Skill nodes are modular units with: id, cost, effect, prerequisites, conditional unlocks
 - Conditional unlocks support run conditions (e.g., "no-damage", "three-kills-without-card-play")

2) Equipment System
- Rarity tiers: Common, Rare, Epic, Legendary
- Slot types: Weapon, Armor, Trinket (expandable via config)
- Acquisition: loot drops, merchant purchases, map events
- Item effects
 - Passive stat boosts (flat and percent)
 - Card modifiers (e.g., +1 draw on Attack cards, add Burn on hit)
 - Skill tree interactions (equip may unlock or modify tree nodes)
- Equip rules
 - One item per slot by default; stackable flags supported for future expansion

3) Blessings System
- Rarity tiers: Common, Rare, Epic, Legendary
- Acquisition: interacting with statues / map objects or special events
- Blessing definition: id, name, rarity, baseEffect, triggers, limitations
- Evolution mechanic
 - Each blessing can define0..n evolution stages with explicit conditions
 - Example: evolve if "win X battles without damage" or "use only attacks that cost0 for Y turns"
 - Evolution path must be data-driven and deterministic
- Blessing stacking and exclusivity rules configurable per blessing

4) Card System
- Card types: Attack, Defense, Utility, Curse (expandable)
- Card data model: id, name, cost, type, rarity, description, effects, tags, upgradePath
- Acquisition: enemy drops, merchants, events
- Deck rules
 - Player builds a deck per run; deck resets every run
 - Cards can be upgraded (single or multi-stage) or removed at merchants/events
 - Hand management: draw size, max hand, discard, recycle rules configurable
- Card effects
 - Effects are modular and data-driven: damage, heal, status application, draw, manipulate deck
 - Support conditional effects and targets (self, enemy, AOE, all enemies)
 - Curse cards behave like normal cards but have negative side effects

5) Combat System
- Turn-based, alternating turns between player and enemy
- Card-driven player actions
- Combat flow
 - Draw Phase ? Action Phase (player may play0..n cards subject to energy/resource) ? Enemy Turn ? Resolution
 - Support interrupt mechanics and priority ordering (e.g., reactions, counters)
- Enemy AI
 - Behavior modes: predictable (patterned), conditional (stateful), random (weighted choices)
 - AI definition should be data-driven (scripted states or behavior tree definitions)
- Status effects
 - Core: Burn, Freeze, Poison, Shield, Stun, Vulnerable, Weak
 - Each effect must define stacking rules and duration semantics

6) Progression & Replayability
- Run-based reset
 - All run state (levels, deck, skill tree choices) resets when a run ends
- Meta progression (optional)
 - Persistent unlocks for classes, cards, blessings, equipment, cosmetics
 - Achievements and milestones tracked separately from runs
- Roguelike hooks
 - Seeds and deterministic RNG for reproducible runs (debugging)

Technical Requirements
- Modular Architecture
 - Systems separated into modules/services: CardSystem, CombatSystem, EquipmentSystem, BlessingSystem, SkillTreeService
 - Use dependency inversion to allow swapping implementations
- Data-driven definitions
 - Prefer `ScriptableObject` (Unity) or JSON for cross-platform editing
 - All gameplay items and effects defined in data files (no hard-coded values in logic)
- Save System
 - Minimum: meta progression persisted (file + versioning)
 - Optional: save current run state for debugging or resume (configurable)
- Performance
 - Target desktop hardware; optimize update loops and GC pressure
 - Use pooling for frequently instantiated objects (cards, effects)
- Offline play
 - No runtime network dependency

Testing & Tools
- Unit tests for core rule resolution (card effects, damage calculation, state transitions)
- Integration tests for run lifecycle and persistence
- Playtest telemetry hooks (non-networked logs) to capture run events

Deliverables (placed in `Assets/docs/`)
- `requirements.md` (this document)
- `requirements.json` (machine-readable summary)
- JSON Schema files:
 - `schemas/cards.schema.json`
 - `schemas/blessings.schema.json`
 - `schemas/equipment.schema.json`
 - `schemas/skilltree.schema.json`
- Optional: UML/flowchart assets (future)

Data Schema Notes
- Schemas are intentionally minimal but extensible; engine should validate data against these schemas during load
- Use `id` fields for stable cross-references; prefer GUID-style ids for data authored externally

Integration Checklist (atomic tasks for implementers)
- Create data loaders for JSON and ScriptableObjects
- Implement Card effect resolution pipeline (pure functions preferred)
- Implement Combat turn manager
- Implement AI behavior driver using data-driven behavior definitions
- Implement SkillTree selection UI and in-run persistence
- Implement Blessing altar interactions and evolution resolution
- Implement item drops, merchant systems, and shops
- Implement meta-save and version migration strategy

Extensibility & Modding Notes
- Expose content packs as JSON bundles with a manifest file listing new classes/cards/equipment/blessings
- Keep engine stable by validating content before activating it in runs (sandboxed parsing)

Enhancements
### Persistence & Save System
- Introduce explicit versioned save schema supporting: meta progression, optional in-run snapshot (debug), achievements state.
- Migration function registry keyed by semantic version (e.g., `1.0.0` -> `1.1.0`).

### Telemetry & Logging
- Structured JSONL event logging (cardPlayed, damageDealt, statusApplied, seed, runEnd, achievementUnlocked).
- Configurable enable flag; log retention policy (max files / rotation size).

### Performance & Profiling
- Baseline metrics: average frame time, GC allocations per turn, object pool hit/miss ratio.
- Benchmark harness producing markdown report under `Assets/docs/perf/`.

### Deterministic RNG
- Central RNG Seed Manager; seed recorded in telemetry and save snapshot.
- Repeatable run reproduction: given seed + content version, run sequence deterministic.

### Achievements Framework
- Achievement schema (id, name, criteria expression, reward references, category, visibility flag).
- Evaluation dispatcher triggered by telemetry events.

### Data Validation Layer
- Startup validation: unique ids across cards/blessings/equipment/skill nodes.
- Cross-reference checks: upgradePath.upgradedId exists, prerequisites exist, equipment cardModifiers refer to existing card types.
- Report categorizing errors vs warnings; fail-fast option for development builds.

### Documentation Generation
- Automated generation of schema field reference and linking into this requirements document.
- Output location: `Assets/docs/generated/schema-reference.md`.

### Acceptance Criteria References
- Each enhancement mapped to new roadmap tasks (3.6, 3.7, 4.3, 4.4, 5.4, 5.5, 5.6, 6.3).

### Future Considerations
- Potential CI integration for data validation once enableCI turns true in future iteration.

Contact & Ownership
- Authors: design & engineering teams (project-specific)
- Version:1.0 � initial requirements
- Change log: update the `requirements.md` header with changes and date

End of Requirements
\n+## Pending Additions (2025-11-04)
The following systems and engines have been proposed and added to the roadmap but were not explicitly defined in the original requirements specification. Each will require detailed design criteria and data schema extensions:

- Encounter System: Defines structured pre-combat and ambient interaction nodes (group composition, modifiers, optional preview mechanics).
- Reward System: Centralized post-encounter and event reward distribution with rarity weighting, dynamic scaling, and configurable reward tables.
- Shop System: Merchant interaction layer supporting inventory generation, pricing strategy, purchase flow, and potential card removal / upgrade services.
- Currency System: Abstract economic layer (primary run currency, optional meta currency) with persistence hooks and formatting rules.
- Event System (Narrative / Random): Weighted branching events with outcomes affecting deck, stats, map path, or progression metadata.
- Character & Enemy System Consolidation: Unified stat + trait framework for playable classes and enemy archetypes, enabling shared buff/debuff pipelines.
- Map & Node Graph System: Data-driven run path graph (encounter, shop, event, rest, boss nodes) with path selection constraints and seeding.
- Run Summary & Telemetry Integration: Aggregation of end-of-run KPIs (turns, damage dealt/received, deck evolution) plus structured telemetry export.
- Synergy Detection Engine: Real-time heuristic analysis of deck composition and active modifiers to surface emergent strategy hints.

Design follow-up: Each item will receive a dedicated schema or schema extension; acceptance criteria to be added alongside new task plan folders.
