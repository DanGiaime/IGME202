using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : Vehicle {

	public float seekWeight = 2f;
	public float fleeWeight = 10f;
	public float avoidWeight = 100f;
	public float maxForce = 10f;
	public World world;
	protected Vector3 ultForce;

	// Use this for initialization
	void Start () {
		ultForce = Vector3.zero;
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

	public Vector3 AvoidObstacle(GameObject obstacle) 
	{
		float distToObj = Vector3.Distance (this.transform.position, obstacle.transform.position);
		Vector3 objCenter = obstacle.transform.position - this.transform.position;


		float dotForward = Vector3.Dot (this.transform.forward, objCenter);

		//Is the object in front of us? If not, no reason to care.
		if (dotForward < 0)
			return Vector3.zero;

		//Vector3 objProjected = Vector3.Project (objCenter, this.transform.right);
		float dotRight = Vector3.Dot (this.transform.right, objCenter);

		//Is the object close to us? If not, no reason to care.
		if (distToObj < maxSpeed) {

			//Is the object to our right? turn Left!
			if (dotRight > 0) {
				return this.transform.right * -1 * avoidWeight;
			} 

			//Is the object to our left? turn Right!
			else if (dotRight < 0) {
				return this.transform.right * avoidWeight;
			} 

			//It's...right in front of us? ... RIGHTTT
			else {
				return this.transform.right * avoidWeight;
			}
		} else {
			return Vector3.zero;
		}

	}



	public abstract void CalcSteeringForces();
}
