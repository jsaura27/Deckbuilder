using System;
using UnityEngine;
using Deckbuilder.DataModels.Equipment;
using Game.Data;

namespace Deckbuilder.ScriptableObjects.Equipment
{
    /// <summary>
    /// Legacy wrapper for equipment data.
    /// Consider migrating to use Game.Data.EquipmentDefinition directly.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEquipment", menuName = "Deckbuilder/Equipment/EquipmentDefinition")]
    public class EquipmentDefinition : ScriptableObject
    {
        public EquipmentData data = new EquipmentData();
    }
}
