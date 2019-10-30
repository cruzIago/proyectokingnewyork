using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    //Enumeration
    public enum diceResult { ENERGY, HEAL, ATTACK, DESTRUCTION,CELEBRITY, OUCH};
    //Parameters
    private diceResult currentResult;
    private bool stays;
    private Player owner;
    private float diceVelocity;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }

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
