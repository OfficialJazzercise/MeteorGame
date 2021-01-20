using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float endLife;
    public float direction;
    public float distance;
    public float height;

    public float rot = 0.0f;
    public float speed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        speed = 200.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
