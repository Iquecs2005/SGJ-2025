using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private static SFXLibrary sfxLibrary;
    private static AudioSource sfxSource;

    private GameObject audioListenerObject;

    [SerializeField] private GameObject sfxSourceObj;

    public void Initialization() 
    {
        instance = this;
        sfxSource = GetComponent<AudioSource>();
        sfxLibrary = GetComponent<SFXLibrary>();
        sfxLibrary.InitializeDictionary();
    }

    //public static void PlaySFX(string sfxName)
    //{
    //    PlaySFX(sfxName, audioListenerObject.transform.position, );
    //}

    public static void PlaySFX(string sfxName, Vector2 soundPos, Transform parent = null)
    {
        float volume = 0;
        float pitchModifier = 0;

        GameObject SourceObj;
        SourceObj = Instantiate(instance.sfxSourceObj, soundPos, Quaternion.identity, parent);
        AudioSource audioSource = SourceObj.GetComponent<AudioSource>();

        //sfxSource.pitch = pitchModifier;
        audioSource.clip = sfxLibrary.GetClipRandomVariation(sfxName, ref volume, ref pitchModifier);
        audioSource.volume = volume;
        audioSource.pitch = pitchModifier;

        audioSource.Play();
    }
}
