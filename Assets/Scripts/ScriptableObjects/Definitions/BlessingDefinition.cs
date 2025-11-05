// Task 2.1: BlessingDefinition ScriptableObject
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Blessings/Blessing", fileName = "Blessing_", order = 20)]
    public class BlessingDefinition : DefinitionBase
    {
        [SerializeField] private string displayName;
        [SerializeField] private Rarity rarity;
        [SerializeField, TextArea] private string description;
        [SerializeField] private EffectData baseEffect;
        [SerializeField] private List<string> triggers = new();
        [SerializeField] private List<BlessingEvolutionStage> evolution = new();

        public string DisplayName => displayName;
        public Rarity Rarity => rarity;
        public string Description => description;
        public EffectData BaseEffect => baseEffect;
        public IReadOnlyList<string> Triggers => triggers;
        public IReadOnlyList<BlessingEvolutionStage> Evolution => evolution;

        public override void CollectValidationIssues(List<string> issues)
        {
            base.CollectValidationIssues(issues);
            if (string.IsNullOrWhiteSpace(displayName))
                issues?.Add($"BlessingDefinition {name}: displayName empty");
            // Evolution stage ordering check
            int lastStage = -1;
            for (int i = 0; i < evolution.Count; i++)
            {
                var stage = evolution[i].Stage;
                if (stage <= lastStage)
                    issues?.Add($"BlessingDefinition {name}: evolution stage {stage} not strictly increasing");
                lastStage = stage;
            }
        }
    }
}
