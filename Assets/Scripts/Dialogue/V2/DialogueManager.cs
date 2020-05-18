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
    private bool IsStarting;
    Player player;
    public System.Action callBack;
    private bool FirstTime;

    void Start()
    {
        FirstTime = true;
        sentences = new Queue<string>();
        DialogueCanvas.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        IsStarting = true;
        DialogueCanvas.gameObject.SetActive(false);
        sentences.Clear();

        callBack = dialogue.callBack;

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
        IsStarting = true;
        callBack();
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
        IsStarting = false;
    }

    public void AddTelekinesisPower(){
        
        if(FirstTime)
        {
            player.AddAbility(AbilityType.Telekinesis);
            FirstTime = false;
        }
        
    }

    public void Update()
    {
        if (Input.GetButtonDown("Interact") && !IsStarting)
        {
            DisplayNextSentence();
        }
    }
}
