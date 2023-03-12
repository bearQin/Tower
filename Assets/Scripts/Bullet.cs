using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform m_target;//目标敌人的位置
    public float speed = 80;//子弹的速度
    public float damage = 50;//子弹攻击力
    public float exploseRadius;//爆炸范围
    public GameObject bulletImpactEffect;
    public void SetTarget(Transform target)
    {
        m_target = target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target == null) 
        {
            Destroy(gameObject);//防止前一颗子弹已将敌人销毁,后一颗子弹在空中无操作
            return; 
        }
           
        Vector3 dir = m_target.position - transform.position;
        if(Vector3.Distance(m_target.position,transform.position) <  speed*Time.deltaTime)
        {
            //击中目标了
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);
        transform.LookAt(m_target);
    }
    private void HitTarget()
    {
        //击中特效
        GameObject bulletEffect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
        Destroy(bulletEffect, 3f);
        //炮弹
        if(exploseRadius > 0)
        {
            Explose();
        }
        else
        {
            EnemyDamage(m_target);    //受伤减血
        }
        Destroy(gameObject);//销毁自己
    }
    private void Explose()
    {
        //返回圆的范围内碰到的所有的collider
        Collider[] colliders = Physics.OverlapSphere(transform.position, exploseRadius);
        foreach (var item in colliders)
        {
            if (item.tag == "Enemy")
            {
                EnemyDamage(item.transform);
            }
        }
    }
    private void EnemyDamage(Transform enemy)
    {
        EnemyHealth enemyHp = enemy.GetComponent<EnemyHealth>();
        if(enemyHp != null)
        {
            enemyHp.Damage(damage);
            Debug.Log("爆炸");
        }
    }
}      

