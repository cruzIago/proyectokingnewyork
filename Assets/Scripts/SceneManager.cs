using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Clase que gestiona la escena de desarrollo del juego*/
public class SceneManager : MonoBehaviour
{
    //Variable de debugging
    public bool debugMode = true;
    //Parameters
    //UX Interface
    #region UX Parameters
    public List<RawImage> playersInfo;
    public RawImage pInfoPrefab;
    public Canvas canvas;
    #endregion
    public List<Player> players;
    public List<Area> areas;
    public Player activePlayer;
    protected int activePId;
    protected Turn turn;
    protected Market market;

    //UI
    public Button buttonTurn;
    public Button buttonYes;
    public Button buttonNo;
    public Button buttonOk;
    public GameObject panel;

    //Methods
    /*Comienza el siguiente turno*/
    protected void NextTurn()
    {
        HighlightActivePlayer(false);
        activePId++;
        if(activePId >= players.Count)
        {
            activePId = 0;
        }
        activePlayer = players[activePId];
        turn.ChangePlayer(activePlayer);
        HighlightActivePlayer(true);
    }

    /*Actualiza la GUI*/
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

    /*Cambia el el numero de un elemento concreto de la UI*/
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

    /*Hace la animacion de marcar o desmarcar la tarjeta del jugador activo*/
    protected void HighlightActivePlayer(bool highlight)
    {
        Animator animator = playersInfo[activePId].GetComponent<Animator>();
        animator.SetBool("isHighlighted", highlight);
    }

    /*Marca la tarjeta de un jugador como muerto haciendola transparente*/
    protected void MarkDeadPlayer(RawImage deadPInfo)
    {
        deadPInfo.CrossFadeAlpha(0.3f, 0.5f, false);
    }

    /*Inicializa la posicion de los jugadores y relaciona jugadores y areas. Indica jugador activo
     Instancia las tarjetas de la GUI de cada jugador*/
    protected void StartGame()
    {

        for(int i = 0; i < players.Count; i++)
        {
            RawImage newPInfo = Instantiate(pInfoPrefab, canvas.transform);
            newPInfo.transform.localPosition = new Vector3(-319, 165 - 71*i, 0);
            playersInfo.Add(newPInfo);
        }

        initButtons();
        activePlayer = players[0];
        activePId = 0;
        HighlightActivePlayer(true);
        foreach (Player p in players)
        {
            p.Move(p.currentArea);
            Debug.Log("Jugadores en " + p.currentArea.GetName() + ": " + p.currentArea.playersInArea.Count);
        }
        foreach (Area a in areas) a.setManager(this);
        panel.SetActive(false);
        if (debugMode) { Debug.Log(activePlayer.GetPlayerName() + " es: " + activePlayer.GetMonsterName() + " , y está en " + activePlayer.GetPosition()); }
        market = new Market();
        turn = new Turn(activePlayer, Turn.State.Begining, this);
    }

    /*Inicializa la visibilidad de todos los botones*/
    protected void initButtons()
    {
        buttonTurn.gameObject.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
        buttonOk.gameObject.SetActive(false);
    }

    /*Visibiliza el boton de continuar*/
    public void CreateConfirmButton()
    {
        buttonTurn.gameObject.SetActive(true);
    }

    /*Evento de pulsado de continuar*/
    public void OnClickConfirm()
    {
        //Inhabilita la posibilidad de moverse a areas y pasa a la fase de mercado
        foreach (Area a in areas) a.movementFlag = false;
        turn.Market();
    }

    /*Evento de pulsado de si*/
    public void OnClickYes()
    {
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
        turn.MoveWithClick();
    }

    /*Evento de pulsado de no*/
    public void OnClickNo()
    {
        turn.Market();
        panel.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
    }

    /*Evento de pulsado de ok*/
    public void OnClickOk()
    {
        panel.SetActive(false);
        buttonOk.gameObject.SetActive(false);
        CreateConfirmButton();
        //Habilita las areas a las que el jugador puede moverse
        foreach (Area a in areas)
        {
            //Comprueba que el área no sea Manhattan, que tenga menos de 2 jugadores y que no sea el área actual
            if (!a.GetName().Contains("Manhattan") && a.playersInArea.Count < 2
                && a.GetName() != activePlayer.currentArea.GetName()) { a.movementFlag = true; }
        }
    }

    // Monobehaviour Methods
    void Start()
    {
        StartGame();
        turn.Move();
        //No borrar, se va a usar en futuras pruebas
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
}
