using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockExplode : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject floatingText;
    public Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

  void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Pos = this.gameObject.transform.position;
            Instantiate(Explosion, Pos, Quaternion.identity);


            Instantiate(floatingText, Pos, Quaternion.identity);
             //GameObject go = Instantiate(Explosion, new Vector3 (0,0,0), Quaternion.identity) as GameObject; 
             //go.transform.parent = GameObject.Find("Meteor").transform;
        }
    }
}
