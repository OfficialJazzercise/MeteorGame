using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeSpawning : MonoBehaviour
{
    public float spawnDelay = 10.0f;
    public int spawnChance = 32;

    public GameObject prefab;

    public List<GBullet> spawnerList;
    public Vector3[] spawnLocations;


    private int index;

    // Start is called before the first frame update
    void Start()
    {
        //creates a list of Gbullet powerups
        GameObject clone;

        for (int i = 0; i < 8; i++)
        {
            clone = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone.SetActive(false);

            spawnerList.Add(clone.GetComponent<GBullet>());
        }

        //causes powerups to spawn in indefinitely
        InvokeRepeating("spawnUpgrade", spawnDelay, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void spawnUpgrade()
    {
        //checks to see if it should spawn the powerup, currently will always spawn
        if(Random.Range(1, spawnChance) == 1)
        {
            int index = Random.Range(0, 7);

                //checks to see if object is currently in use if it is ignore it
                if (spawnerList[index].gameObject.activeSelf)
                {

                }
                else
                {
                    //spawns the gBulletPowerup
                    spawnerList[index].gameObject.transform.position = spawnLocations[index];
                    spawnerList[index].gameObject.SetActive(true);
                    return;
                }         
        }
    }
}
