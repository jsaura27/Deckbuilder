using System;
using System.Collections.Generic;

namespace Deckbuilder.Systems.SkillTree
{
    [Serializable]
    public class SkillTreeRuntimeState
    {
        public string SelectedBranch;
        public HashSet<string> UnlockedNodes = new HashSet<string>();
        public int AvailableSkillPoints;
        public string CurrentClassEvolution; // e.g., "Berserker"

        // Simple snapshot serializer support can be added later
    }
}
