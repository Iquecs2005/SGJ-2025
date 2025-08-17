using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AllChildEnables : MonoBehaviour
{
    [SerializeField] private UnityEvent OnAllChildrenEnabled;

    public void CheckChildren() 
    {
        foreach (Transform childTransform in transform)
        {
            if (!childTransform.gameObject.activeInHierarchy) return;
        }

        OnAllChildrenEnabled.Invoke();
    }
}
