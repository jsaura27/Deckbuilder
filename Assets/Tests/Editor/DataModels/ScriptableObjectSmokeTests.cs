using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Game.Data;

namespace Deckbuilder.Tests.Editor
{
    public class ScriptableObjectSmokeTests
    {
        [Test]
        public void CanCreateCardDefinition()
        {
            // Updated to use Game.Data.CardDefinition
            var asset = ScriptableObject.CreateInstance<CardDefinition>();
            Assert.IsNotNull(asset);
            
            // Game.Data uses private fields with properties - cannot set directly
            // Properties may return empty strings, not null
            Assert.IsNotNull(asset.Effects);
            Assert.IsNotNull(asset.Tags);
        }
    }
}
