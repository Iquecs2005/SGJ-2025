using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    private AudioSource audioSource;
    private bool audioPaused = false;
    private float destructionTimer;

    private void Start()
    {
        float clipLength = GetComponent<AudioSource>().clip.length;

        destructionTimer = clipLength + 0.5f;
    }

    private void FixedUpdate()
    {
        if (!audioPaused) 
        {
            destructionTimer -= Time.deltaTime;
            if (destructionTimer <= 0) Destroy(gameObject);
        }
    }

    public void PauseAudio() 
    {
        audioPaused = true;
        audioSource.Stop();
    }

    public void ResumeAudio() 
    {
        audioPaused = false;
        audioSource.Play();
    }
}
