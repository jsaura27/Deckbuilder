using System;
using System.Collections.Generic;

[Serializable]
public class RunState
{
    public string runId;
    public long seed;
    public string startedAt; // ISO string for portability
    public string endedAt; // ISO string or null

    // Example nested structures (can be expanded)
    public PlayerProgress playerProgress;
    public DeckState deckState;

    public List<string> acquiredBlessings;
    public List<string> acquiredEquipment;
    public SkillTreeSelection skillTreeSelection;

    // Generic metadata bag (serialized cautiously)
    public Dictionary<string, string> metadata;

    public string version = "1.0";

    public RunState()
    {
        runId = Guid.NewGuid().ToString();
        seed = DateTime.UtcNow.Ticks;
        startedAt = DateTime.UtcNow.ToString("o");
        endedAt = null;
        playerProgress = new PlayerProgress();
        deckState = new DeckState();
        acquiredBlessings = new List<string>();
        acquiredEquipment = new List<string>();
        skillTreeSelection = new SkillTreeSelection();
        metadata = new Dictionary<string, string>();
    }
}

[Serializable]
public class PlayerProgress
{
    public int level = 1;
    public int xp = 0;
    public string chosenClass = "";
}

[Serializable]
public class DeckState
{
    public List<string> drawPile = new List<string>();
    public List<string> hand = new List<string>();
    public List<string> discard = new List<string>();
}

[Serializable]
public class SkillTreeSelection
{
    public string chosenBranch = "";
    public List<string> unlockedNodes = new List<string>();
}