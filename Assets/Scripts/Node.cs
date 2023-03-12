using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.green;//鼠标放置时方块颜色
    private Color initColor;
    public Color notEnoughMoneyColor;
    private Renderer render;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    private GameObject turret;
    public Vector3 uiOffset = new Vector3(0, 0, 0);
    public TurretDesign selectedTurretDesign;
    public bool isUpgraded = false;//所选炮塔是否已升级

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        initColor = render.material.color;
    }

    
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (!BuildManager.Instance.CanBuild) return;
        if (!BuildManager.Instance.HasEnoughMoney)
        {
            render.material.color = notEnoughMoneyColor;
            return;
        }
        render.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        render.material.color = initColor;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        //判断当前格子是否有炮塔
        if(turret != null)
        {
            //显示升级和出售的ui,选择节点
            BuildManager.Instance.SelectNode(this);  
            return;
        }
        //取消选择,关闭ui
        BuildManager.Instance.DisSelect();

        if (!BuildManager.Instance.CanBuild) return;
        if(PlayerStates.CurrentMoney >= BuildManager.Instance.SelectedTurret.cost)
        {
            BuildTurret();
            Debug.Log("剩余金币:" + PlayerStates.CurrentMoney);
        }
        else
        {
            Debug.Log("金币不足!");
        }
        
    }
    //创建炮塔
    public void BuildTurret()
    {
        //金钱数量减少
        PlayerStates.CurrentMoney -= BuildManager.Instance.SelectedTurret.cost;
        GameObject _turret = Instantiate(BuildManager.Instance.SelectedTurret.prefab, GetPosition(), Quaternion.identity);
        turret = _turret;
        GameObject buildEffect = Instantiate(BuildManager.Instance.buildEffect, GetPosition(), Quaternion.identity);
        Destroy(buildEffect, 0.3f);
        selectedTurretDesign = BuildManager.Instance.SelectedTurret;
    }
    //获取位置
    private Vector3 GetPosition()
    {
       return transform.position + offset;
    }

    public Vector3 GetUiPosition()
    {
        return transform.position + uiOffset;
    }

    //升级炮塔
    public void UpgradeTurret()
    {
        //判断剩余金币是否能够购买
        if (PlayerStates.CurrentMoney < selectedTurretDesign.upgradeCost) 
        {
            Debug.Log("余额不足!");
            return;
        }
        //金币减少
        PlayerStates.CurrentMoney -= selectedTurretDesign.upgradeCost;
        //销毁原炮塔
        Destroy(turret);
        //创建新炮塔
        GameObject _turret = Instantiate(selectedTurretDesign.upgradedPrefab, GetPosition(), Quaternion.identity);
        turret = _turret;
        GameObject buildEffect = Instantiate(BuildManager.Instance.buildEffect, GetPosition(), Quaternion.identity);
        Destroy(buildEffect, 0.3f);
        isUpgraded = true;
    }

    //出售炮塔
    public void SellTurret()
    {
        //金币增加
        PlayerStates.CurrentMoney += selectedTurretDesign.SellAmount;
        //销毁原炮塔
        Destroy(turret);
        GameObject sellEffect = Instantiate(BuildManager.Instance.sellEffect, GetPosition(), Quaternion.identity);
        Destroy(sellEffect, 3f);
        //选中的模板置为空
        selectedTurretDesign = null;
        isUpgraded = false;     
    }
}
 