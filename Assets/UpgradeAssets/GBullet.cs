using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GBullet : MonoBehaviour
{
    private upgradeSpawning isOccupied;

    public static Action BigBulletPowerUp = delegate { };
    void OnTriggerEnter(Collider other)
    {
        isOccupied = GetComponent<upgradeSpawning>();
        isOccupied.occupied = false;

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
