using System.Collections.Generic;

namespace Deckbuilder.Systems.SkillTree.Conditions
{
    // Minimal conditional evaluator stub. Real expressions and event hooks will be added later.
    public class ConditionalUnlockEvaluator
    {
        // Tracks named counters for simple conditions
        private readonly Dictionary<string, int> counters = new Dictionary<string, int>();

        public void Increment(string key)
        {
            if (!counters.ContainsKey(key)) counters[key] = 0;
            counters[key]++;
        }

        public int Get(string key) => counters.TryGetValue(key, out var v) ? v : 0;

        public bool Evaluate(string expression)
        {
            if (string.IsNullOrEmpty(expression)) return true;
            // Very small evaluator: supports expressions like "has:X" or "count:Y>=N" in future
            // For now, treat any non-empty expression as false (requires integration)
            return false;
        }
    }
}
