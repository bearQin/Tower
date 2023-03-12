using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    public static int EnemyAlive;//记录场上存活的怪物的数量
    public Wave[] waveEnemy;
    public Transform spawnPoint;//生成位置
    public float spawnInterval = 1f;//敌人生成时间
    private float countDown;//倒计时
    private int waveIndex;

    public Text timerText;//倒计时组件
    void Start()
    {
        countDown = spawnInterval;
        EnemyAlive = 0;
    }


    void Update()
    { 
        if(GameManager.GameIsOver == true)
        {
            EnemyAlive = 0;
            return;
        } 
        if (EnemyAlive > 0)
        {
            return;
        }
        if(waveIndex == waveEnemy.Length)
        {
            Debug.Log("Victory");
            GameManager.Instance.GameWin();
            this.enabled = false;
        }

        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);
        string time = string.Format("倒计时:{0:00.00}", countDown);
        timerText.text = time;
        if (countDown <= 0)
        {
            countDown = spawnInterval;//倒计时结束生成敌人并重新计时
            SpawnEnemy();
        }

    }

    private void SpawnEnemy() 
    {

        StartCoroutine(WaveNum()); 
        
    }
    IEnumerator WaveNum()
    {

        if (waveIndex >= waveEnemy.Length)
        {
            yield break;

        }

        //回合数+1
        PlayerStates.Rounds++;
        //取出当前一波的数据
        Wave wave = waveEnemy[waveIndex];
        //记录存活的数量
        EnemyAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1/wave.rate);
        }
        waveIndex++;
    }
}
