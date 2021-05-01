using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class cameraFollow : MonoBehaviour
{
    public GameObject cameraF;
    public float rot = 0.0f;
    public float height = 0.0f;

    public float distance = 10.0f;
    private float speed = 100.0f;
    private float vSpeed = 60.0f;
    private bool canMove = true;

    private float horizontalMovement = 0;
    private float verticalMovement = 0;

    public movement player;

    Vector3 origin = Vector3.zero;

    private void OnEnable()
    {
        LifeWillChange.CityDestroyed += switchMovement;
        SpaceRock.PlayerKilled += switchMovement;
        Enemy.PlayerKilled += switchMovement;
        EnemyProjectile.PlayerKilled += switchMovement;

        Spawner.restoreLife += switchMovement;
        SPSpawner.restoreLife += switchMovement;
    }
    private void OnDisable()
    {
        LifeWillChange.CityDestroyed -= switchMovement;
        SpaceRock.PlayerKilled -= switchMovement;
        Enemy.PlayerKilled -= switchMovement;
        EnemyProjectile.PlayerKilled -= switchMovement;

        Spawner.restoreLife -= switchMovement;
        SPSpawner.restoreLife -= switchMovement;
    }

    void switchMovement() { canMove = !canMove; }

    public void Moving(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        verticalMovement = context.ReadValue<Vector2>().y;
    }

    public void Boosting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            speed = 40f;
            vSpeed = 30f;
        }
        else if (context.canceled)
        {
            speed = 100f;
            vSpeed = 60f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rot = cameraF.transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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

                }
                else
                {
                    height = -11;
                }
            }
            else if (verticalMovement > 0)
            {
                height += verticalMovement * vSpeed * Time.deltaTime;

                if (height <= 68)
                {
                }
                else
                {
                    height = 68;
                }
            }

            cameraF.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, 0, distance);
            cameraF.transform.LookAt(origin);
            cameraF.transform.position = new Vector3(cameraF.transform.position.x, height, cameraF.transform.position.z);
        }
    }
    
}