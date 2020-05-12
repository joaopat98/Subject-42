using UnityEngine;

/// <summary>
/// Chase the player state
/// </summary>
public class GuardChase : GuardAction
{
    private float t;
    public GuardChase(Guard guard) : base(guard)
    {
        Debug.Log("Chasing");
        agent.stoppingDistance = guard.ChaseReachDistance;
        agent.speed = guard.ChaseSpeed;
    }

    void MeleeAttack()
    {
        if (Vector3.Distance(guard.transform.position, player.transform.position) < guard.AttackRange)
        {
            player.Kill();
            t = 0;
        }
    }

    void RangedAttack()
    {
        var dir = player.transform.position - guard.transform.position;
        GameObject.Instantiate(guard.BulletPrefab, guard.transform.position, Quaternion.LookRotation(dir, Vector3.up));
        t = 0;
    }

    public override void Do()
    {
        guard.anim.SetFloat("Speed", agent.speed);

        t += Time.deltaTime;
        // Update the agent's goal to the player's position
        agent.SetDestination(player.transform.position);
        // Change to patrol mode if player is too far away
        if (Vector3.Distance(guard.transform.position, player.transform.position) > guard.ChaseRange)
        {
            guard.action = new GuardPatrol(guard);
        }
        // Perform attack if next attack cycle is reached
        if (t > 1 / guard.AttacksPerSec)
        {
            switch (guard.type)
            {
                case GuardType.Melee:
                    MeleeAttack();
                    break;
                case GuardType.Ranged:
                    RangedAttack();
                    break;
            }
        }
    }
}