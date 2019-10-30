using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    //Enumeration
    //Should be in Manager?
    public enum CardType {Discard, Permanent}

    //Parameters
    string name;
    Decorator effect;
    CardType type;

    //Methods
    public Card(string name, Decorator effect, CardType type)
    {
        this.name = name;
        this.effect = effect;
        this.type = type;
    }

    public string GetName()
    {
        return name;
    }

    public Decorator ApplyEffect()
    {
        return effect;
    }

    public virtual void DiscardEffect()
    {
        Debug.Log("Error in Card: Method is not defined in Child");
    }
}
