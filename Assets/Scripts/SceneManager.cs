using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public bool debugMode = true;
    //Parameters
    //UX Interface????
    #region UX Parameters
    public List<RawImage> playersInfo;
    #endregion
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

    public void UpdateGUI()
    {
        //Cambio de Textos
        foreach (RawImage pInfo in playersInfo)
        {
            Text[] texts = pInfo.GetComponentsInChildren<Text>();
            foreach(Text t in texts)
            {
                switch (tag)
                {
                    case "healthInfo":
                        ChangeHealth(7, t);
                        break;
                    case "starsInfo":
                        ChangeStars(7, t);
                        break;
                    case "energyInfo":
                        ChangeStars(7, t);
                        break;
                    default:
                        Debug.Log("Error, should never happen. (UpdateGUI/switch)");
                        break;
                }
            }
        }
    }

    protected void ChangeHealth(int health, Text healthText)
    {
                
    }

    protected void ChangeStars(int stars, Text starsText)
    {
    }

    protected void ChangeEnergy(int energy, Text energyText)
    {
    }

    protected void HighlightActivePlayer()
    {
        Texture2D longIcon = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Assets/Placeholders/tarjeta personaje activo.png", typeof(Texture2D));
        playersInfo[activePId].texture = longIcon;
        playersInfo[activePId].SetNativeSize();
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
        if (debugMode)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                HighlightActivePlayer();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                activePId = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                activePId = 2;
            }
        }
        
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
