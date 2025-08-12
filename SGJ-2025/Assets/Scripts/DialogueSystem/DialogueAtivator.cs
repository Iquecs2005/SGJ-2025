using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAtivator : MonoBehaviour
{
    [SerializeField] private DialogueAsset activatedDialogue;
    [SerializeField] private Transform speakerPos;

    public void ActivateDialog() 
    {
        if (speakerPos == null) 
        {
            speakerPos = transform;    
        }

        DialogueManager.instance.StartDialogue(activatedDialogue, speakerPos.position);
    }
}
