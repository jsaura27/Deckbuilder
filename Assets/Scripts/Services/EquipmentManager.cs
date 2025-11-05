using System.Collections.Generic;
using System.Linq;
using Deckbuilder.DataModels.Equipment;
using Game.Data;

namespace Deckbuilder.Services
{
    public class EquipmentManager : IEquipmentService
    {
        private readonly Dictionary<string, EquipmentData> equipped = new Dictionary<string, EquipmentData>();

        public bool Equip(EquipmentData equipment)
        {
            if (equipment == null || string.IsNullOrEmpty(equipment.id)) return false;
            if (equipped.ContainsKey(equipment.id)) return false;
            // basic slot enforcement not implemented yet; accept equipment
            equipped[equipment.id] = equipment;
            return true;
        }

        public bool Unequip(string equipmentId)
        {
            return equipped.Remove(equipmentId);
        }

        public IEnumerable<StatModifier> GetActiveStatModifiers()
        {
            return equipped.Values.SelectMany(e => e.statModifiers ?? Enumerable.Empty<StatModifier>());
        }
    }
}
