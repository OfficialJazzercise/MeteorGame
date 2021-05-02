using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SPPlayerLives : MonoBehaviour
{
    private int p1Health = 3;

    public Text P1Text;

    public static Action<int> callFade = delegate { };


    private IEnumerator coroutine;

    private void OnEnable()
    {
        LifeWillChange.CityDestroyed += hurtPlayer;
        SpaceRock.PlayerKilled += hurtPlayer;
        Enemy.PlayerKilled += hurtPlayer;
        EnemyProjectile.PlayerKilled += hurtPlayer;
        SPScore.GiveLife += giveLife;
    }
    private void OnDisable()
    {
        LifeWillChange.CityDestroyed -= hurtPlayer;
        SpaceRock.PlayerKilled -= hurtPlayer;
        Enemy.PlayerKilled -= hurtPlayer;
        EnemyProjectile.PlayerKilled -= hurtPlayer;
        SPScore.GiveLife -= giveLife;
    }
    private void Start()
    {
        if (P1Text != null)
        {
            P1Text.text = "x " + p1Health.ToString("00");
        }
    }

    private void hurtPlayer()
    {
            p1Health--;

            if (P1Text != null)
            {
                P1Text.text = "x " + p1Health.ToString("00");
            }

        if (p1Health <= 0)
        {
            coroutine = WaitAndStop(1f);
            StartCoroutine(coroutine);
        }
    }

    private void giveLife()
    {
            p1Health++;

            if (P1Text != null)
            {
                P1Text.text = "x " + p1Health.ToString("00");
            }
    }

    private IEnumerator WaitAndStop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        callFade(3);
    }
}
