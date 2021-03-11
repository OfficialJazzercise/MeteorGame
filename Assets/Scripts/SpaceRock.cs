using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SpaceRock : MonoBehaviour
{
    public float endLife;
    public float direction;
    public float distance;
    public float height;
    public float changeHeight;

    public int num;

    public float rot = 0.0f;
    public float speed = 100.0f;

    public bool canSplit = false;
    public bool isSplit = false;

    //delegate used for the Score Script
    public static Action IncreaseScore = delegate { };
    public static Action MeteorDestroyed = delegate { };
    public static Action<float, float, Vector3> rockBreak = delegate { }; //set Rotation on Circle, Height

    public AudioSource playSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 52 * Time.deltaTime);
    }

    private void OnDisable()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        //If a bullet hits the meteor destroys the bullet and the meteor
        if (other.CompareTag("Bullet"))
        {
            if(canSplit)rockBreak(rot, height, this.gameObject.transform.position);
            rot = 0;
            height = 25;
            IncreaseScore();
            FindObjectOfType<SoundManager>().Play("Boom");//Finds SFX to play
            ScreenShake.instance.StartShake(.4f, .8f); //Shakes screen upon destroying meteor
            gameObject.SetActive(false);
            canSplit = false;

            if (!isSplit)
            {
                MeteorDestroyed();
            }
            else
            {
                isSplit = false;
            }
        }

        //If the player hits the meteor destroys the player
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<SoundManager>().Play("Dying");
            other.gameObject.SetActive(false);
            SceneManager.LoadScene("SampleScene");

        }
    }
}
