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

    public void YesInterect()
    {
        Time.timeScale = 0f;
    }

    public void NoInterect()
    {
        Time.timeScale = 1f;
    }
}
