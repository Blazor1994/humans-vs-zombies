using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    // TODO: The NPC is statically assinged at the start of the program.
    // This will not work with multiple NPCs.
    public Transform npc;

    public float speed = 5.0f;
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, npc.position, speed * Time.deltaTime);


		
	}
}
