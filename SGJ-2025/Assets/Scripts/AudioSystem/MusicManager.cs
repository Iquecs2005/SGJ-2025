using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static AudioSource musicSource;
    private static MusicLibrary musicLibrary;

    private static string currentMusicName;

    public void Initialization()
    {
        musicSource = GetComponent<AudioSource>();
        musicLibrary = GetComponent<MusicLibrary>();
        musicLibrary.InitializeDictionary();
    }

    public static void PlayMusic(string musicName) 
    {
        float volume = 0;

        if (musicName == currentMusicName) return;

        musicSource.clip = musicLibrary.GetMusicClip(musicName, ref volume);
        musicSource.volume = volume;

        currentMusicName = musicName;

        musicSource.Play();
    }
}
