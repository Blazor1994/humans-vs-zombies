using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviourScript : MonoBehaviour {

    float speed = 0.01f;
    //transform for human which is target
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        transform.LookAt(target.position);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
     
    }
}
