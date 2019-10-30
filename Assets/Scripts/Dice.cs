using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    //Enumeration
    public enum dieResult { Energy, Heal, Attack, Destruction, Celebrity, Ouch}
    //Parameters
    protected dieResult currentResult;
    protected bool stays;
    protected Player owner;

    //Methods
    public Dice(Player owner)
    {
        this.owner = owner;
        this.stays = false;
    }

    public void Roll()
    {
        //Devuelve resultado????
    }

    public void ApplyResult()
    {
        //Do something with currentResult. Devuelve??
    }
}
