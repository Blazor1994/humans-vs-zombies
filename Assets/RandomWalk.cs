using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomWalk : MonoBehaviour {

    // Adapted from https://www.reddit.com/r/Unity3D/comments/2vatyd/methods_of_returning_a_random_position_on_a/
    ZombieNavMesh zombieNav;
   
    public float maxWalkDistance = 5f;

    private void Start() {
        zombieNav = GetComponent<ZombieNavMesh>();
    }

    /// <summary>
    /// Pick a random position, somewhere inside a sphere of the maxWalkingDistance and traverse there
    /// using the NavMesh.
    /// </summary>
    public void Walk() {
        Vector3 direction = Random.insideUnitSphere * maxWalkDistance;
        direction += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(direction, out hit, Random.Range(0f, maxWalkDistance), 1);

        Vector3 destination = hit.position;

        zombieNav.agent.SetDestination(destination);
    }
}