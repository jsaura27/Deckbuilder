using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string version;
    public string createdAt;
    public string contentVersion;
    public string seed;
    public MetaProgression payload = new MetaProgression();
}

[Serializable]
public class MetaProgression
{
    public List<string> unlockedItems = new List<string>();
    public int playerLevel = 0;
}
