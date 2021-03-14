using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTracking : MonoBehaviour
{
    public List<EnemyProjectile> bulletList; // A list the will contain all the default bullets of the game
    Vector3 origin = Vector3.zero;
    public EnemyProjectile prefab;

    private GameObject target; 

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");
        //Creates a new list that contains objects created from the Bullet script and stores 30 cloned bullets into it
        bulletList = new List<EnemyProjectile>();
        GameObject clone;

        for (int i = 0; i < 10; i++)
        {
            clone = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone.SetActive(false);

            bulletList.Add(clone.GetComponent<EnemyProjectile>());
        }
    }

    //a function to activate a bullet when needed
    public void startBullet(float currentRot, float currentHieght, Transform currentLoction)
    {

        float distanceToPlayer = Math.Abs(target.GetComponent<movement>().rot - currentRot);

        if (distanceToPlayer <= 50)
        {

            //cycles through each object in bulletList
            foreach (EnemyProjectile bullet in bulletList)
            {
                //ignores the bullet if it's already active
                if (bullet.canActivate)
                {
                    //Sets the values for the new bullet
                    bullet.height = currentHieght;
                    bullet.rot = currentRot;
                    bullet.targetsHeight = target.GetComponent<movement>().height;
                    bullet.targetsRot = target.GetComponent<movement>().rot;
                    bullet.canActivate = false;

                    //Prevents the bullet's trail from making weird traces
                    bullet.gameObject.SetActive(true);


                    coroutine = destroyBullet(bullet, 5f);
                    StartCoroutine(coroutine);

                    //Leaves the function to skip cycling through the rest of the list
                    return;
                }
            }
        }

    }

    //use for the courtine, will disable both muzzles after a set amount of time
    private IEnumerator destroyBullet(EnemyProjectile bullet, float waitTime)
    {
        findShortestTrip(bullet);

        yield return new WaitForSeconds(waitTime);

        bullet.gameObject.SetActive(false);
        bullet.canActivate = true;

    }

    private IEnumerator moveToPlayer(EnemyProjectile projectile, float duration)
    {
        float timePast = 0.0f;

        float startingRot = projectile.rot;
        float startingHeight = projectile.height;


        while (timePast < duration)
        {
            projectile.rot = Mathf.Lerp(startingRot, projectile.targetsRot, timePast / duration);
            projectile.height = Mathf.Lerp(startingHeight, projectile.targetsHeight, timePast / duration);

            projectile.transform.position = origin + Quaternion.Euler(0, projectile.rot, 0) * new Vector3(0, projectile.height, projectile.distance);
            projectile.transform.LookAt(origin);

            timePast += Time.deltaTime;
            yield return null;
        }
        projectile.rot = projectile.targetsRot;
        projectile.height = projectile.targetsHeight;

        projectile.transform.position = origin + Quaternion.Euler(0, projectile.rot, 0) * new Vector3(0, projectile.height, projectile.distance);
        projectile.transform.LookAt(origin);
    }

    private void findShortestTrip(EnemyProjectile projectile)
    {
        float tripA = 0;
        float tripB = 0;

        float tripStart = projectile.rot;
        float tripEnd = projectile.targetsRot;

        if (tripEnd == 0 && tripEnd != tripStart) { tripEnd = 360; }

        tripA = tripStart + (360 - tripEnd);
        tripB = Mathf.Abs(tripEnd - tripStart);

        if (tripB > tripA)
        {
            projectile.rot += 360;    
        }

        coroutine = moveToPlayer(projectile, 5f);
        StartCoroutine(coroutine);
    }

}
