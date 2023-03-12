using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    private TurretDesign selectdeTurret;
    private Node selectedNode;//��ǰѡ�еĽڵ�
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

    //�Ƿ���Դ�������
    public bool CanBuild
    {
        get
        {
            return selectdeTurret != null && selectdeTurret.prefab != null;
        }
    }

    //�жϽ���Ƿ��ܹ���������
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
        if (selectedNode == node)//ѡ�����ͬһ��
        {
            DisSelect();
            return;
        }
        selectdeTurret = null;
        //ѡ�񷽸�
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
