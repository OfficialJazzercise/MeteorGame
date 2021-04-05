using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class WaveHandler : MonoBehaviour
{
    private int currentWave = 0;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator prepWave(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        currentWave++;
        StartCoroutine(coroutine);

    }
}
