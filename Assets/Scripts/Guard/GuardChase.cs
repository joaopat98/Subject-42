using UnityEngine;

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
        t += Time.deltaTime;
        agent.SetDestination(player.transform.position);
        if (Vector3.Distance(guard.transform.position, player.transform.position) > guard.ChaseRange)
        {
            guard.action = new GuardPatrol(guard);
        }
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