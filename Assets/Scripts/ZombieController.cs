using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    public float speed = 5.0f;
	
	// Update is called once per frame
	void Update () {

        // Move towards the closest
        transform.position = Vector3.MoveTowards(transform.position, FindClosestNPC().transform.position, speed * Time.deltaTime);
	
	}

    // NOTE: This is duplicated in the NPCController.
    public GameObject FindClosestNPC() {
        // Create an array of GameOjects, called 'NPCs', to store local NPCs in.
        GameObject[] NPCs;
        // Store all the GameObjects with the tag 'NPC' in the array
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
        // Create an empty GameObject, 'closestNPC', which will store the closest NPC.
        GameObject closest = null;
        // Initialise the distance with max distance. E.g. everything will be closer than infinity.
        float distance = Mathf.Infinity;
        // Create a new Vector3, 'position', to store this Zombie's position
        Vector3 position = transform.position;
        // For each NPC GameObject in the array of NPCs...
        foreach (GameObject NPC in NPCs) {
            // Create a new Vector3, 'difference', that is the current NPC's position minus our current position.
            Vector3 difference = NPC.transform.position - position;

            float currentDistance = difference.sqrMagnitude;
            if (currentDistance < distance) {
                closest = NPC;
                distance = currentDistance;
            }
        }
        //Debug.Log(closest);
        return closest;
    }
}
