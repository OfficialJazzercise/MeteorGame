using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FloatingScore : MonoBehaviour
{
    public float DestroyTime = .3f;

    private Vector3 CameraPostion;
    private GameObject target;
    private Score Score;

    private void Awake()
    {
        Score = GameObject.FindObjectOfType<Score>();
        Destroy(gameObject, DestroyTime);

        if (target == null)
            target = GameObject.FindGameObjectWithTag("MainCamera");

        GetComponent<TextMesh>().text = Mathf.Round(Score.pointsGiven).ToString();
    }
    

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Rotate(0, 180, 0);

    }
}
