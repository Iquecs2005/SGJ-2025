using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorObjectController : MonoBehaviour
{
    private void FixedUpdate()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        Vector2 cursorScreenPos = Mouse.current.position.ReadValue();
        Vector3 worldScreenPos = Camera.main.ScreenToWorldPoint(cursorScreenPos);
        worldScreenPos.z = 0;
        transform.position = worldScreenPos;
    }
}
