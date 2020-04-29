using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RevealAbility : Ability
{
    private PostProcessVolume detectiveFilter;
    private List<IRevealObject> objects;

    private bool isActive = false;

    public RevealAbility(Player player) : base(player)
    {
        this.detectiveFilter = Camera.main.GetComponentInChildren<PostProcessVolume>();
        this.detectiveFilter.enabled = false;
        objects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IRevealObject>().ToList();
    }

    public override void Update()
    {
        if(player.GetComponent<Rigidbody>().velocity.magnitude >= player.RevealMaxSpeed && isActive)
        {
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity.normalized * player.RevealMaxSpeed;
        }
        if (Input.GetButtonDown("Power")&& !isActive)
        {
            isActive = true;
            foreach(IRevealObject obj in objects)
            {
                obj.RevealObject();
            }
            this.detectiveFilter.enabled = true;

        }
        else if(Input.GetButtonDown("Power") && isActive)
        {
            isActive = false;
            foreach (IRevealObject obj in objects)
            {
                obj.UnRevealObject();
            }
            this.detectiveFilter.enabled = false;
        }
    }

    public override void SwitchAbility(int delta)
    {
        foreach (IRevealObject obj in objects)
        {
            obj.UnRevealObject();
        }
        this.detectiveFilter.enabled = false;
        base.SwitchAbility(delta);
    }
}
