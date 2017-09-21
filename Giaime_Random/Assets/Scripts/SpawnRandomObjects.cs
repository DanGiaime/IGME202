using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomObjects : MonoBehaviour {

	public GameObject prefab;
	public Terrain terrain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawn() {
		for (int i = 0; i < 40; i++) {
			float xPos = Random.Range (0, 50);
			float yPos = Random.Range (0, 50);
			Vector3 position = new Vector3 (xPos, 50, yPos);
			position.y = terrain.SampleHeight (position);
			position.y += prefab.transform.localScale.y / 2;
			Instantiate (prefab, position, Quaternion.identity);
		}

		spawnGaussian ();

	}

	public void spawnGaussian() {
		for (int i = 0; i < 20; i++) {
			float xzscale = 10 * Gaussian (1, 2, Random.Range(0,50)) + 1;
			float yscale = 10 * Gaussian (2, 3, Random.Range(0,50)) + 1;
			Vector3 scale = new Vector3 (xzscale, yscale, xzscale);
			Vector3 position = new Vector3(50 + 3*i, 0, 50);
			float y = terrain.SampleHeight (position);
			GameObject go = Instantiate (prefab, new Vector3(50 + 3*i, y, 50), Quaternion.identity);
			go.transform.localScale = scale;
			go.transform.Translate(0, prefab.transform.localScale.y / 2, 0);

		}
	}

	public float Gaussian(float mean, float variance, float input) {
		float mult = 1 / (variance * Mathf.Sqrt (2 * Mathf.PI));
		float exp = -Mathf.Pow ((input - mean), 2);
		exp /= (2 * Mathf.Pow(variance, 2));
		float answer = mult * Mathf.Exp(exp);
		return answer;
	}
}
