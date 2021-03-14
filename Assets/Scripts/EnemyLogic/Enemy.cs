using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public Transform projectileSpawn;
    public GameObject enemy;
    private GameObject target;

    private float distanceToPlayer;
    private IEnumerator coroutine;

    public float rot = 0.0f;
    public float distance = 100.0f;
    private float speed = 50.0f;

    public float height = 0.0f;

    Vector3 origin = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        rot = projectileSpawn.eulerAngles.y;
        //keeps rot between 0 and 360
        if (rot >= 360)
        {
            rot = 0;
        }
        if (rot < 0)
        {
            rot = 360 + rot;
        }

        height = gameObject.transform.position.y;

        coroutine = WaitAndFire(1f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the players postion on a circular track based on which direction the are traveling, speed, and time

        distanceToPlayer = Math.Abs(target.GetComponent<movement>().rot - rot);

        if (distanceToPlayer <= 50)
        {
            rot -= (1 * speed * Time.deltaTime)/2;
        }
        else
        {
            rot -= 1 * speed * Time.deltaTime;
        }

        //keeps rot between 0 and 360
        if (rot >= 360)
        {
            rot = 0;
        }
        if (rot < 0)
        {
            rot = 360 + rot;
        }

        //Sets the player location and makes the player face the origin point
        enemy.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);
        enemy.transform.LookAt(origin);
    }


    //creates a shot
    void createShot(float heightChange)
    {
        //calls the firing script and activates a bullet
        GetComponent<EnemyTracking>().startBullet(rot, height, projectileSpawn);
    }

    //creates a shot
    void singleShot()
    {
        createShot(0);
    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator WaitAndFire(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        singleShot();
        coroutine = WaitAndFire(4f);
        StartCoroutine(coroutine);
    }

}
