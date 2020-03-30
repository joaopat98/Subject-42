using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        var ElectricObjects = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IElectricObject>();
        IElectricObject currentClosestObject = null;
        float currentLowestAngle = Mathf.Infinity;

        foreach (IElectricObject electricObj in ElectricObjects)
        {

            if (Vector3.Angle(player.transform.forward, electricObj.GetSelectionPosition() - player.transform.position) < currentLowestAngle
                && Vector3.Distance(player.transform.position, electricObj.GetSelectionPosition()) <= ViewRange
                && Vector3.Angle(player.transform.forward, electricObj.GetSelectionPosition() - player.transform.position) < ViewAngle)
            {
                currentClosestObject = electricObj;
                currentLowestAngle = (Vector3.Angle(player.transform.forward, electricObj.GetSelectionPosition() - player.transform.position));
            }
        }
        foreach (IElectricObject electricObj in ElectricObjects)
        {
            electricObj.Highlight(electricObj == currentClosestObject);
        }
        if (Input.GetButtonDown("Fire3"))
        {
            if (currentClosestObject != null)
            {
                currentClosestObject.Activate();
            }
        }

    }
}
