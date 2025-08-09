using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    private void FixedUpdate()
    {
        transform.position = new Vector3(followTarget.position.x, 0, 0);
    }
}
