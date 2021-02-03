using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public Canvas score;

    float rot = 0.0f;
    float shotDelay = 0.0f;
    public float distance = 100.0f;
    float speed = 50.0f;

    bool isRight = true;

    private float height = 0.0f;

    Vector3 origin = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        rot = player.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {

        rot -= Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        height += Input.GetAxis("Vertical") * 10.0f * Time.deltaTime;
        player.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);
        player.transform.LookAt(origin);

        if (Input.GetButton("Jump") && shotDelay <= 0)
        {
            shotDelay = 0.5f;
            singleShot();
        }
        shotDelay -= Time.deltaTime;

        if (Input.GetAxis("Horizontal") < 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
            isRight = false;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
            isRight = true;
        }


    }



    void createShot(float heightChange)
    {
        float direction;

        if (isRight)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        player.GetComponent<Firing>().startBullet(rot, direction, height, 100, heightChange, player.transform, distance);
    }

    void triShot()
    {
        createShot(1);
        createShot(0);
        createShot(-1);
    }

    void singleShot()
    {
        createShot(0);
    }
}
