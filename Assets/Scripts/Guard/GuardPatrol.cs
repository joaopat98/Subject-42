using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : GuardAction
{
    int curDest = 0;
    List<Vector3> path;
    public GuardPatrol(Guard guard) : base(guard)
    {
        agent.speed = guard.PatrolSpeed;
        agent.stoppingDistance = guard.PatrolReachDistance;
        path = guard.DefaultPath.GetAllNodes();
        agent.SetDestination(path[0]);
    }

    public override void Do()
    {
        // Go to next path node if the current one has been reached
        if (guard.HasReachedGoal())
        {
            curDest = (curDest + 1) % path.Count;
            agent.SetDestination(path[curDest]);
        }
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
    }

}