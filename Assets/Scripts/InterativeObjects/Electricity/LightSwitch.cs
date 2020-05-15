using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class LightSwitch : MonoBehaviour, IElectricObject
{
    Player player;
    Outline outline;
    bool TurnedOn;
    public Light RoomLight;
    public int TimeToTurnBackOn;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        TurnedOn = true;
        outline = GetComponentInChildren<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = player.ElectricityColor;
    }
    public void Activate()
    {
        if(TurnedOn)
        {
            TurnedOn = false;
            RoomLight.intensity = 0.5f;
            player.StartCoroutine(TurnOnTheLights());
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
    IEnumerator TurnOnTheLights()
    {
        yield return new WaitForSeconds(TimeToTurnBackOn);
        RoomLight.intensity = 1.0f;
        TurnedOn = true;
    }
}
