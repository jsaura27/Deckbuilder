using System.Collections.Generic;

namespace Deckbuilder.Services
{
    public interface ISkillTreeService
    {
        bool SelectBranch(string branchName);
        bool UnlockNode(string nodeId);
        bool CanUnlockNode(string nodeId);
        IReadOnlyList<string> GetUnlockedNodes();
        IReadOnlyList<string> GetAvailableNodes();
        void ResetTree();
    }
}
