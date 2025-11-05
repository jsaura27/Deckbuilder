using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    /// <summary>
    /// Generates a simple grid-like run path of MapNodes with random types.
    /// Attach this to an empty GameObject in a scene. Provide a Node prefab containing MapNode.
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        [Header("Layout")] public int rows = 6; public int columns = 4; public float spacingX = 2.5f; public float spacingY = 2.0f; public Vector2 jitter = new(0.6f, 0.4f);
        [Header("Prefabs")] public GameObject nodePrefab;
        [Header("Behavior")] public bool autoGenerateOnStart = true; public bool connectForward = true;
        [Header("Random")] public int seed = 0; public bool useSeed = true;

        private readonly List<MapNode> _nodes = new();

        private void Start()
        {
            if (autoGenerateOnStart) Generate();
        }

        [ContextMenu("Generate")] public void Generate()
        {
            ClearChildren();
            _nodes.Clear();
            if (nodePrefab == null)
            {
                Debug.LogWarning("MapGenerator: nodePrefab not assigned.");
                return;
            }
            var rng = useSeed ? new System.Random(seed) : new System.Random();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var pos = new Vector3(c * spacingX, -r * spacingY, 0f);
                    pos.x += (float)(rng.NextDouble() * 2 - 1) * jitter.x;
                    pos.y += (float)(rng.NextDouble() * 2 - 1) * jitter.y;
                    var go = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
                    go.name = $"Node_{r}_{c}";
                    var node = go.GetComponent<MapNode>();
                    if (node == null)
                    {
                        Debug.LogError("MapGenerator: nodePrefab missing MapNode component.");
                        Destroy(go);
                        continue;
                    }
                    node.SetType(ResolveType(r, c, rng));
                    _nodes.Add(node);
                }
            }
            if (connectForward) ConnectRows();
        }

        private MapNodeType ResolveType(int r, int c, System.Random rng)
        {
            if (r == 0) return MapNodeType.Start; if (r == rows - 1) return MapNodeType.Boss;
            // Weighted random selection
            int roll = rng.Next(100);
            if (roll < 40) return MapNodeType.Encounter; if (roll < 55) return MapNodeType.Event; if (roll < 70) return MapNodeType.Rest; if (roll < 85) return MapNodeType.Shop; if (roll < 95) return MapNodeType.Elite; return MapNodeType.Encounter;
        }

        private void ConnectRows()
        {
            // Naive forward connection: each node in row r connects to up to two nodes in row r+1
            MapNode NodeAt(int row, int col) => _nodes.Find(n => n.name == $"Node_{row}_{col}");
            var rng = useSeed ? new System.Random(seed + 1337) : new System.Random();
            for (int r = 0; r < rows - 1; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    var current = NodeAt(r, c); if (current == null) continue;
                    // Choose 1-2 forward columns
                    int connections = rng.Next(1, 3);
                    for (int i = 0; i < connections; i++)
                    {
                        int targetCol = Mathf.Clamp(c + rng.Next(-1, 2), 0, columns - 1);
                        var target = NodeAt(r + 1, targetCol); if (target == null) continue;
                        current.AddConnection(target);
                    }
                }
            }
        }

        private void ClearChildren()
        {
            var toDestroy = new List<GameObject>();
            foreach (Transform child in transform) toDestroy.Add(child.gameObject);
            foreach (var go in toDestroy)
            {
#if UNITY_EDITOR
                DestroyImmediate(go);
#else
                Destroy(go);
#endif
            }
        }
    }
}
