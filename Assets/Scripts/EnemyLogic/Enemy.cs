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
    public float desiredHeight = 0.0f;
    public bool canActivate = true;

    public bool canMove = true;
    private bool canShoot = false;
    public string enemyType = "none";

    Vector3 origin = Vector3.zero;
    Vector3 lookTowards = Vector3.zero;

    public static Action<Vector3> IncreaseScore = delegate { };
    public static Action EnemyDestroyed = delegate { };
    public static Action PlayerKilled = delegate { };
    public static Action<float, float, Transform> shootBullet = delegate { };
    public static Action<Vector3> startExplosion = delegate { };

    private void OnEnable()
    {
        Spawner.resetArena += disableSelf;
        SPSpawner.resetArena += disableSelf;
        startBlasting();
    }
    private void OnDisable()
    {
        Spawner.resetArena -= disableSelf;
        SPSpawner.resetArena -= disableSelf;
    }

    private void disableSelf()
    {
        canActivate = true;
        canMove = false;
        canShoot = false;
        gameObject.SetActive(false);
    }

    private void startBlasting()
    {
        coroutine = WaitAndFire(1f);
        StartCoroutine(coroutine);
    }

    void OnTriggerEnter(Collider other)
    {

        //If a bullet hits the meteor destroys the bullet and the meteor
        if (other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            rot = 0;
            height = 10;
            IncreaseScore(gameObject.transform.position);
            FindObjectOfType<SoundManager>().Play("Boom");//Finds SFX to play
            ScreenShake.instance.StartShake(.4f, .8f); //Shakes screen upon destroying meteor
            canMove = false;
            canShoot = false;
            EnemyDestroyed();
            startExplosion(gameObject.transform.position);
            canActivate = true;
            gameObject.SetActive(false);
        }

        //If the player hits the meteor destroys the player
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().Play("Dying");
            PlayerKilled();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        //keeps rot between 0 and 360
        if (rot >= 360)
        {
            rot = 0;
        }
        if (rot < 0)
        {
            rot = 360 + rot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Sets the players postion on a circular track based on which direction the are traveling, speed, and time
        if (canMove)
        {
            distanceToPlayer = Math.Abs(target.GetComponent<movement>().rot - rot);

            if (distanceToPlayer <= 50)
            {
                rot -= speed * Time.deltaTime / 5;
            }
            else
            {
                rot -= speed * Time.deltaTime;
            }

            //keeps rot between 0 and 360
            if (rot >= 360)
            {
                rot = rot - 360;
            }
            if (rot < 0)
            {
                rot = 360 + rot;
            }

        }
        else
        {
            if (enemyType == "flying")
            {
                if (height > desiredHeight)
                {
                    height -= speed * Time.deltaTime / 3;

                    if (height <= desiredHeight)
                    {
                        canMove = true;
                        canShoot = true;
                    }
                }

            }
            else if (enemyType == "grounded")
            {
                if (height > desiredHeight)
                {
                    height -= speed * Time.deltaTime / 3;

                    if (height <= desiredHeight)
                    {
                        canShoot = true;
                    }
                }
            }
        }

        //Sets the player location and makes the player face the origin point
        enemy.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);

        lookTowards = new Vector3(origin.x, origin.y + height, origin.z);
        enemy.transform.LookAt(lookTowards);
    }


    //creates a shot
    void createShot()
    {
        //calls the firing script and activates a bullet
        
        shootBullet(rot, height, projectileSpawn);
    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator WaitAndFire(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (canShoot)
        {
            
            createShot();
        }


        coroutine = WaitAndFire(1f);
        StartCoroutine(coroutine);
    }

}
