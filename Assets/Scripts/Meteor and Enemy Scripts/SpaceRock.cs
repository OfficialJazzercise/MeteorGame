﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;

public class SpaceRock : MonoBehaviour
{
    public float direction;
    public float distance;
    public float height;
    public float changeHeight;
    

    public int num;

    public float rot = 0.0f;
    public float speed = 100.0f;

    public bool canSplit = false;
    public bool isSplit = false;
    public bool canActivate = true;
    public bool isRight = true;

    public Vector3 lookTowards;



    //delegate used for the Score Script
    public static Action<Vector3> IncreaseScore = delegate { };
    public static Action MeteorDestroyed = delegate { };
    public static Action PlayerKilled = delegate { };
    public static Action<float, float, Vector3> rockBreak = delegate { }; //set Rotation on Circle, Height
    public static Action<Vector3> startExplosion = delegate { };

    public AudioSource playSound;
    int sfx = 0;
    private void OnEnable()
    {
        Spawner.resetArena += disableSelf;
        SPSpawner.resetArena += disableSelf;


        if(!canSplit)
        {
            gameObject.GetComponent<VisualEffect>().Stop();
        }
        else
        {
           gameObject.GetComponent<VisualEffect>().Play();
        }

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
        transform.Rotate(2, 0, 53 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        
        //If a bullet hits the meteor destroys the bullet and the meteor
        if (other.CompareTag("Bullet"))
        {
            if(canSplit)rockBreak(rot, height, this.gameObject.transform.position);
            other.gameObject.SetActive(false);
            IncreaseScore(gameObject.transform.position);
            startExplosion(this.gameObject.transform.position);
            MeteorDestroyed();
            ScreenShake.instance.StartShake(.4f, .8f); //Shakes screen upon destroying meteor
            canSplit = false;

            
            switch(sfx)
            {
                case 0:
                    FindObjectOfType<SoundManager>().Play("Boom");//Finds SFX to play
                    break;

                case 1:
                    FindObjectOfType<SoundManager>().Play("Boom2");//Finds SFX to play
                    sfx = 0;
                    break;
                default:
                    break;
            }
            sfx++;

            if (isSplit)
            {
              isSplit = false;
            }

            gameObject.SetActive(false);

        }

        //If the player hits the meteor destroys the player
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().Play("Dying");
            PlayerKilled();

        }


    }
}
