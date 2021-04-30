using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rotateSentry : MonoBehaviour
{
    private GameObject target;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform);
        transform.Rotate(0, 90, 0);
    }
}
