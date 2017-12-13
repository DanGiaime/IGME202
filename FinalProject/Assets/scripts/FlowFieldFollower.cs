using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFieldFollower : Agent {

    public FlowField field;
    public int size = 20;
    public Vector3 flowFieldBase;

    public override void CalcSteeringForces()
    {
        // Find where we are in the flow field
        int xIndex = (int)((transform.position.x - flowFieldBase.x) / (field.width / size));
        int zIndex = (int)((transform.position.z - flowFieldBase.z) / (field.height / size));

        // Check if we're going out of the flow field
        bool goingOffScreen = this.FuturePosition.x > (field.width - flowFieldBase.x)
                                  || this.FuturePosition.x < flowFieldBase.x
                                  || this.FuturePosition.z < flowFieldBase.z
                                  || this.FuturePosition.z > (field.height - flowFieldBase.z);

        // If we're not going off screen 
        Vector3 desiredVelocity = goingOffScreen ? -velocity : field.flowField[xIndex, zIndex];
        ultForce += desiredVelocity.normalized;
    }

    // Use this for initialization
    public override void Start () {
        field = transform.parent.GetComponent<FlowField>();
        flowFieldBase = transform.parent.position;
        this.size = field.size;
        base.Start();
    }
	
	// Update is called once per frame
	public override void Update () {
        CalcSteeringForces();
        base.Update();
        ultForce = Vector3.zero;
	}
}
