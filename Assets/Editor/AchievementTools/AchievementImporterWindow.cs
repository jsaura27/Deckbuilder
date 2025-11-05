#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class AchievementImporterWindow : EditorWindow
{
    [MenuItem("Tools/Achievements/Importer")]
    public static void ShowWindow() {
        GetWindow<AchievementImporterWindow>("Achievement Importer");
    }

    private string sampleId = "sample_tutorial";

    void OnGUI()
    {
        GUILayout.Label("Achievement Tools", EditorStyles.boldLabel);
        GUILayout.Space(8);
        GUILayout.Label("Create sample Achievement ScriptableObject");
        sampleId = EditorGUILayout.TextField("Sample ID", sampleId);
        if (GUILayout.Button("Create Sample Achievement")) {
            CreateSampleAchievement(sampleId);
        }
    }

    private void CreateSampleAchievement(string id) {
        var asset = ScriptableObject.CreateInstance<AchievementDefinition>();
        asset.id = id;
        asset.title = "Sample Achievement";
        asset.description = "Automatically generated sample achievement.";
        var path = $"Assets/Content/Achievements/{id}.asset";
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
#endif
