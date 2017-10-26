using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour {

    float zombieSpeedNormal = 1f;
	// Use this for initialization
	void Start () {
		
	}
    //Reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Human");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    // Update is called once per frame
    void Update() {
        //get humans distance from zombie
        var distance = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);
        //if the humans distance is less than 15meters
        if(distance < 17)
        {
            transform.LookAt(FindClosestEnemy().transform.position);
            //For documentation on Time.deltaTime https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
            transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, 1 * zombieSpeedNormal * Time.deltaTime);

        }
    }
}
