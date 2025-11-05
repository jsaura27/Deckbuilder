using System;
using System.IO;
using UnityEngine;

public class MetaProgressionService
{
    private const string FileName = "meta_progression.json";
    private string FilePath => Path.Combine(Application.persistentDataPath, FileName);

    public MetaProgressionData Data { get; private set; }

    public void Load()
    {
        if (!File.Exists(FilePath))
        {
            Data = new MetaProgressionData();
            return;
        }

        try
        {
            var json = File.ReadAllText(FilePath);
            Data = JsonUtility.FromJson<MetaProgressionData>(json) ?? new MetaProgressionData();
        }
        catch (Exception)
        {
            Data = new MetaProgressionData();
        }
    }

    public void Save()
    {
        if (Data == null) Data = new MetaProgressionData();
        Data.modifiedAt = DateTime.UtcNow.ToString("o");
        var json = JsonUtility.ToJson(Data, true);
        File.WriteAllText(FilePath, json);
    }

    public void Reset()
    {
        Data = new MetaProgressionData();
        Save();
    }

    // Safely replace the internal data object. Use this rather than assigning to Data directly.
    public void SetData(MetaProgressionData newData)
    {
        Data = newData ?? new MetaProgressionData();
    }
}
