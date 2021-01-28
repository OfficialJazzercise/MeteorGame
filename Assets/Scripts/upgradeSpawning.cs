using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeSpawning : MonoBehaviour
{
    public float spawnDelay = 10.0f;
    public int spawnChance = 32;

    public GameObject GBullet;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnUpgrade", spawnDelay, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnUpgrade()
    {
        if(Random.Range(1, spawnChance) == 1)
        {
            Instantiate(GBullet, transform.position, Quaternion.identity);
        }
    }
}
