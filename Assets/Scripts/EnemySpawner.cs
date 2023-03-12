using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemySpawner : MonoBehaviour
{
    public static int EnemyAlive;//��¼���ϴ��Ĺ��������
    public Wave[] waveEnemy;
    public Transform spawnPoint;//����λ��
    public float spawnInterval = 1f;//��������ʱ��
    private float countDown;//����ʱ
    private int waveIndex;

    public Text timerText;//����ʱ���
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
        string time = string.Format("����ʱ:{0:00.00}", countDown);
        timerText.text = time;
        if (countDown <= 0)
        {
            countDown = spawnInterval;//����ʱ�������ɵ��˲����¼�ʱ
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

        //�غ���+1
        PlayerStates.Rounds++;
        //ȡ����ǰһ��������
        Wave wave = waveEnemy[waveIndex];
        //��¼��������
        EnemyAlive = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(1/wave.rate);
        }
        waveIndex++;
    }
}
