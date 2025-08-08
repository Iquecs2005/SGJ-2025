using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    private void Start()
    {
        float clipLength= GetComponent<AudioSource>().clip.length;

        Destroy(gameObject, clipLength + 0.5f);
    }
}
