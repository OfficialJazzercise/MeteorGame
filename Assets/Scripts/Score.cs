using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{

    public Text scoreText;
    public float currentScore = 0;

    private void OnEnable()
    {
        SpaceRock.IncreaseScore += IncreaseScore;
    }
    private void OnDisable()
    {
        SpaceRock.IncreaseScore -= IncreaseScore;
    }
    private void IncreaseScore() { currentScore += 100; }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString("0");
    }
}
