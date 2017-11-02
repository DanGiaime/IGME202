using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : Vehicle {

	public float seekWeight = 2f;
	public float fleeWeight = 10f;

	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    public Vector3 Seek(GameObject target)
    {
        Vector3 desiredVelocity = target.transform.position - this.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 seekForce = desiredVelocity - this.velocity;
        return seekWeight * seekForce;
    }

    public Vector3 Flee(GameObject target)
    {
        Vector3 desiredVelocity = this.position - target.transform.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 fleeForce = desiredVelocity - this.velocity;
        return fleeWeight * fleeForce;
    }

	public abstract void CalcSteeringForces();
}
