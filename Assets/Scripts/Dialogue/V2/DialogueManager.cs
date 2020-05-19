using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    private Queue<Dialogue> dialogues;
    public TextMeshProUGUI DialogueText;
    public GameObject DialogueCanvas;
    public float TypingSpeed;
    public float WaitTime;
    private bool IsTyping;
    private bool IsStarting;
    private Dialogue dialogue;
    System.Action callBack;
    private bool FirstTime;
    Coroutine toNextSentence = null;

    void Start()
    {
        FirstTime = true;
        sentences = new Queue<string>();
        dialogues = new Queue<Dialogue>();
        DialogueCanvas.gameObject.SetActive(false);
    }

    void StartDialogue(Dialogue d)
    {
        dialogue = d;
        IsStarting = true;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void QueueDialogue(Dialogue d)
    {
        IsStarting = true;
        dialogues.Enqueue(d);
    }

    void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        DialogueCanvas.SetActive(true);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        toNextSentence = StartCoroutine(Type(sentence));
    }

    void EndDialogue()
    {
        if (dialogue.callBack != null)
            dialogue.callBack();
        dialogue = null;
        DialogueCanvas.SetActive(false);
    }

    IEnumerator Type(string sentence)
    {
        IsTyping = true;
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            if (IsTyping)
                yield return new WaitForSeconds(1 / TypingSpeed);
        }
        IsTyping = false;
        if (!dialogue.RequiresConfirmation)
        {
            yield return new WaitForSeconds(WaitTime);
            toNextSentence = null;
            DisplayNextSentence();
        }
    }

    public void Update()
    {
        if (!IsStarting && Input.GetButtonDown("Interact") && dialogue != null)
        {
            if (IsTyping)
                IsTyping = false;
            else
            {
                if (toNextSentence != null)
                {
                    StopCoroutine(toNextSentence);
                }
                DisplayNextSentence();
            }
        }
        IsStarting = false;
        if (dialogue == null && dialogues.Count != 0)
        {
            StartDialogue(dialogues.Dequeue());
        }
    }
}
