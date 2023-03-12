using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public string NextLevel;
    public int NextLevelId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevelBtnClicked()
    {
        //当前处于第几关
        PlayerPrefs.SetInt("CurrentLevel", NextLevelId);
        sceneFader.FadeOut(NextLevel);
    }

    public void MainMenuBtnClicked()
    {
        sceneFader.FadeOut("MainMenu");
    }
}
