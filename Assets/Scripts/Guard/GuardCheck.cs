using System.Collections;
using UnityEngine;

/// <summary>
/// Check an area/object
/// </summary>
public class GuardCheck : GuardAction
{
    private float t;
    private GameObject obj;
    public GuardCheck(Guard guard, GameObject obj) : base(guard)
    {
        Debug.Log("Check sound");
        agent.stoppingDistance = guard.CheckReachDistance;
        agent.speed = guard.CheckSpeed;
        this.obj = obj;
    }

    public override void Do()
    {
        
        // Update the agent's goal to the player's position
        agent.SetDestination(obj.transform.position);

        // Check if the player is within the field of view of the guard or 
        // if its speed and distance is less than a threshold
        if (
            (Vector3.Distance(guard.transform.position, player.transform.position) < guard.ViewRange
            && Vector3.Angle(guard.transform.forward, player.transform.position - guard.transform.position) < guard.ViewAngle)
            ||
            (Vector3.Distance(guard.transform.position, player.transform.position) < guard.playerDistancethreshold
            && player.GetComponent<Rigidbody>().velocity.magnitude > guard.playerSpeedthreshold)
            )
        {
            /*
                Check if there are no obstacles obstructing the 
                guard's line of sight, if not then chase the player
            */
            RaycastHit hit;
            Physics.Raycast(
                guard.transform.position,
                player.transform.position - guard.transform.position,
                out hit,
                guard.ViewRange
                );
            if (hit.collider.CompareTag("Player"))
                guard.action = new GuardChase(guard);
        }


        // Change to patrol mode if player is too far away
        if (Vector3.Distance(guard.transform.position, obj.transform.position) <= agent.stoppingDistance)
        {
            guard.StartCoroutine(CheckAndPatrol());
            

        }


    }

    IEnumerator CheckAndPatrol()
    {
        yield return new WaitForSeconds(guard.TimeToCheck);
        guard.action = new GuardPatrol(guard);
    }
}