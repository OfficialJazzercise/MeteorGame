using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SPScore : MonoBehaviour
{
    public TextMeshProUGUI combo;
    private float comboMultiplier = 1;

    public Text P1ScoreText;
    private float P1Score = 0;
    private int P1LifeTarget = 10000;

    private float scoreMultiplier = 1;
    public float pointsGiven = 0;

    public static Action GiveLife = delegate { };
    public static Action<float, Vector3> flashText = delegate{};

    //when the IncreaseScore delgate is activated will call this Scripts increaseScore function
    private void OnEnable()
    {
        SpaceRock.IncreaseScore += IncreaseScore;
        Enemy.IncreaseScore += IncreaseScore;

        LifeWillChange.CityDestroyed += resetMultiplier;
        SpaceRock.PlayerKilled += resetMultiplier;
        Enemy.PlayerKilled += resetMultiplier;
        EnemyProjectile.PlayerKilled += resetMultiplier;

        SPSpawner.decreaseLife += resetMultiplier;
    }
    private void OnDisable()
    {
        SpaceRock.IncreaseScore -= IncreaseScore;
        Enemy.IncreaseScore -= IncreaseScore;

        LifeWillChange.CityDestroyed -= resetMultiplier;
        SpaceRock.PlayerKilled -= resetMultiplier;
        Enemy.PlayerKilled -= resetMultiplier;
        EnemyProjectile.PlayerKilled -= resetMultiplier;

        SPSpawner.decreaseLife -= resetMultiplier;
    }
    private void IncreaseScore(Vector3 Pos)
    {
        P1Score += 100 * scoreMultiplier;
        comboMultiplier += .1f;

        if (P1Score > P1LifeTarget)
        {
            GiveLife();
            P1LifeTarget += 10000;
        }

        pointsGiven = Mathf.Round(100 * scoreMultiplier);
        scoreMultiplier += .1f;
        flashText(pointsGiven, Pos);

        P1ScoreText.text = P1Score.ToString("000000000000");
        combo.text = comboMultiplier.ToString("0.00");
    }

    private void resetMultiplier()
    {
        scoreMultiplier = 1;
        comboMultiplier = 1;

        combo.text = comboMultiplier.ToString("0.00");
    }

    private void Start()
    {
        P1ScoreText.text = P1Score.ToString("000000000000");
        combo.text = comboMultiplier.ToString("0.00");
    }

}
