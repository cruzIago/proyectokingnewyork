using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market
{
    //Constants
    protected readonly int SHOWNCARDS_NUM = 3;

    //Parameters
    public Stack<Card> deck;
    public Stack<Card> discardedCards;
    public List<Card> shownCards;

    //Methods
    public Market()
    {
        deck = new Stack<Card>();
        discardedCards = new Stack<Card>();
        shownCards = new List<Card>();
    }

    public Card BuyCard(int index)
    {
        return shownCards[index];
    }

    public void ShuffleDeck()
    {
        {
            Card[] cardList = deck.ToArray();
            deck.Clear();
            for (int i = 0; i < cardList.Length; i++)
            {
                Card tmp = cardList[i];
                int r = Random.Range(i, cardList.Length);
                cardList[i] = cardList[r];
                cardList[r] = tmp;
            }
            foreach (Card c in cardList) deck.Push(c);            
        }
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
            ShuffleDeck();
        }
    }
}
