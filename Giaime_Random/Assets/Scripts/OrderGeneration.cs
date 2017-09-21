using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGeneration : MonoBehaviour {

	private SpawnRandomObjects spawnerScript;
	private TerrainGeneration terrainGenerator;

	// Use this for initialization
	void Start () {
		spawnerScript = this.GetComponent<SpawnRandomObjects> ();
		terrainGenerator = this.GetComponent<TerrainGeneration> ();

		terrainGenerator.generate ();
		spawnerScript.Spawn ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
