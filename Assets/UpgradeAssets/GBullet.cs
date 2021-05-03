using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GBullet : MonoBehaviour
{

    //delagate used to activate the BigBulletPowerUp in Firing
    public static Action BigBulletPowerUp = delegate { };

    private void OnEnable()
    {
        Spawner.resetArena += disableSelf;
        SPSpawner.resetArena += disableSelf;
    }

    private void OnDisable()
    {
        Spawner.resetArena -= disableSelf;
        SPSpawner.resetArena -= disableSelf;
    }

    void disableSelf()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        //checks if player collided with the powerup
        if(other.CompareTag("Player"))
        {
            pickUp(other);
        }

    }

    //Starts the delagate then hides the powerup
    void pickUp(Collider player)
    {  
        BigBulletPowerUp();

        FindObjectOfType<SoundManager>().Play("GetItem");//Finds SFX to play
        //sound effect needed


        gameObject.SetActive(false); ;
    }
}
