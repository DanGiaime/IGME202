using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Agent {

	public GameObject zombieClosest;
	public GameObject PSG;

	// Use this for initialization
	void Start () {
		this.zombieClosest = world.zombies [0];
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		CalcSteeringForces ();
		this.ApplyForce (ultForce.normalized * 10f);
		base.Update ();
		ultForce = Vector3.zero;
	}

	public override void CalcSteeringForces() {
		//Find closest zombies
		float minDist = Vector3.Distance (this.transform.position, zombieClosest.transform.position);
		foreach (GameObject zombie in world.zombies) {
			float newDist = Vector3.Distance (this.transform.position, zombie.transform.position);
			if (newDist < minDist) {
				minDist = newDist;
				zombieClosest = zombie;
			}
		}

		//Flee and seek
		ultForce += Flee (zombieClosest);
		ultForce += Seek (PSG);

		//avoid
		foreach (GameObject obj in world.objects) {
			Vector3 avoidForce = AvoidObstacle(obj);
			ultForce += avoidForce;
		}

	}
}
