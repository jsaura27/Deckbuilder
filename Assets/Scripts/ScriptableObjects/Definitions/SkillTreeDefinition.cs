// Task 2.1: SkillTreeDefinition ScriptableObject
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(menuName = "Game/SkillTrees/SkillTree", fileName = "SkillTree_", order = 40)]
    public class SkillTreeDefinition : DefinitionBase
    {
        [SerializeField] private string classId; // Which character class this tree belongs to
        [SerializeField] private List<SkillBranch> branches = new();

        public string ClassId => classId;
        public IReadOnlyList<SkillBranch> Branches => branches;

        public override void CollectValidationIssues(List<string> issues)
        {
            base.CollectValidationIssues(issues);
            if (string.IsNullOrWhiteSpace(classId))
                issues?.Add($"SkillTreeDefinition {name}: classId empty");

            // Validate unique node ids inside tree
            var set = new HashSet<string>();
            foreach (var branch in branches)
            {
                if (branch.Nodes == null) continue;
                foreach (var node in branch.Nodes)
                {
                    if (string.IsNullOrWhiteSpace(node.Id))
                    {
                        issues?.Add($"SkillTreeDefinition {name}: node with empty id in branch {branch.Name}");
                        continue;
                    }
                    if (!set.Add(node.Id))
                    {
                        issues?.Add($"SkillTreeDefinition {name}: duplicate node id {node.Id}");
                    }
                    // prerequisites exist
                    if (node.Prerequisites != null)
                    {
                        foreach (var pre in node.Prerequisites)
                        {
                            if (!set.Contains(pre))
                            {
                                // Note: this simplistic check only works if prerequisite appears earlier; deeper graph validation may run later.
                                // We still log a warning for awareness.
                                issues?.Add($"SkillTreeDefinition {name}: prerequisite {pre} for node {node.Id} not encountered yet (may be forward reference)");
                            }
                        }
                    }
                }
            }
        }
    }
}
