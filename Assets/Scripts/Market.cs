using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market
{
    //Constants
    protected readonly int SHOWNCARDS_NUM = 3;

    //Parameters
    Stack<Card> deck;
    Stack<Card> discardedCards;
    List<Card> shownCards;

    //Methods

    public Card BuyCard(int index)
    {
        return shownCards[index];
    }

    public void ReRollCards()
    {
        //Can't use foreach since we have a value asignment
        for (int i = 0; i < SHOWNCARDS_NUM; i++)
        {
            discardedCards.Push(shownCards[i]);
            shownCards[i] = deck.Pop();
        }
    }

    public void RefillDeck()
    {
        while (discardedCards.Count != 0)
        {
            deck.Push(discardedCards.Pop());
        }
    }
}
