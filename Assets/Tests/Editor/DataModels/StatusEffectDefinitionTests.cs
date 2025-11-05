using NUnit.Framework;
using UnityEngine;

public class StatusEffectDefinitionTests
{
    [Test]
    public void CreateInstance_ShouldNotBeNull()
    {
        var inst = ScriptableObject.CreateInstance<StatusEffectDefinition>();
        Assert.IsNotNull(inst);
    }
}
