﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerLives : MonoBehaviour
{
    private int p1Health = 3;
    private int p2Health = 3;
    private bool Player1Turn = true;

    public Text P1Text;
    public Text P2Text;

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
        if(P1Text != null)
        {
            P1Text.text = "x " + p1Health.ToString("00");
        }
        if (P2Text != null)
        {
            P2Text.text = p2Health.ToString("00") + " x";
        }
    }

    private void hurtPlayer()
    {
        if (Player1Turn)
        {
            p1Health--;

            if (P1Text != null)
            {
                P1Text.text = "x " + p1Health.ToString("00");
            }

            Player1Turn = !Player1Turn;
        }
        else
        {
            p2Health--;

            if (P2Text != null)
            {
                P2Text.text = p2Health.ToString("00") + " x";
            }
            Player1Turn = !Player1Turn;
        }

        if (p1Health == 0 && p2Health == 0)
        {
            coroutine = WaitAndStop(1f);
            StartCoroutine(coroutine);
        }
    }

    private void giveLife()
    {
        if (Player1Turn)
        {
            p1Health++;

            if (P1Text != null)
            {
                P1Text.text = "x " + p1Health.ToString("00");
            }
        }
        else
        {
            p2Health++;
            if (P2Text != null)
            {
                P2Text.text = p2Health.ToString("00") + " x";
            }
        }
    }

    private IEnumerator WaitAndStop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        callFade(3);
    }
}
