using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class DoorSwitch : MonoBehaviour, IElectricObject
{
    bool isDoorOpened;
    Player player;
    Outline outline;

    public bool HasDialog;
    public Dialogue AfterDialogue;
    public GameObject[] doors;

    DialogueManager dialogueManager;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isDoorOpened = false;
        dialogueManager = FindObjectOfType<DialogueManager>();
        outline = GetComponentInChildren<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = player.ElectricityColor;
    }
    public void Activate()
    {
        Debug.Log("Switch Activate");
        if (!isDoorOpened)
        {
            foreach (GameObject door in doors)
            {
                door.GetComponentInChildren<Animator>().SetBool("isOpen", true);
            }
            isDoorOpened = true;
            if (HasDialog)
            {
                Debug.Log("Door Dialogue");
                dialogueManager.QueueDialogue(AfterDialogue);
            }
        }
    }

    public Vector3 GetSelectionPosition()
    {
        return this.transform.position;
    }

    public void Highlight(bool IsActive)
    {
        outline.enabled = IsActive;
    }
}
