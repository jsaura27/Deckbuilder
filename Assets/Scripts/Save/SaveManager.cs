using System;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string SaveFileName = "save_meta.json";

    public static string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFileName);
    }

    public static void Save(SaveData data)
    {
        var json = JsonUtility.ToJson(data, prettyPrint: true);
        var path = GetSavePath();
        var dir = Path.GetDirectoryName(path);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        var tmp = path + ".tmp";
        File.WriteAllText(tmp, json);
        if (File.Exists(path)) File.Replace(tmp, path, null);
        else File.Move(tmp, path);
    }

    public static SaveData Load()
    {
        var path = GetSavePath();
        if (!File.Exists(path)) return null;
        var json = File.ReadAllText(path);
        try
        {
            return JsonUtility.FromJson<SaveData>(json);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
