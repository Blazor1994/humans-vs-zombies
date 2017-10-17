using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanScript : MonoBehaviour {

	// Use this for initialization

	Transform tr_Zombie;
	float f_RotSpeed = 3.0f, f_MoveSpeed = 1.0f;

	void Start () {
			tr_Zombie = GameObject.FindGameObjectWithTag("Zombie").transform;

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, 
												Quaternion.LookRotation(tr_Zombie.position - transform.position),
												f_RotSpeed * Time.deltaTime);
		
		transform.position -= transform.forward*f_MoveSpeed*Time.deltaTime;
	}
}
