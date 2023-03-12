using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;//��Ϸ�������
    public GameObject WinLevelUI;//��Ϸʤ�����
    public PauseUI pauseUI;//��Ϸ��ͣ
    public static bool GameIsOver;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        GameIsOver = false;
    }

    
    void Update()
    {
        //��Ϸ�Ѿ�����
        if (GameIsOver == true)
        {
            return;
        } 
        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseUI.SwitchUI();
            Debug.Log("��ͣ��Ϸ!");
        }
        if(PlayerStates.Lives <= 0)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        GameIsOver = true;
        Debug.Log("��Ϸʧ��");
        GameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        GameIsOver = true;
        WinLevelUI.SetActive(true);
    }
}
