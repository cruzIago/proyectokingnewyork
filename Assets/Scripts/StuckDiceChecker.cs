using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckDiceChecker : MonoBehaviour
{
    private bool canCheck = false;
    [SerializeField]
    private DieSpawner spawner;

    private void Start()
    {
        StartCoroutine(CheckStuckDice());
    }

    private void OnTriggerStay(Collider other)
    {
        if (canCheck)
        {
            Dice die = other.GetComponent<Dice>();
            if (die){
                die.SetHasBeenRolled(false);
                die.Roll();
                canCheck = false;
                StartCoroutine(CheckStuckDice());
            }
        }
    }

    IEnumerator CheckStuckDice()
    {
        yield return new WaitUntil(() => spawner.diceCreated && spawner.AllDiceRolled() && spawner.AllDiceStop());
        Debug.Log("He activado el checkeo");
        canCheck = true;
    }
}
