using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area
{
    //Parameters
    protected string areaName;
    protected int unitsCount;
    protected List<Tile> tiles;
    protected List<Player> playersInArea;
    protected Vector3 position;

    //Methods
    public Area(string areaName, Vector3 position)
    {
        this.areaName = areaName;
        this.unitsCount = 0;
        this.position = position;
        //Tiles & Players filled in SceneManager
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
}
