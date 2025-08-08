using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLibrary : MonoBehaviour
{
    [SerializeField] private SFXGroup[] SFXGroups;
    private Dictionary<string, List<SFXData>> SFXDictionary;

    private void OnValidate()
    {
        InitializeDictionary();
    }

    public void InitializeDictionary() 
    {
        SFXDictionary = new Dictionary<string, List<SFXData>>();
        foreach (SFXGroup group in SFXGroups) 
        {
            SFXDictionary[group.SFXName] = group.SFXVariations;
        }
    }

    public AudioClip GetClipRandomVariation(string clipName, ref float volume, ref float pitchModifier) 
    {
        List<SFXData> currentSFXGroup = SFXDictionary[clipName];
        if (currentSFXGroup.Count > 0) 
        {
            int randomClipNumber = UnityEngine.Random.Range(0, currentSFXGroup.Count);

            SFXData currentSFX = currentSFXGroup[randomClipNumber];

            volume = currentSFX.volume;
            pitchModifier = UnityEngine.Random.Range(currentSFX.minPitchShift, currentSFX.maxPitchShift);

            return currentSFX.audioClip;
        }
        return null;
    }
}

[Serializable]
public class SFXGroup 
{
    public string SFXName;
    public List<SFXData> SFXVariations;
}

[Serializable]
public class SFXData 
{
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 0.5f;
    public float minPitchShift;
    public float maxPitchShift;
} 
