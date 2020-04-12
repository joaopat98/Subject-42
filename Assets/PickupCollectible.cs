using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupCollectible : MonoBehaviour
{

    public Text Name;
    public Text Description;
    public string NameContent;
    public string DescriptionContent;

    void OnTriggerStay(Collider collider)
    {   
        if(collider.tag == "Player" && Input.GetKeyDown(KeyCode.Z))
        {
            Name.text = NameContent;
            Description.text = DescriptionContent;
            Destroy(gameObject);
        }
    }

}
