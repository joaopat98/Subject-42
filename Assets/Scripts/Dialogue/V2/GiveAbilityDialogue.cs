using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAbilityDialogue : MonoBehaviour
{
    Player player;
    public float Range = 1f;
    public Dialogue BeforeDialogue, ExplainDialogue, AfterDialogue;
    public AbilityType AbilityToGive;
    DialogueManager dialogueManager;
    private bool hasTalked;
    private bool isTalking;

    void PlayAnimAndGiveAbility()
    {
        StartCoroutine(PlayAnimAndGiveAbilityRoutine());
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(3.5f);
        player.Sounds.PlayOnce("ObtainAbility");
    }

    IEnumerator PlayAnimAndGiveAbilityRoutine()
    {
        Debug.Log("Receiving Power");
        player.anim.SetTrigger("ObtainPower");
        yield return new WaitForSeconds(8.5f);
        player.AddAbility(AbilityToGive);
        dialogueManager.QueueDialogue(ExplainDialogue);
    }

    IEnumerator ResetTalking(float time)
    {
        yield return new WaitForSeconds(time);
        isTalking = false;
    }

    private void ReleasePlayer()
    {
        StartCoroutine(ResetTalking(0.5f));
        player.Playing = true;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        BeforeDialogue.callBack = PlayAnimAndGiveAbility;
        ExplainDialogue.callBack = ReleasePlayer;
        AfterDialogue.callBack = ReleasePlayer;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact")
            && !isTalking
            && Vector3.Distance(transform.position, player.transform.position) < Range)
        {
            if (!hasTalked)
            {
                isTalking = true;
                hasTalked = true;
                player.Playing = false;
                dialogueManager.QueueDialogue(BeforeDialogue);
            }
            else
            {
                isTalking = true;
                player.Playing = false;
                dialogueManager.QueueDialogue(AfterDialogue);
            }
        }
    }
}