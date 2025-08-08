using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; } 
    public static SFXManager sfxManager { get; private set; } 
    public static MusicManager musicManager { get; private set; } 

    private void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            sfxManager = GetComponentInChildren<SFXManager>();
            sfxManager.Initialization();
            musicManager = GetComponentInChildren<MusicManager>();
            musicManager.Initialization();
        }
    }
}
