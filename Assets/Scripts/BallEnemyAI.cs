using UnityEngine;
using UnityEngine.AI;

public class BallEnemyAI : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    public Transform player;
    private NavMeshAgent navMeshAgent;
    private float movementX;
    private float movementY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>(); // Initiates the navMeshAgent
    }

    void Update()
    {
        if (player != null) // Follow the player
        {
            navMeshAgent.SetDestination(player.position);
        }
    }

    void FixedUpdate() // Rolling speed
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
}