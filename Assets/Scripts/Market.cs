using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Clase que se encarga de la gestion de las operaciones del mercado: cartas a comprar, baraja y pila de descartes*/
public class Market : MonoBehaviour
{
    //Constants
    protected readonly int SHOWNCARDS_NUM = 3;
    
    //Parameters
    public Stack<Card> deck;//Cartas del mazo sin mostrar
    public Stack<Card> discardedCards;//Pila de descartes
    public List<Card> shownCards;//Cartas para comprar
    [SerializeField] private SceneManager manager;

    //Methods
    /*Constructor*/
    public Market()
    {
    }

    /*Devuelve la carta comprada*/
    //TODO: Al comprar se debe reponer la siguiente carta del deck
    public Card BuyCard(int index)
    {
        return shownCards[index];
    }

    public void HideCards() {
        foreach (Card c in shownCards) { c.ChangeVisibility(false); }
    }

    public void ShowCards()
    {
        foreach (Card c in shownCards) {
            c.ChangeVisibility(true);
            c.SetBuyer(manager.activePlayer);
            c.SetFlag(true);
        }
    }

    private void Start()
    {
        this.deck = new Stack<Card>();
        this.discardedCards = new Stack<Card>();
    }

    private void Update()
    {
        
    }

    //Metodos que no se van a usar
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
