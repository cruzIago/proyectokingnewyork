using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        DieSide dieSide = other.GetComponent<DieSide>();
        if (dieSide != null)
        {
            Dice die = dieSide.ownerDie;

            switch (dieSide.currentSide)
            {
                case dieResult.ATTACK:
                    die.currentResult = dieResult.ATTACK;
                    break;
                case dieResult.CELEBRITY:
                    die.currentResult = dieResult.CELEBRITY;
                    break;
                case dieResult.DESTRUCTION:
                    die.currentResult = dieResult.DESTRUCTION;
                    break;
                case dieResult.ENERGY:
                    die.currentResult = dieResult.ENERGY;
                    break;
                case dieResult.HEAL:
                    die.currentResult = dieResult.HEAL;
                    break;
                case dieResult.OUCH:
                    die.currentResult = dieResult.OUCH;
                    break;
                default:
                    Debug.LogError("Impossible Die Side");
                    break;
            }
        }
    }
}
