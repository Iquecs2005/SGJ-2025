using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableOject", menuName = "Dialogue/New Dialogue")]
public class DialogueAsset : ScriptableObject
{
    public DialogueLine[] dialogueData;
}

[Serializable]
public class DialogueLine 
{
    [TextArea]
    public string lineText;
    public string textSoundName;
}
