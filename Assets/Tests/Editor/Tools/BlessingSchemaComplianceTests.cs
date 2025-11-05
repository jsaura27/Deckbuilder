using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BlessingSchemaComplianceTests
{
    [Test]
    public void BlessingJsonFiles_MatchLiteSchema()
    {
        var folder = Path.Combine(Application.dataPath, "Resources", "Blessings");
        if (!Directory.Exists(folder))
        {
            Assert.Ignore($"No Blessings folder found at {folder}; importer not run yet.");
            return;
        }

        var jsonFiles = Directory.GetFiles(folder, "*.json", SearchOption.TopDirectoryOnly);
        if (jsonFiles.Length == 0)
        {
            Assert.Ignore("No blessing JSON files to validate.");
            return;
        }

        var failures = new List<string>();
        foreach (var f in jsonFiles)
        {
            var text = File.ReadAllText(f);
            try
            {
                var parsed = UnityEngine.JsonUtility.FromJson<JsonUtilityWrapper.BlessingJson>(text);
                var (valid, errors) = Deckbuilder.Tests.Utils.JsonSchemaLiteValidator.ValidateBlessing(parsed);
                if (!valid) failures.Add($"{Path.GetFileName(f)}: {string.Join("; ", errors)}");
            }
            catch (System.Exception ex)
            {
                failures.Add($"{Path.GetFileName(f)}: parse error {ex.Message}");
            }
        }

        if (failures.Count > 0)
        {
            Assert.Fail("Schema validation failures:\n" + string.Join("\n", failures));
        }
    }
}
