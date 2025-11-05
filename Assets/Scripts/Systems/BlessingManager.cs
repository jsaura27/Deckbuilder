using System;
using System.Collections.Generic;
using UnityEngine;
using Deckbuilder.DataModels;
using Game.Data;

namespace Deckbuilder.Systems
{
    // Runtime manager for Blessings: registers definitions, tracks acquired blessings, and evaluates simple evolution conditions.
    public class BlessingManager : MonoBehaviour
    {
        public static BlessingManager Instance { get; private set; }

        private readonly Dictionary<string, BlessingDefinition> _definitions = new();
        private readonly List<BlessingInstance> _instances = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public void RegisterDefinition(BlessingDefinition def)
        {
            if (def == null || string.IsNullOrEmpty(def.Id)) return;
            _definitions[def.Id] = def;
        }

        public BlessingDefinition GetDefinition(string id)
        {
            _definitions.TryGetValue(id, out var def);
            return def;
        }

        public IReadOnlyList<BlessingInstance> Instances => _instances;

        /// <summary>
        /// Acquire a blessing by id. If already acquired returns existing instance.
        /// </summary>
        public BlessingInstance AcquireBlessing(string id)
        {
            var existing = _instances.Find(b => b.Definition.Id == id);
            if (existing != null) return existing;

            var def = GetDefinition(id);
            if (def == null) return null;
            var instance = new BlessingInstance(def);
            _instances.Add(instance);
            return instance;
        }

        /// <summary>
        /// Attempt evolution of a blessing instance to next stage if condition passes.
        /// </summary>
        public bool TryEvolveBlessing(BlessingInstance instance, Func<string,bool> conditionResolver = null)
        {
            if (instance == null) return false;
            var nextIndex = instance.CurrentStageIndex + 1;
            if (nextIndex >= instance.Definition.Evolution.Count) return false; // already maxed
            var targetStage = instance.Definition.Evolution[nextIndex];
            var canEvolve = EvaluateCondition(targetStage.Condition, conditionResolver);
            if (!canEvolve) return false;
            instance.CurrentStageIndex = nextIndex;
            return true;
        }

        private bool EvaluateCondition(string condition, Func<string,bool> conditionResolver)
        {
            if (string.IsNullOrEmpty(condition)) return true; // empty means always
            if (condition.Equals("ALWAYS", StringComparison.OrdinalIgnoreCase)) return true;
            if (conditionResolver != null) return conditionResolver(condition);
            // Fallback heuristic: unknown conditions are treated as false to avoid unintended evolution.
            return false;
        }
    }
}
