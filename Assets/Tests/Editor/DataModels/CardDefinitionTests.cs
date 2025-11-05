using NUnit.Framework;
using UnityEngine;
using Game.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Deckbuilder.Tests.Editor.DataModels
{
    public class CardDefinitionTests
    {
        [Test]
        public void CreateAssetMenuAttribute_Exists()
        {
            var attr = typeof(CardDefinition).GetCustomAttribute<CreateAssetMenuAttribute>();
            Assert.IsNotNull(attr, "CreateAssetMenu attribute should exist on CardDefinition");
            StringAssert.Contains("Game/Cards/Card", attr.menuName);
        }

        [Test]
        public void DefaultFields_AreInitialized()
        {
            var inst = ScriptableObject.CreateInstance<CardDefinition>();
            Assert.IsNotNull(inst.Effects);
            Assert.IsNotNull(inst.Tags);
            Assert.AreEqual(0, inst.Effects.Count);
            Assert.AreEqual(0, inst.Tags.Count);
        }

        [Test]
        public void CollectValidationIssues_ReportsMissingFields()
        {
            var inst = ScriptableObject.CreateInstance<CardDefinition>();
            var issues = new List<string>();
            inst.CollectValidationIssues(issues);
            Assert.IsTrue(issues.Exists(s => s.Contains("Id is empty") || s.Contains("Id is empty")));
            Assert.IsTrue(issues.Exists(s => s.Contains("displayName empty") || s.Contains("displayName empty")));
        }

        [Test]
        public void CollectValidationIssues_NoIssuesWhenValid()
        {
            var inst = ScriptableObject.CreateInstance<CardDefinition>();
            var idField = typeof(DefinitionBase).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);
            var displayField = typeof(CardDefinition).GetField("displayName", BindingFlags.NonPublic | BindingFlags.Instance);
            idField.SetValue(inst, "card_1");
            displayField.SetValue(inst, "Strike");

            var issues = new List<string>();
            inst.CollectValidationIssues(issues);
            Assert.IsTrue(issues.Count == 0);
        }
    }
}
