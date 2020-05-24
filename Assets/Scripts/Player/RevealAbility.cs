using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class RevealAbility : Ability
{
    private Volume detectiveFilter;
    private RenderFeatureManager renderFeatureManager;
    private List<IRevealObject> objects;

    private bool isActive = false;

    public RevealAbility(Player player) : base(player)
    {
        this.detectiveFilter = GameObject.Find("RevealVolume").GetComponent<Volume>();
        this.detectiveFilter.enabled = false;
        objects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IRevealObject>().ToList();
        renderFeatureManager = player.GetComponent<RenderFeatureManager>();
    }


    public override void Update()
    {
        if (player.GetComponent<Rigidbody>().velocity.magnitude >= player.RevealMaxSpeed && isActive)
        {
            player.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity.normalized * player.RevealMaxSpeed;
        }
        if (Input.GetButtonDown("Power") && !isActive)
        {
            isActive = true;
            player.StartCoroutine(ActivatePowerAndAnimation());

        }
        else if (Input.GetButtonDown("Power") && isActive)
        {
            isActive = false;
            player.Sounds.StopLoop("LoopReveal");
            FadeOutColor();
            foreach (IRevealObject obj in objects)
            {
                obj.UnRevealObject();
            }
            renderFeatureManager.SeeEnemiesBehind(false);
            this.detectiveFilter.enabled = false;
        }
    }

    IEnumerator ActivatePowerAndAnimation()
    {
        player.Sounds.PlayOnce("StartReveal");
        player.Sounds.PlayLoop("LoopReveal");
        
        player.anim.SetTrigger("Reveal");
        yield return new WaitForSeconds(2.0f);
        foreach (IRevealObject obj in objects)
        {
            obj.RevealObject();
        }
        FadeInColor(player.RevealColor);
        renderFeatureManager.SeeEnemiesBehind(true);
        this.detectiveFilter.enabled = true;


    }
    public override void SwitchAbility(int delta)
    {
        FadeOutColor();
        foreach (IRevealObject obj in objects)
        {
            obj.UnRevealObject();
        }
        renderFeatureManager.SeeEnemiesBehind(false);
        this.detectiveFilter.enabled = false;
        base.SwitchAbility(delta);
    }
}
