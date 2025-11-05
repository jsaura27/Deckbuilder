using System;
using System.Collections.Generic;

[Serializable]
public class MetaProgressionData
{
    public string version = "1.0";
    public List<string> unlockedClasses = new List<string>();
    public List<string> unlockedCards = new List<string>();
    public List<string> unlockedBlessings = new List<string>();
    public List<string> unlockedEquipment = new List<string>();
    public List<string> achievements = new List<string>();
    public string createdAt = DateTime.UtcNow.ToString("o");
    public string modifiedAt = null;
}
