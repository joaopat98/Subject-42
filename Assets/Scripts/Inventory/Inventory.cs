using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{   
    public GameObject[] slots;
    private bool show = false;
    public GameObject Canvas;
    public GameObject Select;
    public Text Name;
    public Text Description;
    public int current = 0;
    public List<string> Names = new List<string>();
    public List<string> Descriptions = new List<string>();

    private bool AxisInUse = false;

    void Update()
    {   
        //XBox: 'Back' PS3: 'Select'
        if (Input.GetButtonDown("Switch Left")){show = !show;}
        
                if(show && Names.Count > 0)
        {
            if (Input.GetAxisRaw("DPAD-h") == -1 && !AxisInUse){
                if(current > 0){
                    current--;
                    AxisInUse = true;
                }
            }

            else if (Input.GetAxisRaw("DPAD-h") == 1 && !AxisInUse){
                if(current <  Names.Count-1){
                    current++;
                    AxisInUse = true;
                }
            }

            else if (Input.GetAxisRaw("DPAD-h") == 0){
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

        Canvas.gameObject.SetActive(show);
    }
}
