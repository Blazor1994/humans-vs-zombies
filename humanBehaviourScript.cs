using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanBehaviourScript : MonoBehaviour
{
    float speed = -0.02f;
    //transform for zombie which is target
    public Transform target;
    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void Update() {
   
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);

    }
}
