﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum dieResult { ENERGY, HEAL, ATTACK, DESTRUCTION, CELEBRITY, OUCH };


public class Dice : MonoBehaviour
{
    //Enumeration
    //Parameters
    public dieResult currentResult { get; set; }
    public bool stays { get; set; }
    private bool applyResult = false;
    public bool stop { get; set; }
    public DieChecker dieChecker;
    private Player owner;
    [SerializeField]
    private float diceVelocity = 0.0f;
    private Rigidbody rb;
    private const float MAX_TORQUE = 90.0f;
    private Vector3 initPos;
    private bool hasBeenRolled = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void Start()
    {
        initPos = transform.position;
        Roll();
    }

    void Update()
    {
        diceVelocity = rb.velocity.magnitude;
        if (IsStop() && !applyResult)
        {
            applyResult = true;
            stop = true;
            rb.isKinematic = true;
            dieChecker.SumResult(currentResult);
        }
        
    }

    public void createDie(DieChecker dieChecker)
    {
        this.dieChecker = dieChecker;
    }

    public void Roll()
    {
        
        rb.isKinematic = false;
        rb.velocity = -transform.up;
        stays = false;
        stop = false;
        applyResult = false;
        Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);
        rb.AddTorque(randomDirection * Random.value * MAX_TORQUE, ForceMode.Acceleration);
        transform.rotation = Quaternion.AngleAxis(Random.value * 360.0f, randomDirection);
        transform.position = initPos;
        hasBeenRolled = true;
    }

    public void ReRoll()
    {
        dieChecker.SubtractResult(currentResult);
        Roll();
    }

    public void ApplyResult()
    {
        //Do something with currentResult. Devuelve??
    }

    public bool IsStop()
    {
        return diceVelocity == 0.0f;
    }

    public bool HasBeenRolled()
    {
        return hasBeenRolled;
    }

    public void SetHasBeenRolled(bool hasBeenRolled) {
        this.hasBeenRolled = hasBeenRolled;
    }
}
