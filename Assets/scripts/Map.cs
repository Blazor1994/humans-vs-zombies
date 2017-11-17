//This was made using the following guide: http://catlikecoding.com/unity/tutorials/maze/ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

	public int sizeX, sizeZ;

	public MapCell mapCellPrefab;

	private MapCell[,] cells;

	public float genarationsStepDelay;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator generate()
	{
		WaitForSeconds delay = new WaitForSeconds(genarationsStepDelay);
		cells = new MapCell[sizeX, sizeZ];
		for(int x = 0;x<sizeX;x++)
		{
			for(int z = 0; z<sizeZ; z++)
			{
				yield return delay;
				createCell(x,z);
			}
		}
	}

	public void createCell(int x, int z)
	{
		MapCell newCell = Instantiate(mapCellPrefab) as MapCell;
		cells[x,z] = newCell;
		newCell.name = "Map Cell" + x + ", " + z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(x-sizeX * 0.5f + 0.5f, 0f, z - sizeZ * 0.5f+0.5f);
	}
}
