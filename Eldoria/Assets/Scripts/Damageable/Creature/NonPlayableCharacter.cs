using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayableCharacter : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Dialogue dialogue;

    private void Start()
    {
        if (dialogueCanvas != null)
        {
            dialogueCanvas.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartDialogue();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EndDialogue();
        }
    }

    private void StartDialogue()
    {
        if (dialogueCanvas != null && dialogue != null)
        {
            dialogueCanvas.SetActive(true);
            dialogue.StartDialogue();
        }
    }

    private void EndDialogue()
    {
        if (dialogueCanvas != null && dialogue != null)
        {
            dialogueCanvas.SetActive(false);
            dialogue.EndDialogue();
        }
    }
}
