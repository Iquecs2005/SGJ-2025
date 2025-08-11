using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonsManager : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitForDestruction());
    }

    private IEnumerator WaitForDestruction()
    {
        yield return new WaitForEndOfFrame();
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        print(scripts.Length);
        if (scripts.Length == 1)
        {
            Destroy(gameObject);
        }
    }
}
