using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;     // Array of patrol points
    public float patrolSpeed = 2f;      // Speed while patrolling
    public float chaseSpeed = 5f;       // Speed while chasing the player
    public float detectionRange = 10f;  // Distance at which the enemy detects the player
    public float attackRange = 2f;      // Distance at which the enemy can attack
    public int damage = 1;              // Damage dealt to the player
    public float stuckDetectionDelay = 2f; // Time delay before resetting to patrol

    private int currentPatrolIndex = 0;     // Current patrol point index
    private Transform player;              // Reference to the player's transform
    private PlayerController playerController; // Reference to the player's controller
    private bool isChasing = false;        // Is the enemy currently chasing the player?
    private float lastPlayerSeenTime;      // Time the player was last in detection range

    private void Start()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned to EnemyAI.");
        }

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Ensure the Player GameObject is tagged as 'Player'.");
        }
        else
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                ChasePlayer();
                lastPlayerSeenTime = Time.time; // Update the time player was last seen
            }
            else if (Time.time - lastPlayerSeenTime > stuckDetectionDelay) // Stuck detection
            {
                isChasing = false; // Stop chasing after delay
            }

            if (!isChasing)
            {
                Patrol();
            }
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPatrolIndex];

        // Move towards the patrol point
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, patrolSpeed * Time.deltaTime);

        // Check if the enemy has reached the patrol point
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Go to the next patrol point, loop back to the start if at the end
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void ChasePlayer()
    {
        isChasing = true;

        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
    }
}

