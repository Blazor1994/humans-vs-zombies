using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestHumanShop : MonoBehaviour {
public static GameObject hum;
	// Use this for initialization
	void Start () {
		
	}
	public GameObject FindClosestEnemy() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Human");
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

		return closest;
    }
	
	// Update is called once per frame
	void Update () {
		hum = FindClosestEnemy();
	}
}
