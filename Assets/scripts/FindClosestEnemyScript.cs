using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemyScript : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        
    }

    public GameObject FindClosestEnemy(Vector3 position) {
        // Create an array of game objects
        GameObject[] closestEnemyArray;
        // Populate the array with all game ojects matching the "Huma" tag.
        closestEnemyArray = GameObject.FindGameObjectsWithTag("Human");

        GameObject closest = null;

        float distance = Mathf.Infinity;
        //Vector3 position = transform.position;

        foreach (GameObject enemy in closestEnemyArray) {
            Debug.Log("Enemies in the closestEnemyArray:  " + enemy);
            Vector3 difference = enemy.transform.position - position;
            float currentDistance = difference.sqrMagnitude;

            if (currentDistance < distance) {
                closest = enemy;
                distance = currentDistance;
            }
        }
        return closest;
    }
}
