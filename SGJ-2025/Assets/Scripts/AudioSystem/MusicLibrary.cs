using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLibrary : MonoBehaviour
{
    [SerializeField] private MusicGroup[] musicList;
    private Dictionary<string, MusicData> musicDictionary;

    private void OnValidate()
    {
        InitializeDictionary();
    }

    public void InitializeDictionary() 
    {
        musicDictionary = new Dictionary<string, MusicData>();
        foreach (MusicGroup musicGroup in musicList) 
        {
            MusicData musicData = new MusicData();

            musicData.musicClip = musicGroup.musicClip;
            musicData.volume = musicGroup.volume;

            musicDictionary[musicGroup.musicName] = musicData;
        }
    }

    public AudioClip GetMusicClip(string musicName, ref float volume) 
    {
        MusicData musicData = musicDictionary[musicName];

        volume = musicData.volume;

        return musicData.musicClip;
    }
}

public class MusicData 
{
    public AudioClip musicClip;
    [Range(0, 1)]
    public float volume;
}

[Serializable]
public class MusicGroup
{
    public string musicName;
    public AudioClip musicClip;
    [Range(0, 1)]
    public float volume = 0.5f;
}