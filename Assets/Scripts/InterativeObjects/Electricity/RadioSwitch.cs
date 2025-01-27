﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSwitch : MonoBehaviour, IElectricObject
{
    private Player player;
    private Outline outline;
    Renderer ObjectRenderer;
    public bool isRadioActivated = false;
    public Collider originalBoxColl;
    public Collider radioRangeCollider;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        outline = GetComponentInChildren<Outline>();
        outline.OutlineColor = player.ElectricityColor;
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        ObjectRenderer = GetComponent<Renderer>();
        originalBoxColl.enabled = true;
        radioRangeCollider.enabled = false;
    }
    public void Activate()
    {
        if (!isRadioActivated)
        {
            isRadioActivated = true;
            this.originalBoxColl.enabled = false;
            this.radioRangeCollider.enabled = true;
            StartCoroutine(RadioActivated());
            player.Sounds.PlayOnce("Radio");
        }
    }

    IEnumerator RadioActivated()
    {

        yield return new WaitForSeconds(3.0f);
        this.originalBoxColl.enabled = true;
        this.radioRangeCollider.enabled = false;
    }

    public Vector3 GetSelectionPosition()
    {
        return this.transform.position;
    }

    public void Highlight(bool isActive)
    {
        outline.enabled = isActive;
    }

}
