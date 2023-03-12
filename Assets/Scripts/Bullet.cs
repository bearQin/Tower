using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform m_target;//Ŀ����˵�λ��
    public float speed = 80;//�ӵ����ٶ�
    public float damage = 50;//�ӵ�������
    public float exploseRadius;//��ը��Χ
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
            Destroy(gameObject);//��ֹǰһ���ӵ��ѽ���������,��һ���ӵ��ڿ����޲���
            return; 
        }
           
        Vector3 dir = m_target.position - transform.position;
        if(Vector3.Distance(m_target.position,transform.position) <  speed*Time.deltaTime)
        {
            //����Ŀ����
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized*speed*Time.deltaTime, Space.World);
        transform.LookAt(m_target);
    }
    private void HitTarget()
    {
        //������Ч
        GameObject bulletEffect = Instantiate(bulletImpactEffect, transform.position, Quaternion.identity);
        Destroy(bulletEffect, 3f);
        //�ڵ�
        if(exploseRadius > 0)
        {
            Explose();
        }
        else
        {
            EnemyDamage(m_target);    //���˼�Ѫ
        }
        Destroy(gameObject);//�����Լ�
    }
    private void Explose()
    {
        //����Բ�ķ�Χ�����������е�collider
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
            Debug.Log("��ը");
        }
    }
}      

