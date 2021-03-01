using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform projectileSpawn;

    private IEnumerator coroutine;

    public float rot = 0.0f;
    public float distance = 100.0f;
    private float speed = 50.0f;

    public float height = 0.0f;

    Vector3 origin = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
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
    }


    //creates a shot
    void createShot(float heightChange)
    {
        //calls the firing script and activates a bullet
        GetComponent<EnemyTracking>().startBullet(rot, height, projectileSpawn);
    }

    //creates 3 shots
    void triShot()
    {
        createShot(1);
        createShot(0);
        createShot(-1);
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
