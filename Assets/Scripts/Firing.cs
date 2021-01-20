using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{ 
    public List<GameObject> bulletList;
    Vector3 origin = Vector3.zero;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
{
    bulletList = new List<GameObject>();
    GameObject clone;

        for (int i = 0; i < 30; i++)
        {
            clone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
            clone.SetActive(false);
            bulletList.Add(clone);
        }
}

// Update is called once per frame
void Update()
{


        foreach (GameObject bullet in bulletList)
        {
            if (bullet.activeSelf)
            {
                bullet.GetComponent<Bullet>().rot -= bullet.GetComponent<Bullet>().direction * bullet.GetComponent<Bullet>().speed * Time.deltaTime;
                bullet.GetComponent<Bullet>().height += bullet.GetComponent<Bullet>().changeHeight * 5.0f * Time.deltaTime;
                bullet.transform.position = origin + Quaternion.Euler(0, bullet.GetComponent<Bullet>().rot, 0) * new Vector3(0, bullet.GetComponent<Bullet>().height, bullet.GetComponent<Bullet>().distance);
                bullet.transform.LookAt(origin);
                bullet.GetComponent<Bullet>().endLife -= Time.deltaTime;

                if (bullet.GetComponent<Bullet>().endLife <= 0)
                {
                    bullet.SetActive(false);
                }
            }
        }



}

    public void startBullet(float newRot, float newDirection, float newHeight, float newSpeed, float changeHeight, Transform newTransform, float newDistance)
    {
        foreach (GameObject bullet in bulletList)
        {
            if(bullet.activeSelf)
            {

            }
            else
            {
                bullet.transform.position = newTransform.position;
                bullet.GetComponent<Bullet>().rot = newRot;
                bullet.GetComponent<Bullet>().distance = newDistance;
                bullet.GetComponent<Bullet>().height = newHeight;
                bullet.GetComponent<Bullet>().endLife = 2;
                bullet.GetComponent<Bullet>().speed = newSpeed;
                bullet.GetComponent<Bullet>().changeHeight = changeHeight;
                bullet.GetComponent<Bullet>().direction = newDirection;
                bullet.SetActive(true);
                return;
            }
        }
    }

}