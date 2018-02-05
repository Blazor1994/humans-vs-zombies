using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roverScript : MonoBehaviour {

	// Use this for initialization

	GameObject map;
	void Start () {

		map = GameObject.FindGameObjectWithTag("map");
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

	 private void OnCollisionEnter (Collision collision) {
		 //Debug.Log("Collision");
	if (collision.gameObject.tag == "Wall") {
				Debug.Log ("Removing house");
				Destroy(collision.gameObject);
				//map.GetComponent<Map>().refreshNavMesh();

				
				}
	else if(collision.gameObject.tag == "Finish")
	{
		Debug.Log("Rover finished!");
		//GetComponent<NavMeshAgent>().enabled = true;
		GetComponent<NavMeshAgentScript>().enabled = false;
		GetComponent<roverScript>().enabled = false;
		Destroy(gameObject);
		map.GetComponent<Map>().generateCharacters();
	}

	}
}
