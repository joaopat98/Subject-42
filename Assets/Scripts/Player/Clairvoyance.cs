using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Clairvoyance : Ability
{
    private PostProcessVolume detectiveFilter;
    private GameObject[] objects;

    private bool isActive = false;

    public Clairvoyance(Player player) : base(player)
    {
        this.detectiveFilter =  Camera.main.GetComponentInChildren<PostProcessVolume>();
        this.detectiveFilter.enabled = false;
        this.objects = GameObject.FindGameObjectsWithTag("Clairvoyance");
        activeObjects(false);
    }
   
    public void activeObjects(bool val)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            //objects[i].SetActive(val);
            objects[i].GetComponent<Renderer>().enabled = val;
            objects[i].GetComponent<PostProcessVolume>().enabled = val;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        if(player.GetComponent<Rigidbody>().velocity.magnitude >= 5.0f)
        {
            isActive = false;
            activeObjects(false);
            this.detectiveFilter.enabled = false;
        }
        else if (Input.GetButtonDown("Fire3") && !isActive)
        {
            isActive = true;
            Debug.Log("Clairvoyance");
            activeObjects(true);
            this.detectiveFilter.enabled = true;

        }
        else if(Input.GetButtonDown("Fire3") && isActive)
        {
            isActive = false;
            activeObjects(false);
            this.detectiveFilter.enabled = false;
        }
    }

    public override void SwitchAbility(int delta)
    {
        activeObjects(false);
        this.detectiveFilter.enabled = false;
        base.SwitchAbility(delta);
    }
}
