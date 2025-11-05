using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Game/Achievement Definition")]
public class AchievementDefinition : ScriptableObject
{
    public string id;
    public string title;
    [TextArea]
    public string description;
    public string category;
    public bool hidden = false;
    public AchievementModel criteriaModel;
}
