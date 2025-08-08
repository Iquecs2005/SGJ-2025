using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScript : MonoBehaviour
{
    [SerializeField] private string audioName;

    public void PlayAudio() 
    {
        SFXManager.PlaySFX(audioName, new Vector3(0,0,0));
    }
}
