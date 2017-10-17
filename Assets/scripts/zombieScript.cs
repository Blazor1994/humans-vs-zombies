using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour {

	// Use this for initialization

	Transform tr_Player;
	float f_RotSpeed = 3.0f, f_MoveSpeed = 0.5f;


	void Start () {
			tr_Player = GameObject.FindGameObjectWithTag("Human").transform;
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.Slerp(transform.rotation, 
												Quaternion.LookRotation(tr_Player.position - transform.position),
												f_RotSpeed * Time.deltaTime);
		
		transform.position += transform.forward*f_MoveSpeed*Time.deltaTime;
	}
}
