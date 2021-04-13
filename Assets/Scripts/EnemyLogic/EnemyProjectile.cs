using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyProjectile : MonoBehaviour
{   public float distance;
    public float height;
    public float targetsHeight;
    public float targetsRot;
    public bool canActivate = true;

    public float rot = 0.0f;
    public float speed = 100.0f;

    public TrailRenderer bulletTrail;

    public static Action PlayerKilled = delegate { };

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

    private void disableSelf()
    {
        gameObject.SetActive(false);
        canActivate = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
       //If the player hits the meteor destroys the player
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().Play("Dying");
            PlayerKilled();
        }
    }

    //clears the trail on the bullet
    public void resetTrail()
    {
        bulletTrail.Clear();
    }

}
