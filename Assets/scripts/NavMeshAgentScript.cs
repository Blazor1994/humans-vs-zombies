using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentScript : MonoBehaviour {

    NavMeshAgent agent;

	bool navMeshSet = false;


    private Transform target;

	// Use this for initialization
	void Start () {
	target = GameObject.FindGameObjectWithTag("Finish").transform;
        
	agent = GetComponent<NavMeshAgent>();
	agent.Warp(gameObject.transform.position);	
	}
	
	// Update is called once per frame
	void Update () {

		agent.SetDestination(target.position);

		
	}

	public void setupNavMesh()
	{
		Debug.Log("Setting up navmesh");
		
		navMeshSet = true;

		Debug.Log("Nav mesh set up");
	}


}
