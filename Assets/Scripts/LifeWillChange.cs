using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LifeWillChange : MonoBehaviour
{
    private float cityLife = 5;
    public Text lifeText;

    private void OnEnable()
    {
        Spawner.decreaseLife += minusLife;
    }
    private void OnDisable()
    {
        Spawner.decreaseLife -= minusLife;
    }

    void minusLife()
    {
        cityLife--;
        lifeText.text = cityLife.ToString("0");

        if(cityLife <= 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
