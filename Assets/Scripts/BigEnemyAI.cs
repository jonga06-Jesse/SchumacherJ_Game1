using UnityEngine;
using UnityEngine.AI;

public class BigEnemyAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Initiates the navMeshAgent
    }

    void Update()
    {
        if (player != null) // Follow the player
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}