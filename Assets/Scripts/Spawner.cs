using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Spawner : MonoBehaviour
{
    public List<SpaceRock> meteorList;
    public GameObject prefab;
    public Transform spawnArea;
    

    public float rate = 3f;
    public float spawnRate = 5;
    public float spawnValueReset = 5; //a value for resetting spawn timer. Must be the same as spawnRate

    public static Action decreaseLife = delegate { };

    Vector3 origin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //creats a meteorList and adds meteors to it
        meteorList = new List<SpaceRock>();
        GameObject clone;

        for (int i = 0; i < 30; i++)
        {
            clone = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone.SetActive(false);

            meteorList.Add(clone.GetComponent<SpaceRock>());
        }
    }


    void Spawn()
    {
        //checks for the first unused meteor then activates it
        foreach (SpaceRock meteor in meteorList)
        {
            if (meteor.gameObject.activeSelf)
            {
                
            }
            else
            {         
                meteor.rot = UnityEngine.Random.Range(0, 360);
                meteor.gameObject.SetActive(true);
                return;
            }
        }
    }

    void Update()
    {
        //checks if a meteor is Active and if it is moves it
        foreach (SpaceRock meteor in meteorList)
        {
            if (meteor.gameObject.activeSelf)
            {
                meteor.rot -= meteor.direction * meteor.speed * Time.deltaTime; //Direction and speed important Direction -1,0,1. no negative speed.
                meteor.height += meteor.changeHeight * 5.0f * Time.deltaTime;
                meteor.transform.position = origin + Quaternion.Euler(0, meteor.rot, 0) * new Vector3(0, meteor.height, meteor.distance);
                meteor.transform.LookAt(origin);
                meteor.endLife -= Time.deltaTime;

                //if a meteor gets to low it hurts the city the resets the meteor for future use
                if (meteor.height <= -8)
                {
                    decreaseLife();

                    meteor.rot = 0;
                    meteor.height = 25;
                    meteor.gameObject.SetActive(false);
                }
            }
        }

        spawnRate -= Time.deltaTime;

        if (spawnRate < 0)
        {
            spawnRate = spawnValueReset;
            Spawn();
        }
    }

    //if the city is out of life causes a game over
    void gameEnd()
    {
     SceneManager.LoadScene("SampleScene");

    }

    /*void Break(SpaceRock meteor)
    {
        meteorList[14].gameObject.SetActive(true);
        meteorList[15].gameObject.SetActive(true);
        meteorList[16].gameObject.SetActive(true);

     

    }*/
}