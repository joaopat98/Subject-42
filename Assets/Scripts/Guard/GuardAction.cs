using UnityEngine;
using UnityEngine.AI;

public abstract class GuardAction
{
    protected NavMeshAgent agent;
    protected Guard guard;
    protected Player player;
    public GuardAction(Guard guard)
    {
        this.guard = guard;
        agent = guard.agent;
        player = guard.player;
    }

    public abstract void Do();
}