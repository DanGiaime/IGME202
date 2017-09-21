using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawn(GameObject prefab, Terrain terrain) {
		for (int i = 0; i < 40; i++) {
			float xPos = Random.Range (0, 200);
			float yPos = Random.Range (0, 200);
			Vector3 position = new Vector3 (xPos, 50, yPos);
			position.y = terrain.SampleHeight (position);
			position.y += prefab.transform.localScale.y / 2;
			Instantiate (prefab, position, Quaternion.identity);
		}
	}




}
