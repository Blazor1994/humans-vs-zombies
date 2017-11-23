using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO: Change this from 'zombieScript' to 'ZombieScript'.
public class zombieScript : MonoBehaviour {

    ZombieNavMesh zombieNav;
    FindClosestEnemyScript findClosest;
    GameObject closestEnemy;

    float speed = 0.2f;

    // Use this for initialization
    void Start() {
        zombieNav = GetComponent<ZombieNavMesh>();
        findClosest = GetComponent<FindClosestEnemyScript>();

    }
    // Update is called once per frame
    void Update() {
        // Get the closest enemy, passing the method our current position.
        closestEnemy = findClosest.FindClosestEnemy(transform.position);
        // Navigate towards the closest enemy, passing the location of the enemy.
        zombieNav.NavigateTowardsHuman(closestEnemy.transform);
    }

     //Check if human has entered zombie collision
 
    // TODO: Make the Zombie do something on collision.
    private void OnCollisionEnter(Collision collision) {
        // If the tag of the collided object matches ''...
        if(collision.gameObject.tag == "Human") {
            Destroy(collision.gameObject);
            Debug.Log("Kill a Human");
          
        }
        if(collision.gameObject.tag == "Wall") {
            Debug.Log("Hit a Wall");
        }
        //Debug.Log("Collided!");
    }
}
