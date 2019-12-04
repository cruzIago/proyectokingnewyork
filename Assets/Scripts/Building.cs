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

    [SerializeField]
    private GameObject unityPrefab;

    //Methods
    public void Init(Area area, int durability, int rewardCount, Reward rewardType)
    {
        this.area = area;
        this.durability = durability;
        this.rewardCount = rewardCount;
        this.rewardType = rewardType;
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
        GenerateUnit();
    }

    public void GenerateUnit()
    {
        //Generate paired Unit from this Bulding
        Unit unit = Instantiate(unityPrefab).GetComponent<Unit>();
        unit.Init(area,Random.Range(1,4),Random.Range(1,4),(Reward)Random.Range(0,3));
    }
}
