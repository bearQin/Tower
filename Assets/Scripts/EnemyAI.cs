using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float startSpeed = 10;
    private float moveSpeed = 10;
    Transform target;
    private int pointIndex = 0;
    void Start()
    {
        target = PathPoints.pathPoints[pointIndex];
        moveSpeed = startSpeed;
    }

    
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        if (Vector3.Distance(target.position, transform.position) < 0.2f)
        {
            pointIndex++;
            //�����յ�
            if(pointIndex >= PathPoints.pathPoints.Length)
            {
                PathEnd();
                return;
            }
            target = PathPoints.pathPoints[pointIndex];
        }
        //�����ٶ�
        moveSpeed = startSpeed;
    }
    private void PathEnd()
    {
        if(PlayerStates.Lives > 0)
        {
            PlayerStates.Lives--;
        }
        EnemySpawner.EnemyAlive--;
        Destroy(gameObject);
    }

    //����
    public void Slow(float pct)
    {
        moveSpeed = startSpeed * (1 - pct);
    }
}
