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
    public float tempoFadeIn = 0.2f;
    public float tempoFadeOut = 0.5f;
    public float offsetLetraY = -10f;

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
            yield return StartCoroutine(FadeOutText(tempoFadeOut));

            currentIndex++;
        }

        OnDialogueEnd();
    }

    private IEnumerator SpeedText()
    {
        dialogueTxt.text = currentLine.lineText;
        dialogueTxt.ForceMeshUpdate();

        TMP_TextInfo textInfo = dialogueTxt.textInfo;
        int totalChars = textInfo.characterCount;

        dialogueTxt.maxVisibleCharacters = 0;

        for (int i = 0; i < totalChars; i++)
        {
            dialogueTxt.maxVisibleCharacters = i + 1;
            dialogueTxt.ForceMeshUpdate();

            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;
            Color32[] colors = textInfo.meshInfo[materialIndex].colors32;

            // Salva a posição original
            Vector3[] originalVertices = new Vector3[4];
            for (int j = 0; j < 4; j++)
            {
                originalVertices[j] = vertices[vertexIndex + j];
            }

            float elapsed = 0f;
            float duration = tempoFadeIn; // tempo de animação por letra
            float offsetY = offsetLetraY;   // quão "baixo" a letra começa

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                float eased = Mathf.SmoothStep(0, 1, t); // suaviza o movimento

                for (int j = 0; j < 4; j++)
                {
                    Vector3 targetPos = originalVertices[j];
                    targetPos.y += offsetY * (1 - eased); // sobe do offsetY para 0
                    vertices[vertexIndex + j] = targetPos;

                    // (Opcional) Fade-in
                    Color32 c = colors[vertexIndex + j];
                    c.a = (byte)(255 * eased);
                    colors[vertexIndex + j] = c;
                }

                dialogueTxt.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Garante que volta à posição normal e totalmente visível
            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j] = originalVertices[j];
                colors[vertexIndex + j].a = 255;
            }

            dialogueTxt.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);

            yield return new WaitForSeconds(tempoEntreLetras);
        }
    }

    private IEnumerator FadeOutText(float duration)
    {
        dialogueTxt.ForceMeshUpdate();
        TMP_TextInfo textInfo = dialogueTxt.textInfo;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                int meshIndex = charInfo.materialReferenceIndex;
                int vertexIndex = charInfo.vertexIndex;
                Color32[] vertexColors = textInfo.meshInfo[meshIndex].colors32;

                for (int j = 0; j < 4; j++)
                {
                    Color32 c = vertexColors[vertexIndex + j];
                    c.a = (byte)(alpha * 255);
                    vertexColors[vertexIndex + j] = c;
                }
            }

            for (int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                dialogueTxt.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        dialogueTxt.text = "";
    }

    private void OnDialogueEnd()
    {
        StartCoroutine(FadeOutThenClose());
    }

    private IEnumerator FadeOutThenClose()
    {
        yield return StartCoroutine(FadeOutText(tempoFadeOut)); // duration in seconds
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