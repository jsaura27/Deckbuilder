using System;
using System.Collections.Generic;

namespace Deckbuilder.DataModels
{
    [Serializable]
    public class Blessing
    {
        public string id;
        public string name;
        public string rarity;
        public string description;
        public object baseEffect;
        public List<string> triggers;
        public List<EvolutionStage> evolution;
    }

    [Serializable]
    public class EvolutionStage
    {
        public int stage;
        public string condition;
        public object resultingEffect;
    }
}
