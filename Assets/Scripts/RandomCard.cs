using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCard
{
    public List<Card> RandomCards(List<Card> cards)
    {
        List<Card> rCards = new List<Card>();
        int index;
        int size = cards.Count;
        while (size>0)
        {
            index = Random.Range(0, size);
            rCards.Add(cards[index]);
            Card te = cards[index];
            cards[index] = cards[size - 1];
            cards[size - 1] = te;
            size--;
        }
        return rCards;
    }
}
