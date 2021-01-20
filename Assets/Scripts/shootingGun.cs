using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingGun : MonoBehaviour
{
    public Rigidbody projectile;
    Vector3 origin = Vector3.zero;
    public float distance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            projectile.transform.position = origin + Quaternion.Euler(0, 20, 0) * new Vector3(0, 0, distance);
        }
    }
}
