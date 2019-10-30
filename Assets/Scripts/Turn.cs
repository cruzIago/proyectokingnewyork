using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    //Enumeration
    //Should be in SceneManager?
    public enum State {Begining, ThrowDice, SolveDice, Movement, Market, EndTurn};
    //Parameters
    protected Player currentPlayer;
    protected State currentState;

    //Methods
    public Turn(Player currentPlayer, State currentState)
    {
        this.currentPlayer = currentPlayer;
        this.currentState = currentState;
    }

    public void NextState()
    {
        currentState++;//Test please
    }

    public void RollDice()
    {
        //Roll
        //Keep
        //ReRoll
    }

    public void SolveDice()
    {
        //Apply Effects
    }

    public void Move()
    {
        //All Movement Logic
    }

    public void Market()
    {
        //All Market Logic
    }

    public void ChangePlayer(Player nextPlayer)
    {
        currentPlayer = nextPlayer;
        currentState = State.Begining;
    }

    public void EndTurn()
    {

    }
}
