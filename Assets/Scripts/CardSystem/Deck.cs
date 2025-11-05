using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Card
{
    public string id;
    public string title;
}

public class Deck
{
    private List<Card> cards = new List<Card>();

    public Deck() { }

    public void Add(Card c) => cards.Add(c);
    public void Shuffle() { /* TODO: implement Fisher-Yates shuffle */ }
    public Card Draw()
    {
        if (cards.Count == 0) return null;
        var c = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return c;
    }
    public int Count => cards.Count;
}
