﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    //Parameters
    public string areaName;
    protected int unitsCount;
    protected List<Tile> tiles;
    protected List<Player> playersInArea;
    protected Vector3 position;

    //Methods
    public Area(string areaName, Vector3 position)
    {
    }

    public void DamageMonster(int playerIndex)
    {
        playersInArea[playerIndex].ChangeLife(unitsCount * (-1));
    }

    public void DamageAllMonstersOnArea()
    {
        foreach (Player p in playersInArea) p.ChangeLife(unitsCount * (-1));
    }

    public void DamageAllMonsters()
    {
        //Lo mismo pero con todos los monstruos del juego!
    }

    public string GetName()
    {
        return areaName;
    }

    public Vector3 GetPosition()
    {
        return position;
    }

    

    private void Start()
    {
        //this.areaName = areaName;
        this.unitsCount = 0;
        this.position = transform.position;

    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("Me has tocao! " + areaName + ": " + position);
            }
        }
    }

}