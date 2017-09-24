using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHorde : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnHorde(GameObject prefab, Terrain terrain) {
		for (int i = 0; i < 80; i++) {
			float prob = Random.Range (0, 1.0f);
			float xPos = 0;
			float zPos = 0;
			if (prob < .1) {
				xPos = Random.Range (150, 170f);
				zPos = Random.Range (150, 165f);
			} else if (prob < .3) {
				xPos = Random.Range (170, 185f);
				zPos = Random.Range (150, 165f);
			} else {
				xPos = Random.Range (185, 195f);
				zPos = Random.Range (150, 165f);
			}
			Vector3 position = new Vector3 (xPos, 50, zPos);
			position.y = terrain.SampleHeight (position);
			position.y += prefab.transform.localScale.y / 2;
			Instantiate (prefab, position, Quaternion.identity);
		}
	}
}
