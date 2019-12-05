using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieChecker : MonoBehaviour
{

    public int [] results;

    // Start is called before the first frame update
    void Start()
    {
        results = new int[6];
        for(int i = 0; i < results.Length; i++)
        {
            results[i] = 0;
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        DieSide dieSide = other.GetComponent<DieSide>();
        if (dieSide != null)
        {
            DetectDieSide(dieSide);
        }
  
    }

    /*Para saber de qué cara a caído el dado, se le comunica al dado en sí qué valor
     se ha obtenido de él*/
    private void DetectDieSide(DieSide dieSide)
    {  
        Dice die = dieSide.ownerDie;

        switch (dieSide.currentSide)
        {
            case dieResult.ATTACK: //3
                die.currentResult = dieResult.ATTACK;
                break;
            case dieResult.CELEBRITY: //5
                die.currentResult = dieResult.CELEBRITY;
                break;
            case dieResult.DESTRUCTION: //2
                die.currentResult = dieResult.DESTRUCTION;
                break;
            case dieResult.ENERGY: //6
                die.currentResult = dieResult.ENERGY;
                break;
            case dieResult.HEAL: //4
                die.currentResult = dieResult.HEAL;
                break;
            case dieResult.OUCH: //1
                die.currentResult = dieResult.OUCH;
                break;
            default:
                Debug.LogError("Impossible Die Side");
                break;
        }
        
    }

    public void SumResult(dieResult result)
    {
        results[(int)result]++;
    }

    public void SubtractResult(dieResult result)
    {
        results[(int)result]--;
    }

    public void PrintResult()
    {
        string[] dieResultNames = System.Enum.GetNames(typeof(dieResult));
        string textResult = "";
        for (int i = 0; i < results.Length; i++)
        {
            textResult += dieResultNames[i] + ": " + results[i] + "\n";
        }

        Debug.Log(textResult);
    }
}
