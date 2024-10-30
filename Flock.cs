using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Flock : MonoBehaviour
{
    public Drone agentPrefab;
    List<Drone> agents = new List<Drone>();
    public FlockBehavior behavior;

    [Range(10, 5000)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            Drone newAgent = Instantiate(
                agentPrefab,
                UnityEngine.Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)),
                transform
            );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this, i);
            agents.Add(newAgent);
        }
    }

    void PartitionDronesByAmmo()
    {
        if (agents.Count == 0) return;

        int pivotAmmo = agents[0].Ammo;
        foreach (Drone agent in agents)
        {
            var partitionStopwatch = Stopwatch.StartNew();

            if (agent.Ammo <= pivotAmmo)
            {
                agent.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.3f, 0.1f); // Dark Green
            }
            else
            {
                agent.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.5f, 0.4f); // Tan/Brown
            }

            partitionStopwatch.Stop();
            UnityEngine.Debug.Log($"Partitioning time for {agent.name}: {partitionStopwatch.Elapsed.TotalMilliseconds:F3} ms");
        }
    }

    void Update()
    {
        PartitionDronesByAmmo();

        foreach (Drone agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(Drone agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
