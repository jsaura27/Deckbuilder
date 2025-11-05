using System;
using System.Collections.Generic;

namespace Deckbuilder.DataModels
{
    [Serializable]
    public class EquipmentSchema
    {
        public string id;
        public string name;
        public string slotType; // Weapon, Armor, Trinket
        public string rarity;
        public List<object> statModifiers;
        public List<object> cardModifiers;
        public List<object> onEquipEffects;
    }
}
