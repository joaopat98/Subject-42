using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IElectricObject
{
    Renderer ObjectRenderer;
    public Material ElectricMaterial;
    public Material NormalMaterial;
    public Light RoomLight;
    bool TurnedOn;

    public void Start()
    {
        ObjectRenderer = GetComponent<Renderer>();
    }
    public void Activate()
    {
        if(TurnedOn)
        {
            TurnedOn = false;
            RoomLight.intensity = 0.5f;
        }
        else
        {
            TurnedOn = true;
            RoomLight.intensity = 1.0f;
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
            ObjectRenderer.material = ElectricMaterial;
        }
        else
        {
            ObjectRenderer.material = NormalMaterial;
        }
    }
}
