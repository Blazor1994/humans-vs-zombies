using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapDirection {

	North,
	East,
	South,
	West
}


public static class MapDirections{

	private static IntVector2[] vectors = {
		new IntVector2(0,1),
		new IntVector2(1,0),
		new IntVector2(0,-1),
		new IntVector2(-1,0)
	};
	public const int Count = 4;

	public static MapDirection RandomValue{
		get{
			return (MapDirection)Random.Range(0, Count);
		}
	}

	public static IntVector2 ToIntVector2(this MapDirection direction)
	{
		return vectors[(int)direction];
	}
}
