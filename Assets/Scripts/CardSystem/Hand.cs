using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    private List<Card> cards = new List<Card>();

    public void Add(Card c) => cards.Add(c);
    public void Remove(Card c) => cards.Remove(c);
    public IReadOnlyList<Card> Cards => cards.AsReadOnly();
}
