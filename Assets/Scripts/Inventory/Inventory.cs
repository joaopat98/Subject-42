using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class Inventory : MonoBehaviour
{
    private List<Transform> slots;
    private bool show = false;
    public GameObject Select;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    [HideInInspector] public static int current = 0;
    public static List<string> Names = new List<string>();
    public static List<string> Descriptions = new List<string>();
    AudioPlayer Sounds;
    public static List<Sprite> Images = new List<Sprite>();
    private bool AxisInUse = false;
    public Image currentItem;
    private Canvas canvas;

    void Start()
    {
        slots = transform.Find("Panel").Find("Slots").GetChildren();
        Sounds = FindObjectOfType<AudioPlayer>();
        canvas = GetComponent<Canvas>();
        for (int i = 0; i < Images.Count; i++)
        {
            var slot = slots[i].GetComponent<Image>();
            slot.sprite = Images[i];
            slot.color = new Color(slot.color.r, slot.color.g, slot.color.b, 1f);
        }
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
            currentItem.sprite = Images[current];
            currentItem.color = new Color(currentItem.color.r, currentItem.color.g, currentItem.color.b, 1f);
        }
        else
        {
            Name.text = "Empty";
            Description.text = "When you collect an item you'll see its description here.";
        }

        canvas.enabled = show;
    }

    public void AddCollectible(PickupCollectible collectible)
    {
        Names.Add(collectible.NameContent);
        Descriptions.Add(collectible.DescriptionContent);
        current = Names.Count - 1;
        Image newEntrySlot = slots[current].GetComponent<Image>();
        newEntrySlot.sprite = collectible.hiddenIcon;
        newEntrySlot.color = new Color(newEntrySlot.color.r, newEntrySlot.color.g, newEntrySlot.color.b, 1f);
        Images.Add(collectible.normalIcon);
    }
}
