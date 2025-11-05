// Task 2.1: EquipmentDefinition ScriptableObject
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/Equipment/Equipment", fileName = "Equipment_", order = 30)]
    public class EquipmentDefinition : DefinitionBase
    {
        [SerializeField] private string displayName;
        [SerializeField] private EquipmentSlotType slotType;
        [SerializeField] private Rarity rarity;
        [SerializeField, TextArea] private string description;
        [SerializeField] private List<StatModifier> statModifiers = new();
        [SerializeField] private List<CardModifier> cardModifiers = new();
        [SerializeField] private List<EffectData> onEquipEffects = new();

        public string DisplayName => displayName;
        public EquipmentSlotType SlotType => slotType;
        public Rarity Rarity => rarity;
        public string Description => description;
        public IReadOnlyList<StatModifier> StatModifiers => statModifiers;
        public IReadOnlyList<CardModifier> CardModifiers => cardModifiers;
        public IReadOnlyList<EffectData> OnEquipEffects => onEquipEffects;

        public override void CollectValidationIssues(List<string> issues)
        {
            base.CollectValidationIssues(issues);
            if (string.IsNullOrWhiteSpace(displayName))
                issues?.Add($"EquipmentDefinition {name}: displayName empty");
        }
    }
}
