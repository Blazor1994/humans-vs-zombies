using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO: Change this from 'zombieScript' to 'ZombieScript'.
public class zombieScript : MonoBehaviour {

    ZombieNavMesh zombieNav;
    FindClosestEnemyScript findClosest;
    RandomWalk randomWalk;
    GameObject closestEnemy;

    float speed = 0.2f;
    string enemyTag = "Human";

    // Use this for initialization
    void Start() {
        zombieNav = GetComponent<ZombieNavMesh>();
        findClosest = GetComponent<FindClosestEnemyScript>();
        randomWalk = GetComponent<RandomWalk>();

    }
    // Update is called once per frame
    void Update() {
        // Get the closest enemy, passing the method our current position.
        closestEnemy = findClosest.FindClosestEnemy(transform.position, enemyTag);
        // Navigate towards the closest enemy, passing the location of the enemy.
        // NOTE: When the current target is removed, a NullReferenceException is thrown.
        zombieNav.NavigateTowardsHuman(closestEnemy.transform);

        // If the Zombie is stopped...
        if(zombieNav.agent.isStopped == true) {
            randomWalk.Walk();
            Debug.Log("Random Walk!");
        }
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
