using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forces : MonoBehaviour {

	Vehicle[] children;
	public GameObject smallPrefab;
	public GameObject mediumPrefab;
	public GameObject largePrefab;
	const float MAX_X = 5f;
	const float MIN_X = -5f;
	const float MAX_Y = 5f;
	const float MIN_Y = -5f;
	bool applyFriction = false;

	// Use this for initialization
	void Start () {
		GameObject small = Instantiate (smallPrefab, randomPosition(), Quaternion.identity, transform);
		small.GetComponentInChildren<Vehicle>().Mass = 1f;
		small.transform.localScale = new Vector3(1, 1, 1);
		GameObject medium = Instantiate (mediumPrefab, randomPosition(), Quaternion.identity, transform);
		medium.GetComponentInChildren<Vehicle>().Mass = 5f;
		small.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
		GameObject large = Instantiate (largePrefab, randomPosition(), Quaternion.identity, transform);
		large.GetComponentInChildren<Vehicle>().Mass = 20f;
		small.transform.localScale = new Vector3(2, 2, 2);

		children = this.GetComponentsInChildren<Vehicle> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			applyFriction = !applyFriction;
			Debug.Log("Friction: " + applyFriction);
		}
		foreach (Vehicle v in children) {
			v.Bounce ();
			if (applyFriction) {
				v.ApplyFriction ();
			}
			if (Input.GetMouseButton (0)) {
				v.ApplyMouseForce ();
			}
		}
	}

	Vector2 randomPosition() {
		return new Vector2 (Random.Range(MIN_X, MAX_X), Random.Range(MIN_Y, MAX_Y));
	}
}
