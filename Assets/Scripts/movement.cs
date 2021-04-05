using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public GameObject player;
    public GameObject muzzleRight;
    public GameObject muzzleLeft;
    public GameObject Explosion;
    public Transform projectileSpawn;

    private IEnumerator coroutine;

    public float rot = 0.0f;
    float shotDelay = 0.0f;
    public float distance = 100.0f;
    private float speed = 50.0f;
    private float cacheHeight;

    public bool isRight = true;

    public float height = 0.0f;

    private bool isFiring = false;
    private float horizontalMovement = 0;
    private float verticalMovement = 0;

    public Vector3 origin = Vector3.zero;
    Vector3 lookTowards = Vector3.zero;

    private void OnEnable()
    {
        LifeWillChange.CityDestroyed += shipDestroyed;
        SpaceRock.PlayerKilled += shipDestroyed;
        Enemy.PlayerKilled += shipDestroyed;
        EnemyProjectile.PlayerKilled += shipDestroyed;
        Spawner.restoreLife += shipRevived;
    }
    private void OnDisable()
    {
        LifeWillChange.CityDestroyed -= shipDestroyed;
        SpaceRock.PlayerKilled -= shipDestroyed;
        Enemy.PlayerKilled -= shipDestroyed;
        EnemyProjectile.PlayerKilled -= shipDestroyed;
        Spawner.restoreLife -= shipRevived;
    }

    void shipDestroyed()
    {
        Instantiate(Explosion, player.transform.position, Quaternion.identity);
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

            if (context.ReadValue<Vector2>().x < 0)
            {
                isRight = false;
            }
            else if (context.ReadValue<Vector2>().x > 0)
            {
                isRight = true;
            }
    }

    public void Boosting(InputAction.CallbackContext context)
    {
            if (context.performed)
            {
                speed = 100f;
            }
            else if (context.canceled)
            {
                speed = 50f;
            }
    }

    public void Pausing(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Application.Quit();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rot = player.transform.eulerAngles.y;
        cacheHeight = transform.position.y;

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

        if (verticalMovement < 0 && height >= -11)
        {
            FindObjectOfType<SoundManager>().Play("Thrusters");//SFX
            height += verticalMovement * 40.0f * Time.deltaTime;
        }
        else if (verticalMovement > 0 && height <= 70)
        {
            FindObjectOfType<SoundManager>().Play("Thrusters");//SFX
            height += verticalMovement * 40.0f * Time.deltaTime;
        }

        //Sets the player location and makes the player face the origin point
        player.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);

            lookTowards = new Vector3(origin.x, origin.y + height, origin.z);
            player.transform.LookAt(lookTowards);


        if (isFiring)
        {
            if (shotDelay <= 0)
            {
                FindObjectOfType<SoundManager>().Play("Laser1");//Will give variations later
                shotDelay = 0.2f;
                singleShot();
            }
        }

            shotDelay -= Time.deltaTime;

    }
    

    //creates a shot
    void createShot(float heightChange)
    {
        FindObjectOfType<SoundManager>().Play("Laser1");//SFX
        //determines the direction and which muzzle to used based on weither the player is facing the right

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
