using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScript : MonoBehaviour
{
    [SerializeField] private string audioName;
    [SerializeField] private Vector3 pos;

    public void PlayAudio() 
    {
        SFXManager.PlaySFX(audioName, pos);
    }
}
