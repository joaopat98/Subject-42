using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElectricityAbility : Ability
{


    public ElectricityAbility(Player player) : base(player)
    {

    }

    List<IElectricObject> GetElectricObjects()
    {
        return GameObject.FindObjectsOfType<MonoBehaviour>().Where(obj => obj.enabled).OfType<IElectricObject>().ToList();
    }

    IElectricObject GetClosestObject()
    {

        IElectricObject currentClosestObject = null;
        float currentLowestAngle = Mathf.Infinity;

        foreach (IElectricObject obj in GetElectricObjects())
        {
            RaycastHit hit;
            bool didHit = Physics.Raycast(
                player.Center,
                transform.forward,
                out hit,
                player.ViewRange,
                ~LayerMask.GetMask("Player")
            );
            if (Vector3.Angle(player.transform.forward, obj.GetSelectionPosition() - player.transform.position) < currentLowestAngle
                && Vector3.Distance(player.transform.position, obj.GetSelectionPosition()) <= player.ViewRange
                && Vector3.Angle(player.transform.forward, obj.GetSelectionPosition() - player.transform.position) < player.ViewAngle
                && (!didHit || hit.collider.gameObject.layer == LayerMask.NameToLayer("Electricity")))
            {
                currentClosestObject = obj;
                currentLowestAngle = (Vector3.Angle(player.transform.forward, obj.GetSelectionPosition() - player.transform.position));
            }
        }
        if (currentClosestObject == null)
        {
            RaycastHit hit;
            if (Physics.SphereCast(
                player.Center,
                player.CastSelectRadius,
                transform.forward,
                out hit,
                player.ViewRange,
                ~LayerMask.GetMask("Player"))
                && hit.collider.gameObject.layer == LayerMask.NameToLayer("Electricity"))
            {
                currentClosestObject = hit.collider.GetComponent<ElectricCollider>().GetElectricObject();
            }
        }
        return currentClosestObject;
    }
    public override void Update()
    {
        IElectricObject currentClosestObject = GetClosestObject();
        foreach (IElectricObject electricObj in GetElectricObjects())
        {
            electricObj.Highlight(electricObj == currentClosestObject);
        }
        if (Input.GetButtonDown("Power"))
        {
            if (currentClosestObject != null && !player.IsAnimTrigger())
            {
                player.anim.SetTrigger("Electricity");
                currentClosestObject.Activate();
            }
        }

    }
}
