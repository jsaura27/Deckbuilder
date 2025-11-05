using System;
using System.Collections.Generic;
using Game.Data;

namespace Deckbuilder.DataModels.Equipment
{
    /// <summary>
    /// Runtime equipment data instance. 
    /// Uses Game.Data enums and structs for consistency.
    /// </summary>
    [Serializable]
    public class EquipmentData
    {
        public string id;
        public string name;
        public EquipmentSlotType slotType;
        public Rarity rarity;
        public List<StatModifier> statModifiers = new List<StatModifier>();
        public List<CardModifier> cardModifiers = new List<CardModifier>();
    }
}
