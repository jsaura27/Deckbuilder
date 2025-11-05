using System;
namespace Deckbuilder.DataModels
{
    [Serializable]
    public enum Rarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    [Serializable]
    public enum CardType
    {
        Attack,
        Defense,
        Utility,
        Curse
    }

    [Serializable]
    public enum SlotType
    {
        Weapon,
        Armor,
        Trinket
    }
}
