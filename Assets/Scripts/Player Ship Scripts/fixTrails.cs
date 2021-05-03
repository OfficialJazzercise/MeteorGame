using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class fixTrails : MonoBehaviour
{
    public TrailRenderer trail;

    private void OnEnable()
    {
        trail.Clear();
    }

    public void Moving(InputAction.CallbackContext context)
    {
       float horizontalMovement = context.ReadValue<Vector2>().x;
       float verticalMovement = context.ReadValue<Vector2>().y;

        if (horizontalMovement == 0)
        {
            trail.enabled = false;
        }
        else
        {
            trail.enabled = true;
        }
    }
}
