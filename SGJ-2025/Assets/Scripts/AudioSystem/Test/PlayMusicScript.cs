using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicScript : MonoBehaviour
{
    [SerializeField] private string audioName;

    public void PlayAudio()
    {
        MusicManager.PlayMusic(audioName);
    }
}
