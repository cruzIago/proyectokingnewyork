using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Reward { stars, energy, health };

public class Building : MonoBehaviour, ITile
{
    [SerializeField]
    private int durability;
    [SerializeField]
    private int rewardCount;
    [SerializeField]
    private Reward rewardType;

    private Area area;

    public void Awake()
    {
        GetComponent<Unit>().enabled = false;
    }

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
        area.tiles.Add(GetComponent<Unit>());
        transform.eulerAngles = Vector3.zero;
        GetComponent<Unit>().enabled = true;
        enabled = false;
        
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
