using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Spawner : MonoBehaviour
{
    public GameObject Player;


    public List<SpaceRock> meteorList;
    public List<Enemy> flyingList;
    public List<Enemy> groundedList;

    public GameObject meteorPrefab;
    public GameObject flyingEnemyPrefab;
    public GameObject groundEnemyPrefab;
    public GameObject cityHitScreenFlash;
    public Transform spawnArea;

    public float rate = 3f;
    private float spawnRate = 2.5f;
    public float spawnValueReset = 5; //a value for resetting spawn timer. Must be the same as spawnRate

    private int waveSize = 0;
    private int meteorsSpawned = 0;
    private bool waveEnd = true;

    private int currentWave = 0;
    public Text waveText;
    public Text PlayerText;

    private bool PlayerActive = true;
    private bool Player1Turn = true;
    private int P1Wave = 0;
    private int P2Wave = 0;

    private bool enemiesAlive = true;



    public static Action resetArena = delegate{ };
    public static Action restoreLife = delegate { };
    public static Action decreaseLife = delegate { };
    public static Action<Vector3> impactExplosion = delegate { };


    private IEnumerator coroutine;

    Vector3 origin = Vector3.zero;

    private void OnEnable()
    {
        SpaceRock.MeteorDestroyed += decreaseMeterCount;
        SpaceRock.rockBreak += rockBreak;
        SpaceRock.PlayerKilled += changePlayer;
        Enemy.EnemyDestroyed += decreaseMeterCount;
        Enemy.PlayerKilled += changePlayer;
        EnemyProjectile.PlayerKilled += changePlayer;
        LifeWillChange.CityDestroyed += changePlayer;
    }
    private void OnDisable()
    {
        SpaceRock.MeteorDestroyed -= decreaseMeterCount;
        SpaceRock.rockBreak -= rockBreak;
        SpaceRock.PlayerKilled -= changePlayer;
        Enemy.EnemyDestroyed -= decreaseMeterCount;
        Enemy.PlayerKilled -= changePlayer;
        EnemyProjectile.PlayerKilled -= changePlayer;
        LifeWillChange.CityDestroyed -= changePlayer;
    }
    private void decreaseMeterCount()
    {
        meteorsSpawned--;

        if (meteorsSpawned <= 0 && !waveEnd)
        {
            waveEnd = true;
            coroutine = prepWave(5f);
            StartCoroutine(coroutine);
        }
    }

    private void changePlayer()
    {
            PlayerActive = false;
            Player1Turn = !Player1Turn;

            disableWave();

            coroutine = switchPlayers(2.5f);
            StartCoroutine(coroutine);
    }

    // Start is called before the first frame update
    void Start()
    {
        //creates a meteorList and adds meteors to it
        meteorList = new List<SpaceRock>();
        flyingList = new List<Enemy>();
        groundedList = new List<Enemy>();
        GameObject clone;

        for (int i = 0; i < 30; i++)
        {
            clone = Instantiate(meteorPrefab, meteorPrefab.transform.position, meteorPrefab.transform.rotation).gameObject;
            clone.SetActive(false);

            meteorList.Add(clone.GetComponent<SpaceRock>());
        }

        for (int i = 0; i < 10; i++)
        {
            clone = Instantiate(flyingEnemyPrefab, flyingEnemyPrefab.transform.position, flyingEnemyPrefab.transform.rotation).gameObject;
            clone.SetActive(false);

            flyingList.Add(clone.GetComponent<Enemy>());
        }

        for (int i = 0; i < 10; i++)
        {
            clone = Instantiate(groundEnemyPrefab, groundEnemyPrefab.transform.position, groundEnemyPrefab.transform.rotation).gameObject;
            clone.SetActive(false);

            groundedList.Add(clone.GetComponent<Enemy>());
        }

        coroutine = prepWave(5f);
        StartCoroutine(coroutine);
    }


    void spawnRegular()
    {
        foreach (SpaceRock meteor in meteorList)
        {
            if (meteor.canActivate)
            {
                meteor.rot = UnityEngine.Random.Range(0, 360);
                meteor.canSplit = false;
                meteor.canActivate = false;
                meteor.height = 200;
                meteor.transform.position = new Vector3(200, 200, 200);
                meteor.gameObject.SetActive(true);

                coroutine = moveMeteor(meteor, 10f);
                StartCoroutine(coroutine);
                return;
            }
        }
    }

    void spawnSplitter()
    {
        foreach (SpaceRock meteor in meteorList)
        {
            if (meteor.canActivate)
            {
                meteor.rot = UnityEngine.Random.Range(0, 360);
                meteor.height = 200;
                meteor.canActivate = false;
                meteor.canSplit = true;
                meteor.transform.position = new Vector3(200, 200, 200);
                meteor.gameObject.SetActive(true);


                coroutine = moveMeteor(meteor, 10f);
                StartCoroutine(coroutine);
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
                if (meteor.canActivate)
                {
                    if (i == 0)
                    {
                        meteor.rot = desiredRot;
                        meteor.height = desiredHeight;
                        meteor.canActivate = false;
                        meteor.direction = -1;
                        meteor.canSplit = false;
                        meteor.isSplit = true;
                        meteor.isRight = true;
                        meteor.transform.position = spawnLocation;
                        //meteor.transform.localScale = new Vector3(10, 10, 10);
                        meteor.gameObject.SetActive(true);


                        coroutine = moveMeteor(meteor, 5f);
                        StartCoroutine(coroutine);

                        break;
                    }
                    else
                    {
                        meteor.rot = desiredRot;
                        meteor.height = desiredHeight;
                        meteor.direction = 1;
                        meteor.canActivate = false;
                        meteor.canSplit = false;
                        meteor.isSplit = true;
                        meteor.isRight = false;
                        meteor.transform.position = spawnLocation;
                        // meteor.transform.localScale = new Vector3(10, 10, 10);
                        meteor.gameObject.SetActive(true);


                        coroutine = moveMeteor(meteor, 5f);
                        StartCoroutine(coroutine);
                        break;
                    }
                }
            }
        }
    }

    void Update()
    {
        if (cityHitScreenFlash.GetComponent<Image>().color.a > 0)
        {
            var color = cityHitScreenFlash.GetComponent<Image>().color;
            color.a -= 0.01f;

            cityHitScreenFlash.GetComponent<Image>().color = color;
        }
    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator spawnEnemy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        int num;//how many rocks var

        num = UnityEngine.Random.Range(0, 25);

        if (PlayerActive)
        {

            if (num >= 16)
            {
                spawnSplitter();
            }
            else if (num >= 13)
            {
                spawnFlyingEnemy();
            }
            else if (num >= 10)
            {
                spawnGroundEnemy();
            }
            else
            {
                spawnRegular();
            }

            if (!waveEnd)
            {
                coroutine = spawnEnemy(spawnRate);
                StartCoroutine(coroutine);
            }
        }
    }

    void spawnFlyingEnemy()
    {
        spawnRegular();

        foreach (Enemy enemy in flyingList)
        {
            if (enemy.canActivate)
            {
                enemy.enemyType = "flying";
                enemy.rot = UnityEngine.Random.Range(0, 360);
                enemy.canActivate = false;
                enemy.height = 200;
                enemy.canMove = false;
                enemy.desiredHeight = UnityEngine.Random.Range(10, 60);
                enemy.transform.position = new Vector3(200, 200, 200);
                enemy.gameObject.SetActive(true);


                return;
            }
        }
    }

    void spawnGroundEnemy()
    {
        spawnRegular();

        foreach (Enemy enemy in groundedList)
        {
            if (enemy.canActivate)
            {
                enemy.enemyType = "grounded";
                enemy.rot = UnityEngine.Random.Range(0, 360);
                enemy.canActivate = false;
                enemy.height = 200;
                enemy.canMove = false;
                enemy.desiredHeight = -12;
                enemy.transform.position = new Vector3(200, 200, 200);
                enemy.gameObject.SetActive(true);


                return;
            }
        }
    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator prepWave(float waitTime)
    {
        while (enemiesAlive)
        {
            checkifAlive();
            yield return null;
        }

        if (!PlayerActive)
        {
            yield break;
        }

        currentWave++;
        waveText.text = "Wave: " + currentWave.ToString();
        waveText.gameObject.SetActive(true);
        PlayerText.gameObject.SetActive(true);
        waveSize = currentWave * 10;




        if (waveSize > 100)
        {
            spawnRate = 1f;
        }
        else if (waveSize > 75)
        {
            spawnRate = 1.5f;
        }
        else if (waveSize > 50)
        {
            spawnRate = 2f;
        }
        else if (waveSize > 40)
        {
            spawnRate = 2.1f;
        }
        else if (waveSize > 30)
        {
            spawnRate = 2.2f;
        }
        else if (waveSize > 15)
        {
            spawnRate = 2.3f;
        }
        else if (waveSize > 5)
        {
            spawnRate = 2.4f;
        }
        else
        {
            spawnRate = 2.5f;
        }

        yield return new WaitForSeconds(waitTime);

        meteorsSpawned = waveSize;
        waveEnd = false;
        enemiesAlive = true;

        waveText.gameObject.SetActive(false);
        PlayerText.gameObject.SetActive(false);

        coroutine = spawnEnemy(0f);
        StartCoroutine(coroutine);

    }

    private IEnumerator moveMeteor(SpaceRock meteor, float duration)
    {
        float timePast = 0.0f;

        float startingRot = meteor.rot;
        float startingHeight = meteor.height;
        float targetRot = 0;


        if (meteor.isRight)
        {
            targetRot = meteor.rot + 100;
        }
        else
        {
            targetRot = meteor.rot - 100;
        }

        float targetHeight = -10;

        while (timePast < duration && meteor.gameObject.activeSelf)
        {
            meteor.rot = Mathf.Lerp(startingRot, targetRot, timePast / duration);
            meteor.height = Mathf.Lerp(startingHeight, targetHeight, timePast / duration);

            meteor.transform.position = origin + Quaternion.Euler(0, meteor.rot, 0) * new Vector3(0, meteor.height, meteor.distance);

            meteor.lookTowards = new Vector3(origin.x, origin.y + meteor.height, origin.z);
            meteor.transform.LookAt(meteor.lookTowards);

            timePast += Time.deltaTime;
            yield return null;
        }

        meteor.rot = targetRot;
        meteor.height = targetHeight;

        meteor.transform.position = origin + Quaternion.Euler(0, meteor.rot, 0) * new Vector3(0, meteor.height, meteor.distance);

        meteor.lookTowards = new Vector3(origin.x, origin.y + meteor.height, origin.z);
        meteor.transform.LookAt(meteor.lookTowards);

        if (meteor.gameObject.activeSelf)
        {
            meteorCrashed(meteor);
        }
        else
        {
            meteor.canActivate = true;
        }
    }

    private void meteorCrashed(SpaceRock meteor)
    {
        decreaseMeterCount();
        decreaseLife();

        ScreenShake.instance.StartShake(1.2f, 1.8f); //Shakes screen upon hitting city
        meteor.rot = 0;
        meteor.height = 25;
        meteor.canSplit = false;

        impactExplosion(meteor.transform.position);
        meteor.gameObject.SetActive(false);

        var color = cityHitScreenFlash.GetComponent<Image>().color;
        color.a = 0.5f;

        cityHitScreenFlash.GetComponent<Image>().color = color;

        FindObjectOfType<SoundManager>().Play("Boom");//Finds SFX to play
    }

    private IEnumerator switchPlayers(float waitTime)
    {
        if (Player1Turn)
        {
            PlayerText.text = "P1";
            P2Wave = currentWave - 1;
            currentWave = P1Wave;
        }
        else
        {
            PlayerText.text = "P2";
            P1Wave = currentWave - 1;
            currentWave = P2Wave;
        }

        yield return new WaitForSeconds(waitTime);

        Player.SetActive(true);
        PlayerActive = true;

        //PlayerSpawnSound

        restoreLife();

        coroutine = prepWave(5f);
        StartCoroutine(coroutine);

    }

    private void disableWave()
    {
        resetArena();
    }

    private void checkifAlive()
    {
        foreach (SpaceRock meteor in meteorList)
        {
            if (meteor.gameObject.activeSelf)
            {
                enemiesAlive = true;
                return;
            }
        }

        foreach (Enemy enemy in groundedList)
        {
            if (enemy.gameObject.activeSelf)
            {
                enemiesAlive = true;
                return;
            }
        }

        foreach (Enemy enemy in flyingList)
        {
            if (enemy.gameObject.activeSelf)
            {
                enemiesAlive = true;
                return;
            }
        }

        enemiesAlive = false;
        return;
    }
}