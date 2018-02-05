using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemyScript : MonoBehaviour {

    /// <summary>
    /// A method to find and return the closest enemy Game Object to the current Game Object.
    /// </summary>
    /// <param name="position">A Vector 3 of the current Game Object's position.</param>
    /// <param name="enemyTag">A string, with the tag of the enemy.</param>
    /// <returns>The closest Game Object with the matching tag.</returns>
    public GameObject FindClosestEnemy(Vector3 position, string enemyTag) {
        // Create an array of game objects
        GameObject[] closestEnemyArray;
        // Populate the array with all game ojects matching the "Human" tag.
        closestEnemyArray = GameObject.FindGameObjectsWithTag(enemyTag);

        GameObject closest = null;

        float distance = Mathf.Infinity;
        //Vector3 position = transform.position;

        foreach (GameObject enemy in closestEnemyArray) {
            //Debug.Log("Enemies in the closestEnemyArray:  " + enemy);
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
