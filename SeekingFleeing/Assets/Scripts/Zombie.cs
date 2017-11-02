using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Agent {

	public GameObject humanTarget;

	// Use this for initialization
	void Start () {
		this.maxSpeed = 5f;
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForces ();
		base.Update ();
	}

	public override void CalcSteeringForces() {
		Vector3 ultForce = Seek (humanTarget);
		this.ApplyForce (ultForce.normalized * 10f);
	}

}
