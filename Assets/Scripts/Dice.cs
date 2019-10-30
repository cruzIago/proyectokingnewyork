using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dieResult { ENERGY, HEAL, ATTACK, DESTRUCTION, CELEBRITY, OUCH };


public class Dice : MonoBehaviour
{
    //Enumeration
    //Parameters
    public dieResult currentResult { get; set; }
    private bool stays = false;
    private Player owner;
    [SerializeField]
    private float diceVelocity = 0.0f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        diceVelocity = rb.velocity.magnitude;
        if (isStop())
        {
            //mostrar el resultado por pantalla
            Debug.Log(currentResult);
        }
        
    }

    public void Roll()
    {
        //Devuelve resultado????
    }

    public void ApplyResult()
    {
        //Do something with currentResult. Devuelve??
    }

    private bool isStop()
    {
        return diceVelocity == 0.0f;
    }
}
