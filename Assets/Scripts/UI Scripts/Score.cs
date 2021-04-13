using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI combo;
    private float comboMultiplier = 1;

    public Text P1ScoreText;
    private float P1Score = 0;
    private int P1LifeTarget = 10000;

    public Text P2ScoreText;
    private float P2Score = 0;
    private int P2LifeTarget = 10000;

    private float scoreMultiplier = 1;
    public float pointsGiven = 0;

    private bool player1Turn = true;

    public static Action GiveLife = delegate { };

    //when the IncreaseScore delgate is activated will call this Scripts increaseScore function
    private void OnEnable()
    {
        SpaceRock.IncreaseScore += IncreaseScore;
        Enemy.IncreaseScore += IncreaseScore;

        LifeWillChange.CityDestroyed += changePlayer;
        SpaceRock.PlayerKilled += changePlayer;
        Enemy.PlayerKilled += changePlayer;
        EnemyProjectile.PlayerKilled += changePlayer;

        Spawner.decreaseLife += resetMultiplier;
    }
    private void OnDisable()
    {
        SpaceRock.IncreaseScore -= IncreaseScore;
        Enemy.IncreaseScore -= IncreaseScore;

        LifeWillChange.CityDestroyed -= changePlayer;
        SpaceRock.PlayerKilled -= changePlayer;
        Enemy.PlayerKilled -= changePlayer;
        EnemyProjectile.PlayerKilled -= changePlayer;

        Spawner.decreaseLife -= resetMultiplier;
    }
    private void IncreaseScore()
    {
        if (player1Turn)
        {
            P1Score += 100 * scoreMultiplier;
            comboMultiplier += .1f;
        }
        else
        {
            P2Score += 100 * scoreMultiplier;
            comboMultiplier += .1f;
        }

        if (P1Score > P1LifeTarget)
        {
            GiveLife();
            P1LifeTarget += 10000;
        }

        
        if(P2Score > P2LifeTarget)
        {
            GiveLife();
            P2LifeTarget += 10000;
        }

        scoreMultiplier += .1f;
        pointsGiven = 100 * scoreMultiplier;
    }

    private void resetMultiplier()
    {
        scoreMultiplier = 1;
        comboMultiplier = 1;
    }

    private void changePlayer()
    {
        player1Turn = !player1Turn;
        resetMultiplier();
    }

    // Update is called once per frame
    void Update()
    {
        //prints out the currentscore and rounds it to nearest 0
        P1ScoreText.text = P1Score.ToString("000000000000");
        P2ScoreText.text = P2Score.ToString("000000000000");
        combo.text = comboMultiplier.ToString("0.00");

    }

    private void Start()
    {
        P1ScoreText.text = P1Score.ToString("000000000000");
        P2ScoreText.text = P2Score.ToString("000000000000");
        combo.text = comboMultiplier.ToString("0.00");
    }
}
