using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public NavMeshAgent agent;
    public float stoppedTime = 8.0f;
    private float elapsedTime = 0;
    [Range(0, 100)] public float speed;
    [Range(1, 500)] public float walkRadius;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent != null)
        {
            agent.stoppingDistance = 0.3f;
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
    }

    public void Update()
    {
        if (elapsedTime >= stoppedTime)
        {
            Debug.Log("Se paso el tiempo");
            elapsedTime = 0;
            agent.SetDestination(RandomNavMeshLocation());
        }
        else elapsedTime += Time.deltaTime;


        if (agent != null && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(RandomNavMeshLocation());
            elapsedTime = 0;
        }
    }
    private Vector3 RandomNavMeshLocation()
    {
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            return hit.position;
        }
        else return transform.position;
    }
}
    