using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���״̬:
public class PlayerStates : MonoBehaviour
{
    public int StartMoney = 500;
    public static int CurrentMoney;
    public int StartLives = 3;//��ʼ����
    public static int Lives;//��ǰ����

    public static int Rounds;//�غ�����¼
    private void Awake()
    {
        CurrentMoney = StartMoney;
        Lives = StartLives;
        Rounds = 0;
    }
}
