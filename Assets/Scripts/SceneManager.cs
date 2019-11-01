using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public bool debugMode = true;
    //Parameters
    //UX Interface????
    protected List<Player> players;
    protected List<Area> areas;
    protected Player activePlayer;
    protected Turn turn;
    protected Market market;

    //Methods
    protected void NextTurn()
    {
        
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

    }

    protected void MarkDeadPlayer()
    {

    }

    //Testing Methods
    protected void ClickOnArea()
    {
        //Character Movement
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Area targetArea = areas.Find(x => x.GetName() == hit.transform.gameObject.name);
                activePlayer.Move(targetArea);
                GameObject playerToChange = GameObject.Find(activePlayer.GetPlayerName());
                playerToChange.transform.position = activePlayer.GetPosition();
            }
        }
    }

    // Monobehaviour Methods

    private void Awake()
    {
        players = new List<Player>();
        areas = new List<Area>();
    }

    void Start()
    {
        foreach(Transform child in transform)
        {
            switch (child.tag)
            {
                case "Area":
                    Area newArea = new Area(child.gameObject.name, child.gameObject.transform.position);
                    Debug.Log(newArea.GetName() + ": " + newArea.GetPosition());
                    areas.Add(newArea);
                    break;
                case "Monster":
                    Player newPlayer = new Player(child.gameObject.name, "Captain Fish", child.gameObject.transform.position);
                    players.Add(newPlayer);
                    break;
                default:
                    Debug.Log("Bravely");
                    break;
            }
        }
        activePlayer = players[0];
        Debug.Log(activePlayer.GetPlayerName() + " es: " + activePlayer.GetMonsterName() + " , y está en " + activePlayer.GetPosition());

        market = new Market();
        Decorator e = new Decorator();//Testing
        market.deck.Push(new Card("Carta 1", e, Card.CardType.Permanent));
        market.deck.Push(new Card("Carta 2", e, Card.CardType.Permanent));
        market.deck.Push(new Card("Carta 3", e, Card.CardType.Discard));
        market.deck.Push(new Card("Carta 4", e, Card.CardType.Permanent));
        market.deck.Push(new Card("Carta 5", e, Card.CardType.Discard));
        market.deck.Push(new Card("Carta 6", e, Card.CardType.Permanent));
        market.ShuffleDeck();
        while (market.deck.Count != 0) Debug.Log("Carta que sale: " + market.deck.Pop().GetName());

    }

    void Update()
    {
        if (debugMode)
        {
            ClickOnArea();
        }
    }

}
