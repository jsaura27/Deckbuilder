using System;

[Serializable]
public class AchievementModel
{
    public string id;
    public string title;
    public string description;
    public string category;
    public string visibility = "public";
    public AchievementCriteria criteria = new AchievementCriteria();
}

[Serializable]
public class AchievementCriteria
{
    public string eventName;
    public string op; // e.g. gte, count
    public int threshold;
}
