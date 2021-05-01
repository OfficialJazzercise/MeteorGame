using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SelfDestruct : MonoBehaviour
{
    public GameObject Thingy;
    public float x = 0;
    // Start is called before the first frame update
    private void OnEnable()
    {
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(x < 2)
            x = x + 1 * Time.deltaTime;
        else if(x >= 2)
        {
            gameObject.SetActive(false);
        }
            
            
    }
}
