using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deckbuilder.UI
{
    public class SkillTreeUI : MonoBehaviour
    {
        [Header("Wiring")]
        public GameObject nodePrefab; // small UI element representing a node
        public Transform nodesContainer; // where nodes will be instantiated
        public Button closeButton;

        // Optional pooling for node UI elements. Lazily initialized.
        private Deckbuilder.Utilities.Pooling.GameObjectPool? _nodePool;

        // Simple method to show branch selection (placeholder)
        public void ShowBranchSelection()
        {
            // In a full implementation this would populate branch buttons and lock choice.
            Debug.Log("ShowBranchSelection called (placeholder)");
        }

        // Populates the panel with a few placeholder nodes for editor testing
        public void ShowSkillTreePanel()
        {
            Debug.Log("ShowSkillTreePanel called (placeholder)");
            if (nodePrefab == null || nodesContainer == null) return;

            // Clear existing (return to pool if available)
            for (int i = nodesContainer.childCount - 1; i >= 0; i--)
            {
                var child = nodesContainer.GetChild(i).gameObject;
                if (_nodePool != null)
                {
                    _nodePool.Release(child);
                }
                else
                {
                    DestroyImmediate(child);
                }
            }

            // Lazy init pool
            if (_nodePool == null && nodePrefab != null)
            {
                _nodePool = new Deckbuilder.Utilities.Pooling.GameObjectPool(nodePrefab, initial: 5);
            }

            // Create a few placeholder nodes
            for (int i = 0; i < 5; i++)
            {
                GameObject go;
                if (_nodePool != null)
                {
                    go = _nodePool.Get();
                    go.transform.SetParent(nodesContainer, false);
                }
                else
                {
                    go = Instantiate(nodePrefab, nodesContainer);
                }

                go.name = $"Node_{i}";
                var txt = go.GetComponentInChildren<Text>();
                if (txt != null) txt.text = $"Node {i}\nCost: {i + 1}";
            }
        }
    }
}
