using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameObject player;
    float rot = 0.0f;
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
        player.transform.position = origin + Quaternion.Euler(0, rot, 0) * new Vector3(0, 0, distance);
        player.transform.LookAt(origin);
        player.transform.position = new Vector3(player.transform.position.x, height, player.transform.position.z);
    }
}
