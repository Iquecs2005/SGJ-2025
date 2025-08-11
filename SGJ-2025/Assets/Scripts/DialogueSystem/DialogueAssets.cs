using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueAssets : ScriptableObject
{
    [TextArea]
    public string[] dialogue;
    // public AudioClip[] sound;
}
