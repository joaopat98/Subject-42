using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardTip : MonoBehaviour
{
    static bool hasDied, hasSaidFirst, hasSaidSecond;
    Player player;
    DialogueManager dialogueManager;
    public Dialogue BeforeDying, AfterDying;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (hasDied && !hasSaidSecond)
            {
                hasSaidSecond = true;
                dialogueManager.QueueDialogue(AfterDying);
            }
            else if (!hasSaidFirst)
            {
                hasSaidFirst = true;
                dialogueManager.QueueDialogue(BeforeDying);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDied && !player.isAlive)
        {
            hasDied = true;
        }
    }
}
