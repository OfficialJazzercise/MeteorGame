using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BreakerRock : MonoBehaviour
{
    public List<BreakerRock> meteorList;
    public GameObject prefab;
    public Transform spawnArea;

    public float endLife;
    public float direction;
    public float distance;
    public float height;
    public float changeHeight;

    public float rot = 0.0f;
    public float speed = 100.0f;

    public static Action IncreaseScore = delegate { };

    public AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {
        meteorList = new List<BreakerRock>();
        GameObject clone, clone2, clone3;

        for (int i = 0; i < 30; i++)
        {
            clone = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone.SetActive(false);
            clone2 = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone2.SetActive(false);
            clone3 = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone3.SetActive(false);

            meteorList.Add(clone.GetComponent<BreakerRock>());
            meteorList.Add(clone2.GetComponent<BreakerRock>());
            meteorList.Add(clone3.GetComponent<BreakerRock>());
        }
    }

    void Spawn()
    {
        foreach (BreakerRock meteor in meteorList)
        {
            if (meteor.gameObject.activeSelf)
            {

            }
            else
            {
                meteor.rot = 1;
                meteor.gameObject.SetActive(true);
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Meteor Down!");

            rot = 0;
            height = 25;
            IncreaseScore();

            gameObject.SetActive(false);
           


            //playSound.Play();

        }
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
        }
    }
}