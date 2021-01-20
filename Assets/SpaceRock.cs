using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public int enemyCount;
    
    void Start()
    {
        StartCoroutine(RockDrop());
        
    }

    IEnumerator RockDrop()
    {
        while (enemyCount < 1)
        {
            xPos = Random.Range(1, 5);
            zPos = Random.Range(1, 5);
            Instantiate(theEnemy, new Vector3(xPos, 8, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);// sets the spawn rate
            enemyCount += 1; //limit of rocks

        }
    }
}
