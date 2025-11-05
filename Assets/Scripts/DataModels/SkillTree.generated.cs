using System;
using System.Collections.Generic;

namespace Deckbuilder.DataModels
{
    [Serializable]
    public class SkillTree
    {
        public string id;
        public string classId;
        public List<Branch> branches;
    }

    [Serializable]
    public class Branch
    {
        public string name;
        public List<Node> nodes;
    }

    [Serializable]
    public class Node
    {
        public string id;
        public string type;
        public int cost;
        public List<string> prerequisites;
        public string conditionalUnlock;
        public object effect;
    }
}
