using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipAnimationController : MonoBehaviour
{
    public Animator anim;
    public movement player;
    private bool isRight = false;

    private float horizontalMovement = 0;
    private float verticalMovement = 0;

    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        horizontalMovement = 0;
        verticalMovement = 0;
        isRight = false;
    }
    private void OnDisable()
    {
    }

    public void ShipAnim(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        verticalMovement = context.ReadValue<Vector2>().y;
    }
    // Update is called once per frame
        void Update()
     {
        if (verticalMovement > 0)
        {
            anim.SetFloat("Vertical", 1);
        }

        if (verticalMovement < 0)
        {
            anim.SetFloat("Vertical", -1);
        }

        if (verticalMovement == 0 || horizontalMovement == 0 || player.topMap || player.bottomMap)
        {
            anim.SetFloat("Vertical", 0);
        }

        if (horizontalMovement < 0 && isRight == false)
        {
            isRight = true;
            anim.SetTrigger("FlipR");
        }

        if (horizontalMovement > 0 && isRight == true)
        {
            isRight = false;
            anim.SetTrigger("FlipL");
        }

    }

}
