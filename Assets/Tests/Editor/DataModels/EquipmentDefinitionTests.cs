using NUnit.Framework;
using UnityEngine;
using Game.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Deckbuilder.Tests.Editor.DataModels
{
    public class EquipmentDefinitionTests
    {
        [Test]
        public void CreateAssetMenuAttribute_Exists()
        {
            var attr = typeof(EquipmentDefinition).GetCustomAttribute<CreateAssetMenuAttribute>();
            Assert.IsNotNull(attr);
            StringAssert.Contains("Game/Equipment/Equipment", attr.menuName);
        }

        [Test]
        public void DefaultLists_AreInitialized()
        {
            var inst = ScriptableObject.CreateInstance<EquipmentDefinition>();
            Assert.IsNotNull(inst.StatModifiers);
            Assert.IsNotNull(inst.CardModifiers);
            Assert.IsNotNull(inst.OnEquipEffects);
        }
    }
}
