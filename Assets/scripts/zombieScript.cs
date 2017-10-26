using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO: Change this from 'zombieScript' to 'ZombieScript'.
public class zombieScript : MonoBehaviour {

    float speed = 1f;

    // Update is called once per frame
    void Update() {

        // Get the distance between the closest Human and this Zombie.
        float distance = Vector3.Distance(transform.position, FindClosestEnemy().transform.position);

        // If the Human is less than 17 metres away from this Zombie.
        if (distance < 17) {
            // Turn to face the closest Human.
            transform.LookAt(FindClosestEnemy().transform.position);
            // Move towards the cloeset Human
            // Reference: https://docs.unity3d.com/ScriptReference/Time-deltaTime.html
            transform.position = Vector3.MoveTowards(transform.position, FindClosestEnemy().transform.position, 1 * speed * Time.deltaTime);

        }
    }
    // TODO: Move this to a seperate Utility Class.
    // Reference: https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Human");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 difference = go.transform.position - position;
            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance)
            {
                closest = go;
                distance = currentDistance;
            }
        }
        return closest;
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Collided!");
    }
}
