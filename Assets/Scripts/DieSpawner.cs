using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpawner : MonoBehaviour
{
    public Dice die;
    public int numDice = 6;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numDice; i++)
        {
            GameObject.Instantiate(die,transform.TransformPoint(new Vector3(i,0,0)),Quaternion.identity);
        }
    }
}
