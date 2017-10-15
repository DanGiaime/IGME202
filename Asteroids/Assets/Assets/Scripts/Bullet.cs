using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Vector3 velocity;

	// Use this for initialization
	void Start () {
		velocity = gameObject.GetComponentInParent<Vehicle>().velocity;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (velocity * Time.deltaTime);
	}
}
