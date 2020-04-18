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
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player" && Input.GetKeyDown(KeyCode.Z))
        {
            inventory.Names.Add(NameContent);
            inventory.Descriptions.Add(DescriptionContent);
            inventory.current = inventory.Names.Count-1;
            Instantiate(itemButton, inventory.slots[inventory.current].transform, false);
            Destroy(gameObject);
        }
    }

}
