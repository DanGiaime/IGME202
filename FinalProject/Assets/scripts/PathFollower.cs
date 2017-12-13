using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Agent {

    public PathFollowingManager pathManager;
    public Transform[] path;
    public Transform currentTarget;
    public int currentTargetInt;

    public override void CalcSteeringForces()
    {
        if (currentTarget != null)
        {
            ultForce += Seek(currentTarget.position, false);
        }
    }

    // Use this for initialization
    public override void Start () {
        path = pathManager.path;
        currentTarget = path[1];
        currentTargetInt = 1;
        base.Start();
	}
	
	// Update is called once per frame
    public override void Update () {
        CalcSteeringForces();

        // Swap targets when we get close, mod so we go back to start of list
        if(currentTarget != null && Vector3.Distance(this.position, currentTarget.position) < radiusOfCaring) {
            currentTargetInt = (currentTargetInt + 1) % path.Length;
            currentTarget = path[currentTargetInt];
        }
        base.Update();
        ultForce = Vector3.zero;
	}
}
