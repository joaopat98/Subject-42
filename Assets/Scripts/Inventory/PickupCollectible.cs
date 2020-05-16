using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupCollectible : MonoBehaviour
{
    private Inventory inventory;
    private Dialogue dialogue;
    public GameObject itemButton;
    public string NameContent;
    [TextArea(3, 10)]
    public string DescriptionContent;
    private string CollectibleText;

    public string CollectibleType;

    public void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
        dialogue = GameObject.FindObjectOfType<Dialogue>();
        CollectibleText = "You found " + CollectibleType + "!";
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            inventory.AddCollectible(this);
            dialogue.AddSentence(CollectibleText);
            Destroy(gameObject);
        }
    }

}
