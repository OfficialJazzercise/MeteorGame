using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BoostTrailToggle : MonoBehaviour
{
    public GameObject Maintrail;
    private bool isBoosting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void boosting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBoosting = true;
        }
        else if (context.canceled)
        {
            isBoosting = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isBoosting)
        {
            if (Maintrail != null)
            Maintrail.SetActive (false);
        }
        else
        {
            if (Maintrail != null)
            Maintrail.SetActive (true);
        }
    }
    
}
