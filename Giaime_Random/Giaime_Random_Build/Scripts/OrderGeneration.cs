using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGeneration : MonoBehaviour {

	public GameObject prefab;
	public Terrain terrain;

	private SpawnRandomObjects spawnerScript;
	private TerrainGeneration terrainGenerator;
	private SpawnHorde hordeScript;
	private SpawnGaussian gaussScript;

	// Use this for initialization
	void Start () {
		spawnerScript = this.GetComponent<SpawnRandomObjects> ();
		terrainGenerator = this.GetComponent<TerrainGeneration> ();
		hordeScript = this.GetComponent<SpawnHorde> ();
		gaussScript = this.GetComponent<SpawnGaussian> ();

		terrainGenerator.generate (terrain.GetComponent<TerrainCollider>());
		spawnerScript.Spawn (prefab, terrain);
		hordeScript.spawnHorde (prefab, terrain);
		gaussScript.spawnGaussian (prefab, terrain);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
