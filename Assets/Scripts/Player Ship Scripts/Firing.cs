using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Firing : MonoBehaviour
{
    public List<Bullet> bulletList; // A list the will contain all the default bullets of the game
    Vector3 origin = Vector3.zero;
    public Bullet prefab;
    public movement player;
    private bool bBigBullet;
    int sfx = 0;

    private IEnumerator coroutine;


    //A delegate to let the HUD know the GiantBulletPowerup has ended

    public static Action GiantBulletEnded = delegate { };

    //Activated from the GBullet Script will enable the use of the GiantBulletPowerup
    private void OnEnable()
    {
        GBullet.BigBulletPowerUp += BigBulletPowerUp;
        Spawner.resetArena += resetBigBullet;
        SPSpawner.resetArena += resetBigBullet;
    }
    private void OnDisable()
    {
        GBullet.BigBulletPowerUp -= BigBulletPowerUp;
        Spawner.resetArena -= resetBigBullet;
        SPSpawner.resetArena -= resetBigBullet;
    }

    private void BigBulletPowerUp()
    {
        //starts Powerup

        bBigBullet = true;

        coroutine = endPowerup(15f);
        StartCoroutine(coroutine);
    }

    private void resetBigBullet() 
    { 
        //Deactivates powerup


        bBigBullet = false; 
        GiantBulletEnded();
        
    }

    // Start is called before the first frame update
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

       GiantBulletEnded();
    }
    //a function to activate a bullet when needed
    public void startBullet(Transform newTransform, Quaternion newRotation)
    {
        
        //cycles through each object in bulletList
        foreach (Bullet bullet in bulletList)
        {
            

            //ignores the bullet if it's already active
            if (bullet.canActivate)
            {
                //Sets the values for the new bullet
                bullet.transform.localRotation = newRotation;
                bullet.transform.position = newTransform.position;
                bullet.canActivate = false;
                bullet.rot = player.rot;
                bullet.rotChanges = 0;
                bullet.height = player.height;
                bullet.targetHeight = bullet.height;
                bullet.isRight = player.isRight;
               
                switch (sfx)
                {
                    case 0:
                        FindObjectOfType<SoundManager>().Play("PShot1");
                        Debug.Log("PS1");

                        break;
                    case 1:
                        FindObjectOfType<SoundManager>().Play("PShot2");

                        Debug.Log("PS2");
                        break;
                    case 2:
                        FindObjectOfType<SoundManager>().Play("PShot3");
                        sfx = -1;
                        Debug.Log("PS3");
                        break;
                    default:

                        break;
                }
                sfx++;
                

                if (bullet.isRight)
                {

                    bullet.rotChanges = -50;
                }
                else
                {
                    bullet.rotChanges = 50;
                }

                bullet.targetRot = bullet.rot + bullet.rotChanges;

                //Prevents the bullet's trail from making weird traces
                bullet.resetTrail();


                //checks and then applies the BigBulletPowerup
                if (bBigBullet)
                {
                    bullet.transform.localScale = new Vector3(12f, 12f, 12f);
                    bullet.bulletTrail.startWidth = 1.2f;
                    bullet.bulletTrail.endWidth = .6f;
                }
                else
                {
                    bullet.transform.localScale = new Vector3(6f, 6f, 6f);
                    bullet.bulletTrail.startWidth = .6f;
                    bullet.bulletTrail.endWidth = .3f;
                }

                bullet.gameObject.SetActive(true);

                coroutine = destroyBullet(bullet, .3f);
                StartCoroutine(coroutine);

                //Leaves the function to skip cycling through the rest of the list
                return;
            }
            
            

        }
    }
    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator destroyBullet(Bullet bullet, float waitTime)
    {

        coroutine = moveToTarget(bullet, waitTime);
        StartCoroutine(coroutine);

        yield return new WaitForSeconds(waitTime);

        bullet.gameObject.SetActive(false);
        bullet.canActivate = true;

    }

    private IEnumerator endPowerup(float waitTime)
    {
        float timePast = 0;

        while (timePast < waitTime && bBigBullet)
        {          
            timePast += Time.deltaTime;
            yield return null;
        }

        resetBigBullet();
    }

    private IEnumerator moveToTarget(Bullet projectile, float duration)
    {
        float timePast = 0.0f;

        float startingRot = projectile.rot;
        float startingHeight = projectile.height;

            if (player.verticalMovement > 0 && player.horizontalMovement != 0 && !player.topMap && !player.bottomMap)
            {
                startingHeight += 3f;
                projectile.targetHeight += 45f;
            }
            else if (player.verticalMovement < 0 && player.horizontalMovement != 0 && !player.topMap && !player.bottomMap)
            {
                startingHeight += -2.8f;
                projectile.targetHeight += -45f;
            }
            else
            {
                startingHeight = projectile.height;
                projectile.targetHeight = projectile.height;
            }

        if (player.horizontalMovement != 0)
        {
            projectile.bulletTrail.time = .02f;

        }
        else
        {
            projectile.bulletTrail.time = .05f;
        }


        while (timePast < duration && projectile.gameObject.activeSelf)
        {
            startingRot = player.rot;

                if (player.isRight)
                {
                    startingRot += -2.5f;
                }
                else
                {
                    startingRot += 2.5f;
                }

            projectile.targetRot = startingRot + projectile.rotChanges;

            projectile.rot = Mathf.Lerp(startingRot, projectile.targetRot, timePast / duration);
            projectile.height = Mathf.Lerp(startingHeight, projectile.targetHeight, timePast / duration);

            projectile.transform.position = origin + Quaternion.Euler(0, projectile.rot, 0) * new Vector3(0, projectile.height, projectile.distance);
            projectile.transform.LookAt(origin);

            timePast += Time.deltaTime;
            yield return null;
        }

        projectile.rot = projectile.targetRot;
        projectile.height = projectile.targetHeight;

        projectile.transform.position = origin + Quaternion.Euler(0, projectile.rot, 0) * new Vector3(0, projectile.height, projectile.distance);
        projectile.transform.LookAt(origin);
    }
}