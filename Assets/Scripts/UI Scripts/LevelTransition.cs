using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelTransition : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    private void OnEnable()
    {
        MainMenuManager.beginFade += FadeToLevel;
    }
    private void OnDisable()
    {
        MainMenuManager.beginFade -= FadeToLevel;
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
