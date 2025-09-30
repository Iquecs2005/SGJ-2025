using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private string defaultMusicName;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeOutTime;

    [Header("Configuration")]
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool shouldFadeIn;

    public void Start()
    {
        if (playOnStart) PlayMusic();
    }

    public void PlayMusic() 
    {
        PlayMusic(defaultMusicName);
    }

    public void PlayMusic(string desiredMusic)
    {
        if (shouldFadeIn) 
            MusicManager.FadeInMusic(desiredMusic, fadeInTime, fadeOutTime);
        else
            MusicManager.PlayMusic(desiredMusic);
    }

    public void StopMusic() 
    {
        //TODO
    }

    public void FadeOut() 
    {
        MusicManager.StartFadeOut();
    }
}
