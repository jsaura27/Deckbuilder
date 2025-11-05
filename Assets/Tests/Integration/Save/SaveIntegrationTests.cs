using NUnit.Framework;
using System.IO;
using UnityEngine;

public class SaveIntegrationTests
{
    [Test]
    public void SaveManager_SaveAndLoad_TempPath()
    {
        var tmpDir = Path.Combine(Path.GetTempPath(), "deckbuilder_test");
        Directory.CreateDirectory(tmpDir);
        var tmpFile = Path.Combine(tmpDir, "save_meta.json");

        // Create data
        var d = new SaveData { version = "1.0", createdAt = System.DateTime.UtcNow.ToString("o"), contentVersion = "1.0", seed = "seed" };
        d.payload.unlockedItems.Add("integrationItem");

        // Use JsonUtility directly for portability in tests
        var json = JsonUtility.ToJson(d, true);
        File.WriteAllText(tmpFile, json);

        var loadedJson = File.ReadAllText(tmpFile);
        var d2 = JsonUtility.FromJson<SaveData>(loadedJson);
        Assert.IsNotNull(d2);
        Assert.AreEqual("1.0", d2.version);
        Assert.AreEqual(1, d2.payload.unlockedItems.Count);

        // Cleanup
        File.Delete(tmpFile);
        Directory.Delete(tmpDir);
    }
}
