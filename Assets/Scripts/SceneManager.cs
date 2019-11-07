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
    public List<Player> players;
    public List<Area> areas;
    public Player activePlayer;
    public Turn turn;
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
        
    }

    /*Actualiza los elementos de la GUI*/
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

    /*Inicializa la posicion de los jugadores y relaciona jugadores y areas. Indica jugador activo*/
    protected void StartGame()
    {
        initButtons();
        activePlayer = players[0];
        foreach (Player p in players)
        {
            p.Move(p.currentArea);
            Debug.Log("Jugadores en " + p.currentArea.GetName() + ": " + p.currentArea.playersInArea.Count);
        }
        foreach (Area a in areas) a.setManager(this);
        panel.SetActive(false);
        if (debugMode) { Debug.Log(activePlayer.GetPlayerName() + " es: " + activePlayer.GetMonsterName() + " , y está en " + activePlayer.GetPosition()); }
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
