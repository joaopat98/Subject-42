using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricObject : MonoBehaviour, IElectricObject
{

    // Electric Object Type
    public ElectricObjectType type;


    public MeshRenderer objectRender;
    public Material electricMaterial;
    public Material normalMaterial;
    public bool isDoorOpened = false;
    public GameObject door;
    public Light light;

    public void Start()
    {
        
    }
    public void Activate()
    {
        switch (type)
        {
            case ElectricObjectType.LightsSwitch:
                if(light.intensity > 0.25f)
                {
                    light.intensity = 0.25f;
                }
                else
                {
                    light.intensity = 1.0f;
                }
                break;
            case ElectricObjectType.DoorSwitch:
                if (!isDoorOpened)
                {
                    door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 1, door.transform.position.z);
                    isDoorOpened = true;
                }
                break;
            default:
                break;
        }
        

    }
}
