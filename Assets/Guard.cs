using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{

    public Path DefaultPath;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public Player player;
    [HideInInspector] public GuardAction action;

    [Header("Patrol")]
    public float PatrolSpeed = 1;
    [Range(0, 180)] public float ViewAngle = 45;
    public float ViewRange = 3f;

    [Header("Chase")]
    public float ChaseSpeed = 3;
    public float ChaseRange = 5;

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
    }

    void Update()
    {
        action.Do();
    }
}