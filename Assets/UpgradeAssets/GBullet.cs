﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GBullet : MonoBehaviour
{

    //delagate used to activate the BigBulletPowerUp in Firing
    public static Action BigBulletPowerUp = delegate { };

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
        Debug.Log("Upgrade Successfully Activated");
  
        BigBulletPowerUp();
        gameObject.SetActive(false); ;
    }
}
