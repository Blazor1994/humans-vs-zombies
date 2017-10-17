using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanBehaviourScript : MonoBehaviour
{
    float humanSpeedNormal = 1;
    //transform for zombie which is target
    public Transform zombieTarget;
    public Transform target;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        var zombieDistance = Vector3.Distance(transform.position, zombieTarget.position);
        if (zombieDistance < 10)
        {
          
            transform.LookAt(2 * transform.position - zombieTarget.position);
            transform.position = Vector3.MoveTowards(transform.position, zombieTarget.position, -1 * humanSpeedNormal * Time.deltaTime);
        }
    }
}
