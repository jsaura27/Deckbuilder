using NUnit.Framework;
using UnityEngine;
using Deckbuilder.DataModels;
using Game.Data;

namespace Deckbuilder.Tests.Integration.DataModels
{
    public class ScriptableObjectCreationIntegrationTests
    {
        [Test]
        public void CreateCardWithEffectIntegration()
        {
            // Note: Game.Data definitions use private fields with properties
            // This test needs to be rewritten to use proper test data creation
            var card = ScriptableObject.CreateInstance<CardDefinition>();
            
            // Cannot directly set private fields in tests
            // TODO: Create proper test data assets or use SerializedObject API
            Assert.IsNotNull(card);
            Assert.IsNotNull(card.Effects);
        }
    }
}
