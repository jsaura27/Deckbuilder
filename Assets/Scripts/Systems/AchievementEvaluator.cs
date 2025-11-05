using System.Collections.Generic;
using UnityEngine;

public class AchievementEvaluator : MonoBehaviour
{
    // In-memory progress tracking for runtime; persistent storage handled elsewhere.
    private Dictionary<string, int> counters = new Dictionary<string, int>();

    public void RecordEvent(string eventName, int amount = 1)
    {
        if (!counters.ContainsKey(eventName)) counters[eventName] = 0;
        counters[eventName] += amount;
        // Evaluate achievements subscribed to this event (stubbed)
    }

    public int GetCount(string eventName)
    {
        counters.TryGetValue(eventName, out var v);
        return v;
    }
}
