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

    void Update()
    {
        if (Input.GetButtonDown("Start Button")){show = !show;}
        
                if(show && Names.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.K)){
                if(current > 0){
                    current--;
                }
            }

            else if (Input.GetKeyDown(KeyCode.L)){
                if(current <  Names.Count-1){
                    current++;
                }
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
