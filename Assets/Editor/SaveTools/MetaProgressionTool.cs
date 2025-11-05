using System.IO;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class MetaProgressionTool : EditorWindow
{
    private string _importPath = "";

    [MenuItem("Tools/Save/Meta Progression Tool")]
    public static void Open()
    {
        GetWindow<MetaProgressionTool>("Meta Progression");
    }

    void OnGUI()
    {
        GUILayout.Label("Meta Progression Import/Export", EditorStyles.boldLabel);
        if (GUILayout.Button("Export Current"))
        {
            var svc = new MetaProgressionService();
            svc.Load();
            var path = EditorUtility.SaveFilePanel("Export Meta Progression", "", "meta_progression_export.json", "json");
            if (!string.IsNullOrEmpty(path)) File.WriteAllText(path, JsonUtility.ToJson(svc.Data, true));
        }

        _importPath = EditorGUILayout.TextField("Import Path", _importPath);
        if (GUILayout.Button("Import"))
        {
            if (File.Exists(_importPath))
            {
                var json = File.ReadAllText(_importPath);
                var data = JsonUtility.FromJson<MetaProgressionData>(json);
                var svc = new MetaProgressionService();
                svc.Load();
                svc.SetData(data ?? new MetaProgressionData());
                svc.Save();
                EditorUtility.DisplayDialog("Import", "Imported meta progression.", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Import", "File not found.", "OK");
            }
        }
    }
}
#endif
