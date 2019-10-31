using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpawner : MonoBehaviour
{
    public Dice diePrefab;
    public int numDice = 6;
    public DieChecker dieChecker;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numDice; i++)
        {
            Dice die = Instantiate(diePrefab,transform.TransformPoint(new Vector3(i,0,0)),Quaternion.identity);
            die.createDie(dieChecker);
        }
    }
}
