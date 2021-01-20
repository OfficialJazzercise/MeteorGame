using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingGun : MonoBehaviour
{
    public Rigidbody projectile;

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
            clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
    }
}
