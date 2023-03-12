using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家状态:
public class PlayerStates : MonoBehaviour
{
    public int StartMoney = 500;
    public static int CurrentMoney;
    public int StartLives = 3;//初始生命
    public static int Lives;//当前生命

    public static int Rounds;//回合数记录
    private void Awake()
    {
        CurrentMoney = StartMoney;
        Lives = StartLives;
        Rounds = 0;
    }
}
