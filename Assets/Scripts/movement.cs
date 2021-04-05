using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Start is called before the first frame update
    void Start()
    {
        rot = player.transform.eulerAngles.y;
        cacheHeight = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        //handles dash
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 100f;
        }
        else
        {
            speed = 50f;
        }

            //Sets the players postion on a circular track based on which direction the are traveling, speed, and time
            rot -= Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        //keeps rot between 0 and 360
            if (rot >= 360)
            {
                rot = rot - 360;
            }
            if (rot < 0)
            {
                rot = 360 + rot;
            }


            //Allows the player to go down if above -7 and go up if below 12
            if (Input.GetAxis("Vertical") < 0 && height >= -11)
            {
                FindObjectOfType<SoundManager>().Play("Thrusters");//SFX
                height += Input.GetAxis("Vertical") * 40.0f * Time.deltaTime;
            }
            else if (Input.GetAxis("Vertical") > 0 && height <= 70)
            {
                height += Input.GetAxis("Vertical") * 40.0f * Time.deltaTime;
            }

            //Sets the player location and makes the player face the origin point
            player.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);

            lookTowards = new Vector3(origin.x, origin.y + height, origin.z);
            player.transform.LookAt(lookTowards);

            //handles shooting
            if (Input.GetKey(KeyCode.Space) && shotDelay <= 0)
            {
                FindObjectOfType<SoundManager>().Play("Laser1");//Will give variations later
                shotDelay = 0.2f;
                singleShot();
            }

            shotDelay -= Time.deltaTime;

            //flips the player depending on which direction they are going
            if (Input.GetAxis("Horizontal") < 0)
            {
                FindObjectOfType<SoundManager>().Play("Thrusters");//SFX
                isRight = false;
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                FindObjectOfType<SoundManager>().Play("Thrusters");//SFX
                isRight = true;
            }

        //quits the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

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
