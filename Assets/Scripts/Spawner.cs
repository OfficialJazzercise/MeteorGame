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
    public static Action breakRock = delegate { };

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
        int type;//What kind of meteor spawns 0 normal, 1 breaker;
        type = UnityEngine.Random.Range(0, 1);
        
        foreach (SpaceRock meteor in meteorList)
        {
            if (!meteor.gameObject.activeSelf)
            {
                meteor.rot = UnityEngine.Random.Range(0, 360);
                meteor.gameObject.SetActive(true);
                return;
            }
        }
    }
    void rockBreak(float desiredRot, float desiredHeight)
    {
        Debug.Log("anything");
        int num;//how many rocks var

        num = UnityEngine.Random.Range(1,2);
        

        //checks for the first unused meteor then activates it
        foreach (SpaceRock meteor in meteorList)
        {
            if (!meteor.gameObject.activeSelf)
            {
                if (num == 1)
                {
                    meteorList[15].rot = desiredRot;
                    meteorList[16].rot = desiredRot;

                    meteorList[15].height = desiredHeight;
                    meteorList[16].height = desiredHeight;

                    meteorList[15].direction = UnityEngine.Random.Range(0, 35);
                    meteorList[16].direction = UnityEngine.Random.Range(-35, 0);
                    //meteor.transform.position = position;

                    meteorList[15].transform.localScale = new Vector3(10, 10, 10);
                    meteorList[16].transform.localScale = new Vector3(10, 10, 10);


                    meteorList[15].gameObject.SetActive(true);
                    meteorList[16].gameObject.SetActive(true);
                }
                else
                {
                    meteorList[15].rot = desiredRot;
                    meteorList[16].rot = desiredRot;
                    meteorList[17].rot = desiredRot;

                    meteorList[15].height = desiredHeight;
                    meteorList[16].height = desiredHeight;
                    meteorList[17].height = desiredHeight;

                    meteorList[15].direction = UnityEngine.Random.Range(0, 35);
                    meteorList[16].direction = UnityEngine.Random.Range(-35, 0);
                    meteorList[17].direction = UnityEngine.Random.Range(-15, 15);
                    //meteor.transform.position = position;

                    meteorList[15].transform.localScale = new Vector3(10, 10, 10);
                    meteorList[16].transform.localScale = new Vector3(10, 10, 10);
                    meteorList[17].transform.localScale = new Vector3(10, 10, 10);


                    meteorList[15].gameObject.SetActive(true);
                    meteorList[16].gameObject.SetActive(true);
                    meteorList[17].gameObject.SetActive(true);

                }
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
            //rockBreak();
        }
    }
    private void OnEnable()
    {
        SpaceRock.rockBreak += rockBreak;
    }

    public void OnDisable()
    {
        SpaceRock.rockBreak -= rockBreak;
        
    }
    //if the city is out of life causes a game over
    void gameEnd()
    {
        SceneManager.LoadScene("SampleScene");

    }
}