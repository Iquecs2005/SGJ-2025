using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueTxt;
    public float tempoEntreLetras = 0.05f;
    public float tempoEntreFalas = 1f;

    private DialogueAssets currentDialogue;
    private int currentIndex;

    [HideInInspector] public static DialogueManager instance { get; private set; }


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

    public void StartDialogue(DialogueAssets dialogueData)
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(true);

        currentDialogue = dialogueData;
        currentIndex = 0;

        StartCoroutine(ShowDialogueLines(dialogueData.dialogue, currentIndex));
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
        dialogueTxt.text = null;
    }

    IEnumerator SpeedText(string line)
    {
        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;
            dialogueTxt.text = textBuffer;
            yield return new WaitForSeconds(tempoEntreLetras);
        }
    }

    IEnumerator ShowDialogueLines(string[] lines, int startIndex)
    {
        for (int i = startIndex; i < lines.Length; i++)
        {
            // SFXManager.PlaySFX(name, soundPosition);

            yield return StartCoroutine(SpeedText(lines[i]));
            yield return new WaitForSeconds(tempoEntreFalas);

            currentIndex++;
        }

        EndDialogue();

    }

    public int PauseDialogue()
    {
        EndDialogue();
        return currentIndex;
    }

    public void ResumeDialogue(DialogueAssets dialogueData, int desiredIndex)
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(true);

        currentDialogue = dialogueData;
        currentIndex = desiredIndex;

        StartCoroutine(ShowDialogueLines(currentDialogue.dialogue, currentIndex));
    }

    public void PauseDialogueButton()
    {
        PauseDialogue();
    }

    public void ResumeDialogueButton()
    {
        ResumeDialogue(currentDialogue, currentIndex);
    }
}