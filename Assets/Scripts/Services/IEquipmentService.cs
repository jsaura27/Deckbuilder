using System.Collections.Generic;
using Deckbuilder.DataModels.Equipment;
using Game.Data;

namespace Deckbuilder.Services
{
    public interface IEquipmentService
    {
        bool Equip(EquipmentData equipment);
        bool Unequip(string equipmentId);
        IEnumerable<StatModifier> GetActiveStatModifiers();
    }
}
