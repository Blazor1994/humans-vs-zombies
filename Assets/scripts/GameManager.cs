//This was made using the following guide: http://catlikecoding.com/unity/tutorials/maze/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Map mapPrefab;

	private Map mapInstance;
	private void Start () {
		//BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			RestartGame();
		}
	}

	public void BeginGame (float cells, float cars, float humans, float zombies) {
		mapInstance = Instantiate(mapPrefab) as Map;
		//StartCoroutine(mapInstance.generate());
		mapInstance.generate(cells, cars, humans, zombies);
	}

	private void RestartGame () {

		//StopAllCoroutines();
		mapInstance.removeNavMesh();
		Destroy(mapInstance.gameObject);
		//BeginGame();
	}
}
