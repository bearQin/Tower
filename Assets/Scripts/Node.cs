using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.green;//������ʱ������ɫ
    private Color initColor;
    public Color notEnoughMoneyColor;
    private Renderer render;
    public Vector3 offset = new Vector3(0, 0.5f, 0);
    private GameObject turret;
    public Vector3 uiOffset = new Vector3(0, 0, 0);
    public TurretDesign selectedTurretDesign;
    public bool isUpgraded = false;//��ѡ�����Ƿ�������

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
        //�жϵ�ǰ�����Ƿ�������
        if(turret != null)
        {
            //��ʾ�����ͳ��۵�ui,ѡ��ڵ�
            BuildManager.Instance.SelectNode(this);  
            return;
        }
        //ȡ��ѡ��,�ر�ui
        BuildManager.Instance.DisSelect();

        if (!BuildManager.Instance.CanBuild) return;
        if(PlayerStates.CurrentMoney >= BuildManager.Instance.SelectedTurret.cost)
        {
            BuildTurret();
            Debug.Log("ʣ����:" + PlayerStates.CurrentMoney);
        }
        else
        {
            Debug.Log("��Ҳ���!");
        }
        
    }
    //��������
    public void BuildTurret()
    {
        //��Ǯ��������
        PlayerStates.CurrentMoney -= BuildManager.Instance.SelectedTurret.cost;
        GameObject _turret = Instantiate(BuildManager.Instance.SelectedTurret.prefab, GetPosition(), Quaternion.identity);
        turret = _turret;
        GameObject buildEffect = Instantiate(BuildManager.Instance.buildEffect, GetPosition(), Quaternion.identity);
        Destroy(buildEffect, 0.3f);
        selectedTurretDesign = BuildManager.Instance.SelectedTurret;
    }
    //��ȡλ��
    private Vector3 GetPosition()
    {
       return transform.position + offset;
    }

    public Vector3 GetUiPosition()
    {
        return transform.position + uiOffset;
    }

    //��������
    public void UpgradeTurret()
    {
        //�ж�ʣ�����Ƿ��ܹ�����
        if (PlayerStates.CurrentMoney < selectedTurretDesign.upgradeCost) 
        {
            Debug.Log("����!");
            return;
        }
        //��Ҽ���
        PlayerStates.CurrentMoney -= selectedTurretDesign.upgradeCost;
        //����ԭ����
        Destroy(turret);
        //����������
        GameObject _turret = Instantiate(selectedTurretDesign.upgradedPrefab, GetPosition(), Quaternion.identity);
        turret = _turret;
        GameObject buildEffect = Instantiate(BuildManager.Instance.buildEffect, GetPosition(), Quaternion.identity);
        Destroy(buildEffect, 0.3f);
        isUpgraded = true;
    }

    //��������
    public void SellTurret()
    {
        //�������
        PlayerStates.CurrentMoney += selectedTurretDesign.SellAmount;
        //����ԭ����
        Destroy(turret);
        GameObject sellEffect = Instantiate(BuildManager.Instance.sellEffect, GetPosition(), Quaternion.identity);
        Destroy(sellEffect, 3f);
        //ѡ�е�ģ����Ϊ��
        selectedTurretDesign = null;
        isUpgraded = false;     
    }
}
 