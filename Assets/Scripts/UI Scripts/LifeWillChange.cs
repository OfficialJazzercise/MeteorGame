using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LifeWillChange : MonoBehaviour
{
    private int cityLife = 5;
    public Sprite[] lifeImages;
    public Image lifeSwap;
    private AudioSource siren;

    public static Action CityDestroyed = delegate { };

    void Start()
    {
        siren = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Spawner.decreaseLife += minusLife;
        Spawner.resetArena += resetCity;


        SPSpawner.decreaseLife += minusLife;
        SPSpawner.resetArena += resetCity;
    }
    private void OnDisable()
    {
        Spawner.decreaseLife -= minusLife;
        Spawner.resetArena -= resetCity;

        SPSpawner.decreaseLife -= minusLife;
        SPSpawner.resetArena -= resetCity;
    }

    void minusLife()
    {
        cityLife--;
        
        if (cityLife <= 0)
        {
            CityDestroyed();
        }

        if (cityLife != 0)
        {
            lifeSwap.sprite = lifeImages[cityLife - 1];
        }

        if(cityLife == 1)
        {
            siren.Play();   
        }
    }

    void resetCity()
    {
        cityLife = 5;
        lifeSwap.sprite = lifeImages[4];
        siren.Stop();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
