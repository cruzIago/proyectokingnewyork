using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, ITile
{
    
    [SerializeField]
    private int durability;
    [SerializeField]
    private int rewardCount;
    [SerializeField]
    private Reward rewardType;
    private Area area;

    //Methods
    public void Init(Area area, int durability, int rewardCount, Reward rewardType)
    {
        this.area = area;
        this.durability = durability;
        this.rewardCount = rewardCount;
        this.rewardType = rewardType;
        area.tiles.Add(this);
        area.unitsCount++;
    }

    public void DestroyTile(Player player)
    {
        //Remove object and set reward to player
        switch (rewardType)
        {
            case Reward.energy:
                player.ChangeEnergy(rewardCount);
                break;
            case Reward.health:
                player.ChangeLife(rewardCount);
                break;
            case Reward.stars:
                player.ChangeStars(rewardCount);
                break;
        }
        area.unitsCount--;
        area.tiles.Remove(this);
    }

    
}
