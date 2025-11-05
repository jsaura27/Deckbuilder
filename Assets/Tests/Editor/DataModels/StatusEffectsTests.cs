using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor.DataModels
{
    public class StatusEffectsTests
    {
        [Test]
        public void StatusEffectDefinition_CreateInstance_NotNull()
        {
            var def = ScriptableObject.CreateInstance<StatusEffectDefinition>();
            Assert.IsNotNull(def);
        }
    }
}
