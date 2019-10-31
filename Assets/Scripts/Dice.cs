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
    private bool applyResult = false;
    [SerializeField]
    private DieChecker dieChecker;
    private Player owner;
    [SerializeField]
    private float diceVelocity = 0.0f;
    private Rigidbody rb;
    private const float MAX_TORQUE = 90.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Vector3 randomDirection = new Vector3(Random.value, Random.value, Random.value);
        rb.AddTorque(randomDirection * Random.value * MAX_TORQUE, ForceMode.Acceleration);

        transform.rotation = Quaternion.AngleAxis(Random.value * 360.0f, randomDirection);
    }

    void Update()
    {
        diceVelocity = rb.velocity.magnitude;
        if (isStop() && !applyResult)
        {
            applyResult = true;
            Debug.Log("Me he parado");
            dieChecker.SumResult(currentResult);
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
