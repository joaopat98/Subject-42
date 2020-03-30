using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IElectricObject
{

    public MeshRenderer ObjectRender;
    public Material ElectricMaterial;
    public Material NormalMaterial;
    public bool isDoorOpened = false;
    public GameObject door;


    public void Activate()
    {
        if (!isDoorOpened)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 1, door.transform.position.z);
            isDoorOpened = true;
        }
    }

    public Vector3 GetSelectionPosition()
    {
        return this.transform.position;
    }

    public void Highlight(bool isActive)
    {
        if (isActive)
        {
            ObjectRender.material = ElectricMaterial;
        }
        else
        {
            ObjectRender.material = NormalMaterial;
        }
    }
}
