using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITile
{

    void DestroyTile(Player player);
    int GetDurability();
    int GetRewardCount();
    Reward GetRewardType();
}

