using System;
using System.Collections.Generic;

namespace Deckbuilder.DataModels
{
    [Serializable]
    public class Card
    {
        public string id;
        public string name;
        public string type; // Attack/Defense/Utility/Curse
        public int cost;
        public string rarity;
        public string description;
        public List<Effect> effects;
        public List<string> tags;
        public UpgradePath upgradePath;
    }

    [Serializable]
    public class Effect
    {
        public string effectType;
        public string target; // Self, Enemy, AllEnemies, AllAllies
        // value is left generic; further typing may be added by hand
        public object value;
    }

    [Serializable]
    public class UpgradePath
    {
        public string upgradedId;
        public int cost;
    }
}
