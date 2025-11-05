using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectDefinition", menuName = "Data/StatusEffectDefinition", order = 100)]
public class StatusEffectDefinition : ScriptableObject
{
    public string id;
    public string displayName;
    [TextArea]
    public string description;

    [System.Serializable]
    public struct Stacking
    {
        public string mode; // "stack" | "refresh" | "override"
        public int maxStacks;
    }
    public Stacking stacking;

    [System.Serializable]
    public struct Duration
    {
        public string type; // "turns" | "seconds"
        public float value;
    }
    public Duration duration;

    [System.Serializable]
    public class EffectEntry
    {
        public string type;
        [TextArea]
        public string paramsJson;
    }

    public EffectEntry[] effects;

    public string[] sourceWhitelist;
}
