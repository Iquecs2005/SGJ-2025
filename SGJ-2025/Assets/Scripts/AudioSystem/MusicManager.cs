using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static AudioSource musicSource;
    private static MusicLibrary musicLibrary;

    private static MusicGroup currentMusic;
    private static MusicGroup desiredFadeInMusic;
    //private static string bufferMusic;

    private static bool fading;
    private static bool fadingIn;
    private static bool fadingOut;

    private static float timeElapsed = 0;
    [SerializeField]
    private float fadeInTime = 1;
    [SerializeField]
    private float fadeOutTime = 1;

    public void Initialization()
    {
        musicSource = GetComponent<AudioSource>();
        musicLibrary = GetComponent<MusicLibrary>();
        musicLibrary.InitializeDictionary();
        
        fading = false;
        fadingIn = false;
        fadingOut = false;
        currentMusic = null;
        desiredFadeInMusic = null;
    }

    private void FixedUpdate()
    {
        if (fading) 
        {
            if (fadingOut) 
            {
                FadeOutStep();
            }
            else if (fadingIn) 
            {
                FadeInStep();
            }
        }
    }

    private static void SetAudioSource(MusicGroup desiredMusic, bool setAudio)
    {
        if (desiredMusic != null) 
        {
            musicSource.clip = desiredMusic.musicClip;

            if (setAudio)
            {
                musicSource.volume = desiredMusic.volume;
            }

            musicSource.Play();
        }
        else 
        {
            musicSource.clip = null;
        }

        currentMusic = desiredMusic;
    }

    public static void PlayMusic(string musicName) 
    {
        if (currentMusic != null && musicName == currentMusic.musicName) return;

        fading = false;
        fadingIn = false;
        fadingOut = false;

        SetAudioSource(musicLibrary.GetMusicClip(musicName), true);
    }

    public static void FadeInMusic(string musicName) 
    {
        if (currentMusic != null && musicName == currentMusic.musicName) return;

        desiredFadeInMusic = musicLibrary.GetMusicClip(musicName);

        fading = true;

        if (currentMusic == null) 
        {
            fadingIn = true;
            musicSource.volume = 0;
            SetAudioSource(desiredFadeInMusic, false);
        }
        else 
        {
            if (fadingIn) 
            {
                fadingIn = false;
                timeElapsed = 0;
                currentMusic.volume = musicSource.volume;
            }
            fadingOut = true;
        }
    }

    public static void StartFadeOut() 
    {
        if (currentMusic == null) return;

        desiredFadeInMusic = null;

        if (fadingOut) return;

        fading = true;
        fadingOut = true;
        timeElapsed = 0;

        if (fadingIn) 
        {
            fadingIn = false;
            currentMusic.volume = musicSource.volume;
        }
    }

    private void FadeOutStep()
    {
        timeElapsed += Time.deltaTime;
        float ratio = timeElapsed / fadeOutTime;

        musicSource.volume = Mathf.Lerp(currentMusic.volume, 0, ratio);

        if (ratio >= 1)
        {
            fadingOut = false;

            if (desiredFadeInMusic != null) 
            {
                fadingIn = true;
            }

            SetAudioSource(desiredFadeInMusic, false);
            timeElapsed = 0;

        }
    }

    private void FadeInStep() 
    {
        timeElapsed += Time.deltaTime;
        float ratio = timeElapsed / fadeInTime;

        musicSource.volume = Mathf.Lerp(0, desiredFadeInMusic.volume, ratio);

        if (ratio >= 1)
        {
            fading = false;
            fadingIn = false;

            timeElapsed = 0;
        }
    }
}