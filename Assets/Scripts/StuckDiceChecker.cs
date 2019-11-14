using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckDiceChecker : MonoBehaviour
{
    
    [SerializeField]
    private DieSpawner spawner;

    private bool canCheckForStuckDice = false;

    private void Start()
    {
        StartCoroutine(CheckStuckDice());
    }

    private void OnTriggerStay(Collider other)
    {
        if (canCheckForStuckDice)
        {
            Dice die = other.GetComponent<Dice>();
            if (die){
                RerollStuckDie(die);
            }
        }
    }

    /*Rerrolea el dado atascado*/
    private void RerollStuckDie(Dice die)
    {
        die.SetHasBeenRolled(false);
        die.Roll();
        canCheckForStuckDice = false;
        StartCoroutine(CheckStuckDice());

    }

    /*Corutina que espera a que los dados estes quietos para luego dar permiso a que
    compruebe si hay alguno atascado*/
    IEnumerator CheckStuckDice()
    {
        yield return new WaitUntil(() => spawner.areAllDiceCreated && spawner.AllDiceRolled() && spawner.AllDiceStop());
        Debug.Log("He activado el checkeo");
        canCheckForStuckDice = true;
    }
}
