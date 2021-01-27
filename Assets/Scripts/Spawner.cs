using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float rate = 3f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 2f, rate);
    }


    void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}