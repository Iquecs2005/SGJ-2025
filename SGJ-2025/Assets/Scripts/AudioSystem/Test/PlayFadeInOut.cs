using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFadeInOut : MonoBehaviour
{
    [SerializeField] private string audioName;

    public void PlayAudio()
    {
        MusicManager.FadeInMusic(audioName);
    }
}
