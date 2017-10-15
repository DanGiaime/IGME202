using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	public Vector3 velocity;
	public float maxVel = 20f;

	// Use this for initialization
	void Start () {
		velocity = new Vector3 (Random.Range(-maxVel, maxVel), Random.Range(-maxVel, maxVel), 0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (velocity * Time.deltaTime);
	}
}
