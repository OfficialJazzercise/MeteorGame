using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipAnimationController : MonoBehaviour
{
    public Animator anim;
    private bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Spawner.restoreLife += fixLRFlip;
    }
    private void OnDisable()
    {
        Spawner.restoreLife -= fixLRFlip;
    }

    void fixLRFlip()
    {
        isRight = false;
    }

    // Update is called once per frame
   /* void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.SetFloat("Vertical", 1);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            anim.SetFloat("Vertical", -1);
        }

        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            anim.SetFloat("Vertical", 0);
        }

        if(Input.GetKeyDown(KeyCode.A) && isRight == false)
        {
            isRight = true;
            anim.SetTrigger("FlipR");
        }

        if(Input.GetKeyDown(KeyCode.D) && isRight == true)
        {
            isRight = false;
            anim.SetTrigger("FlipL");
        }
    }
    */
}
