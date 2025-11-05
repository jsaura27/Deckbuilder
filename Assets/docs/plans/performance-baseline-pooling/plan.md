# Task: Performance Baseline & Pooling
## Objective
Establish a performance baseline for core gameplay systems and introduce object pooling utilities to reduce runtime allocations and GC pressure.

## Prerequisites
- Access to repository workspace: `${workspaceFolder}`
- Roadmap entry: `Assets/docs/roadmap.json`
- Requirements and schemas: `Assets/docs/requirements.md`, `Assets/docs/schemas/`
- Existing code to profile: gameplay systems under `Assets/Core/`, `Assets/Scripts/`

## Step-by-Step Instructions
1. Instrumentation & Baseline
   - Identify hot paths: gameplay update loops, card draw/discard, combat resolution, instantiation-heavy flows.
   - Add lightweight timing markers and allocation counters (use Unity Profiler markers, System.Diagnostics.Stopwatch for editor runs).
   - Run representative scenarios and record metrics (frame time, GC allocations, spike events). Store results under `Assets/docs/plans/performance-baseline-pooling/benchmarks/`.

2. Low-Risk Pooling Implementation
   - Design a small, reusable pooling API (e.g., `Pool<T>` + non-generic `GameObjectPool`) under `Assets/Scripts/Utilities/Pooling/`.
   - Prioritize pools for types with high churn (projectiles, temporary VFX, runtime card visuals, runtime status effect instances).
   - Add Create/Release semantics and optionally a warm-up initializer.

3. Integrate & Replace Instantiation Sites
   - Replace direct `Instantiate`/`Destroy` in the highest-allocation hotspots with the pooling API.
   - Keep changes small and covered by unit tests where possible.

4. Re-benchmark & Validate
   - Re-run the same benchmark scenarios and compare allocations, frame-times, and spikes.
   - Confirm improvements; if regressions occur, revert and iterate.

5. Documentation & Deliverables
   - Document API usage and migration notes.
   - Add a short migration guide showing before/after examples.

## Deliverables
- `plan.json` (machine-readable mirror in the same folder)
- `benchmarks/` folder with raw measurement logs
- Pooling implementation (suggested path: `Assets/Scripts/Utilities/Pooling/`)
- Migration guide and sample usage snippet in this plan folder

## Notes
- Use schema references for any persisted data referenced by pooled objects.
- Keep allocations low in editor tests by running headless/batch mode for automation where possible.
- Follow Unity best practices: avoid per-frame allocations, prefer struct reuse for small value types.
