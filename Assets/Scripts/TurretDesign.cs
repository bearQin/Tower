using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretDesign 
{
    public GameObject prefab;//������Ԥ����
    public GameObject upgradedPrefab;//�����������Ԥ����
    public int cost;//����
    public int upgradeCost;//�������軨��
    public int SellAmount
    {
        get
        {
            return cost/2;
        }
    }
}
