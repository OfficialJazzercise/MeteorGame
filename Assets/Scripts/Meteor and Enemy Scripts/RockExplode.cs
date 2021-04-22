using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockExplode : MonoBehaviour
{
    public GameObject Explosion;
    public FloatingScore floatingText;
    public GameObject Impact;
    public Vector3 Pos;

    public List<GameObject> explosionList;
    public List<FloatingScore> textList;

    private void OnEnable()
    {
        SpaceRock.startExplosion += airExplosion;
        Enemy.startExplosion += airExplosion;
        movement.startExplosion += airExplosion;

        Score.flashText += popInText;
        SPScore.flashText += popInText;
    }
    private void OnDisable()
    {
        SpaceRock.startExplosion -= airExplosion;
        Enemy.startExplosion -= airExplosion;
        movement.startExplosion += airExplosion;

        Score.flashText -= popInText;
        SPScore.flashText -= popInText;
    }

    private void Start()
    {
        GameObject clone;

        for (int i = 0; i < 10; i++)
        {
            clone = Instantiate(Explosion, Explosion.transform.position, Explosion.transform.rotation);
            clone.SetActive(false);

            explosionList.Add(clone);
        }

        for (int i = 0; i < 10; i++)
        {
            clone = Instantiate(floatingText, floatingText.transform.position, floatingText.transform.rotation).gameObject;
            clone.SetActive(false);

            textList.Add(clone.GetComponent<FloatingScore>());
        }
    }

    private void airExplosion(Vector3 Pos)
    {
        //cycles through each object in bulletList
        foreach (GameObject explosion in explosionList)
        {
            if (explosion != null)
            {
                //ignores the bullet if it's already active
                if (!explosion.activeSelf)
                {
               
                        explosion.transform.position = Pos;
                        explosion.gameObject.SetActive(true);
                
                    break;
                }
            }
        }

    }

    private void popInText(float amount, Vector3 Pos)
    {
        //cycles through each object in bulletList
        foreach (FloatingScore text in textList)
        {
                //ignores the bullet if it's already active
                if (!text.gameObject.activeSelf)
                {

                    text.gameObject.transform.position = Pos;
                    text.points = amount;
                    text.gameObject.SetActive(true);

                    break;
                }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Floor"))
        {
            Pos = this.gameObject.transform.position;
            Instantiate(Impact, Pos, Quaternion.identity);

            FindObjectOfType<SoundManager>().Play("Impact");
        }
    }
}
