using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Agent {

    public GameObject pathFollower;
    public static float waterLevel = 4.8f;

    [HideInInspector] private static int id = 3;

    // Update is called once per frame
    public override void Update()
    {
        this.pathFollower = gameObject.GetComponentInParent<FlockerManager>().pathFollower;
        CalcSteeringForces();
        base.Update();
        ultForce = Vector3.zero;
    }

	public override void CalcSteeringForces() {
        if (world != null)
        {
            if (world.IsInBounds(position))
            {

                // Flock
                ultForce += Flock(Flocker.id);
                ultForce += Flee(pathFollower.transform.position, true);

                // Slow down in water
                if(transform.position.y < waterLevel) {
                    maxSpeed = defaultMaxSpeed * 0.2f;
                }
                else {
                    maxSpeed = defaultMaxSpeed;
                }
            }
            else {
                ultForce += Seek(world.center, false);
            }

        }
	}

}
