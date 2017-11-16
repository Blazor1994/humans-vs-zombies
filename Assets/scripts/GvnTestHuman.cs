using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GvnTestHuman : MonoBehaviour {
    float humanSpeedNormal = 4;
    // Target, a destination for the Human.
    [Tooltip("Select a destination for the Human.")]
    public Transform target;
    public RaycastHit hit;
    // Use this for initialization
    void Start() {

    }
    //Reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    public GameObject FindClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        Debug.Log("The closest Zombie is: " + closest);
        return closest;
    }
    void Update() {

        FindClosestEnemy();

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, 15) && hit.transform.tag != "Zombie") {
            Debug.Log("Enemy spotted");
            //Get the zombie distance from the human
            var zombieDistance = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);
            //If the zombie is less than 10 meters away
            if (zombieDistance < 15) {

                transform.LookAt(2 * transform.position - FindClosestEnemy().transform.position);
                //For documentation on Time.deltaTime https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
                transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, -1 * humanSpeedNormal * Time.deltaTime);
            }

        } else {
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, 1 * humanSpeedNormal * Time.deltaTime);
        }
    }
}
