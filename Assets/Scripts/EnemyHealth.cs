using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float InitHealth = 100;//初始生命值
    private float currentHealth;
    public Image hpBar;//血条
    public GameObject deadEffect;
    void Start()
    {
        currentHealth = InitHealth;
    }

    void Update()
    {
        
    }
    public void Damage(float amount)
    {      
        currentHealth -= amount;
        hpBar.fillAmount = currentHealth / InitHealth;
        //生命值减为0 
        if(currentHealth <= 0)
        {
            EnemyDie();
        }

    }

    private void EnemyDie()
    {
        EnemySpawner.EnemyAlive--;
        GameObject effect = Instantiate(deadEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5);
        Destroy(gameObject);//销毁敌人
    }
}
