using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieNavMesh : MonoBehaviour {

    NavMeshAgent agent;

    // Use this for initialization
    void Start() {

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void NavigateTowardsHuman(Transform target) {

        // Draw a ray from this gameobject to it's target.
        // If the ray collides with an object, its details are returned.
        // Reference: https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.Raycast.html
        NavMeshHit hit;
        // If this game object CAN see the target...
        if (!agent.Raycast(target.position, out hit)) {
            Debug.Log("Target IS in sight.");
            agent.SetDestination(target.position);
        } else {
            Debug.Log("Target NOT in sight.");
        }
    }




}
