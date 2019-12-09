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
    public void Init(Area area)
    {
        this.area = area;
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
        area.tiles.Remove(this);
        Destroy(gameObject);
    }

    public int GetDurability()
    {
        return durability;
    }

    public int GetRewardCount()
    {
        return rewardCount;
    }

    public Reward GetRewardType()
    {
        return rewardType;
    }
}
