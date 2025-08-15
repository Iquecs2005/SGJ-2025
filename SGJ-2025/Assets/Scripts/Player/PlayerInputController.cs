using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public UnityEvent OnMouseDownEvent;
    public UnityEvent OnMouseUpEvent;

    public void OnMouseClick(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            OnMouseDownEvent.Invoke();
        }
        else if (context.canceled) 
        {
            OnMouseUpEvent.Invoke();
        }
    }
}
