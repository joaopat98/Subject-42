using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private List<Transform> slots;
    private bool show = false;
    public GameObject Select;
    public Text Name;
    public Text Description;
    [HideInInspector] public int current = 0;
    public List<string> Names = new List<string>();
    public List<string> Descriptions = new List<string>();
    AudioPlayer Sounds;
    private bool AxisInUse = false;

    void Start()
    {
        slots = transform.Find("Panel").Find("Slots").GetChildren();
        Sounds = FindObjectOfType<AudioPlayer>();
    }
    void Update()
    {
        //XBox: 'Back' PS3: 'Select'
        if (Input.GetButtonDown("Inventory"))
        {
            Sounds.PlayOnce("OpenMenu");
            show = !show;
        }

        if (show && Names.Count > 0)
        {
            if (Input.GetAxisRaw("DPad X") == -1 && !AxisInUse)
            {
                if (current > 0)
                {
                    Sounds.PlayOnce("ChangedCollectible");
                    current--;
                    AxisInUse = true;
                }
            }

            else if (Input.GetAxisRaw("DPad X") == 1 && !AxisInUse)
            {
                if (current < Names.Count - 1)
                {
                    Sounds.PlayOnce("ChangedCollectible");
                    current++;
                    AxisInUse = true;
                }
            }

            else if (Input.GetAxisRaw("DPad X") == 0)
            {
                AxisInUse = false;
            }

            Name.text = Names[current];
            Description.text = Descriptions[current];
            Select.transform.position = slots[current].transform.position;
        }

        else
        {
            Name.text = "Empty";
            Description.text = "When you collect an item you'll see its description here.";
        }

        GetComponent<Canvas>().enabled = show;
    }

    public void AddCollectible(PickupCollectible collectible)
    {
        Names.Add(collectible.NameContent);
        Descriptions.Add(collectible.DescriptionContent);
        current = Names.Count - 1;
        Instantiate(collectible.itemButton, slots[Names.Count - 1], false);
    }
}
