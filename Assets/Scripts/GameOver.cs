using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public Text roundsText;
    private void OnEnable()
    {
        StartCoroutine(ShowRoundsText());
    }

    public void RetryBtn()
    {
        Debug.Log("����");
        sceneFader.FadeOut(SceneManager.GetActiveScene().name);
    }
    
    public void MenuBtn()
    {
        Debug.Log("�˵�");
        sceneFader.FadeOut("MainMenu");
    }

    IEnumerator ShowRoundsText()
    {
        int rounds = 0;//��¼��ǰֵ
        roundsText.text = rounds.ToString();
        yield return new WaitForSeconds(0.5f);
        while(rounds < PlayerStates.Rounds)
        {
            rounds++;
            roundsText.text = rounds.ToString();
            yield return new WaitForSeconds(0.07f);
        }
    }
}
