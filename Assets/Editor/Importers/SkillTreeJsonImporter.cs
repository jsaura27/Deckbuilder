#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using Game.Data;

namespace Deckbuilder.Editor.Importers
{
    public static class SkillTreeJsonImporter
    {
        [MenuItem("Deckbuilder/Import/Import SkillTree JSON...")]
        public static void ImportSkillTreeJson()
        {
            var path = EditorUtility.OpenFilePanel("Select skill tree JSON", "", "json");
            if (string.IsNullOrEmpty(path)) return;

            var text = File.ReadAllText(path);
            try
            {
                // Create a new ScriptableObject instance
                var asset = ScriptableObject.CreateInstance<SkillTreeDefinition>();
                
                // Use JsonUtility to populate the asset
                JsonUtility.FromJsonOverwrite(text, asset);

                var assetPath = "Assets/Content/SkillTrees/" + 
                    (string.IsNullOrEmpty(asset.Id) ? asset.ClassId : asset.Id) + ".asset";
                Directory.CreateDirectory(Path.GetDirectoryName(assetPath));
                
                AssetDatabase.CreateAsset(asset, assetPath);
                AssetDatabase.SaveAssets();
                EditorUtility.DisplayDialog("Import Success", "SkillTree asset created at: " + assetPath, "OK");
            }
            catch (System.Exception ex)
            {
                EditorUtility.DisplayDialog("Import Error", ex.Message, "OK");
            }
        }
    }
}
#endif
