using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject cameraF;
    float rot = 0.0f;
    public float distance = 10.0f;
    private float speed = 50.0f;

    Vector3 origin = Vector3.zero;


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

        rot -= Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        cameraF.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, 0, distance);
        cameraF.transform.LookAt(origin);
        cameraF.transform.position = new Vector3(cameraF.transform.position.x, 0, cameraF.transform.position.z);
    }
}