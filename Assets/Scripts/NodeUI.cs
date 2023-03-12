using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public GameObject ui;
    private Node target;
    public Text costText;
    public Text sellText;
    public Button upgradeBtn;

    void Start()
    {

    }


    void Update()
    {
       
    }

    public void ShowUI()
    {
        StartCoroutine(FadeIn());
    }

    public void HideUI()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0;
        ui.SetActive(true);
        while(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime*5;
            yield return null;
        }

    }

    IEnumerator FadeOut()
    {
        canvasGroup.alpha = 1;
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 5;
            yield return null;
        }
        ui.SetActive(false);
    }

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetUiPosition();
        if (target.isUpgraded)
        {
            costText.text = "已升级";
            upgradeBtn.interactable = false;
        }
        else
        {
            costText.text = "花费" + target.selectedTurretDesign.upgradeCost;
            upgradeBtn.interactable = true; 
        }
        
        sellText.text = "售价" + "¥" + target.selectedTurretDesign.SellAmount;
    }

    public void UpgradeBtnClicked()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DisSelect();
    }

    public void SellBtnClicked()
    {
        target.SellTurret();
        BuildManager.Instance.DisSelect();
    }
}
