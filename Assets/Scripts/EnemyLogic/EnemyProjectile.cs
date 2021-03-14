using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{   public float distance;
    public float height;
    public float targetsHeight;
    public float targetsRot;
    public bool canActivate = true;

    public float rot = 0.0f;
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

}
