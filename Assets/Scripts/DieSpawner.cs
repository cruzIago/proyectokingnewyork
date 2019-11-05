﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSpawner : MonoBehaviour
{
    public Dice diePrefab;
    public int numDice = 6;
    public DieChecker dieChecker;
    public List<Dice> dice = new List<Dice>();
    private bool allDiceStop = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < numDice; i++)
        {
            Dice die = Instantiate(diePrefab,transform.TransformPoint(new Vector3(i,0,0)),Quaternion.identity);
            die.transform.parent = transform;
            die.createDie(dieChecker);
            dice.Add(die);
        }
    }

    public bool AllDiceStop()
    {
        if (!allDiceStop)
        {
            foreach (Dice die in dice)
            {
                if (!die.stop)
                    return false;
            }

            allDiceStop = true;
        }

        return allDiceStop;
    }
}
