using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimationController : MonoBehaviour
{
    public Animator anim;
    private bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.Play("TiltUp");
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            anim.Play("TiltUpRevert");
        }


        if(Input.GetKeyDown(KeyCode.S))
        {
            anim.Play("TiltDown");
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            anim.Play("TiltDownRevert");
        }

        if (Input.GetKeyDown(KeyCode.D) && isRight == false)
        {
            isRight = true;
            anim.Play("HoriShipTurn");
        }

        if(Input.GetKeyDown(KeyCode.A) && isRight == true)
        {
            isRight = false;
            anim.Play("HoriShipTurnRevert");
        }
    }
}
