#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using Game.Data;

namespace Deckbuilder.Editor.Importers
{
    public static class BlessingJsonImporter
    {
        [MenuItem("Deckbuilder/Import/Import Blessing JSON...")]
        public static void ImportBlessingJson()
        {
            var path = EditorUtility.OpenFilePanel("Select blessing JSON", "", "json");
            if (string.IsNullOrEmpty(path)) return;

            var text = File.ReadAllText(path);
            try
            {
                // Create a new ScriptableObject instance
                var asset = ScriptableObject.CreateInstance<BlessingDefinition>();
                
                // Use JsonUtility to populate the asset
                // Note: This requires fields to be public or [SerializeField]
                JsonUtility.FromJsonOverwrite(text, asset);

                var assetPath = "Assets/Content/Blessings/" + 
                    (string.IsNullOrEmpty(asset.Id) ? asset.DisplayName : asset.Id) + ".asset";
                Directory.CreateDirectory(Path.GetDirectoryName(assetPath));
                
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                EditorUtility.DisplayDialog("Import Success", "Blessing asset created at: " + assetPath, "OK");
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Import Error", ex.Message, "OK");
            }
        }
    }
}
#endif
