using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrol : GuardAction
{
    int curDest = 0;
    List<Vector3> path;
    public GuardPatrol(Guard guard) : base(guard)
    {
        Debug.Log("Patrolling");
        agent.speed = guard.PatrolSpeed;
        agent.stoppingDistance = guard.PatrolReachDistance;
        path = guard.DefaultPath.GetAllNodes();
        agent.SetDestination(path[0]);
    }

    public override void Do()
    {
        if (guard.HasReachedGoal())
        {
            curDest = (curDest + 1) % path.Count;
            agent.SetDestination(path[curDest]);
        }
        if (
            Vector3.Distance(guard.transform.position, player.transform.position) < guard.ViewRange
            && Vector3.Angle(guard.transform.forward, player.transform.position - guard.transform.position) < guard.ViewAngle
            )
        {
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