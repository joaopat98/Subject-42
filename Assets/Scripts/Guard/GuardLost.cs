using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardLost : GuardAction
{
    float t = 0;
    float recoveryTime;
    public GuardLost(Guard guard, float recoveryTime) : base(guard)
    {
        guard.anim.SetBool("Lost", true);
        guard.anim.SetBool("Checking", false);
        guard.anim.SetBool("Chasing", false);
        agent.SetDestination(guard.transform.position);
        this.recoveryTime = recoveryTime;
        guard.ViewRange = guard.LostView;
    }

    public override void Do()
    {
        Debug.Log("Lost");
        t += Time.deltaTime;
        if (t >= recoveryTime)
        {
            Debug.Log("ENTREEE");
            guard.action = new GuardPatrol(guard);
        }
    }

}