//This was made using the following guide: http://catlikecoding.com/unity/tutorials/maze/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour {

	public MapCell mapCellPrefab;
	public MapCell grassCellPrefab;

	List<MapCell> activeCells = new List<MapCell>();

    [SerializeField] LayerMask navMeshLayers;

    private NavMeshBuildSettings navSettings = new NavMeshBuildSettings();

	NavMeshDataInstance navMeshDataInstance;
	private MapCell[,] cells;

	public float genarationsStepDelay;

	public IntVector2 RandomCoordinates{
		get
		{
			return new IntVector2 (Random.Range(0, size.x), Random.Range(0, size.z));
		}
	}

	public bool ContainsCoordinates(IntVector2 coordinate)
	{
		return coordinate.x>=0 && coordinate.x<size.x && coordinate.z>=0 && coordinate.z<size.z;
	}

	public MapCell GetCell (IntVector2 coordinates)
	{
		return cells[coordinates.x, coordinates.z];
	}

	public IntVector2 size; 
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
/* 
	public IEnumerator generate()
	{
		WaitForSeconds delay = new WaitForSeconds(genarationsStepDelay);
		cells = new MapCell[size.x, size.z];
		
		DoFirstGenerationStep(activeCells);
		while(activeCells.Count>0)
		{
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}*/

	public IEnumerator generate() {
		WaitForSeconds delay = new WaitForSeconds(genarationsStepDelay);
		cells = new MapCell[size.x, size.z];
		
		IntVector2 now = RandomCoordinates;
		int i = 0;
		while (i < 100) {
			if (cells[now.x, now.z] == null) { createCell(now); ++i; }
			IntVector2 nxt = now + MapDirections.RandomValue.ToIntVector2();
			if (ContainsCoordinates(nxt)) now = nxt;
			yield return delay;
		}

		generateNavMesh();
		Debug.Log("Map Built");
	}

	public MapCell createCell(IntVector2 coordinates)
	{
		MapCell newCell = Instantiate(mapCellPrefab) as MapCell;
		cells[coordinates.x, coordinates.z] = newCell;
		activeCells.Add(newCell);
		newCell.coordinates = coordinates;
		newCell.name = "Map Cell" + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x-size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f+0.5f);

		return newCell;
	}

	public void DoFirstGenerationStep(List<MapCell> activeCells)
	{
		activeCells.Add(createCell(RandomCoordinates));
	}

	public void DoNextGenerationStep(List<MapCell> activeCells)
	{
		int currentIndex = activeCells.Count -1;
		MapCell currentCell = activeCells[currentIndex];
		MapDirection direction  = MapDirections.RandomValue;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if(ContainsCoordinates(coordinates)&& GetCell(coordinates) == null)
		{
			activeCells.Add(createCell(coordinates));
		}
		else
		{
			activeCells.RemoveAt(currentIndex);
		}
	}

	public void generateNavMesh()
	{
		//https://community.gamedev.tv/t/modify-navmesh-dynamically/25849/3
		List<NavMeshBuildSource> buildSources  = new List<NavMeshBuildSource>();
		NavMeshBuilder.CollectSources(transform, navMeshLayers, NavMeshCollectGeometry.RenderMeshes, 0, new List<NavMeshBuildMarkup>(), buildSources);

		NavMeshData navData = NavMeshBuilder.BuildNavMeshData(navSettings, buildSources, new Bounds(Vector3.zero, new Vector3(10000, 10000, 10000)), Vector3.down,
                                Quaternion.Euler(Vector3.up));

		navMeshDataInstance = NavMesh.AddNavMeshData(navData);
		Debug.Log(buildSources.Count);

	}
}
