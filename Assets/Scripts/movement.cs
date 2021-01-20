using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;

    float rot = 0.0f;
    float shotDelay = 0.0f;
    public float distance = 10.0f;
    float speed = 100.0f;

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
        height += Input.GetAxis("Vertical") * 5.0f * Time.deltaTime;
        player.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, height, distance);
        player.transform.LookAt(origin);

        if (Input.GetButton("Jump") && shotDelay <= 0)
        {
            shotDelay = 0.2f;
            singleShot();
        }
        shotDelay -= Time.deltaTime;
    }

    void createShot(float heightChange)
    {
        GameObject clone;
        clone = Instantiate(bullet, player.transform.position, player.transform.rotation);

        clone.GetComponent<Bullet>().rot = rot;
        clone.GetComponent<Bullet>().distance = distance;
        clone.GetComponent<Bullet>().height = height;
        clone.GetComponent<Bullet>().endLife = 2;
        clone.GetComponent<Bullet>().speed = 200;
        clone.GetComponent<Bullet>().changeHeight = heightChange;

        if (Input.GetAxis("Horizontal") < 0)
        {
            clone.GetComponent<Bullet>().direction = -1;
            player.GetComponent<Firing>().bulletList.Add(clone);
        }
        else
        {
            clone.GetComponent<Bullet>().direction = 1;
            player.GetComponent<Firing>().bulletList.Add(clone);
        }
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
