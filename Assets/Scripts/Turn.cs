using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Clase que gestiona todo el flujo de un turno*/
public class Turn : MonoBehaviour
{
    #region parameters
    //Enumeration
    public enum State { ThrowDice, SolveDice, Movement, Market, EndTurn };
    private int NUMSTATES = 6;
    //Parameters
    protected SceneManager manager;
    protected Player activePlayer;
    protected State currentState;

    [SerializeField]
    private GameObject diceBoardPrefab;
    private DiceBoard diceBoard;
    public GameObject diceBoardPos;
    #endregion

    //Methods
    /*Constructor*/
    public Turn(Player activePlayer, State currentState, SceneManager manager)
    {
        this.activePlayer = activePlayer;
        this.currentState = currentState;
        this.manager = manager;
    }

    #region States Methods
    /*Lanza los dados*/
    public void RollDice()
    {
        //Roll
        //Keep
        //ReRoll
        diceBoard = Instantiate(diceBoardPrefab, diceBoardPos.transform).GetComponent<DiceBoard>();
        diceBoard.turn = this;
        diceBoard.mainCamera = Camera.main;
        manager.createDiceHUD();
    }

    /*Aplica los efectos de los dados*/
    public void SolveDice()
    {
        //Apply Effects
        manager.hideDiceHUD();
        diceBoard.SolveDice(activePlayer);
        Destroy(diceBoard.gameObject);
        NextState();
    }

    /*Fase de movimiento*/
    public void Move()
    {
        //Muestra el mensaje de la GUI de que entra en fase de movimiento
        manager.panel.SetActive(true);
        Text textPanel = manager.panel.GetComponentInChildren<Text>();
        textPanel.text = "Bust'a move";

        //Si el jugador esta en Manhattan:
        if (activePlayer.currentArea.GetName().Contains("Manhattan"))
        {
            //Si esta en Manhattan Sur, mueve a Manhattan Medio
            if (activePlayer.currentArea.GetName().Contains("Sur"))
            {
                activePlayer.Move(manager.areas[1]);
                NextState();
            }
            //Si esta en Manhattan Medio, mueve a Manhattan Norte
            else if (activePlayer.currentArea.GetName().Contains("Medio"))
            {
                activePlayer.Move(manager.areas[2]);
                NextState();
            }
            //Si esta en Manhattan Norte, deja elegir otro distrito
            else
            {
                MoveWithClick();
            }
        }
        //Si no esta en Manhattan, comprueba si esta libre
        else if ((manager.areas[0].playersInArea.Count + manager.areas[1].playersInArea.Count +
            manager.areas[2].playersInArea.Count) < 2)
        {
            activePlayer.Move(manager.areas[0]);
            activePlayer.ChangeStars(1);
            activePlayer.ChangeEnergy(2);
            NextState();
        }
        //Si Manhattan no esta libre, comprueba si quiere moverse
        else
        {
            AskForMovement();
        }
    }

    /*Fase de mercado*/
    public void Market()
    {
        //All Market Logic
        manager.market.ShowCards();
        manager.panel.gameObject.SetActive(false);
    }

    /*Cambia al jugador indicado y vuelve al estado inicial*/
    public void ChangePlayer(Player nextPlayer)
    {
        Debug.Log("CHANGE PLAYER");
        activePlayer = nextPlayer;
        NextState();
    }

    #endregion

    #region Flow Methods
    public void StartTurn(Player activePlayer, State currentState, SceneManager manager)
    {
        this.activePlayer = activePlayer;
        this.currentState = currentState;
        this.manager = manager;
        RollDice();
    }

    /*Cambia el estado del juego al siguiente. Debe tener un switch que llame a las funciones*/
    public void NextState()
    {
        currentState++;
        switch (currentState)
        {
            case State.ThrowDice:
                RollDice();
                break;
            case State.SolveDice:
                SolveDice();
                break;
            case State.Movement:
                Move();
                break;
            case State.Market:
                Market();
                break;
            case State.EndTurn:
                manager.NextTurn();
                break;
            default:
                currentState = State.ThrowDice;
                RollDice();
                break;
        }
    }
    #endregion

    #region Auxiliar Methods
    /*Avisa al jugador que puede moverse haciendo click en un área y habilita el movimiento a las áreas disponibles*/
    public void MoveWithClick()
    {
        //Muestra un mensaje al jugador
        manager.panel.SetActive(true);
        manager.buttonOk.gameObject.SetActive(true);
        Text textPanel = manager.panel.GetComponentInChildren<Text>();
        textPanel.text = "Escoge donde moverte";
    }

    /*Pregunta al jugador si quiere moverse o quedarse donde esta*/
    protected void AskForMovement()
    {
        //Muestra un mensaje al jugador y crea los botones de confirmacion
        manager.panel.SetActive(true);
        manager.buttonYes.gameObject.SetActive(true);
        manager.buttonNo.gameObject.SetActive(true);
        Text textPanel = manager.panel.GetComponentInChildren<Text>();
        textPanel.text = "¿Quieres moverte?";
    }

    public void RethrowDice()
    {
        diceBoard.InputedReroll();
    }

    public void lastRethrow()
    {
        manager.CreateConfirmButton();
        manager.hideDiceHUD();
    }
    #endregion

    #region Getters Setters
    public State getState()
    {
        return currentState;
    }
    #endregion


}
