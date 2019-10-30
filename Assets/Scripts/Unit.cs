using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : Tile
{
    //Methods
    public Unit(int durability, int rewardCount, Reward rewardType) : base(durability, rewardCount, rewardType)
    {
        this.durability = durability;
        this.rewardCount = rewardCount;
        this.rewardType = rewardType;
    }

    public override void DestroyTile()
    {
        //Remove object and set Reward to player
    }


}
