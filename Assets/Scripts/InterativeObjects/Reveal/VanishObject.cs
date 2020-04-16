using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VanishObject : MonoBehaviour, IRevealObject
{
   
    public void RevealObject()
    {
        this.gameObject.SetActive(false);

        
    }

    public void UnRevealObject()
    {
        this.gameObject.SetActive(true);
       
        
    }




}
