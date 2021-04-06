﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float direction;
    public float distance;
    public float height;
    public float targetHeight;
    public bool isRight = true;
    public bool canActivate = true;

    public float rot = 0.0f;
    public float rotChanges = 0.0f;
    public float targetRot = 0.0f;
    public float speed = 100.0f;

    public TrailRenderer bulletTrail;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //clears the trail on the bullet
    public void resetTrail()
    {
        bulletTrail.Clear();
    }
}