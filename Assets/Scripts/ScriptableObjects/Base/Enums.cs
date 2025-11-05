// Auto-generated for Task 2.1: Create ScriptableObject Base Classes
// Defines core enumerations and shared serializable structs used by gameplay ScriptableObjects.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    public enum CardType { Attack, Defense, Utility, Curse }
    public enum Rarity { Common, Rare, Epic, Legendary }
    public enum EquipmentSlotType { Weapon, Armor, Trinket }

    /// <summary>
    /// Generic effect container used before effect system specialization (Phase3).
    /// Keep fields broad/flexible; downstream tooling can evolve serialization.
    /// </summary>
    [Serializable]
    public struct EffectData
    {
        [SerializeField] private string effectType; // semantic key (e.g., Damage, Heal, ApplyStatus)
        [SerializeField] private string target;     // Self, Enemy, AllEnemies, AllAllies
        [SerializeField] private float value;       // numeric magnitude (if applicable)
        [SerializeField] private string jsonPayload;// optional JSON snippet for complex params

        public string EffectType => effectType;
        public string Target => target;
        public float Value => value;
        public string JsonPayload => jsonPayload;
    }

    [Serializable]
    public struct UpgradePath
    {
        [SerializeField] private string upgradedId;
        [SerializeField] private int cost; // optional additional cost or upgrade cost reference
        public string UpgradedId => upgradedId;
        public int Cost => cost;
    }

    [Serializable]
    public struct StatModifier
    {
        [SerializeField] private string stat;  // e.g., Health, Strength, Draw
        [SerializeField] private float flat;
        [SerializeField] private float percent; // 0.10 = +10%
        public string Stat => stat;
        public float Flat => flat;
        public float Percent => percent;
    }

    [Serializable]
    public struct CardModifier
    {
        [SerializeField] private string filterTag;   // apply to cards containing tag
        [SerializeField] private string modification;// e.g., AddDraw, AddStatusOnPlay
        [SerializeField] private float value;        // magnitude context-dependent
        public string FilterTag => filterTag;
        public string Modification => modification;
        public float Value => value;
    }

    [Serializable]
    public struct BlessingEvolutionStage
    {
        [SerializeField] private int stage;
        [SerializeField] private string condition;      // textual condition expression
        [SerializeField] private EffectData resultingEffect; // simplified placeholder
        public int Stage => stage;
        public string Condition => condition;
        public EffectData ResultingEffect => resultingEffect;
    }

    [Serializable]
    public struct SkillNode
    {
        [SerializeField] private string id;
        [SerializeField] private string type;
        [SerializeField] private int cost;
        [SerializeField] private List<string> prerequisites;
        [SerializeField] private string conditionalUnlock;
        [SerializeField] private EffectData effect;

        public string Id => id;
        public string Type => type;
        public int Cost => cost;
        public IReadOnlyList<string> Prerequisites => prerequisites;
        public string ConditionalUnlock => conditionalUnlock;
        public EffectData Effect => effect;
    }

    [Serializable]
    public struct SkillBranch
    {
        [SerializeField] private string name;
        [SerializeField] private List<SkillNode> nodes;
        public string Name => name;
        public IReadOnlyList<SkillNode> Nodes => nodes;
    }
}
