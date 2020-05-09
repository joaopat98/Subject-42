using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class RevealAbility : Ability
{
    private Volume detectiveFilter;
    private List<IRevealObject> objects;

    private bool isActive = false;

    public RevealAbility(Player player) : base(player)
    {
        this.detectiveFilter = GameObject.Find("RevealVolume").GetComponent<Volume>();
        this.detectiveFilter.enabled = false;
        objects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IRevealObject>().ToList();
    }

    public override void Update()
    {
        if (player.GetComponent<Rigidbody>().velocity.magnitude >= player.clairVoyanceMaxSpeed && isActive)
        {
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity.normalized * player.clairVoyanceMaxSpeed;
        }
        if (Input.GetButtonDown("Power") && !isActive)
        {
            FadeInColor(player.RevealColor);
            isActive = true;
            foreach (IRevealObject obj in objects)
            {
                obj.RevealObject();
            }
            this.detectiveFilter.enabled = true;

        }
        else if (Input.GetButtonDown("Power") && isActive)
        {
            isActive = false;
            FadeOutColor();
            foreach (IRevealObject obj in objects)
            {
                obj.UnRevealObject();
            }
            this.detectiveFilter.enabled = false;
        }
    }

    public override void SwitchAbility(int delta)
    {
        FadeOutColor();
        foreach (IRevealObject obj in objects)
        {
            obj.UnRevealObject();
        }
        this.detectiveFilter.enabled = false;
        base.SwitchAbility(delta);
    }
}
