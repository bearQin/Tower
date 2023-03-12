using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    public SceneFader sceneFader;
    public GameObject ui;

    public void ContinueBtnClicked()
    {
        SwitchUI();
    }

    public void RetryBtnClicked()
    {
        SwitchUI();
        sceneFader.FadeOut(SceneManager.GetActiveScene().name);
    }

    public void MainMenuBtnClicked()   
    {
        SwitchUI();
        sceneFader.FadeOut("MainMenu");
    }

    public void SwitchUI()
    {
        ui.SetActive(!ui.activeSelf);
        if(ui.activeSelf)//ÔÝÍ£
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
