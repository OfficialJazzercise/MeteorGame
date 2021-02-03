using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firing : MonoBehaviour
{ 
    public List<Bullet> bulletList;
    Vector3 origin = Vector3.zero;
    public Bullet prefab;
    private bool bBigBullet;
    private float GBulletTimer = 20f; // This is the timer, in seconds, that the GBullet upgrade lasts
    // Start is called before the first frame update

    private void OnEnable()
    {
        GBullet.BigBulletPowerUp += BigBulletPowerUp;
    }
    private void OnDisable()
    {
        GBullet.BigBulletPowerUp -= BigBulletPowerUp;
    }
    private void BigBulletPowerUp() {bBigBullet = true;}

    void Start()
{
    bulletList = new List<Bullet>();
    GameObject clone;

        for (int i = 0; i < 30; i++)
        {
            clone = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone.SetActive(false);
           
            bulletList.Add(clone.GetComponent<Bullet>());
        }
}

// Update is called once per frame
void Update()
{
        foreach (Bullet bullet in bulletList)
        {
            if (bullet.gameObject.activeSelf)
            {
                bullet.rot -= bullet.direction * bullet.speed * Time.deltaTime;
                bullet.height += bullet.changeHeight * 5.0f * Time.deltaTime;
                bullet.transform.position = origin + Quaternion.Euler(0, bullet.rot, 0) * new Vector3(0, bullet.height, bullet.distance);
                bullet.transform.LookAt(origin);
                bullet.endLife -= Time.deltaTime;

                if (bullet.endLife <= 0)
                {
                    bullet.gameObject.SetActive(false);
                }
            }
        }

        //Timer for GBullet upgrade
        if(bBigBullet == true && GBulletTimer > 0)
        {
            GBulletTimer -= Time.deltaTime;
            if(GBulletTimer < 0)
            {
                bBigBullet = false;
                GBulletTimer = 20;
            }
        }
}

    public void startBullet(float newRot, float newDirection, float newHeight, float newSpeed, float changeHeight, Transform newTransform, float newDistance)
    {
        foreach (Bullet bullet in bulletList)
        {
            if(bullet.gameObject.activeSelf)
            {

            }
            else
            {
                bullet.transform.position = newTransform.position;
                bullet.rot = newRot;
                bullet.distance = newDistance;
                bullet.height = newHeight;
                bullet.endLife = 2;
                bullet.speed = newSpeed;
                bullet.changeHeight = changeHeight;
                bullet.direction = newDirection;
                bullet.gameObject.SetActive(true);

                if (bBigBullet) bullet.transform.localScale = new Vector3(12f, 12f, 12f);
                else bullet.transform.localScale = new Vector3(6f, 6f, 6f);

                return;
            }
        }
    }

}