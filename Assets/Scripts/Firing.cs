﻿using System.Collections;
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
    private float GBulletTimer = 20f; // This is the timer, in seconds, that the GBullet upgrade lasts

    private IEnumerator coroutine;


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
    private void BigBulletPowerUp()
    {
        bBigBullet = true;
        coroutine = endPowerup(20f);
        StartCoroutine(coroutine);
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
                if (bBigBullet) bullet.transform.localScale = new Vector3(12f, 12f, 12f);
                else bullet.transform.localScale = new Vector3(6f, 6f, 6f);

                bullet.gameObject.SetActive(true);

                coroutine = destroyBullet(bullet, .5f);
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
        yield return new WaitForSeconds(waitTime);

        GiantBulletEnded();
        bBigBullet = false;
    }

    private IEnumerator moveToTarget(Bullet projectile, float duration)
    {
        float timePast = 0.0f;

        float startingRot = projectile.rot;
        float startingHeight = projectile.height;


        while (timePast < duration && projectile.gameObject.activeSelf)
        {
            projectile.targetRot = player.rot + projectile.rotChanges;

            projectile.rot = Mathf.Lerp(player.rot, projectile.targetRot, timePast / duration);
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