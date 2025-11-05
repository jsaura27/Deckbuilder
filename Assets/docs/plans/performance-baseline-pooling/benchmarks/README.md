# Benchmarks — Performance Baseline & Pooling

This folder stores benchmark runs for the Performance Baseline & Pooling task.

How to record a benchmark (recommended):
1. Open Unity Editor and run a representative scenario (e.g., a scene that spawns many projectiles or VFX).
2. Use the Unity Profiler to capture CPU/frame-time and GC allocations.
3. Save profiler snapshots and export key metrics to JSON or CSV into this folder.

Example filenames:
- `2025-11-02_sample-scenario-1.json` — summary metrics for scenario 1
- `2025-11-02_full-session-1.raw` — raw profiler capture (or Unity's .data snapshot)

Place any exported metrics here for traceability.
