using UnityEngine;

public class GuardChase : GuardAction
{
    public GuardChase(Guard guard) : base(guard)
    {
        Debug.Log("Chasing");
        agent.stoppingDistance = guard.ChaseReachDistance;
        agent.speed = guard.ChaseSpeed;
    }

    public override void Do()
    {
        agent.SetDestination(player.transform.position);
        if (Vector3.Distance(guard.transform.position, player.transform.position) > guard.ChaseRange)
        {
            guard.action = new GuardPatrol(guard);
        }
    }
}