using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject cameraF;
    public float rot = 0.0f;
    public float height = 0.0f;

    public float distance = 10.0f;
    private float speed = 50.0f;
    private bool canMove = true;

    public movement player;

    Vector3 origin = Vector3.zero;

    private void OnEnable()
    {
        LifeWillChange.CityDestroyed += switchMovement;
        SpaceRock.PlayerKilled += switchMovement;
        Enemy.PlayerKilled += switchMovement;
        EnemyProjectile.PlayerKilled += switchMovement;
        Spawner.restoreLife += switchMovement;
    }
    private void OnDisable()
    {
        LifeWillChange.CityDestroyed -= switchMovement;
        SpaceRock.PlayerKilled -= switchMovement;
        Enemy.PlayerKilled -= switchMovement;
        EnemyProjectile.PlayerKilled -= switchMovement;
        Spawner.restoreLife -= switchMovement;
    }

    void switchMovement() { canMove = !canMove; }

    // Start is called before the first frame update
    void Start()
    {
        rot = cameraF.transform.eulerAngles.y;
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

        if (canMove)
        {

            if (Input.GetAxis("Vertical") < 0 && height >= -7 && player.height <= height - 5)
            {
                FindObjectOfType<SoundManager>().Play("Thrusters");//SFX
                height += Input.GetAxis("Vertical") * 40.0f * Time.deltaTime;
            }
            else if (Input.GetAxis("Vertical") > 0 && height <= 60 && player.height >= height + 5)
            {
                height += Input.GetAxis("Vertical") * 40.0f * Time.deltaTime;
            }

            rot -= Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            cameraF.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, 0, distance);
            cameraF.transform.LookAt(origin);
            cameraF.transform.position = new Vector3(cameraF.transform.position.x, height, cameraF.transform.position.z);
        }
    }
}