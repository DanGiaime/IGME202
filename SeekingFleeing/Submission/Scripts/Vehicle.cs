using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

	Vector2 acceleration;
	public Vector2 velocity;
	Vector2 position;
	private float mass = 2f;
	public bool friction = true;
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;

	// Use this for initialization
	void Start () {
		this.acceleration = Vector2.zero;
		this.velocity = Vector2.zero;
		this.position = transform.position;

		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;

		// Calculations assume map is position at the origin
		minX = Camera.main.transform.position.x - horzExtent;
		maxX = Camera.main.transform.position.x + horzExtent;
		minY = Camera.main.transform.position.x - vertExtent;
		maxY = Camera.main.transform.position.x + vertExtent;

	}
	
	// Update is called once per frame
	void Update () {
		velocity += acceleration;
		position += velocity * Time.deltaTime;
		transform.position = position;
		acceleration = Vector2.zero;
	}

	public void ApplyForce(Vector2 force) {
		acceleration += force / mass;
	}

	public void ApplyFriction() {
		acceleration += -1 * velocity * .01f;
	}

	public void ApplyMouseForce() {
		Vector3 mousePosition = Input.mousePosition + new Vector3(0, 0, 10);
		mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
		Vector2 mouseForce = ((Vector3)this.position - mousePosition)/6;
		Debug.Log (this.position);
		Debug.Log (mousePosition/300);
		ApplyForce(mouseForce);
	}

	public void Bounce() {
		if (position.x < minX || position.x > maxX) {
			velocity.x *= -1;
		}
		if (position.y < minY || position.y > maxY) {
			velocity.y *= -1;
		}
	}

	public float Mass {
		get { return mass;}
		set { mass = value;}
	}
}
