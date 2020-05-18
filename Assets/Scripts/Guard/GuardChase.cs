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
        guard.anim.SetBool("Checking", false);
        guard.anim.SetBool("Chasing", true);
        agent.angularSpeed = guard.ChaseAngularSpeed;
        agent.stoppingDistance = guard.ChaseReachDistance;
        agent.speed = guard.ChaseSpeed;
        player.Sounds.PlayLoop("DetectedEffect");
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
        GameObject.Instantiate(guard.BulletPrefab, guard.Weapon.transform.position, Quaternion.LookRotation(dir, Vector3.up));
        player.Sounds.PlayOnce("Shoot");
        t = 0;
    }

    public override void Do()
    {
        guard.anim.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        t += Time.deltaTime;
        // Update the agent's goal to the player's position
        agent.SetDestination(player.transform.position);
        if (guard.HasReachedGoal())
        {
            var lookDir = player.transform.position - guard.transform.position;
            lookDir.y = 0;
            lookDir = lookDir.normalized;
            guard.transform.rotation = Quaternion.Lerp(
                guard.transform.rotation,
                Quaternion.LookRotation(lookDir, Vector3.up),
                agent.angularSpeed * Time.deltaTime);
        }
        // Change to patrol mode if player is too far away
        if (Vector3.Distance(guard.transform.position, player.transform.position) > guard.ChaseRange)
        {
            guard.action = new GuardPatrol(guard);
            Debug.Log("stop breathing");
            player.Sounds.StopLoop("DetectedEffect");
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