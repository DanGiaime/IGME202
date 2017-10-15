using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public Vector3 velocity;
	public float maxVel = 20f;
	public GameObject smallerPrefab = null;

	// Use this for initialization
	void Start () {
		velocity = new Vector3 (Random.Range(-maxVel, maxVel), Random.Range(-maxVel, maxVel), 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
	}

	public List<GameObject> Split() {
		if (smallerPrefab != null) {
			List<GameObject> smallerAsteroids = new List<GameObject> ();
			smallerAsteroids.Add (Instantiate (smallerPrefab, transform.position, Quaternion.identity));
			smallerAsteroids.Add (Instantiate (smallerPrefab, transform.position, Quaternion.identity));
			Destroy (gameObject);
			return smallerAsteroids;
		} else {
			Destroy (gameObject);
			return null;
		}
	}

}
