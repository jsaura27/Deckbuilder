# Migration Guide — Replacing Instantiate/Destroy with Pooling

Steps to migrate a high-churn instantiation site:

1. Identify hotspot: search for `Instantiate(` and `Destroy(` usage in gameplay scripts.
2. Create a `GameObjectPool` instance for the prefab (preferably via a manager or via dependency injection).
3. Replace `Instantiate(prefab)` with `pool.Get()` and `Destroy(go)` with `pool.Release(go)`.
4. Add warm-up calls to `pool` at scene load if needed: pre-allocate expected count.
5. Test with profiler to confirm reduced allocations/spikes.

Example:
```csharp
// Before
var go = Instantiate(projectilePrefab);
// After
var go = projectilePool.Get();
```

Notes:
- Keep changes small and localized; prefer replacing a single hotspot per commit.
- Use `UnityEngine` conditional compilation in shared libraries where appropriate.
