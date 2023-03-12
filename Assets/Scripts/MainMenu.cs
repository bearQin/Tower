using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

     public void BackBtnClicked()
    {
        sceneFader.FadeOut("SelectLevel");
    }

    public void QuitBtnClicked()
    {
        Application.Quit();
    }
}
