using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public bool debugMode = true;
    //Parameters
    //UX Interface????
    public List<Player> players;
    public List<Area> areas;
    protected Player activePlayer;
    protected int activePId;
    protected Turn turn;
    protected Market market;

    //Methods
    protected void NextTurn()
    {
        if(activePId < players.Count)
        {
            activePId++;
        }
        else
        {
            activePId = 0;
        }
        activePlayer = players[activePId];
        turn.ChangePlayer(activePlayer);
        HighlightActivePlayer();
    }

    protected void UpdateGUI()
    {

    }

    protected void ChangeHealth(int health)
    {
    }

    protected void ChangeStars(int stars)
    {
    }

    protected void ChangeEnergy()
    {
    }

    protected void HighlightActivePlayer()
    {
        if (debugMode)
        {
            Debug.Log(activePlayer.GetPlayerName() + " es: " + activePlayer.GetMonsterName() + " , y está en " + activePlayer.GetPosition());
        }
        
    }

    protected void MarkDeadPlayer()
    {

    }

    // Monobehaviour Methods
    void Start()
    {
        activePlayer = players[0];
        Debug.Log(activePlayer.GetPlayerName() + " es: " + activePlayer.GetMonsterName() + " , y está en " + activePlayer.GetPosition());

        /*market = new Market();
        Decorator e = new Decorator();//Testing
        market.deck.Push(new Card("Carta 1", e, Card.CardType.Permanent));
        market.deck.Push(new Card("Carta 2", e, Card.CardType.Permanent));
        market.deck.Push(new Card("Carta 3", e, Card.CardType.Discard));
        market.deck.Push(new Card("Carta 4", e, Card.CardType.Permanent));
        market.deck.Push(new Card("Carta 5", e, Card.CardType.Discard));
        market.deck.Push(new Card("Carta 6", e, Card.CardType.Permanent));
        market.ShuffleDeck();
        while (market.deck.Count != 0) Debug.Log("Carta que sale: " + market.deck.Pop().GetName());*/

    }

    void Update()
    {
    }

    //Testing Methods
    /*public void ClickOnArea()
    {
        //Character Movement
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("Area " + areaName + ": " + position);
            }
        }
    }*/

}
