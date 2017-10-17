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
        //get humans distance from zombie
        var distance = Vector3.Distance(transform.position, target.position);
        //if the humans distance is less than 15meters
        if(distance < 15)
        {
            transform.LookAt(target.position);
            //For documentation on Time.deltaTime https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
            transform.position = Vector3.MoveTowards(transform.position, target.position, 1 * zombieSpeedNormal * Time.deltaTime);

        }
    }
}
