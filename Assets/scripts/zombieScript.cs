using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO: Change this from 'zombieScript' to 'ZombieScript'.
public class zombieScript : MonoBehaviour {
    int zombieHealth = 7;
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
        zombieNav.agent.Warp(gameObject.transform.position);

    }
    // Update is called once per frame
    void Update() {
        // Get the closest enemy, passing the method our current position.
        closestEnemy = findClosest.FindClosestEnemy(transform.position, enemyTag);
        // Navigate towards the closest enemy, passing the location of the enemy.
        if(closestEnemy)
        {
            zombieNav.NavigateTowardsHuman(closestEnemy.transform);
        }
        

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
      
        if(collision.gameObject.tag == "Wall") {
          //  Debug.Log("Hit a Wall");
        }
        //Debug.Log("Collided!");
         if (collision.gameObject.tag == "dmgBullet")
        {
            zombieHealth--;
            Debug.Log("Zombie got hurt, takes 1 HP damage. Current HP: " + zombieHealth);
      
           
                if (zombieHealth == 0){
                     Destroy(gameObject);
                }
    }
    }
}

