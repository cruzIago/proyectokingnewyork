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
    public RawImage pInfoPrefab;
    public Canvas canvas;
    #endregion
    public List<Player> players;
    public List<Area> areas;
    public int nPlayers;
    protected Player activePlayer;
    protected int activePId;
    protected Turn turn;
    protected Market market;

    //Methods
    protected void NextTurn()
    {
        HighlightActivePlayer(false);
        activePId++;
        if(activePId >= nPlayers)
        {
            activePId = 0;
        }
        //activePlayer = players[activePId];
        //turn.ChangePlayer(activePlayer);
        HighlightActivePlayer(true);
    }

    public void UpdateGUI()
    {
        //Cambio de Textos
        for(int i = 0; i < playersInfo.Count; i++)
        {
            Text[] texts = playersInfo[i].GetComponentsInChildren<Text>();
            foreach(Text t in texts)
            {
                switch (t.tag)
                {
                    case "healthInfo":
                        changeUINumber(players[i].remainingHealth, t, true);
                        break;
                    case "starsInfo":
                        changeUINumber(players[i].stars, t, true);
                        break;
                    case "energyInfo":
                        changeUINumber(players[i].energy, t, false);
                        break;
                    case "cardsInfo":
                        changeUINumber(players[i].GetNumberOfCards(), t, true);
                        break;
                    default:
                        Debug.Log("Error, should never happen. (UpdateGUI/switch)");
                        break;
                }
            }
            if(players[i].remainingHealth <= 0)
            {
                MarkDeadPlayer(playersInfo[i]);
            }
        }
    }

    protected void changeUINumber(int number, Text text, bool leftOfIcon)
    {
        if (leftOfIcon)
        {
            text.text = number + "x";
        }
        else
        {
            text.text = "x" + number;
        }
    }

    protected void HighlightActivePlayer(bool highlight)
    {
        Animator animator = playersInfo[activePId].GetComponent<Animator>();
        animator.SetBool("isHighlighted", highlight);
    }

    protected void MarkDeadPlayer(RawImage deadPInfo)
    {
        deadPInfo.CrossFadeAlpha(0.3f, 0.5f, false);
    }

    // Monobehaviour Methods
    void Start()
    {
        /*activePlayer = players[0];
        Debug.Log(activePlayer.GetPlayerName() + " es: " + activePlayer.GetMonsterName() + " , y está en " + activePlayer.GetPosition());*/

        for(int i = 0; i < nPlayers; i++)
        {
            RawImage newPInfo = Instantiate(pInfoPrefab, canvas.transform);
            newPInfo.transform.localPosition = new Vector3(-319, 165 - 71*i, 0);
            playersInfo.Add(newPInfo);
        }

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
                HighlightActivePlayer(true);
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextTurn();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                activePId = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                activePId = 2;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                UpdateGUI();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                MarkDeadPlayer(playersInfo[0]);
            }
        }
        
    }
}
