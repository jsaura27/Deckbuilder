using System;
using UnityEngine;

// Minimal persistence stub for achievements. Real persistence should integrate with SaveManager.
public static class AchievementPersistence
{
    // Placeholder save/load. Integrate with project's SaveManager.
    public static void SaveUnlocked(string achievementId)
    {
        // TODO: wire to SaveManager. For now, log for visibility.
        Debug.Log($"[AchievementPersistence] SaveUnlocked: {achievementId}");
    }

    public static bool IsUnlocked(string achievementId)
    {
        // TODO: query SaveManager. Default false.
        return false;
    }
}
