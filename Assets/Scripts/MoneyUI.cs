using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;
    void Start()
    {
        
    }

    void Update()
    {
        moneyText.text= "Ê£Óà½ð±Ò:£¤"+PlayerStates.CurrentMoney;

    }
}
