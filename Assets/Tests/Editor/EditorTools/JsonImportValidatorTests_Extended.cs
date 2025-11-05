using NUnit.Framework;
using UnityEngine;
using System.IO;
using System.Reflection;
using Deckbuilder.EditorTools;

namespace Deckbuilder.Tests.Editor.EditorTools
{
    public class JsonImportValidatorTests_Extended
    {
        private string tempDir;

        [SetUp]
        public void SetUp()
        {
            tempDir = Path.Combine(Path.GetTempPath(), "Deckbuilder_Test_JsonImport_" + System.Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempDir);
        }

        [TearDown]
        public void TearDown()
        {
            try { if (Directory.Exists(tempDir)) Directory.Delete(tempDir, true); } catch { }
        }

        [Test]
        public void RefreshFileList_FindsJsonFilesAndSetsMessage()
        {
            // create sample json files
            var f1 = Path.Combine(tempDir, "one.json");
            var f2 = Path.Combine(tempDir, "two.json");
            File.WriteAllText(f1, "{}");
            File.WriteAllText(f2, "[]");

            // instantiate the EditorWindow type via reflection (constructor is internal due to EditorWindow)
            var type = typeof(JsonImportValidator);
            // EditorWindow must be instantiated via ScriptableObject.CreateInstance to initialize Unity native state properly
            var instance = (JsonImportValidator)ScriptableObject.CreateInstance(type) ;

            // set folderPath field
            var fp = type.GetField("folderPath", BindingFlags.NonPublic | BindingFlags.Instance);
            fp.SetValue(instance, tempDir);

            // call RefreshFileList
            var method = type.GetMethod("RefreshFileList", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(method, "RefreshFileList method should exist");
            method.Invoke(instance, null);

            // check jsonFiles and message fields
            var jsonFilesField = type.GetField("jsonFiles", BindingFlags.NonPublic | BindingFlags.Instance);
            var messageField = type.GetField("message", BindingFlags.NonPublic | BindingFlags.Instance);
            var jsonFiles = (string[])jsonFilesField.GetValue(instance);
            var message = (string)messageField.GetValue(instance);

            Assert.IsNotNull(jsonFiles);
            Assert.AreEqual(2, jsonFiles.Length);
            Assert.IsTrue(message.Contains("Found 2 JSON files"));
        }

        [Test]
        public void DoDryRunValidation_ReportsValidAndInvalidCounts()
        {
            // create one valid and one invalid json
            var valid = Path.Combine(tempDir, "valid.json");
            var invalid = Path.Combine(tempDir, "invalid.json");
            File.WriteAllText(valid, "{}\n");
            File.WriteAllText(invalid, "notjson");

            var type = typeof(JsonImportValidator);
            var instance = (JsonImportValidator)ScriptableObject.CreateInstance(type);
            var fp = type.GetField("folderPath", BindingFlags.NonPublic | BindingFlags.Instance);
            fp.SetValue(instance, tempDir);

            // prime jsonFiles
            var refresh = type.GetMethod("RefreshFileList", BindingFlags.NonPublic | BindingFlags.Instance);
            refresh.Invoke(instance, null);

            var dry = type.GetMethod("DoDryRunValidation", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(dry);
            dry.Invoke(instance, null);

            var messageField = type.GetField("message", BindingFlags.NonPublic | BindingFlags.Instance);
            var message = (string)messageField.GetValue(instance);

            Assert.IsTrue(message.Contains("Dry-run complete"));
            Assert.IsTrue(message.Contains("1 valid"));
            Assert.IsTrue(message.Contains("1 invalid"));
        }
    }
}
