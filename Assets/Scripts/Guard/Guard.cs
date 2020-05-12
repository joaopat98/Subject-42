using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    /// <summary>
    /// Path the guard will follow when Patrolling
    /// </summary>
    public Path DefaultPath;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Player player;
    /// <summary>
    /// current action state
    /// </summary>
    [HideInInspector] public GuardAction action;
    /// <summary>
    /// Speed of the guard when patrolling
    /// </summary>
    [Header("Patrol")] public float PatrolSpeed = 1;
    /// <summary>
    /// Field of view of the guard when patrolling
    /// </summary>
    [Range(0, 180)] public float ViewAngle = 45;
    /// <summary>
    /// Distance in front of the guard within which he can see the player
    /// </summary>
    public float ViewRange = 3f;
    /// <summary>
    /// Distance to each Path node the guard will get to before moving to next node
    /// </summary>
    public float PatrolReachDistance = 0.5f;
    /// <summary>
    /// Speed of the guard when checking
    /// </summary>
    [Header("Check")] public float CheckSpeed = 1;
    /// <summary>
    /// Distance from the player at which the guard will stop moving
    /// </summary>
    public float CheckReachDistance = 1;
    /// <summary>
    /// Time to wait when reach his target
    /// </summary>
    public float TimeToCheck = 2;
    /// <summary>
    /// Speed at which the guard will chase the player
    /// </summary>
    [Header("Chase")] public float ChaseSpeed = 5;
    /// <summary>
    /// Maximum distance the guard can be from the player before giving up the chase
    /// </summary>
    public float ChaseRange = 5;
    /// <summary>
    /// Distance from the player at which the guard will stop moving
    /// </summary>
    public float ChaseReachDistance = 1;
    /// <summary>
    /// Maximum speed that the player can not be detected
    /// </summary>
    public float playerSpeedthreshold;
    /// <summary>
    /// Maximum distance that the player can not be detected
    /// </summary>
    public float playerDistancethreshold;

    /// <summary>
    /// Type of the guard (Melee or Ranged)
    /// </summary>
    [Header("Attack")] public GuardType type;
    /// <summary>
    /// Number of attacks the guard will perform per second, bullets if the guard is ranged, melee attacks otherwise
    /// </summary>
    public float AttacksPerSec;
    /// <summary>
    /// Maximum distance for melee attacks to hit
    /// </summary>
    public float AttackRange;
    public GameObject BulletPrefab;

    /// <summary>
    /// Animator of the guard 
    /// </summary>
    [HideInInspector] public Animator anim;

    /// <summary>
    /// Calculate if the NavMeshAgent has reached its current goal
    /// </summary>
    /// <returns></returns>

    public bool HasReachedGoal()
    {
        return agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance < agent.stoppingDistance;
    }

    void Start()
    {
        if (DefaultPath == null) gameObject.UnassignedReference("DefaultPath");
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        action = new GuardPatrol(this);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        action.Do();
    }
}