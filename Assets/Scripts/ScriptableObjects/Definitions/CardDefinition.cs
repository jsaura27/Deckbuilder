// Task 2.1: CardDefinition ScriptableObject
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Cards/Card", fileName = "Card_", order = 10)]
    public class CardDefinition : DefinitionBase
    {
        [SerializeField] private string displayName;
        [SerializeField] private CardType type;
        [SerializeField] private int cost;
        [SerializeField] private Rarity rarity;
        [SerializeField, TextArea] private string description;
        [SerializeField] private List<EffectData> effects = new();
        [SerializeField] private List<string> tags = new();
        [SerializeField] private UpgradePath upgradePath;

        public string DisplayName => displayName;
        public CardType Type => type;
        public int Cost => cost;
        public Rarity Rarity => rarity;
        public string Description => description;
        public IReadOnlyList<EffectData> Effects => effects;
        public IReadOnlyList<string> Tags => tags;
        public UpgradePath UpgradePath => upgradePath;

        public override void CollectValidationIssues(List<string> issues)
        {
            base.CollectValidationIssues(issues);
            if (cost < 0)
                issues?.Add($"CardDefinition {name}: cost < 0");
            if (string.IsNullOrWhiteSpace(DisplayName))
                issues?.Add($"CardDefinition {name}: displayName empty");
        }
    }
}
