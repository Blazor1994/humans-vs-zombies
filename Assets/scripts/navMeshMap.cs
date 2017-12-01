using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navMeshMap : MonoBehaviour {
    [SerializeField] LayerMask navMeshLayers;

    private NavMeshBuildSettings navSettings = new NavMeshBuildSettings();

	NavMeshDataInstance navMeshDataInstance;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generateNavMesh()
	{
		//https://community.gamedev.tv/t/modify-navmesh-dynamically/25849/3
		List<NavMeshBuildSource> buildSources  = new List<NavMeshBuildSource>();
		NavMeshBuilder.CollectSources(transform, navMeshLayers, NavMeshCollectGeometry.RenderMeshes, 0, new List<NavMeshBuildMarkup>(), buildSources);

		NavMeshData navData = NavMeshBuilder.BuildNavMeshData(navSettings, buildSources, new Bounds(Vector3.zero, new Vector3(10000, 10000, 10000)), Vector3.down,
                                Quaternion.Euler(Vector3.up));

		navMeshDataInstance = NavMesh.AddNavMeshData(navData);
		//Debug.Log(buildSources.Count);
		//Debug.Log("Map Built");
	}
}
