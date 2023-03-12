using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public SceneFader sceneFader;
    public Button[] levels; 

    void Start()
    {
        int current = PlayerPrefs.GetInt("CurrentLevel",1);
        for(int i  = 0; i < levels.Length; i++)
        {
            if(i+1 <= current)
            {
                levels[i].interactable = true;
            }
            else
            {
                levels[i].interactable = false;
            }
        }
    }

  
    void Update()
    {
        
    }

    public void LevelSelect(string name)
    {
        sceneFader.FadeOut(name);
    }
}
