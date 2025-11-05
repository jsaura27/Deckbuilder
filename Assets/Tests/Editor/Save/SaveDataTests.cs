using NUnit.Framework;
using UnityEngine;

public class SaveDataTests
{
    [Test]
    public void SaveData_CanInstantiate()
    {
        var d = new SaveData();
        Assert.IsNotNull(d);
        Assert.IsNotNull(d.payload);
    }

    [Test]
    public void SaveData_SerializationRoundTrip()
    {
        var d = new SaveData { version = "1.0", createdAt = System.DateTime.UtcNow.ToString("o"), contentVersion = "1.0", seed = "seed" };
        d.payload.unlockedItems.Add("testItem");
        var json = JsonUtility.ToJson(d);
        var d2 = JsonUtility.FromJson<SaveData>(json);
        Assert.AreEqual(d.version, d2.version);
        Assert.AreEqual(d.payload.unlockedItems.Count, d2.payload.unlockedItems.Count);
    }
}
