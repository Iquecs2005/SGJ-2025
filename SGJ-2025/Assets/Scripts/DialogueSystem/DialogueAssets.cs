using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableOject", menuName = "Dialogue/New Dialogue")]
public class DialogueAssets : ScriptableObject
{
    [TextArea]
    public string[] dialogue;
    // public AudioClip[] sound;
}
