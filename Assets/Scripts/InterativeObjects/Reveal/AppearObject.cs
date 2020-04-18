﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AppearObject : MonoBehaviour, IRevealObject
{
    public void Start()
    {
        UnRevealObject();
    }
    public void RevealObject()
    {
        this.GetComponent<Renderer>().enabled = true;
        this.GetComponent<PostProcessVolume>().enabled = true;
    }

    public void UnRevealObject()
    {
        this.GetComponent<Renderer>().enabled = false;
        this.GetComponent<PostProcessVolume>().enabled = false;
    }

}