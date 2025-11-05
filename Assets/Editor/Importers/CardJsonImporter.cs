#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using Game.Data;

namespace Deckbuilder.Editor.Importers
{
    public static class CardJsonImporter
    {
        [MenuItem("Deckbuilder/Import/Import Card JSON...")]
        public static void ImportCardJson()
        {
            var path = EditorUtility.OpenFilePanel("Select card JSON", "", "json");
            if (string.IsNullOrEmpty(path)) return;

            var text = File.ReadAllText(path);
            try
            {
                // Create a new ScriptableObject instance
                var asset = ScriptableObject.CreateInstance<CardDefinition>();
                
                // Use JsonUtility to populate the asset
                JsonUtility.FromJsonOverwrite(text, asset);

                var assetPath = "Assets/Content/Cards/" + 
                    (string.IsNullOrEmpty(asset.Id) ? asset.DisplayName : asset.Id) + ".asset";
                Directory.CreateDirectory(Path.GetDirectoryName(assetPath));
                
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                EditorUtility.DisplayDialog("Import Success", "Card asset created at: " + assetPath, "OK");
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Import Error", ex.Message, "OK");
            }
        }
    }
}
#endif
