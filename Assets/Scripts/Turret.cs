using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{   
    [Header("����")]
    public float range = 5;
    public string enemytag = "Enemy";
    public Transform target;//����Ŀ��
    public Transform partRotate;//��ת����ͷ
    public Transform bulletPoint;//�ӵ�����λ��
    public float rotSpeed = 10;

    [Header("ʹ���ӵ�(Ĭ��")]
    public GameObject bulletPrefab;//�ӵ�Ԥ����
    public float bulletRate = 2f;//�����ӵ�������
    private float countDown = 0;

    [Header("�Ƿ�ʹ�ü���")]

    public bool useLaser;//�ж��Ƿ�ʹ�ü���
    public LineRenderer lineRender;//����
    public float overTimeHp = 15;//��ʱ���Ѫ
    public float slowPct = 0.3f;//���ٰٷֱ�
    private EnemyAI enemyMove;//���˼���
    private EnemyHealth enemyHp;//���˼�Ѫ
    public ParticleSystem impactEffect;//������Ч
    public Light pointLight;//�ƹ�

    void Start()
    {
        InvokeRepeating("UpdateTarget",0,0.5f);
        countDown = 1 / bulletRate;
    }

    
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                lineRender.enabled = false;//����ȡ��
                impactEffect.Stop();//ֹͣ������Ч
                pointLight.enabled = false;
            }
            return;
        }
        
        LockTarget();
        //����Ǽ�������
        if (useLaser)
        {
            Laser();
        }
        else
        {
            Shoot();
        }

    }
    private void Laser()
    {
        //1.���˼�Ѫ
        enemyHp.Damage(overTimeHp * Time.deltaTime);
        //2.�����ٶȼ���
        enemyMove.Slow(slowPct);
        if(!lineRender.enabled)//��ǰ���ⱻ����
        {
            lineRender.enabled = true;//��������
            impactEffect.Play();//������Ч
            pointLight.enabled = true;
        }
        //���ü�����ʼλ��
        lineRender.SetPosition(0, bulletPoint.position);
        lineRender.SetPosition(1, target.position);

        //��Чλ�ú���ת
        Vector3 dir = bulletPoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * 1;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    private void Shoot()
    {
        //����ʱ�����ӵ�
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            //�����ӵ�
            Debug.Log("�����ӵ�");
            GameObject bulletGo = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            Bullet bullet = bulletGo.GetComponent<Bullet>();
            if (bullet == null)
            {
                bullet = bulletGo.AddComponent<Bullet>();
            }
            bullet.SetTarget(target);
            countDown = 1 / bulletRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpdateTarget()
    {
       GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemytag);
        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;
        foreach(var enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;

            }
        }
        if (minDistance < range)
        {
            target = nearestEnemy;
            enemyHp = target.GetComponent<EnemyHealth>();
            enemyMove = target.GetComponent<EnemyAI>();
        }
        else
        {
            target = null;
        }
    }
    private void LockTarget()//ʹ��������Ŀ��
    {
        Vector3 dir = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);//�ó���Ԫ��rotation
        Quaternion lerpRot = Quaternion.Lerp(partRotate.rotation, rotation, Time.deltaTime * rotSpeed);//��ֵ
        partRotate.rotation = Quaternion.Euler(new Vector3(0, lerpRot.eulerAngles.y, 0));//ŷ����
    }
}
