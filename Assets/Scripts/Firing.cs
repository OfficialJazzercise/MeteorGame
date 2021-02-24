using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Firing : MonoBehaviour
{ 
    public List<Bullet> bulletList; // A list the will contain all the default bullets of the game
    Vector3 origin = Vector3.zero; 
    public Bullet prefab;
    private bool bBigBullet;
    private float GBulletTimer = 20f; // This is the timer, in seconds, that the GBullet upgrade lasts
    // Start is called before the first frame update

    //A delegate to let the HUD know the GiantBulletPowerup has ended
    public static Action GiantBulletEnded = delegate { };

    //Activated from the GBullet Script will enable the use of the GiantBulletPowerup
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
    //Creates a new list that contains objects created from the Bullet script and stores 30 cloned bullets into it
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
        //Cycles through all items in the bulletList
        foreach (Bullet bullet in bulletList)
        {
            //Checks to see if the current bullet is active
            if (bullet.gameObject.activeSelf)
            {
                //handles dash
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    bullet.speed = 150f;
                }
                else
                {
                    bullet.speed = 100f;
                }

                //Will change the current bullets position on a circular track
                bullet.rot -= bullet.direction * bullet.speed * Time.deltaTime;
                bullet.height += bullet.changeHeight * 5.0f * Time.deltaTime;
                bullet.transform.position = origin + Quaternion.Euler(0, bullet.rot, 0) * new Vector3(0, bullet.height, bullet.distance);
                bullet.transform.LookAt(origin);


                //makes bullet face the origin point
                bullet.transform.LookAt(origin);

                //Crappy Timer needs to be changed
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
                GiantBulletEnded();
                bBigBullet = false;
                GBulletTimer = 20;
            }
        }
}
    //a function to activate a bullet when needed
    public void startBullet(float newRot, float newDirection, float newHeight, float newSpeed, float changeHeight, Transform newTransform,  Quaternion newRotation)
    {
        //cycles through each object in bulletList
        foreach (Bullet bullet in bulletList)
        {
            //ignores the bullet if it's already active
            if(bullet.gameObject.activeSelf)
            {

            }
            else
            {
                //Sets the values for the new bullet
                bullet.transform.localRotation = newRotation;
                bullet.transform.position = newTransform.position;
                bullet.rot = newRot;
                bullet.height = newHeight;
                bullet.endLife = 2;
                bullet.speed = newSpeed;
                bullet.changeHeight = changeHeight;
                bullet.direction = newDirection;

                //Prevents the bullet's trail from making weird traces
                bullet.resetTrail();
                bullet.gameObject.SetActive(true);

                //checks and then applies the BigBulletPowerup
                if (bBigBullet) bullet.transform.localScale = new Vector3(12f, 12f, 12f);
                else bullet.transform.localScale = new Vector3(6f, 6f, 6f);

                //Leaves the function to skip cycling through the rest of the list
                return;
            }
        }
    }

}