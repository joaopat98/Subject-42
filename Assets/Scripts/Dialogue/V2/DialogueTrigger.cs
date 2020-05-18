using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    Player player;
    public float Range = 1f;
    public Dialogue dialogue;
    DialogueManager DialogueManager;
    public bool Playing;

    public void TriggerDialogue()
    {
        DialogueManager.StartDialogue(dialogue);
    }

    public void TriggerNextSentence()
    {
        DialogueManager.DisplayNextSentence();
    }

    public void StopDialogue()
    {
        Playing = false;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        DialogueManager = FindObjectOfType<DialogueManager>();
        Playing = false;
    }

    void Update()
    {
        if ((Input.GetButtonDown("Interact") || Input.GetKeyDown(KeyCode.Z)) &&
            Vector3.Distance(transform.position, player.transform.position) < Range)
        {
            if(!Playing)
            {
                TriggerDialogue();
                Playing = true;
            }

            else
            {
                TriggerNextSentence();
            }
            
        }
    }
}