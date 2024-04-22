using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    private bool isTyping = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndDialogue();
        }
    }

    void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
}
