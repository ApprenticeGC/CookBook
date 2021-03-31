using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class NeedHelpFromScript : MonoBehaviour
{
    public void OnFire()
    {
        Debug.Log("NeedHelpFromScript - OnFire");
    }

    public void OnFireByUnityEvent(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("NeedHelpFromScript - OnFireByUnityEvent - started");
        }

        if (context.performed)
        {
            Debug.Log("NeedHelpFromScript - OnFireByUnityEvent - performed");
        }

        if (context.canceled)
        {
            Debug.Log("NeedHelpFromScript - OnFireByUnityEvent - canceled");
        }
    }
}
