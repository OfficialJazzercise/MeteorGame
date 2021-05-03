using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class FloatingScore : MonoBehaviour
{
    public float x = .3f;
    public float points;

    private GameObject target;

    private void OnEnable()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("MainCamera");

        GetComponent<TextMeshPro>().text = points.ToString();
        GetComponent<Animator>().Play("same state you are", -1, 0f);
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Rotate(0, 180, 0);

    }

    public void resetText()
    {
        gameObject.SetActive(false);
    }
}
