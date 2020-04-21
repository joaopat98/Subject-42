using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupCollectible : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;
    public string NameContent;
    [TextArea(3, 10)]
    public string DescriptionContent;

    public void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            inventory.AddCollectible(this);
            Destroy(gameObject);
        }
    }

}
