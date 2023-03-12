using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{   
    [Header("设置")]
    public float range = 5;
    public string enemytag = "Enemy";
    public Transform target;//攻击目标
    public Transform partRotate;//旋转的炮头
    public Transform bulletPoint;//子弹生成位置
    public float rotSpeed = 10;

    [Header("使用子弹(默认")]
    public GameObject bulletPrefab;//子弹预制体
    public float bulletRate = 2f;//发射子弹的速率
    private float countDown = 0;

    [Header("是否使用激光")]

    public bool useLaser;//判断是否使用激光
    public LineRenderer lineRender;//画线
    public float overTimeHp = 15;//随时间减血
    public float slowPct = 0.3f;//减速百分比
    private EnemyAI enemyMove;//敌人减速
    private EnemyHealth enemyHp;//敌人减血
    public ParticleSystem impactEffect;//激光特效
    public Light pointLight;//灯光

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
                lineRender.enabled = false;//画线取消
                impactEffect.Stop();//停止播放特效
                pointLight.enabled = false;
            }
            return;
        }
        
        LockTarget();
        //如果是激光炮塔
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
        //1.敌人见血
        enemyHp.Damage(overTimeHp * Time.deltaTime);
        //2.敌人速度减慢
        enemyMove.Slow(slowPct);
        if(!lineRender.enabled)//当前激光被禁用
        {
            lineRender.enabled = true;//重新启用
            impactEffect.Play();//播放特效
            pointLight.enabled = true;
        }
        //设置激光起始位置
        lineRender.SetPosition(0, bulletPoint.position);
        lineRender.SetPosition(1, target.position);

        //特效位置和旋转
        Vector3 dir = bulletPoint.position - target.position;
        impactEffect.transform.position = target.position + dir.normalized * 1;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    private void Shoot()
    {
        //倒计时发射子弹
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            //发射子弹
            Debug.Log("发射子弹");
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
    private void LockTarget()//使炮塔锁定目标
    {
        Vector3 dir = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);//得出四元数rotation
        Quaternion lerpRot = Quaternion.Lerp(partRotate.rotation, rotation, Time.deltaTime * rotSpeed);//插值
        partRotate.rotation = Quaternion.Euler(new Vector3(0, lerpRot.eulerAngles.y, 0));//欧拉角
    }
}
