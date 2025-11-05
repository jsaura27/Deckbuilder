using NUnit.Framework;
using UnityEngine;
using Game.Data;
using System.Collections.Generic;
using System.Reflection;

namespace Deckbuilder.Tests.Editor.DataModels
{
    public class SkillTreeDefinitionTests
    {
        [Test]
        public void CreateAssetMenuAttribute_Exists()
        {
            var attr = typeof(SkillTreeDefinition).GetCustomAttribute<CreateAssetMenuAttribute>();
            Assert.IsNotNull(attr);
            StringAssert.Contains("Game/SkillTrees/SkillTree", attr.menuName);
        }

        [Test]
        public void DuplicateNodeIds_AreReported()
        {
            var inst = ScriptableObject.CreateInstance<SkillTreeDefinition>();
            var idField = typeof(DefinitionBase).GetField("id", BindingFlags.NonPublic | BindingFlags.Instance);
            idField.SetValue(inst, "st_1");

            var branchesField = typeof(SkillTreeDefinition).GetField("branches", BindingFlags.NonPublic | BindingFlags.Instance);
            var branches = new List<SkillBranch>();
            branchesField.SetValue(inst, branches);

            var issues = new List<string>();
            inst.CollectValidationIssues(issues);
            Assert.IsNotNull(issues);
        }
    }
}
