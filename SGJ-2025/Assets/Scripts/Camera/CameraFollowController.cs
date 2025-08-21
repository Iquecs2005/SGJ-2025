using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private float lowerBoundary;

    private bool shouldMove = true;

    private void FixedUpdate()
    {
        checkForLowerBoundary();

        if (shouldMove)
            transform.position = new Vector3(followTarget.position.x, 0, 0);
    }

    private void checkForLowerBoundary() 
    {
        Vector2 screenTargetPos = Camera.main.WorldToScreenPoint(followTarget.position);

        if (screenTargetPos.y < lowerBoundary) 
        {
            SetMovement(false);
        }
        else 
        {
            SetMovement(true);
        }
    }

    public void SetMovement(bool value) 
    {
        shouldMove = value;

        if (!shouldMove)
        {
            Vector2 screenCenter = new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
            transform.position = Camera.main.ScreenToWorldPoint(screenCenter);
        }
    }
}
