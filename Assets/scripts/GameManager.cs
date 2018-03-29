//This was made using the following guide: http://catlikecoding.com/unity/tutorials/maze/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Map mapPrefab;

	private Map mapInstance;

	public Camera menuCam;
	public Camera followCam;

	public Canvas cameraOverlay;
	private void Start () {
		//BeginGame();
	}
	
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			StopGame();
		}
	}

	public void BeginGame (float cells, float cars, float humans, float zombies) {
		mapInstance = Instantiate(mapPrefab) as Map;
		//StartCoroutine(mapInstance.generate());
		menuCam.enabled = false;
		cameraOverlay.targetDisplay = 0;
		mapInstance.generate(cells, cars, humans, zombies);
		followCam.enabled = true;
	}

	private void StopGame () {

		//StopAllCoroutines();
		mapInstance.removeNavMesh();
		Destroy(mapInstance.gameObject);
		menuCam.enabled = true;
		followCam.enabled = false;
		cameraOverlay.targetDisplay = 3;
	}
}
