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
        if (TurnedOn)
        {
            player.Sounds.PlayOnce("Switch");
            TurnedOn = false;
            RoomLight.enabled = false;
            StartCoroutine(TurnOnTheLights());
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
        player.Sounds.PlayOnce("Switch");
        yield return new WaitForSeconds(0.1f);
        player.Sounds.PlayOnce("LightSwitch");
        yield return new WaitForSeconds(TimeToTurnBackOn);
        RoomLight.enabled = true;
        TurnedOn = true;
    }
}
