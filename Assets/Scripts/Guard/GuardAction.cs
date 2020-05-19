using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for guard action states
/// </summary>
public abstract class GuardAction
{
    public static Dictionary<System.Type, GuardActionType> Types = new Dictionary<System.Type, GuardActionType>()
    {
        {typeof(GuardChase),GuardActionType.Chase},
        {typeof(GuardCheck),GuardActionType.Check},
        {typeof(GuardPatrol),GuardActionType.Patrol},
        {typeof(GuardLost),GuardActionType.Lost},
    };

    public GuardActionType type { get { return Types[GetType()]; } }
    protected NavMeshAgent agent;
    protected Guard guard;
    protected Player player;

    /// <param name="guard">Guard to perform the actual (usually this when instantiating from <see cref="Guard"/>, <see cref="guard"/> when in other action)</param>
    public GuardAction(Guard guard)
    {
        this.guard = guard;
        agent = guard.agent;
        player = guard.player;
    }

    /// <summary>
    /// Update the guard according to the current action state
    /// </summary>
    public abstract void Do();
}