using NUnit.Framework;
using UnityEngine;

namespace Deckbuilder.Tests.Editor.Combat
{
    public class CombatRuntimeTests
    {
        [Test]
        public void TurnManager_StartTurn_IncrementsCurrentTurn()
        {
            var go = new GameObject("TurnManagerTest");
            var tm = go.AddComponent<Deckbuilder.Combat.TurnManager>();

            Assert.AreEqual(0, tm.CurrentTurn);
            tm.StartTurn();
            Assert.AreEqual(1, tm.CurrentTurn);
            tm.StartTurn();
            Assert.AreEqual(2, tm.CurrentTurn);

            Object.DestroyImmediate(go);
        }

        [Test]
        public void StatusEffect_CreateBurnEffect_Defaults()
        {
            var asset = ScriptableObject.CreateInstance<Deckbuilder.Combat.BurnEffect>();
            Assert.IsNotNull(asset);
            Assert.AreEqual(0, asset.duration);
            ScriptableObject.DestroyImmediate(asset);
        }
    }
}
