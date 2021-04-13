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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Spawner.restoreLife += fixLRFlip;
        SPSpawner.restoreLife += fixLRFlip;
    }
    private void OnDisable()
    {
        Spawner.restoreLife -= fixLRFlip;
        SPSpawner.restoreLife -= fixLRFlip;
    }

    void fixLRFlip()
    {
        isRight = false;
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
