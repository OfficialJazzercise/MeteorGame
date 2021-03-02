using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public GameObject player;
    public GameObject muzzleRight;
    public GameObject muzzleLeft;
    public Transform projectileSpawn;
    public Canvas score;
    public Transform ship;

    private IEnumerator coroutine;

    public float rot = 0.0f;
    float shotDelay = 0.0f;
    public float distance = 100.0f;
    private float speed = 50.0f;
    private float turningSpeed = 1.0f;
    private float cacheHeight;

    bool isRight = true;

    public float height = 0.0f;

    Vector3 origin = Vector3.zero;


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
            rot = 0;
        }
        if (rot < 0)
        {
            rot = 360 + rot;
        }

        //Allows the player to go down if above -7 and go up if below 12
        if(Input.GetAxis("Vertical") < 0 && height >= -7)
        {
            height += Input.GetAxis("Vertical") * 20.0f * Time.deltaTime;
        }
        else if(Input.GetAxis("Vertical") > 0 && height <= 12)
        {
            height += Input.GetAxis("Vertical") * 20.0f * Time.deltaTime;
        }

        //Sets the player location and makes the player face the origin point
        player.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);
        player.transform.LookAt(origin);

        //handles shooting
        if (Input.GetKey(KeyCode.Space) && shotDelay <= 0)
        {
            shotDelay = 0.2f;
            singleShot();
        }

        shotDelay -= Time.deltaTime;

        //flips the player depending on which direction they are going
        if (Input.GetAxis("Horizontal") < 0)
        {
            isRight = false;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            isRight = true;
        }

        // Turns the player ship depending on direction
        if(isRight == false)
        {
           transform.Rotate(0, 180, 0);
        }

            //kills the player
            if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("SampleScene");
        }

        //quits the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        HeightRotation();

    }

    //Ship rotation code
    private void HeightRotation()
    {
        float angle = ship.localRotation.eulerAngles.x;

        if(cacheHeight - transform.position.y < 0)
        {
            angle -= Time.deltaTime * 5;
            angle = Mathf.Clamp(angle, -110, -90);
        }
        else if (cacheHeight - transform.position.y > 0)
        {
            angle += Time.deltaTime * 5;
            angle = Mathf.Clamp(angle, -90, -60);
        }
        else
        {
            angle = Mathf.Lerp(angle, -90, Time.deltaTime * 5);
        }

        ship.localRotation = Quaternion.Euler(angle, ship.localRotation.y, ship.localRotation.z);
        cacheHeight = transform.position.y;
    }


    //creates a shot
    void createShot(float heightChange)
    {
        float direction;

        //determines the direction and which muzzle to used based on weither the player is facing the right
        if (isRight)
        { 
            direction = 1;
            muzzleRight.SetActive(true);

            //coroutine used to Deactivate the muzzles after a while
            coroutine = WaitAndDisable(shotDelay);
            StartCoroutine(coroutine);
        }
        else
        {
            direction = -1;
            muzzleLeft.SetActive(true);

            //coroutine used to Deactivate the muzzles after a while
            coroutine = WaitAndDisable(shotDelay);
            StartCoroutine(coroutine);
        }

        //calls the firing script and activates a bullet
        GetComponent<Firing>().startBullet(rot, direction, height, speed + 50, heightChange, projectileSpawn, player.transform.rotation);
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
    private IEnumerator WaitAndDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        muzzleRight.SetActive(false);
        muzzleLeft.SetActive(false);
        Debug.Log("can fire");
    }

}
