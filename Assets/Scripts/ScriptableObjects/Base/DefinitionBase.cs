// Auto-generated base classes for Task 2.1
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    /// <summary>
    /// Contract for ScriptableObjects that can self-validate their serialized data.
    /// </summary>
    public interface IValidatable
    {
        void CollectValidationIssues(List<string> issues);
    }

    /// <summary>
    /// Abstract foundation for content definition assets. Provides Id and validation pattern.
    /// </summary>
    public abstract class DefinitionBase : ScriptableObject, IValidatable
    {
        [SerializeField] private string id; // Intentionally mutable in early phase; may restrict later.
        public string Id => id;

        // Unified hook for subclasses.
        public virtual void CollectValidationIssues(List<string> issues)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                issues?.Add($"{GetType().Name}: Id is empty");
            }
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            // Lightweight inline validation; avoid noise by only logging errors for now.
            var issues = new List<string>();
            CollectValidationIssues(issues);
            foreach (var issue in issues)
            {
                Debug.LogWarning(issue, this);
            }
        }
#endif
    }
}
