using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    // TODO: The zombie is statically assigned at the start of the program. 
    // This will not work with multiple zombies.
    public Transform zombie;

    public float speed = 5.0f;
	
	// Update is called once per frame
	void Update () {

        //transform.position = Vector3.MoveTowards(transform.position, zombie.position, -1 * speed * Time.deltaTime);	
	}
}
