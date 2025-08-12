using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector] public static DialogueManager instance { get; private set; }

    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueTxt;
    public float tempoEntreLetras = 0.05f;
    public float tempoEntreFalas = 1f;

    private DialogueAsset currentDialogue;
    private DialogueLine currentLine;
    private int currentIndex;
    private Vector2 currentSpeakerPosition;
    private GameObject soundSourceObject = null;

    private List<QueueElement> dialogQueue;
    private bool OnDialog = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void StartDialogue(DialogueAsset dialogueData, Vector2 speakerPos, int StartIndex = 0)
    {
        if (OnDialog) return;

        OnDialog = true;
        dialoguePanel.SetActive(true);

        currentDialogue = dialogueData;
        currentIndex = StartIndex;
        currentSpeakerPosition = speakerPos;

        StartCoroutine(ShowDialogueLines());
    }

    private IEnumerator ShowDialogueLines()
    {
        DialogueLine[] dialogueLines = currentDialogue.dialogueData;

        while (currentIndex < dialogueLines.Length)
        {
            currentLine = dialogueLines[currentIndex];

            if (currentLine.textSoundName != null)
                soundSourceObject = SFXManager.PlaySFX(currentLine.textSoundName, currentSpeakerPosition);

            yield return StartCoroutine(SpeedText());
            yield return new WaitForSeconds(tempoEntreFalas);

            currentIndex++;
        }

        OnDialogueEnd();
    }

    private IEnumerator SpeedText()
    {
        string textBuffer = null;
        foreach (char c in currentLine.lineText)
        {
            textBuffer += c;
            dialogueTxt.text = textBuffer;
            yield return new WaitForSeconds(tempoEntreLetras);
        }
    }

    private void OnDialogueEnd()
    {
        dialoguePanel.SetActive(false);
        soundSourceObject = null;
        dialogueTxt.text = null;
        currentDialogue = null;
        currentIndex = 0;
        currentSpeakerPosition = Vector2.zero;
        OnDialog = false;
    }

    public void EndDialog() 
    {
        StopAllCoroutines();
        OnDialogueEnd();
    }

    public void PauseDialogue()
    {
        StopAllCoroutines();

        if (soundSourceObject != null) 
        {
            Destroy(soundSourceObject);
        }
        dialoguePanel.SetActive(false);
        dialogueTxt.text = null;
        OnDialog = false;
    }

    public void ResumeDialogue()
    {
        StartDialogue(currentDialogue, currentSpeakerPosition, currentIndex);
    }

    private struct QueueElement 
    {
        public DialogueAsset dialogAsset;
        public Vector2 speakerPosition;
    }
}