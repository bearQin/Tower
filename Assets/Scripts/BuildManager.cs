using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private TurretDesign selectdeTurret;
    private Node selectedNode;//当前选中的节点
    public NodeUI nodeUI;
    public GameObject buildEffect;
    public GameObject sellEffect;

    public TurretDesign SelectedTurret
    {
        get
        {
            return selectdeTurret;
        }
        set
        {
            selectdeTurret = value;
        }
    }

    //是否可以创建炮塔
    public bool CanBuild
    {
        get
        {
            return selectdeTurret != null && selectdeTurret.prefab != null;
        }
    }

    //判断金币是否能够购买炮塔
    public bool HasEnoughMoney
    {
        get
        {
            return PlayerStates.CurrentMoney >= SelectedTurret.cost;
        }
    }
    private void Awake()
    {
        Instance = this; 
    }
  
    public void SelectNode(Node node)
    {
        if (selectedNode == node)//选择的是同一个
        {
            DisSelect();
            return;
        }
        selectdeTurret = null;
        //选择方格
        selectedNode = node;
        nodeUI.SetTarget(selectedNode);
        nodeUI.ShowUI();
    }

    public void DisSelect()
    {
        selectedNode = null;
        nodeUI.HideUI();  
    }
}
