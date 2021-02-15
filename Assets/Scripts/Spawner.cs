using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public List<SpaceRock> meteorList;
    public GameObject prefab;
    public Transform spawnArea;
    

    public float rate = 3f;
    private float shittyTimer = 5;
    private float cityLife = 5;

    Vector3 origin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
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
        foreach (SpaceRock meteor in meteorList)
        {
            if (meteor.gameObject.activeSelf)
            {
                
            }
            else
            {         
                meteor.rot = Random.Range(0, 360);
                meteor.gameObject.SetActive(true);
                return;
            }
        }
    }

    void Update()
    {
        foreach (SpaceRock meteor in meteorList)
        {
            if (meteor.gameObject.activeSelf)
            {
                meteor.rot -= meteor.direction * meteor.speed * Time.deltaTime; //Direction and speed important Direction -1,0,1. no negative speed.
                meteor.height += meteor.changeHeight * 5.0f * Time.deltaTime;
                meteor.transform.position = origin + Quaternion.Euler(0, meteor.rot, 0) * new Vector3(0, meteor.height, meteor.distance);
                meteor.transform.LookAt(origin);
                meteor.endLife -= Time.deltaTime;
                if (meteor.height <= -8)
                {
                    cityLife--;

                    if (cityLife <= 0)
                    {
                        gameEnd();
                    }

                    meteor.rot = 0;
                    meteor.height = 25;
                    meteor.gameObject.SetActive(false);
                }
            }
        }

        shittyTimer -= Time.deltaTime;

        if (shittyTimer < 0)
        {
            shittyTimer = 5;
            Spawn();
        }
    }

    void gameEnd()
    {
     SceneManager.LoadScene("SampleScene");

    }
}