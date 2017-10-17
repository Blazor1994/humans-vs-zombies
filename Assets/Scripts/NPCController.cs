using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public Transform zombie;
    public float speed = 5.0f;
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.MoveTowards(transform.position, zombie.position, -1 * speed * Time.deltaTime);	
	}
}
