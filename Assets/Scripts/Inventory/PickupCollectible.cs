using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupCollectible : MonoBehaviour
{
    private Inventory inventory;
    private Dialogue dialogue;
    public Sprite hiddenIcon;
    public Sprite normalIcon;
    public string NameContent;
    [TextArea(3, 10)]
    public string DescriptionContent;
    private string CollectibleText;

    public string CollectibleType;

    [HideInInspector]
    public int CollectibleId;

    void Awake()
    {
        CollectibleId = transform.position.GetHashCode();
    }

    void Start()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().Sounds.PlayOnce("ObtainCollectible");
            inventory.AddCollectible(this);
            DataCollector.AddCollectible();
            Destroy(gameObject);
        }
    }

}
