using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;//游戏结束面板
    public GameObject WinLevelUI;//游戏胜利面板
    public PauseUI pauseUI;//游戏暂停
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
        //游戏已经结束
        if (GameIsOver == true)
        {
            return;
        } 
        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseUI.SwitchUI();
            Debug.Log("暂停游戏!");
        }
        if(PlayerStates.Lives <= 0)
        {
            GameEnd();
        }
    }

    private void GameEnd()
    {
        GameIsOver = true;
        Debug.Log("游戏失败");
        GameOverUI.SetActive(true);
    }

    public void GameWin()
    {
        GameIsOver = true;
        WinLevelUI.SetActive(true);
    }
}
