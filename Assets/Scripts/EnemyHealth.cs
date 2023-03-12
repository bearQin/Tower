using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float InitHealth = 100;//��ʼ����ֵ
    private float currentHealth;
    public Image hpBar;//Ѫ��
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
        //����ֵ��Ϊ0 
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
        Destroy(gameObject);//���ٵ���
    }
}
