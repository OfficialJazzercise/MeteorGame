using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpaceRock : MonoBehaviour
{
    public float endLife;
    public float direction;
    public float distance;
    public float height;
    public float changeHeight;

    public float rot = 0.0f;
    public float speed = 100.0f;

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
       

        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Meteor Down!");
           
            rot = 0;
            height = 25;
            IncreaseScore();
            gameObject.SetActive(false);

            //playSound.Play();

        }
        if(other.CompareTag("Player"))
            {
            other.gameObject.SetActive(false);
        }
    }
}