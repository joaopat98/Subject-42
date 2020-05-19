using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardLost : GuardAction
{
    float t = 0;
    float recoveryTime;
    float prevRange;
    public GuardLost(Guard guard, float recoveryTime) : base(guard)
    {
        guard.anim.SetBool("Lost", true);
        agent.SetDestination(guard.transform.position);
        this.recoveryTime = recoveryTime;
        prevRange = guard.ViewRange;
        guard.ViewRange = 0.5f;
    }

    public override void Do()
    {
        t += Time.deltaTime;
        if (t >= recoveryTime)
        {
            guard.ViewRange = prevRange;
            guard.action = new GuardPatrol(guard);
        }
    }

}