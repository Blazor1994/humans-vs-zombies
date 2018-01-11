using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNavMesh : MonoBehaviour {

    NavMeshAgent agent;
    /// <summary>
    /// The maxium distance a Zombie can be agroed from.
    /// </summary>
    public float chaseDistance = 0;

    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
    }
    
    /// <summary>
    /// Navigate towards the nearest Human, stopping when the Human is too far away or when they leave
    /// line of sight.
    /// </summary>
    /// <param name="target">Transform, the cloests enemy to the Zombie.</param>
    public void NavigateTowardsHuman(Transform target) {

        // Draw a ray from this gameobject to it's target.
        // If the ray collides with an object, its details are returned.
        // Reference: https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.Raycast.html
        NavMeshHit hit;
        // If this game object CAN see the target...
        if (!agent.Raycast(target.position, out hit)) {
            Debug.Log("Target IS in sight.");
            agent.SetDestination(target.position);
            // If the target is further away than the chase distance...
            if (agent.remainingDistance > chaseDistance) {
                // Stop the Zombie
                agent.isStopped = true;
                agent.ResetPath();
                Debug.Log("Target is too far away. Distance: " + agent.remainingDistance);
            }
        } else {
            Debug.Log("Target NOT in sight.");
            // Stop the Zombie
            agent.isStopped = true;
            agent.ResetPath();
        }
    }
}
