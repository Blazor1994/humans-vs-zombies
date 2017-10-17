using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviourScript : MonoBehaviour {

    float zombieSpeedNormal = 0.8f;
    //transform for human which is target
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
      
        var distance = Vector3.Distance(transform.position, target.position);
        if(distance < 15)
        {
            transform.LookAt(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, 1 * zombieSpeedNormal * Time.deltaTime);

        }
    }
}
