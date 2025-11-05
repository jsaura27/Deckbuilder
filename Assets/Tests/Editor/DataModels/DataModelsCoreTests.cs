using NUnit.Framework;
using UnityEngine;
using Deckbuilder.DataModels;
using System.Collections.Generic;

namespace Deckbuilder.Tests.Editor.DataModels
{
    public class DataModelsCoreTests
    {
        [Test]
        public void Card_Serialization_Roundtrip()
        {
            var card = new Deckbuilder.DataModels.Card
            {
                id = "card_001",
                name = "Strike",
                type = "Attack",
                cost = 1,
                rarity = "Common",
                description = "Deal 6 damage.",
                effects = new List<Deckbuilder.DataModels.Effect> {
                    new Deckbuilder.DataModels.Effect { effectType = "Damage", target = "Enemy", value = 6 }
                },
                tags = new List<string> { "starter", "physical" },
                upgradePath = new Deckbuilder.DataModels.UpgradePath { upgradedId = "card_001_up", cost = 1 }
            };

            var json = JsonUtility.ToJson(card);
            Assert.IsNotNull(json);

            var round = JsonUtility.FromJson<Deckbuilder.DataModels.Card>(json);
            Assert.AreEqual(card.id, round.id);
            Assert.AreEqual(card.name, round.name);
            Assert.AreEqual(card.cost, round.cost);
            Assert.IsNotNull(round.effects);
            Assert.AreEqual(1, round.effects.Count);
            Assert.AreEqual("Damage", round.effects[0].effectType);
            Assert.IsNotNull(round.upgradePath);
            Assert.AreEqual("card_001_up", round.upgradePath.upgradedId);
        }

        [Test]
        public void Blessing_Serialization_Roundtrip()
        {
            var blessing = new Deckbuilder.DataModels.Blessing
            {
                id = "bl_001",
                name = "Fortitude",
                rarity = "Rare",
                description = "Gain +1 max HP.",
                baseEffect = null,
                triggers = new List<string> { "onStart", "onKill" },
                evolution = new List<Deckbuilder.DataModels.EvolutionStage> {
                    new Deckbuilder.DataModels.EvolutionStage { stage = 1, condition = "reachLevel2", resultingEffect = null }
                }
            };

            var json = JsonUtility.ToJson(blessing);
            Assert.IsNotNull(json);

            var round = JsonUtility.FromJson<Deckbuilder.DataModels.Blessing>(json);
            Assert.AreEqual(blessing.id, round.id);
            Assert.AreEqual(blessing.triggers.Count, round.triggers.Count);
            Assert.IsNotNull(round.evolution);
            Assert.AreEqual(1, round.evolution.Count);
            Assert.AreEqual(1, round.evolution[0].stage);
        }

        [Test]
        public void Equipment_DefaultFields_Assignment()
        {
            var eq = new Deckbuilder.DataModels.EquipmentSchema
            {
                id = "eq_001",
                name = "Iron Sword",
                slotType = "Weapon",
                rarity = "Common",
                statModifiers = new System.Collections.Generic.List<object> { 1, 2 },
                cardModifiers = new System.Collections.Generic.List<object>(),
                onEquipEffects = new System.Collections.Generic.List<object> { "slash" }
            };

            Assert.AreEqual("eq_001", eq.id);
            Assert.AreEqual("Weapon", eq.slotType);
            Assert.IsNotNull(eq.statModifiers);
            Assert.AreEqual(2, eq.statModifiers.Count);
        }

        [Test]
        public void SkillTree_NestedNodes_Roundtrip()
        {
            var node = new Deckbuilder.DataModels.Node { id = "n1", type = "Passive", cost = 1, prerequisites = new System.Collections.Generic.List<string>(), conditionalUnlock = null, effect = null };
            var branch = new Deckbuilder.DataModels.Branch { name = "Offense", nodes = new System.Collections.Generic.List<Deckbuilder.DataModels.Node> { node } };
            var tree = new Deckbuilder.DataModels.SkillTree { id = "st_001", classId = "warrior", branches = new System.Collections.Generic.List<Deckbuilder.DataModels.Branch> { branch } };

            var json = JsonUtility.ToJson(tree);
            Assert.IsNotNull(json);

            var round = JsonUtility.FromJson<Deckbuilder.DataModels.SkillTree>(json);
            Assert.AreEqual(tree.id, round.id);
            Assert.IsNotNull(round.branches);
            Assert.AreEqual(1, round.branches.Count);
            Assert.AreEqual("Offense", round.branches[0].name);
            Assert.IsNotNull(round.branches[0].nodes);
            Assert.AreEqual("n1", round.branches[0].nodes[0].id);
        }
    }
}
