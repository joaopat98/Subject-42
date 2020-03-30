using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Clairvoyance : Ability
{
    private PostProcessVolume detectiveFilter;
    private GameObject[] objects;

    public Clairvoyance(Player player) : base(player)
    {
        this.detectiveFilter =  player.GetComponent<PostProcessVolume>();
        this.detectiveFilter.enabled = false;
        this.objects = GameObject.FindGameObjectsWithTag("Clairvoyance");
        activeObjects(false);
    }
   
    public void activeObjects(bool val)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(val);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("Clairvoyance");
            activeObjects(true);
            this.detectiveFilter.enabled = true;

        }
    }

    public override void SwitchAbility(int delta)
    {
        activeObjects(false);
        this.detectiveFilter.enabled = false;
        base.SwitchAbility(delta);
    }
}
