using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTracking : MonoBehaviour
{
    public List<EnemyProjectile> bulletList; // A list the will contain all the default bullets of the game
    Vector3 origin = Vector3.zero;
    public EnemyProjectile prefab;
    int sfx = 0;
    private GameObject target; 

    private IEnumerator coroutine;

    private void OnEnable()
    {
        Enemy.shootBullet += startBullet;
    }
    private void OnDisable()
    {
        Enemy.shootBullet -= startBullet;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
            
        //Creates a new list that contains objects created from the Bullet script and stores 30 cloned bullets into it
        bulletList = new List<EnemyProjectile>();
        GameObject clone;

        for (int i = 0; i < 30; i++)
        {
            clone = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation).gameObject;
            clone.SetActive(false);

            bulletList.Add(clone.GetComponent<EnemyProjectile>());
        }
    }

    //a function to activate a bullet when needed
    public void startBullet(float currentRot, float currentHieght, Transform currentLoction)
    {

        float distanceToPlayerA = Math.Abs(target.GetComponent<movement>().rot - currentRot);
        float distanceToPlayerB = Math.Abs(target.GetComponent<movement>().rot + 360 - currentRot);
        float distanceToPlayer = Math.Abs(target.GetComponent<movement>().rot - currentRot);

        if (distanceToPlayerA > distanceToPlayerB)
        {
            distanceToPlayer = distanceToPlayerB;
        }
        else
        {
            distanceToPlayer = distanceToPlayerA;
        }

        float heightToPlayer = Math.Abs(target.GetComponent<movement>().height - currentHieght);
         
        if (distanceToPlayer <= 50 && heightToPlayer <= 15)
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

                    switch (sfx)
                    {
                        case 0:
                            FindObjectOfType<SoundManager>().Play("EL1");
                            sfx = 1;
                            Debug.Log("EL1");
                            break;
                        case 1:
                            FindObjectOfType<SoundManager>().Play("EL2");
                            
                            Debug.Log("EL2");
                            break;
                        case 2:
                            FindObjectOfType<SoundManager>().Play("EL3");
                            sfx = 0;
                            Debug.Log("EL3");
                            break;
                        default:
                            sfx = 0;
                            break;
                    }
                    sfx++;                    
                    //Prevents the bullet's trail from making weird traces
                    bullet.resetTrail();
                    
                    bullet.gameObject.SetActive(true);                
                    findShortestTrip(bullet);

                    //Leaves the function to skip cycling through the rest of the list
                    return;
                }
            }
        }

    }

    private IEnumerator moveToPlayer(EnemyProjectile projectile, float speed, bool isRight)
    {
        float timePast = 0.0f;

        float startingRot = projectile.rot;
        float startingHeight = projectile.height;

        float HorizontalDistance = startingRot - projectile.targetsRot;
        float VerticalDistance = startingHeight - projectile.targetsHeight;

        HorizontalDistance *= 4;
        VerticalDistance *= 4;

        float distance = Mathf.Sqrt(Mathf.Pow(HorizontalDistance, 2) + Mathf.Pow(VerticalDistance, 2));

        projectile.targetsRot = startingRot - HorizontalDistance;
        projectile.targetsHeight = startingHeight - VerticalDistance;

        float duration = distance / speed;


        while (timePast < duration && projectile.height < 100 & projectile.height > -15)
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

        projectile.gameObject.SetActive(false);
        projectile.canActivate = true;
    }

    private void findShortestTrip(EnemyProjectile projectile)
    {
        float tripStart = projectile.rot;
        float tripEnd = projectile.targetsRot;

        bool isRight = false;

        if (tripStart < tripEnd)
        {
            if (tripEnd - tripStart < tripStart + 360 - tripEnd)
            {
                isRight = false;
            }
            else
            {
                isRight = true;
                projectile.rot += 360;
            }
        }
        else
        {
            if (tripStart - tripEnd < tripEnd + 360 - tripStart)
            {
                isRight = false;
            }
            else
            {
                isRight = true;
                projectile.targetsRot += 360;
            }
        }

        coroutine = moveToPlayer(projectile, 45f, isRight);
        StartCoroutine(coroutine);
    }

}
