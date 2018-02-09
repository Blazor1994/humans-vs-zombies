//This was made using the following guide: http://catlikecoding.com/unity/tutorials/maze/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour {

	IntVector2 north;
	IntVector2 south;
	IntVector2 east;
	IntVector2 west;
	public MapCell mapCellPrefab;
	public MapCell grassCellPrefab;

	public GameObject human;

	public GameObject zombie;

	public GameObject rover;

	public GameObject destination;

	public int cellCount;
	public int humanCount;

	public int zombieCount;

	List<MapCell> activeCells = new List<MapCell>();
	List<MapCell> activeCellsTemp = new List<MapCell>();
	List<MapCell> grassCells = new List<MapCell>();

	public int grassWidth;
	List<MapCell> grassCellsTemp = new List<MapCell>();

	public int roadObjectsLimit;
	List<GameObject> roadObjects = new List<GameObject>();

	public GameObject carOne;
	public GameObject carTwo;
	public GameObject carThree;

	public int grassObjectsLimit;
	List<GameObject> grassObjects = new List<GameObject>();

	public GameObject treeOne;
	public GameObject treeTwo;
	public GameObject treeThree;

    [SerializeField] LayerMask navMeshLayers;

    private NavMeshBuildSettings navSettings = new NavMeshBuildSettings();

	NavMeshDataInstance navMeshDataInstance;
	private MapCell[,] cells;
	private MapCell[,] cellsBackup;

	public float genarationsStepDelay;

	bool firstRun = true;
	IntVector2 initalCoordinates;

	IntVector2 finalCoordinates;
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
		MapCell cell = null;
		try{
			cell = cells[coordinates.x, coordinates.z];
		}
		catch
		{
		}
		
		return cell;
	}

	public IntVector2 size; 
	// Use this for initialization
	void Start () {



		//roadObjects.Add(carOne);
		

		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void fillObjectArrays()
	{

		north = MapDirection.North.ToIntVector2();
		south = MapDirection.South.ToIntVector2();
		east = MapDirection.East.ToIntVector2();
		west = MapDirection.West.ToIntVector2();

		roadObjects.Add(carOne);
		roadObjects.Add(carTwo);
		roadObjects.Add(carThree);
		grassObjects.Add(treeOne);
		grassObjects.Add(treeTwo);
		grassObjects.Add(treeThree);
	}

	public void generate(float cellCount, float cars, float humans, float zombies) {
		
		this.cellCount = (int)cellCount;
		roadObjectsLimit = (int) cars;
		humanCount = (int) humans;
		zombieCount = (int) zombies;
		
		fillObjectArrays();
		WaitForSeconds delay = new WaitForSeconds(genarationsStepDelay);
		cells = new MapCell[size.x, size.z];
		
		//IntVector2 now = RandomCoordinates;
		IntVector2 now = new IntVector2(size.x/2, size.z/2);
		int i = 0;
		while (i < cellCount) {
			if (cells[now.x, now.z] == null) { createCell(now); ++i; }
			IntVector2 nxt = now + MapDirections.RandomValue.ToIntVector2();
			if (ContainsCoordinates(nxt)) now = nxt;
			//yield return delay;
		}


		generateObjects("road");
		generateNavMesh();
		createPrefab("destination", finalCoordinates);
		
		//deployRover();
		generateCharacters();
		

		
		Debug.Log("Map Built");
	}


	public void generateCharacters()
	{
		
		refreshNavMesh();
		generateGrass();

		for(var i=0; i<humanCount;i++)
		{
			createPrefab("human", initalCoordinates);
		}

		for(var i = 0; i<zombieCount; i++)
		{
			createPrefab("zombie", finalCoordinates);
		}

		GameObject humanObjects = GameObject.FindWithTag("Human");
		humanObjects.GetComponent<NavMeshAgent>().enabled = true;
		humanObjects.GetComponent<humanScript>().enabled = true;

		GameObject zombieObjects = GameObject.FindWithTag("Zombie");
		zombieObjects.GetComponent<NavMeshAgent>().enabled = true;
		zombieObjects.GetComponent<ZombieNavMesh>().enabled = true;
		zombieObjects.GetComponent<RandomWalk>().enabled = true;
		zombieObjects.GetComponent<zombieScript>().enabled = true;

		generateObjects("grass");
	}
	private void generateObjects(string type)
	{
		if(type.Equals("road"))
		{
			for(int i = 0; i<roadObjectsLimit; i++)
			{
				createPrefab("roadObject", RandomCoordinates);
			}
		}
		else if(type.Equals("grass"))
		{
			for(int i = 0; i<grassObjectsLimit; i++)
			{
				createPrefab("grassObject", RandomCoordinates);
			}
		}

	}
	public void generateGrass()
	{

		IntVector2 northCell;
		IntVector2 southCell;
		IntVector2 eastCell;
		IntVector2 westCell;

		List<MapCell> listType = activeCells;

		//MapDirection north = MapDirections.ToIntVector2(North);
		for(var i = 0; i<grassWidth; i++)
		{
			//Debug.Log(listType.Count);
			foreach(MapCell currentCell in listType)
			{
				northCell = currentCell.coordinates + north;
				southCell = currentCell.coordinates + south;
				eastCell = currentCell.coordinates + east;
				westCell = currentCell.coordinates + west;

				//Debug.Log("Current Cell Coords: (" + currentCell.coordinates.x + "," + currentCell.coordinates.z + ") + (" + north.x + "," + north.z + ") = (" + northCell.x + "," + northCell.z + ")");

				if(GetCell(northCell)==null)
				{
					createPrefab("grass", northCell);
				}
				if(GetCell(southCell)==null)
				{
					createPrefab("grass", southCell);
				}
				if(GetCell(eastCell)==null)
				{
					createPrefab("grass", eastCell);
				}
				if(GetCell(westCell)==null)
				{
					createPrefab("grass", westCell);
				}
			}
			listType = grassCells;
			grassCells.AddRange(grassCellsTemp);
		}

	}

	public bool validPlacement(MapCell currentCell)
	{
		IntVector2	northCell = currentCell.coordinates + north;
		IntVector2	southCell = currentCell.coordinates + south;
		IntVector2	eastCell = currentCell.coordinates + east;
		IntVector2	westCell = currentCell.coordinates + west;

		return true;

	}
	
	public MapCell createCell(IntVector2 coordinates)
	{
		MapCell newCell = Instantiate(mapCellPrefab) as MapCell;
		if(firstRun)initalCoordinates = coordinates;
		finalCoordinates = coordinates;

		cells[coordinates.x, coordinates.z] = newCell;
		activeCells.Add(newCell);
		newCell.coordinates = coordinates;
		newCell.name = "Map Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x-size.x * 0.5f + 0.5f, 0.01f, coordinates.z - size.z * 0.5f+0.5f);
		firstRun = false;
		return newCell;
	}

	public void createPrefab(string type, IntVector2 coords)
	{
		IntVector2 coordinates = coords;
		MapCell cell = null;
		GameObject prefab = null;
		float height = 0.0f;

		if(type.Equals("human"))
		{
			coordinates = coords;
			prefab = Instantiate(human);
			height = 0.0f;
		}
		else if(type.Equals("destination"))
		{
			coordinates = coords;
			prefab = Instantiate(destination);
			height = 0.3f;
			cells[coords.x, coords.z] = null;
		}
		else if(type.Equals("roadObject"))
		{
			int randomIndex = Mathf.RoundToInt(Random.Range(0.0f, activeCells.Count-1));
			int randomIndexObject = Mathf.RoundToInt(Random.Range(0.0f, roadObjects.Count-1));

			coordinates = activeCells[randomIndex].coordinates;

			if(cells[coordinates.x, coordinates.z]!=null)
			{
				prefab = Instantiate(roadObjects[randomIndexObject]);
				height = 0.12f;
				cells[coords.x, coords.z] = null;
				activeCells.RemoveAt(randomIndex);

			}
			
	
		}
		else if(type.Equals("grassObject"))
		{
			int randomIndex = Mathf.RoundToInt(Random.Range(0.0f, grassCells.Count-1));
			int randomIndexObject = Mathf.RoundToInt(Random.Range(0.0f, roadObjects.Count-1));
			coordinates = grassCells[randomIndex].coordinates;
			prefab = Instantiate(grassObjects[randomIndexObject]);

			if(randomIndexObject==1)
			{
				height = 0.20f;	
			}
			else
			{
				height = 0.35f;
			}
			
		}
		else if(type.Equals("zombie"))
		{
			int randomIndex = Mathf.RoundToInt(Random.Range(0.0f, activeCells.Count-1));
			coordinates = activeCells[randomIndex].coordinates;
			prefab = Instantiate(zombie);
			height = 0.0f;
			
		}
		else if(type.Equals("grass"))
		{
			//Debug.Log("Grass Coords: X: " + coordinates.x + " Z: " + coordinates.z);
			cell = Instantiate(grassCellPrefab) as MapCell;
			cells[coords.x, coords.z] = cell;
			grassCellsTemp.Add(cell);
			cell.coordinates = coords;
			cell.name = "Grass Cell " + coords.x + ", " + coords.z;
			height = 0.0f;
		}
		else if(type.Equals("rover"))
		{
			coordinates = coords;
			prefab = Instantiate(rover);
			height = 0.0f;
		}
		if(cell==null)
		{
			prefab.transform.parent = transform;
			prefab.transform.localPosition = new Vector3(coordinates.x-size.x * 0.5f + 0.5f, height, coordinates.z - size.z * 0.5f+0.5f);
		}
		else
		{
			cell.transform.parent = transform;
			cell.transform.localPosition = new Vector3(coordinates.x-size.x * 0.5f + 0.5f, height, coordinates.z - size.z * 0.5f+0.5f);
		}

	}

	private void deployRover()
	{
		createPrefab("rover", initalCoordinates);
		GameObject roverObject = GameObject.FindGameObjectWithTag("rover");
		roverObject.GetComponent<NavMeshAgent>().enabled = true;
		roverObject.GetComponent<roverScript>().enabled = true;
		roverObject.GetComponent<NavMeshAgentScript>().enabled = true;
	}

	public void refreshNavMesh()
	{
		//navMeshDataInstance.Remove();
		generateNavMesh();
	}
	public void generateNavMesh()
	{
		removeNavMesh();
		
		//https://community.gamedev.tv/t/modify-navmesh-dynamically/25849/3
		List<NavMeshBuildSource> buildSources  = new List<NavMeshBuildSource>();
		NavMeshBuilder.CollectSources(transform, navMeshLayers, NavMeshCollectGeometry.RenderMeshes, 0, new List<NavMeshBuildMarkup>(), buildSources);

		NavMeshData navData = NavMeshBuilder.BuildNavMeshData(navSettings, buildSources, new Bounds(Vector3.zero, new Vector3(10000, 10000, 10000)), Vector3.down,
                                Quaternion.Euler(Vector3.up));

		navMeshDataInstance = NavMesh.AddNavMeshData(navData);
	}

	public void removeNavMesh()
	{
		navMeshDataInstance.Remove();
	}
}