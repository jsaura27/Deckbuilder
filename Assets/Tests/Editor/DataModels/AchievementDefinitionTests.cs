using NUnit.Framework;
using UnityEngine;

public class AchievementDefinitionTests
{
    [Test]
    public void CreateAchievementDefinition_ScriptableObject_NotNull()
    {
        var inst = ScriptableObject.CreateInstance<AchievementDefinition>();
        Assert.IsNotNull(inst);
    }
}
