using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitch : MonoBehaviour, IElectricObject
{

    bool isActive;
    Player player;
    Outline outline;
    public GameObject platform;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isActive = false;
        outline = GetComponentInChildren<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = player.ElectricityColor;
    }

    public void Activate()
    {
        if (isActive)
        {

        }
    }

    public Vector3 GetSelectionPosition()
    {
        return this.transform.position;
    }

    public void Highlight(bool IsActive)
    {
        outline.enabled = IsActive;
    }

}
