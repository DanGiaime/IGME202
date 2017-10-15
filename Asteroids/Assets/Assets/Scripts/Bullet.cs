using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Vector3 velocity;
	public float rotation;
	private float speed = 10.0f;

	// Use this for initialization
	void Start () {
		velocity = (transform.up) * speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
	}

}
