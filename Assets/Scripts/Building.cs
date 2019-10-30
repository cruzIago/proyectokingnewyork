using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Tile
{
    //Methods
    public Building(int durability, int rewardCount, Reward rewardType) : base(durability, rewardCount, rewardType)
    {
        this.durability = durability;
        this.rewardCount = rewardCount;
        this.rewardType = rewardType;
    }

    public override void DestroyTile()
    {
        //Remove object and set reward to player
    }

    public void GenerateUnit()
    {
        //Generate paired Unit from this Bulding
    }
}
