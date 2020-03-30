using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityAbility : Ability
{
    /// <summary>
    /// Field of view where the player can use the power
    /// </summary>
    [Range(0, 180)] public float ViewAngle = 45;

    /// <summary>
    /// Distance in front of the player. It limits how far can he use the power.
    /// </summary>
    public float ViewRange = 7f;

    public ElectricityAbility(Player player) : base(player)
    {

    }
    public override void Update()
    {
        var ElectricObjects = ElectricObject.FindObjectsOfType(typeof(ElectricObject));
        ElectricObject currentClosetObject = null;
        float currentLowestAngle = Mathf.Infinity;

        foreach (ElectricObject electricObj in ElectricObjects)
        {
            
            if (Vector3.Angle(player.transform.forward, electricObj.transform.position - player.transform.position) < currentLowestAngle 
                && Vector3.Distance(player.transform.position, electricObj.transform.position) <= ViewRange
                && Vector3.Angle(player.transform.forward, electricObj.transform.position - player.transform.position) < ViewAngle)
            {
                currentClosetObject = electricObj;
                electricObj.objectRender.material = electricObj.electricMaterial;
                currentLowestAngle = (Vector3.Angle(player.transform.forward, electricObj.transform.position - player.transform.position));
            }
            else
            {
                electricObj.objectRender.material = electricObj.normalMaterial;
            }
        }
       
        if (Input.GetButtonDown("Fire3"))
        {
            
            if (currentClosetObject != null)
            { 
                currentClosetObject.Activate();
            }  
        }
    }


}
