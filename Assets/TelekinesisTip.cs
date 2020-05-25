using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisTip : MonoBehaviour
{
    public Dialogue Tip;
    public Animator FinalDoor;
    public float IdleTime = 15;
    bool gaveTip;
    float timer = 0;
    bool isInside;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isInside = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            isInside = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!gaveTip && !FinalDoor.GetBool("Open"))
        {
            if (isInside)
            {
                timer += Time.deltaTime;
            }
            if (timer >= IdleTime)
            {
                gaveTip = true;
                FindObjectOfType<DialogueManager>().QueueDialogue(Tip);
            }
        }

    }
}
