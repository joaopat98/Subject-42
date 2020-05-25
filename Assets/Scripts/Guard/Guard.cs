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
    /// <summary>
    /// Distance in front of the guard within which he can see the player
    /// </summary>
    public float ViewRange = 3f;

    [Header("Patrol")] public float PatrolSpeed = 1;
    /// <summary>
    /// Field of view of the guard when patrolling
    /// </summary>
    [Range(0, 180)] public float ViewAngle = 45;
    /// <summary>
    /// Field of view of the guard when patrolling
    /// </summary>
    public float PatrolView = 5;
    /// <summary>
    /// Distance to each Path node the guard will get to before moving to next node
    /// </summary>
    public float PatrolReachDistance = 0.5f;
    public float PatrolAngularSpeed = 4;
    /// <summary>
    /// Speed of the guard when checking
    /// </summary>
    [Header("Check")] public float CheckSpeed = 1;
    /// <summary>
    /// Distance from the player at which the guard will stop moving
    /// </summary>
    public float CheckReachDistance = 1;
    public float CheckAngularSpeed = 3;
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
    public float ChaseAngularSpeed = 5;
    /// <summary>
    /// Maximum speed that the player can not be detected
    /// </summary>
    public float playerSpeedthreshold;
    /// <summary>
    /// Maximum distance that the player can not be detected
    /// </summary>
    public float playerDistancethreshold;
    /// <summary>
    /// View range of the lost guard
    /// </summary>
    [Header("Lost")] public float LostView = 0.5f;

    [Header("Light")]
    public Color PatrolColor;
    public Color CheckColor;
    public Color ChaseColor;

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
    /// Weapon attached to guard's right hand
    /// </summary>
    public GameObject Weapon;

    /// <summary>
    /// Animator of the guard 
    /// </summary>
    [HideInInspector] public Animator anim;
    private Rigidbody rb;

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
        rb = GetComponent<Rigidbody>();
        if (DefaultPath == null) gameObject.UnassignedReference("DefaultPath");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        anim = GetComponent<Animator>();
        action = new GuardPatrol(this);
    }

    void Update()
    {
        var velocity = agent.velocity;
        velocity.y = 0;
        velocity = velocity.normalized;
        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity, Vector3.up), agent.angularSpeed * Time.deltaTime);
        }
        action.Do();
    }
}