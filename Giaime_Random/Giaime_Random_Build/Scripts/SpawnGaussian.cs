using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGaussian : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void spawnGaussian(GameObject prefab, Terrain terrain) {
		for (int i = 0; i < 20; i++) {
			float xzscale = 10 * Gaussian (1, 2, Random.Range(-3f,3f)) + 1;
			float yscale = 10 * Gaussian (2, 3, Random.Range(-5f,5f)) + 1;
			Vector3 scale = new Vector3 (xzscale, yscale, xzscale);
			Vector3 position = new Vector3(50 + 3*i, 0, 50);
			float y = terrain.SampleHeight (position);
			GameObject go = Instantiate (prefab, new Vector3(50 + 3*i, y, 50), Quaternion.identity);
			go.transform.localScale = scale;
			go.transform.Translate(0, prefab.transform.localScale.y / 2, 0);

		}
	}

	private float Gaussian(float mean, float variance, float input) {
		float mult = 1 / (variance * Mathf.Sqrt (2 * Mathf.PI));
		float exp = -Mathf.Pow ((input - mean), 2);
		exp /= (2 * Mathf.Pow(variance, 2));
		float answer = mult * Mathf.Exp(exp);
		return answer;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
