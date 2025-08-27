using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public static SFXManager sfxManager { get; private set; }
    public static MusicManager musicManager { get; private set; }

    [SerializeField] AudioMixer mixer;

    [HideInInspector] public const string MASTER_KEY = "masterVolume";
    [HideInInspector] public const string MUSIC_KEY = "musicVolume";
    [HideInInspector] public const string SFX_KEY = "sfxVolume";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            sfxManager = GetComponentInChildren<SFXManager>();
            Destroy(sfxManager.gameObject);
            musicManager = GetComponentInChildren<MusicManager>();
            Destroy(musicManager.gameObject);
            Destroy(this);
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

        LoadVolume();
    }

    void LoadVolume()  //Volume salvo no audioSlider
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(AudioSlider.MIXER_MASTER, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(AudioSlider.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(AudioSlider.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
}
