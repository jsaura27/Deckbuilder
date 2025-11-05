using System;
using System.Collections.Generic;
using UnityEngine;
using Deckbuilder.Services;

namespace Deckbuilder.Systems.SkillTree
{
    public class SkillTreeManager : MonoBehaviour, ISkillTreeService
    {
        public static SkillTreeManager Instance { get; private set; }

        private SkillTreeRuntimeState state = new SkillTreeRuntimeState();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);
        }

        public bool SelectBranch(string branchName)
        {
            if (!string.IsNullOrEmpty(state.SelectedBranch)) return false; // already selected
            state.SelectedBranch = branchName;
            return true;
        }

        public bool UnlockNode(string nodeId)
        {
            if (!CanUnlockNode(nodeId)) return false;
            state.UnlockedNodes.Add(nodeId);
            // Effects application to be handled by effect system
            return true;
        }

        public bool CanUnlockNode(string nodeId)
        {
            if (state.AvailableSkillPoints <= 0) return false;
            if (state.UnlockedNodes.Contains(nodeId)) return false;
            // Defer deep prerequisite checks to validation system / evaluator for now
            return true;
        }

        public IReadOnlyList<string> GetUnlockedNodes() => new List<string>(state.UnlockedNodes);

        public IReadOnlyList<string> GetAvailableNodes() => new List<string>(); // placeholder

        public void ResetTree()
        {
            state = new SkillTreeRuntimeState();
        }
    }
}
