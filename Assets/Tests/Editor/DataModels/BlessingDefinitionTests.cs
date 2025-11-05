using NUnit.Framework;
using UnityEngine;
using Game.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Deckbuilder.Tests.Editor.DataModels
{
    public class BlessingDefinitionTests
    {
        [Test]
        public void CreateAssetMenuAttribute_Exists()
        {
            var attr = typeof(BlessingDefinition).GetCustomAttribute<CreateAssetMenuAttribute>();
            Assert.IsNotNull(attr);
            StringAssert.Contains("Game/Blessings/Blessing", attr.menuName);
        }

        [Test]
        public void EvolutionOrdering_ChecksAreReported()
        {
            var inst = ScriptableObject.CreateInstance<BlessingDefinition>();
            var idField = typeof(DefinitionBase).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);
            var displayField = typeof(BlessingDefinition).GetField("displayName", BindingFlags.NonPublic | BindingFlags.Instance);
            idField.SetValue(inst, "bl_1");
            displayField.SetValue(inst, "Fortitude");

            var evolutionField = typeof(BlessingDefinition).GetField("evolution", BindingFlags.NonPublic | BindingFlags.Instance);
            var list = new List<BlessingEvolutionStage> { new BlessingEvolutionStage() };
            evolutionField.SetValue(inst, list);

            var issues = new List<string>();
            inst.CollectValidationIssues(issues);
            Assert.IsNotNull(issues);
        }
    }
}
