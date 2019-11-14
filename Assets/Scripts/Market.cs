using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Clase que se encarga de la gestion de las operaciones del mercado: cartas a comprar, baraja y pila de descartes*/
public class Market
{
    //Constants
    protected readonly int SHOWNCARDS_NUM = 3;
    
    //Parameters
    public Stack<Card> deck;//Cartas del mazo sin mostrar
    public Stack<Card> discardedCards;//Pila de descartes
    public List<Card> shownCards;//Cartas para comprar

    //Methods
    /*Constructor*/
    public Market()
    {
        deck = new Stack<Card>();
        discardedCards = new Stack<Card>();
        shownCards = new List<Card>();
    }

    /*Devuelve la carta comprada*/
    //TODO: Al comprar se debe reponer la siguiente carta del deck
    public Card BuyCard(int index)
    {
        return shownCards[index];
    }

    /*Aleatoriza el mazo*/
    public void ShuffleDeck()
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

    /*Manda al descarte las cartas mostradas y saca nuevas*/
    public void ReRollCards()
    {
        //TODO: foreach para evitar que den problemas las ultimas
        //Can't use foreach since we have a value asignment
        for (int i = 0; i < SHOWNCARDS_NUM; i++)
        {
            discardedCards.Push(shownCards[i]);
            shownCards[i] = deck.Pop();
        }
    }

    /*Rellena la baraja con la pila de descartes*/
    public void RefillDeck()
    {
        while (discardedCards.Count != 0)
        {
            deck.Push(discardedCards.Pop());
        }
    }
}
