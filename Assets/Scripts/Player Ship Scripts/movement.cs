using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public GameObject player;
    public GameObject muzzleRight;
    public GameObject muzzleLeft;
    public Transform projectileSpawn;
    public GameObject IdleThruster;
    public GameObject MovingThruster;

    private IEnumerator coroutine;

    public float rot = 0.0f;
    float shotDelay = 0.0f;
    public float distance = 100.0f;
    private float speed = 100.0f;
    private float vSpeed = 60.0f;
    private float cacheHeight;

    public bool isRight = true;

    public float height = 0.0f;

    private bool isFiring = false;

    public bool isBoosting = false;
    public float horizontalMovement = 0;
    public float verticalMovement = 0;

    public bool topMap = false;
    public bool bottomMap = false;

    public Vector3 origin = Vector3.zero;
    Vector3 lookTowards = Vector3.zero;

    public static Action<Vector3> startExplosion = delegate { };

    private void OnEnable()
    {
        LifeWillChange.CityDestroyed += shipDestroyed;
        SpaceRock.PlayerKilled += shipDestroyed;
        Enemy.PlayerKilled += shipDestroyed;
        EnemyProjectile.PlayerKilled += shipDestroyed;
        Spawner.restoreLife += shipRevived;
        SPSpawner.restoreLife += shipRevived;
    }
    private void OnDisable()
    {
        LifeWillChange.CityDestroyed -= shipDestroyed;
        SpaceRock.PlayerKilled -= shipDestroyed;
        Enemy.PlayerKilled -= shipDestroyed;
        EnemyProjectile.PlayerKilled -= shipDestroyed;
        Spawner.restoreLife -= shipRevived;
        SPSpawner.restoreLife -= shipRevived;
    }

    void shipDestroyed()
    {
        startExplosion(player.transform.position);
        player.SetActive(false);
    }

    void shipRevived()
    {
        player.SetActive(true);

    }

    public void Shooting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isFiring = true;
        }
        else if (context.canceled)
        {
            isFiring = false;
        }
    }

    public void Moving(InputAction.CallbackContext context)
    {
            horizontalMovement = context.ReadValue<Vector2>().x;
            verticalMovement = context.ReadValue<Vector2>().y;

        if (horizontalMovement != 0 && context.started)
        {
            IdleThruster.SetActive(false);
            MovingThruster.SetActive(true);
        }
        else if(horizontalMovement == 0)
        {
            IdleThruster.SetActive(true);
            MovingThruster.SetActive(false);
            
        }

        if (context.ReadValue<Vector2>().x < 0)
        {
                isRight = false;
        }
        else if (context.ReadValue<Vector2>().x > 0)
        {
                isRight = true;
        }

        if(context.started)
        {
            gameObject.GetComponent<AudioSource>().Play();//SFX
        }
        else if (context.canceled)
        {
            gameObject.GetComponent<AudioSource>().Stop();//SFX
        }
    }

    public void Boosting(InputAction.CallbackContext context)
    {
            if (context.performed)
            {
                speed = 40f;
                vSpeed = 30f;
                isBoosting = true;
                gameObject.GetComponent<AudioSource>().volume = .3f;
            }
            else if (context.canceled)
            {
                speed = 100f;
                vSpeed = 60f;
                isBoosting = false;
                gameObject.GetComponent<AudioSource>().volume = .5f;
            }
    }

    public void Pausing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rot = player.transform.eulerAngles.y;
        cacheHeight = transform.position.y;

        FindObjectOfType<SoundManager>().Play("SpawnIn");

    }

    // Update is called once per frame
     void Update()
    {

        rot -= horizontalMovement * speed * Time.deltaTime;

        if (rot >= 360)
        {
            rot = rot - 360;
        }
        if (rot < 0)
        {
            rot = 360 + rot;
        }

        if (verticalMovement < 0)
        {
            height += verticalMovement * vSpeed * Time.deltaTime;

            if (height >= -11)
            {
                bottomMap = false;
                topMap = false;
            }
            else
            {
                height = -11;
                bottomMap = true;
            }
        }
        else if (verticalMovement > 0)
        {
            height += verticalMovement * vSpeed * Time.deltaTime;

            if (height <= 68)
            {
                bottomMap = false;
                topMap = false;
            }
            else
            {
                height = 68;
                topMap = true;
            }
        }

        //Sets the player location and makes the player face the origin point
        player.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);

            lookTowards = new Vector3(origin.x, origin.y + height, origin.z);
            player.transform.LookAt(lookTowards);


        if (isFiring)
        {
            if (shotDelay <= 0)
            {
                //i took out the thing for sfx bullets
                shotDelay = 0.15f;
                singleShot();
            }
        }

            shotDelay -= Time.deltaTime;

    }
    

    //creates a shot
    void createShot(float heightChange)
    {
        //calls the firing script and activates a bullet
        GetComponent<Firing>().startBullet(projectileSpawn, player.transform.rotation);
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


}
