using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinFlying : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 120 * Time.deltaTime);
    }
}
