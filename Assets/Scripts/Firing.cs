using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{ 
    public List<GameObject> bulletList;
    Vector3 origin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
{
    bulletList = new List<GameObject>();
}

// Update is called once per frame
void Update()
{


        foreach (GameObject bullet in bulletList)
        {
                bullet.GetComponent<Bullet>().rot -= bullet.GetComponent<Bullet>().direction * bullet.GetComponent<Bullet>().speed * Time.deltaTime;
                bullet.GetComponent<Bullet>().height += bullet.GetComponent<Bullet>().changeHeight * 5.0f * Time.deltaTime;
                bullet.transform.position = origin + Quaternion.Euler(0, bullet.GetComponent<Bullet>().rot, 0) * new Vector3(0, bullet.GetComponent<Bullet>().height, bullet.GetComponent<Bullet>().distance);
                bullet.transform.LookAt(origin);
                bullet.GetComponent<Bullet>().endLife -= Time.deltaTime;
        }

        for (int i = bulletList.Count - 1; i >= 0; i--)
        {
            if (bulletList[i].GetComponent<Bullet>().endLife <= 0)
            {
                Destroy(bulletList[i]);
                bulletList.RemoveAt(i);
            }
        }


    }
}