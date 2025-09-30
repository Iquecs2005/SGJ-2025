using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    private static SFXLibrary sfxLibrary;
    private static AudioSource sfxSource;

    [SerializeField] private GameObject sfxSourceObj;

    public void Initialization() 
    {
        instance = this;
        sfxSource = GetComponent<AudioSource>();
        sfxLibrary = GetComponent<SFXLibrary>();
        sfxLibrary.InitializeDictionary();
    }

    public static GameObject PlaySFX(string sfxName)
    {
        GameObject sfxObject = PlaySFX(sfxName, Vector2.zero, Camera.main.transform);
        sfxObject.GetComponent<AudioSource>().spatialBlend = 0;
        return sfxObject;
    }

    public static GameObject PlaySFX(string sfxName, Vector2 soundPos, Transform parent = null)
    {
        float volume = 0;
        float pitchModifier = 0;

        AudioClip randomClip = sfxLibrary.GetClipRandomVariation(sfxName, ref volume, ref pitchModifier);

        if (randomClip == null) return null;

        GameObject SourceObj;

        SourceObj = Instantiate(instance.sfxSourceObj, soundPos, Quaternion.identity, parent);
        AudioSource audioSource = SourceObj.GetComponent<AudioSource>();

        //sfxSource.pitch = pitchModifier;
        audioSource.clip = sfxLibrary.GetClipRandomVariation(sfxName, ref volume, ref pitchModifier);
        audioSource.volume = volume;
        audioSource.pitch = pitchModifier;

        audioSource.Play();

        return SourceObj;
    }
}
