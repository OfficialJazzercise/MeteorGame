using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SpaceRock : MonoBehaviour
{
    public float endLife;
    public float direction;
    public float distance;
    public float height;
    public float changeHeight;

    public float rot = 0.0f;
    public float speed = 100.0f;

    //delegate used for the Score Script
    public static Action IncreaseScore = delegate { };

    public AudioSource playSound;

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
       
        //If a bullet hits the meteor destroys the bullet and the meteor
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Meteor Down!");

            other.gameObject.SetActive(false);
            rot = 0;
            height = 25;
            IncreaseScore();
            gameObject.SetActive(false);

            //playSound.Play();

        }

        //If the player hits the meteor destroys the player
        if(other.CompareTag("Player"))
            {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");
        }
    }
}