# Task: Card System

## Objective
Implement the core Card System for the deckbuilder: deck construction, draw/discard mechanics, and an effect resolution pipeline that can evaluate card actions and interact with combat and character state.

## Prerequisites
- Assets/docs/requirements.md
- Assets/docs/requirements.json
- Assets/docs/schemas/ (for card schemas and related types)
- Existing C# data models under Assets/Scripts/DataModels/
- ScriptableObject base classes for CardDefinition under Assets/Scripts/ScriptableObjects/ (see Phase2)

## Step-by-Step Instructions
1. Design data model mapping
   - Review card schema in `Assets/docs/schemas/card.schema.json`.
   - Ensure data models exist in `Assets/Scripts/DataModels/` with serializable fields.
2. Implement core deck & hand types
   - Create C# classes: Deck, Hand, DiscardPile, DrawPile under `Assets/Scripts/CardSystem/`.
   - Provide interfaces ICard, ICardEffect and basic implementations.
3. Build draw/discard mechanics
   - Implement Draw(int n), Discard(Card), Shuffle() with pooling and minimal allocations.
   - Add events (OnCardDrawn, OnCardPlayed, OnCardDiscarded) for systems to subscribe.
4. Create effect resolution pipeline
   - Design pipeline with phases: ResolveTargeting, ApplyCosts, ApplyEffects, PostResolve.
   - Support immediate and queued effects; allow asynchronous callbacks for animations.
5. Integrate with Combat System
   - Define contracts for resolving damage, healing, and status effects.
   - Add adapters for Effect -> CombatAction mapping.
6. Editor tools & ScriptableObject import
   - Add JSON import tool entry to convert card JSON into ScriptableObjects.
7. Tests & validation
   - Add unit tests using Unity Test Framework for Draw/Shuffle mechanics and effect resolution.

## Deliverables
- `Assets/Scripts/CardSystem/Deck.cs`, `Hand.cs`, `DrawPile.cs`, `DiscardPile.cs`
- `Assets/Scripts/CardSystem/EffectPipeline.cs`
- Unit tests under `Assets/Tests/CardSystemTests/`
- `plan.json` (machine representation)

## Notes
- Keep allocation minimal; use object pools where possible.
- Use events for decoupling; prefer explicit interfaces over static singletons.
- Reference: Assets/docs/requirements.md
