#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

// Creates placeholder UI prefabs for the SkillTree UI. Run from Unity Editor menu: Tools/SkillTree/Create Placeholder Prefabs
public static class CreateSkillTreePrefabs
{
    [MenuItem("Tools/SkillTree/Create Placeholder Prefabs")]
    public static void CreatePrefabs()
    {
        // Ensure folder exists
        var prefabFolder = "Assets/Prefabs/UI/SkillTree";
        System.IO.Directory.CreateDirectory(prefabFolder);

        // Create a simple Canvas + Panel GameObject hierarchy
        var canvasGO = new GameObject("SkillTreeCanvas");
        var canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        var panel = new GameObject("SkillTreePanel");
        panel.transform.SetParent(canvasGO.transform, false);
        var rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.sizeDelta = new Vector2(800, 600);

        var image = panel.AddComponent<UnityEngine.UI.Image>();
        image.color = new Color(0.1f, 0.1f, 0.1f, 0.8f);

        // Add a placeholder title
        var titleGO = new GameObject("Title");
        titleGO.transform.SetParent(panel.transform, false);
        var titleRT = titleGO.AddComponent<RectTransform>();
        titleRT.anchoredPosition = new Vector2(0, 260);
        var text = titleGO.AddComponent<UnityEngine.UI.Text>();
        text.text = "Skill Tree (Placeholder)";
        text.alignment = TextAnchor.MiddleCenter;
        text.fontSize = 24;
        text.color = Color.white;

        // Create the prefab asset
        var prefabPath = System.IO.Path.Combine(prefabFolder, "SkillTree_Placeholder.prefab");
        // Use PrefabUtility to save
        var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(canvasGO, prefabPath, InteractionMode.UserAction);

        // Clean up temporary scene objects
        Object.DestroyImmediate(canvasGO);

        Debug.Log($"Created placeholder prefab at {prefabPath}");
    }
}
#endif
