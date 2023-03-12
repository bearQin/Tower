using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretDesign 
{
    public GameObject prefab;//炮塔的预制体
    public GameObject upgradedPrefab;//升级后的炮塔预制体
    public int cost;//花费
    public int upgradeCost;//升级所需花费
    public int SellAmount
    {
        get
        {
            return cost/2;
        }
    }
}
