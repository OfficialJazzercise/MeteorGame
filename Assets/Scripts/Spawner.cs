using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class Spawner : MonoBehaviour
{
    public List<SpaceRock> meteorList;
    public List<Enemy> enemyList;

    public GameObject meteorPrefab;
    public GameObject enemyPrefab;
    public GameObject cityHitScreenFlash;
    public Transform spawnArea;

    public float rate = 3f;
    private float spawnRate = 2.5f;
    public float spawnValueReset = 5; //a value for resetting spawn timer. Must be the same as spawnRate

    private int waveSize = 0;
    private int meteorsSpawned = 0;
    private int meteorsDestroyed = 0;
    private int currentWave = 0;
    public Text waveText;

    public static Action decreaseLife = delegate { };
    public static Action breakRock = delegate { };

    private IEnumerator coroutine;

    Vector3 origin = Vector3.zero;

    private void OnEnable()
    {
        SpaceRock.MeteorDestroyed += decreaseMeterCount;
        SpaceRock.rockBreak += rockBreak;
    }
    private void OnDisable()
    {
        SpaceRock.MeteorDestroyed -= decreaseMeterCount;
        SpaceRock.rockBreak -= rockBreak;
    }
    private void decreaseMeterCount()
    {
        meteorsDestroyed++;
        Debug.Log(meteorsDestroyed);
    }

    // Start is called before the first frame update
    void Start()
    {
        //creats a meteorList and adds meteors to it
        meteorList = new List<SpaceRock>();
        GameObject clone;

        for (int i = 0; i < 30; i++)
        {
            clone = Instantiate(meteorPrefab, meteorPrefab.transform.position, meteorPrefab.transform.rotation).gameObject;
            clone.SetActive(false);

            meteorList.Add(clone.GetComponent<SpaceRock>());
        }

        for (int i = 0; i < 10; i++)
        {
            clone = Instantiate(enemyPrefab, enemyPrefab.transform.position, enemyPrefab.transform.rotation).gameObject;
            clone.SetActive(false);

            enemyList.Add(clone.GetComponent<Enemy>());
        }

        coroutine = prepWave(5f);
        StartCoroutine(coroutine);
    }


    void spawnRegular()
    {
        int type;//What kind of meteor spawns 0 normal, 1 breaker;
        type = UnityEngine.Random.Range(0, 1);
        
        foreach (SpaceRock meteor in meteorList)
        {
            if (!meteor.gameObject.activeSelf)
            {
                meteor.rot = UnityEngine.Random.Range(0, 360);
                meteor.canSplit = false;
                meteor.gameObject.SetActive(true);
                return;
            }
        }
    }

    void spawnSplitter()
    {
        int type;//What kind of meteor spawns 0 normal, 1 breaker;
        type = UnityEngine.Random.Range(0, 1);

        foreach (SpaceRock meteor in meteorList)
        {
            if (!meteor.gameObject.activeSelf)
            {
                meteor.rot = UnityEngine.Random.Range(0, 360);
                meteor.canSplit = true;
                meteor.gameObject.SetActive(true);
                return;
            }
        }
    }

    void rockBreak(float desiredRot, float desiredHeight, Vector3 spawnLocation)
    {
        int num;//how many rocks var

        num = 2;
            
            //UnityEngine.Random.Range(0,2);


        //checks for the first unused meteor then activates it


        for (int i = 0; i < num; i++)
        {
            foreach (SpaceRock meteor in meteorList)
            {
                if (!meteor.gameObject.activeSelf)
                {
                    if (i == 0)
                    {
                        meteor.rot = desiredRot;
                        meteor.height = desiredHeight;
                        meteor.direction = -1;
                        meteor.canSplit = false;
                        meteor.isSplit = true;
                        meteor.transform.position = spawnLocation;
                        //meteor.transform.localScale = new Vector3(10, 10, 10);
                        meteor.gameObject.SetActive(true);
                        break;
                    }
                    else
                    {
                        meteor.rot = desiredRot;
                        meteor.height = desiredHeight;
                        meteor.direction = 1;
                        meteor.canSplit = false;
                        meteor.isSplit = true;
                        meteor.transform.position = spawnLocation;
                        // meteor.transform.localScale = new Vector3(10, 10, 10);
                        meteor.gameObject.SetActive(true);
                        break;         
                    }
                }
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

                //keeps rot between 0 and 360
                if (meteor.rot >= 360)
                {
                    meteor.rot = 0;
                }
                if (meteor.rot < 0)
                {
                    meteor.rot = 360 + meteor.rot;
                }

                meteor.height += meteor.changeHeight * 5.0f * Time.deltaTime;
                meteor.transform.position = origin + Quaternion.Euler(0, meteor.rot, 0) * new Vector3(0, meteor.height, meteor.distance);
                meteor.transform.LookAt(origin);
                meteor.endLife -= Time.deltaTime;

                //if a meteor gets to low it hurts the city the resets the meteor for future use
                if (meteor.height <= -10)
                {
                    decreaseMeterCount();

                    decreaseLife();
                    screenFlashes(); // flash screen for damage
                    ScreenShake.instance.StartShake(1.2f, 1.8f); //Shakes screen upon hitting city
                    meteor.rot = 0;
                    meteor.height = 25;
                    meteor.canSplit = false;
                    meteor.gameObject.SetActive(false);
                }
            }

        }

        // removes screen flash
        if(cityHitScreenFlash != null)
        {
            if(cityHitScreenFlash.GetComponent<Image>().color.a > 0)
            {
                var color = cityHitScreenFlash.GetComponent<Image>().color;
                color.a -= 0.01f;

                cityHitScreenFlash.GetComponent<Image>().color = color;
            }
        }

        if (meteorsDestroyed == waveSize)
        {
            coroutine = prepWave(5f);
            StartCoroutine(coroutine);
        }

    }

    void screenFlashes()
    {
        var color = cityHitScreenFlash.GetComponent<Image>().color;
        color.a = 0.5f;

        cityHitScreenFlash.GetComponent<Image>().color = color;
    }

    //if the city is out of life causes a game over
    void gameEnd()
    {
        SceneManager.LoadScene("SampleScene");

    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator spawnMeteor(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        int num;//how many rocks var

        num = UnityEngine.Random.Range(0,25);

        if (num >= 20)
        {
            spawnSplitter();
        }
        else {
            spawnRegular();
        }

        if (meteorsSpawned > 1)
        {
            meteorsSpawned--;
            coroutine = spawnMeteor(spawnRate);
            StartCoroutine(coroutine);
        }

    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator prepWave(float waitTime)
    {
        currentWave++;
        waveText.text = "Wave: " + currentWave.ToString();
        waveText.gameObject.SetActive(true);
        waveSize = currentWave * 10;
        meteorsDestroyed = 0;
        spawnRate = 2.5f - waveSize / 50f;

    yield return new WaitForSeconds(waitTime);

        waveText.gameObject.SetActive(false);

        meteorsSpawned = waveSize;

        coroutine = spawnMeteor(0f);
        StartCoroutine(coroutine);

    }
}