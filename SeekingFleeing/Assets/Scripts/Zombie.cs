using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Agent {

	public GameObject humanTarget;

	// Use this for initialization
	void Start () {
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
		float minDist = Vector3.Distance (this.transform.position, world.humans [0].transform.position);
		GameObject closestHuman = world.humans [0];

		foreach (GameObject human in world.humans) {
			float newDist = Vector3.Distance (this.transform.position, human.transform.position);
			if (newDist < minDist) {
				minDist = newDist;
				humanTarget = human;
			}
		}

		ultForce += Seek (humanTarget);

		foreach (GameObject obj in world.objects) {
			Vector3 avoidForce = AvoidObstacle(obj);
			ultForce += avoidForce;
		}

	}

}
