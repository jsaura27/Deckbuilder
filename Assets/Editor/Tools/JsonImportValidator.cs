#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Deckbuilder.EditorTools
{
    public class JsonImportValidator : EditorWindow
    {
        private string folderPath = "";
        private Vector2 scroll;
        private string[] jsonFiles = new string[0];
        private string message = "";

        [MenuItem("Tools/JSON Import Validator")]
        public static void ShowWindow()
        {
            var w = GetWindow<JsonImportValidator>("JSON Import Validator");
            w.minSize = new Vector2(450, 300);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("JSON Import & Validation Tool", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Folder:", GUILayout.Width(50));
            folderPath = EditorGUILayout.TextField(folderPath);
            if (GUILayout.Button("Browse", GUILayout.Width(80)))
            {
                var selected = EditorUtility.OpenFolderPanel("Select JSON folder", Application.dataPath, "");
                if (!string.IsNullOrEmpty(selected))
                {
                    // convert absolute path to project-relative if inside project
                    if (selected.StartsWith(Application.dataPath, StringComparison.OrdinalIgnoreCase))
                    {
                        folderPath = "Assets" + selected.Substring(Application.dataPath.Length).Replace("\\", "/");
                    }
                    else
                    {
                        folderPath = selected;
                    }
                    RefreshFileList();
                }
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Refresh Files")) RefreshFileList();
            EditorGUILayout.Space();

            if (jsonFiles.Length > 0)
            {
                scroll = EditorGUILayout.BeginScrollView(scroll);
                foreach (var f in jsonFiles)
                {
                    EditorGUILayout.LabelField(f);
                }
                EditorGUILayout.EndScrollView();

                if (GUILayout.Button("Dry-run Validate JSONs"))
                {
                    DoDryRunValidation();
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No JSON files found in selected folder.", MessageType.Info);
            }

            if (!string.IsNullOrEmpty(message))
            {
                EditorGUILayout.HelpBox(message, MessageType.None);
            }
        }

        private void RefreshFileList()
        {
            try
            {
                var abs = Path.IsPathRooted(folderPath) ? folderPath : Path.Combine(Directory.GetCurrentDirectory(), folderPath);
                if (Directory.Exists(abs))
                {
                    jsonFiles = Directory.GetFiles(abs, "*.json", SearchOption.TopDirectoryOnly)
                        .Select(p => Path.GetFullPath(p))
                        .ToArray();
                    message = $"Found {jsonFiles.Length} JSON files.";
                }
                else
                {
                    jsonFiles = new string[0];
                    message = "Folder does not exist.";
                }
            }
            catch (Exception ex)
            {
                message = "Error reading folder: " + ex.Message;
                jsonFiles = new string[0];
            }
        }

        private void DoDryRunValidation()
        {
            int valid = 0;
            int invalid = 0;
            foreach (var abs in jsonFiles)
            {
                try
                {
                    var txt = File.ReadAllText(abs);
                    // basic JSON parse check
                    var trimmed = txt.TrimStart();
                    if (trimmed.StartsWith("{") || trimmed.StartsWith("["))
                    {
                        valid++;
                    }
                    else
                    {
                        invalid++;
                        Debug.LogWarning($"Invalid JSON format (not object/array): {abs}");
                    }

                    // heuristic schema check: look for a schema file named like the json file without extension + ".schema.json" under Assets/docs/schemas/
                    var name = Path.GetFileNameWithoutExtension(abs);
                    var candidate = Path.Combine(Application.dataPath, "../Assets/docs/schemas", name + ".schema.json");
                    candidate = Path.GetFullPath(candidate);
                    if (!File.Exists(candidate))
                    {
                        Debug.Log($"Schema not found for {name}, expected at Assets/docs/schemas/{name}.schema.json");
                    }
                }
                catch (Exception ex)
                {
                    invalid++;
                    Debug.LogError($"Error parsing {abs}: {ex.Message}");
                }
            }

            message = $"Dry-run complete: {valid} valid, {invalid} invalid (see Console).";
        }
    }
}
#endif
