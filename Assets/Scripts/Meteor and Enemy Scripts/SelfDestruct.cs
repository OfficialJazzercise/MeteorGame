using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public GameObject Thingy;
    int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(x < 120)
            x = x + 1;
        else if(x >= 120)
            Destroy(Thingy);
    }
}
