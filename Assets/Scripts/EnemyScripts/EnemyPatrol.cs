using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float chaseSpeed = 4f;
    public float patrolSpeed = 2f;
    public float searchDuration = 3f;

    private int currentPointIndex = 0;
    private NavMeshAgent agent;
    private EnemyVision vision;

    private enum State { Patrol, Chase, Search }
    private State currentState = State.Patrol;

    private Vector3 lastKnownPlayerPosition;
    private float searchTimer = 0f;

    void Start()
    {
        GameObject PatrolPointsParent = GameObject.Find("PatrolPoint");
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = PatrolPointsParent.transform.GetChild(i);
        }
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<EnemyVision>();

        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    void Update()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case State.Patrol:
                PatrolUpdate();
                if (vision.canSeePlayer)
                {
                    currentState = State.Chase;
                    agent.speed = chaseSpeed;
                }
                break;

            case State.Chase:
                if (vision.canSeePlayer)
                {
                    agent.SetDestination(vision.player.position);
                    lastKnownPlayerPosition = vision.player.position;
                }
                else
                {
                    currentState = State.Search;
                    searchTimer = 0f;
                    agent.SetDestination(lastKnownPlayerPosition);
                }
                break;

            case State.Search:
                searchTimer += Time.deltaTime;

                if (vision.canSeePlayer)
                {
                    currentState = State.Chase;
                    agent.speed = chaseSpeed;
                }
                else if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    // На месте, где видел игрока — "осматривается"
                    if (searchTimer >= searchDuration)
                    {
                        currentState = State.Patrol;
                        agent.speed = patrolSpeed;
                        agent.SetDestination(patrolPoints[currentPointIndex].position);
                    }
                }
                break;
        }
    }

    void PatrolUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
    }
}
