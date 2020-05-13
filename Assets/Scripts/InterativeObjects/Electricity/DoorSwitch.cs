﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IElectricObject
{
    bool isDoorOpened;
    Player player;
    Outline outline;
    public GameObject[] doors;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isDoorOpened = false;
        outline = GetComponentInChildren<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = player.ElectricityColor;
    }
    public void Activate()
    {
        if (!isDoorOpened)
        {
            foreach(GameObject door in doors)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 1, door.transform.position.z);
            }
            isDoorOpened = true;
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
