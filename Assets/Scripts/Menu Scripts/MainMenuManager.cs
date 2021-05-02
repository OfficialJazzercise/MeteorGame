using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuManager : MonoBehaviour
{
    public static Action<int> beginFade = delegate { };

    public void Singleplayer()
    {
        beginFade(SceneManager.GetActiveScene().buildIndex + 2);
        //SceneManager.LoadScene(2);
    }

    public void Multiplayer()
    {
        beginFade(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene(1);
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

    public void CreditsToMainMenu()
    {
        beginFade(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void HSToMainMenu()
    {
        beginFade(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void HighScores()
    {
        beginFade(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void Credits()
    {
        beginFade(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void itsaSecret()
    {
        FindObjectOfType<SoundManager>().Play("RileyButton");//Finds SFX to play
    }
}
