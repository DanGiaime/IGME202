using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : Vehicle {

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    public void Seek(GameObject target)
    {
        Vector2 desiredVelocity = (Vector2)target.transform.position - this.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector2 seekForce = desiredVelocity - this.velocity;
        this.ApplyForce(seekForce);
    }

    public void Flee(GameObject target)
    {
        Vector2 desiredVelocity = this.position - (Vector2)target.transform.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector2 fleeForce = desiredVelocity - this.velocity;
        this.ApplyForce(fleeForce);
    }
}
