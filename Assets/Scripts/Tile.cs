using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile
{
    //Enumeration
    //Should be in SceneManager?
    public enum Reward {stars, energy, health};

    //Parameters
    protected int durability;
    protected int rewardCount;
    protected Reward rewardType;

    //Methods
    public Tile(int durability, int rewardCount, Reward rewardType)
    {
        this.durability = durability;
        this.rewardCount = rewardCount;
        this.rewardType = rewardType;
    }

    public virtual void DestroyTile()
    {
        Debug.Log("Error: Method not defined in Children");
    }

}

