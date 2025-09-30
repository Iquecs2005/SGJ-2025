using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private string[] sfxName;
    [SerializeField] private bool localized;
    [SerializeField] private Vector2 pos;
    [SerializeField] private Transform soundParent;

    public void PlaySFX(int i) 
    {
        if (localized)
            SFXManager.PlaySFX(sfxName[i], pos, soundParent);
        else
            SFXManager.PlaySFX(sfxName[i]);
    }
}
