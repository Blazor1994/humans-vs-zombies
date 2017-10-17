using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public Transform npc;

    public float speed = 5.0f;
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, npc.position, speed * Time.deltaTime);


		
	}
}
