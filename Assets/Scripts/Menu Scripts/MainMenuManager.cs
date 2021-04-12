using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuManager : MonoBehaviour
{
    public static Action<int> beginFade = delegate { };

    public void StartGame()
    {
        beginFade(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartGame()
    {
        beginFade(0);
    }

    public void ExitGame()
    {
        Debug.Log("App Quit");
        Application.Quit();
        
    }
}
