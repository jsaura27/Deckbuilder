#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using Game.Data;

namespace Deckbuilder.Editor.Importers
{
    public static class EquipmentJsonImporter
    {
        [MenuItem("Deckbuilder/Import/Import Equipment JSON...")]
        public static void ImportEquipmentJson()
        {
            var path = EditorUtility.OpenFilePanel("Select equipment JSON", "", "json");
            if (string.IsNullOrEmpty(path)) return;

            var text = File.ReadAllText(path);
            try
            {
                // Create a new ScriptableObject instance
                var asset = ScriptableObject.CreateInstance<EquipmentDefinition>();
                
                // Use JsonUtility to populate the asset
                JsonUtility.FromJsonOverwrite(text, asset);

                var assetPath = "Assets/Content/Equipment/" + 
                    (string.IsNullOrEmpty(asset.Id) ? asset.DisplayName : asset.Id) + ".asset";
                Directory.CreateDirectory(Path.GetDirectoryName(assetPath));
                
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                EditorUtility.DisplayDialog("Import Success", "Equipment asset created at: " + assetPath, "OK");
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Import Error", ex.Message, "OK");
            }
        }
    }
}
#endif
