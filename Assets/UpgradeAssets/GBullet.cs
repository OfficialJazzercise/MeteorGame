using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GBullet : MonoBehaviour
{


    public static Action BigBulletPowerUp = delegate { };

    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            pickUp(other);
        }

    }

    void pickUp(Collider player)
    {
        Debug.Log("Upgrade Successfully Activated");
  
        BigBulletPowerUp();
        Destroy(gameObject);
    }
}
