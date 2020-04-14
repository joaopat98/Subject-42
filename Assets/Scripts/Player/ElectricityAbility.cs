using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElectricityAbility : Ability
{


    public override void Update()
    {

        var ElectricObjects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IElectricObject>();
        IElectricObject currentClosestObject = null;
        float currentLowestAngle = Mathf.Infinity;

        foreach (IElectricObject electricObj in ElectricObjects)
        {

            if (Vector3.Angle(player.transform.forward, electricObj.GetSelectionPosition() - player.transform.position) < currentLowestAngle
                && Vector3.Distance(player.transform.position, electricObj.GetSelectionPosition()) <= player.ViewRange
                && Vector3.Angle(player.transform.forward, electricObj.GetSelectionPosition() - player.transform.position) < player.ViewAngle)
            {
                currentClosestObject = electricObj;
                currentLowestAngle = (Vector3.Angle(player.transform.forward, electricObj.GetSelectionPosition() - player.transform.position));
            }
        }
        foreach (IElectricObject electricObj in ElectricObjects)
        {
            electricObj.Highlight(electricObj == currentClosestObject);
        }
        if (Input.GetButtonDown("Power"))
        {
            if (currentClosestObject != null)
            {
                currentClosestObject.Activate();
            }
        }

    }
    public override void SwitchAbility(int delta)
    {
        var ElectricObjects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IElectricObject>();
        foreach (IElectricObject electricObj in ElectricObjects)
        {
            electricObj.Highlight(false);
        }
        base.SwitchAbility(delta);
    }
}
