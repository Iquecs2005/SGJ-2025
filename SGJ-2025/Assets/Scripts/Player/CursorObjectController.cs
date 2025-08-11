using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class CursorObjectController : MonoBehaviour
{
    [SerializeField] private Light2D cursorLight;
    [SerializeField] private CircleCollider2D lightTrigger;

    private void Start()
    {
        lightTrigger.radius = cursorLight.pointLightOuterRadius;
    }

    private void FixedUpdate()
    {
        FollowCursor();
    }

    private void FollowCursor()
    {
        Vector2 cursorScreenPos = Mouse.current.position.ReadValue();

        int cameraWidth = Camera.main.pixelWidth;
        int cameraHeight = Camera.main.pixelHeight;

        float clampedXValue = Mathf.Clamp(cursorScreenPos.x, 0, cameraWidth);
        float clampedYValue = Mathf.Clamp(cursorScreenPos.y, 0, cameraHeight);

        cursorScreenPos = new Vector2(clampedXValue, clampedYValue);
        Vector3 worldScreenPos = Camera.main.ScreenToWorldPoint(cursorScreenPos);

        worldScreenPos.z = 0;
        transform.position = worldScreenPos;
    }
}
