using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] public UnityEvent OnInteraction;

    private void OnMouseDown()
    {
        OnInteraction.Invoke();
    }
}
