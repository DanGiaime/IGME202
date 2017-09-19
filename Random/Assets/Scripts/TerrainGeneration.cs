using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour {

	private TerrainData myTerrainData;
	public Vector3 worldSize = new Vector3(200, 50, 200);
	public int resolution = 128;


	// Use this for initialization
	void Start () {
		myTerrainData = gameObject.GetComponent<TerrainCollider>().terrainData; 
		
		myTerrainData.size = worldSize;
		myTerrainData.heightmapResolution = resolution;
		float[,] heightArray = new float[resolution, resolution];
		makeZero (heightArray);
		makePerlin (heightArray, resolution);
		myTerrainData.SetHeights (0, 0, heightArray);
	}

	void makeZero(float[,] heights) {
		for(int i = 0; i < heights.GetLength(0); i++) {
			for(int j = 0; j < heights.GetLength(1); j++) {
				heights [i, j] = 0;
			}
		}
	}

	void makeRamp(float[,] heights) {
		for(int i = 0; i < heights.GetLength(0); i++) {
			for(int j = 0; j < heights.GetLength(1); j++) {
				heights [i, j] = i / (float)heights.GetLength(0);
			}
		}
	}

	void makePerlin(float[,] heights, float resolution) {
		float rand = Random.Range (0, 100000);
		for(int i = 0; i < heights.GetLength(0); i++) {
			for(int j = 0; j < heights.GetLength(1); j++) {
				heights [i, j] = Mathf.PerlinNoise (i/resolution + rand, j/resolution + rand);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
