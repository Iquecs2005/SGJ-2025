using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueTxt;
    public float tempoEntreLetras;
    public float tempoEntreFalas;

    private DialogueAssets dialogue;
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
        StartCoroutine(ShowDialogueLines(dialogueData.dialogue));

        currentIndex = 0;
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
            yield return new WaitForSeconds(1 / tempoEntreLetras);
        }
    }

    IEnumerator ShowDialogueLines(string[] lines)
    {
        foreach (string line in lines)
        {
            yield return StartCoroutine(SpeedText(line));
            yield return new WaitForSeconds(tempoEntreFalas);

            currentIndex++;
        }
        EndDialogue();
    }

    public int PauseDialogue(DialogueAssets dialogueData)
    {
        EndDialogue();
        return currentIndex;
    }

    public void ResumeDialogue(DialogueAssets dialogueData, int desiredIndex)
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(true);
        // StartCoroutine(ShowDialogueLines());
    }
}