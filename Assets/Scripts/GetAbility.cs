using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    public AbilityType abilityType;
    private Dialogue dialogue;
    public float Range = 1f;
    Player player;
    // Start is called before the first frame update
    public string[] DialogueText;
    private bool ToSend;

    private bool PowerSent;
    private bool DialogueSent;
    /*
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogue = GameObject.FindObjectOfType<Dialogue>();
        PowerSent = false;
        DialogueSent = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Interact") || Input.GetKeyDown(KeyCode.Z)) &&
            Vector3.Distance(transform.position, player.transform.position) < Range)
            {
                switch(DialogueSent)
                {
                    case true:
                        DialogueSent = false;
                        break;
                    
                    default:
                        for(int i = 0; i < DialogueText.Length; i++)
                        {
                            //dialogue.AddSentence(DialogueText[i]);
                        }
                        if(!PowerSent)
                        {
                            //dialogue.ReceiveAbility(abilityType);
                            PowerSent = true;
                        }
                        DialogueSent = true;
                        break;
                }
        }
    }*/
}
