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
    [HideInInspector] public int current = 0;
    public List<string> Names = new List<string>();
    public List<string> Descriptions = new List<string>();
    AudioPlayer Sounds;
    public List<Sprite> Images = new List<Sprite>();
    private bool AxisInUse = false;
    public Image currentItem;

    void Awake()
    {
        int numbersOfInvetory = FindObjectsOfType(GetType()).Length;
        if (numbersOfInvetory > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }
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
            currentItem.sprite = Images[current];
            currentItem.color = new Color(currentItem.color.r, currentItem.color.g,currentItem.color.b, 1f);
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
        string slotName = "Slot" + Names.Count;
        Image newEntry = GameObject.Find(slotName).GetComponent<Image>();
        newEntry.sprite = collectible.hiddenIcon;
        newEntry.color = new Color(newEntry.color.r, newEntry.color.g, newEntry.color.b, 1f);
        Images.Add(collectible.normalIcon);
    }
}
