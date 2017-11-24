using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentScript : MonoBehaviour {

    NavMeshAgent agent;



    private Transform target;

	// Use this for initialization
	void Start () {
	target = GameObject.FindGameObjectWithTag("Finish").transform;
        agent = GetComponent<NavMeshAgent>();
		
	}
	
	// Update is called once per frame
	void Update () {

        agent.SetDestination(target.position);
		
	}


}
