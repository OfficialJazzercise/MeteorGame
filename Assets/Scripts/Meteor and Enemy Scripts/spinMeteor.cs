using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinMeteor : MonoBehaviour
{
    int rotation;
    int[] integers = new int[] { -200, -100, 100, 100 };
    private void OnEnable()
    {
        int rand = Random.Range(0, integers.Length);
        rotation = integers[rand];
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime, rotation * Time.deltaTime, 0);
    }
}
