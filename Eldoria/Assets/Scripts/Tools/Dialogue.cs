using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public LocalizedString[] localizedLines;
    public float textSpeed;
    private int index;
    private bool isTyping = false;
    private string[] lines;
    private Coroutine typingCoroutine;

    private void Start()
    {
        textComponent.text = string.Empty;
        InitializeLocalizedLines();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleDialogueInput();
        }
    }

    public void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        StartTypingLine();
    }

    public void EndDialogue()
    {
        StopTyping();
        textComponent.text = string.Empty;
    }

    private void StartTypingLine()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeLine());
    }

    private void StopTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        isTyping = false;
    }

    private IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = string.Empty;

        foreach (char c in lines[index])
        {
            textComponent.text += c;
            yield return TimerManager.Instance.StartTimer(textSpeed, null);
        }

        isTyping = false;
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartTypingLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void InitializeLocalizedLines()
    {
        lines = new string[localizedLines.Length];

        for (int i = 0; i < localizedLines.Length; i++)
        {
            int indexCopy = i;
            localizedLines[i].StringChanged += (localizedString) => lines[indexCopy] = localizedString;
            lines[i] = localizedLines[i].GetLocalizedString();
        }
    }

    private void HandleDialogueInput()
    {
        if (isTyping)
        {
            StopTyping();
            textComponent.text = lines[index];
        }
        else if (textComponent.text == lines[index])
        {
            NextLine();
        }
    }
}