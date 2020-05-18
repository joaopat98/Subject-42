using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public TextMeshProUGUI DialogueText;
    public GameObject DialogueCanvas;
    public float typingSpeed;

    void Start()
    {
        sentences = new Queue<string>();
        DialogueCanvas.gameObject.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        DialogueCanvas.gameObject.SetActive(false);
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueCanvas.gameObject.SetActive(true);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Type(sentence));
    }

    void EndDialogue()
    {
        DialogueCanvas.gameObject.SetActive(false);
        FindObjectOfType<DialogueTrigger>().StopDialogue();
    }

    IEnumerator Type(string sentence)
    {
        DialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
