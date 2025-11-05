using System.Collections.Generic;using UnityEngine;

namespace Game.Map
{
    /// <summary>
    /// Represents a single node in the run path graph.
    /// </summary>
    public class MapNode : MonoBehaviour
    {
        [SerializeField] private MapNodeType nodeType;
        [SerializeField] private List<MapNode> connections = new();

        public MapNodeType NodeType => nodeType;
        public IReadOnlyList<MapNode> Connections => connections;
        public void SetType(MapNodeType t) => nodeType = t;
        public void AddConnection(MapNode other)
        {
            if (other != null && !connections.Contains(other)) connections.Add(other);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = NodeColor(nodeType);
            Gizmos.DrawSphere(transform.position, 0.35f);
            // Draw connections
            Gizmos.color = Color.yellow;
            foreach (var c in connections)
            {
                if (c == null) continue;
                Gizmos.DrawLine(transform.position, c.transform.position);
            }
        }

        private Color NodeColor(MapNodeType t) => t switch
        {
            MapNodeType.Start => Color.green,
            MapNodeType.Encounter => new Color(0.6f,0.6f,0.9f),
            MapNodeType.Elite => new Color(0.8f,0.2f,0.2f),
            MapNodeType.Shop => new Color(1f,0.8f,0.2f),
            MapNodeType.Event => new Color(0.5f,0.2f,0.8f),
            MapNodeType.Rest => new Color(0.2f,0.9f,0.4f),
            MapNodeType.Boss => Color.red,
            _ => Color.white
        };
    }
}
