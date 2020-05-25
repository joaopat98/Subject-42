using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class DistractSwitch : MonoBehaviour, IElectricObject
{
    Player player;
    Outline outline;
    bool TurnedOn;
    public Light[] Lights;
    public Guard[] Guards;
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
           
            TurnedOn = false;
            foreach (var light in Lights)
            {
                light.enabled = false;
            }
            foreach (var guard in Guards)
            {
                Debug.Log("Apague la luz");
                guard.action = new GuardLost(guard, TimeToTurnBackOn);
            }
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
        foreach (var light in Lights)
        {
            light.enabled = true;
        }
        TurnedOn = true;
    }
}
