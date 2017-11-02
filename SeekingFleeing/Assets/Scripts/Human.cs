using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Agent {

	public GameObject zombieClosest;
	public GameObject PSG;

	// Use this for initialization
	void Start () {
		this.maxSpeed = 10f;
		this.seekWeight = 10f;
		this.fleeWeight = 12f;
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForces ();
		base.Update ();
	}

	public override void CalcSteeringForces() {
		Vector3 ultForce = Flee (zombieClosest);
		ultForce += Seek (PSG);
		this.ApplyForce (ultForce.normalized * 10f);
	}
}
