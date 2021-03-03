using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimationController : MonoBehaviour
{
    public Animator anim;
    private bool isRight = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks for which direction ship is facing

        if(Input.GetKeyDown(KeyCode.W) && isRight == true) // When facing right and moving up, ship tilts left
        {
            anim.Play("ShipTurn2");
        }
        else if(Input.GetKeyDown(KeyCode.S) && isRight == true) // When facing right and moving down, ship tilts right
        {
            anim.Play("ShipTurn1");
        }

        if(Input.GetKeyDown(KeyCode.W) && isRight == false) // When facing left and moving up, ship tilts right
        {
            anim.Play("ShipTurn2Left");
        }
        else if(Input.GetKeyDown(KeyCode.S) && isRight == false) // When facing left and moving down, ship tilts left
        {
            anim.Play("ShipTurn1Left");
        }

        if(Input.GetKeyDown(KeyCode.A) && isRight == true) // When turning left, turning animation (towards player)
        {
            isRight = false;
            anim.Play("HoriShipTurn1");
        }

        if(Input.GetKeyDown(KeyCode.D) && isRight == false) // When turning right, turning animation (towards player)
        {
            isRight = true;
            anim.Play("HoriShipTurn2");
        }
    }
}
