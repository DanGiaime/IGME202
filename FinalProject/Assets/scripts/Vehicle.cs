using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

	protected Vector3 acceleration;
	public Vector3 velocity;
    public Vector3 position;
    public Transform rotation;
    protected float defaultMaxSpeed;
    public float maxSpeed = 1f;
	private float mass = 2f;
    public float radius = 0.5f;

    // Use this for initialization
    public virtual void Start () {
        this.defaultMaxSpeed = maxSpeed;
		this.acceleration = Vector3.zero;
		this.velocity = Vector3.zero;
		this.position = transform.position;

		float vertExtent = Camera.main.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;

	}

    // Update is called once per frame
    public virtual void Update () {
		velocity += acceleration * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
		position += velocity * Time.deltaTime;
        transform.position = new Vector3(position.x, transform.position.y, position.z);
		acceleration = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, Mathf.Rad2Deg * Mathf.Atan2(velocity.x, velocity.z), 0);
	}

	public void ApplyForce(Vector3 force) {
		acceleration += force / mass;
	}

	public float Mass {
		get { return mass;}
		set { mass = value;}
	}

    private void OnRenderObject()
    {
        LineManager.DrawLine(transform.position, transform.position + this.velocity);
    }

}
