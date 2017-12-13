using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent : Vehicle {
    
    public float maxForce = 10f;
    public float wanderRadius = 2f;
    public Material lineMaterial;

    /// <summary>
    /// When to stop seeking something
    /// </summary>
    public float radiusOfCaring;

	public World world;
	protected Vector3 ultForce;

    Animator anim;

	// Use this for initialization
	public override void Start () {
		ultForce = Vector3.zero;
        anim = GetComponent<Animator>();

        base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
        //Debug.Log(ultForce);
        if (ultForce.magnitude < .1f)
        {
            //Debug.Log("wander");
            ultForce += Wander();
        }

        // Apply forces
        Vector3 final = ultForce.normalized * maxForce;
        ApplyForce(final);
        base.Update();
	}

    public Vector3 FuturePosition {
        get {
            return this.position + this.velocity;
        }
    }

    /// <summary>
    /// Seek the specified target and closerIsStronger.
    /// </summary>
    /// <returns>The seek force.</returns>
    /// <param name="target">Target.</param>
    /// <param name="closerIsStronger">If set to <c>true</c> closer is stronger.</param>
    public Vector3 Seek(Vector3 target, bool closerIsStronger)
    {
        Vector3 desiredVelocity = target - this.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 seekForce = desiredVelocity - this.velocity;
        if(closerIsStronger){
            return ForceWeight(target) * seekForce;
        }
        else {
            return InverseForceWeight(target) * seekForce;
        }
    }

    /// <summary>
    /// Seek the specified target.
    /// </summary>
    /// <returns>The seek force.</returns>
    /// <param name="target">Target.</param>
    public Vector3 Seek(Vector3 target)
    {
        Vector3 desiredVelocity = target - this.position;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 seekForce = desiredVelocity - this.velocity;
        return ForceWeight(target) * seekForce;
    }

    /// <summary>
    /// Flee the specified target.
    /// </summary>
    /// <returns>The flee force.</returns>
    /// <param name="target">Target.</param>
    public Vector3 Flee(Vector3 target)
    {
        Vector3 desiredVelocity = this.position - target;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 fleeForce = desiredVelocity - this.velocity;
        return ForceWeight(target) * fleeForce;
    }

    /// <summary>
    /// Flee the specified target.
    /// </summary>
    /// <returns>The flee force.</returns>
    /// <param name="target">Target.</param>
    public Vector3 Flee(Vector3 target, bool closerIsStronger)
    {
        Vector3 desiredVelocity = this.position - target;
        desiredVelocity = desiredVelocity.normalized * this.maxSpeed;
        Vector3 fleeForce = desiredVelocity - this.velocity;
        if (closerIsStronger)
        {
            return ForceWeight(target) * fleeForce;
        }
        else
        {
            return InverseForceWeight(target) * fleeForce;
        }
    }

    /// <summary>
    /// Pursue the specified agent.
    /// </summary>
    /// <returns>The pursue target.</returns>
    /// <param name="agent">Agent.</param>
    public Vector3 Pursue(Agent agent)
    {
        Vector3 agentFuturePosition = agent.position + agent.velocity;
        return Seek(agentFuturePosition);
    }

    /// <summary>
    /// Evade the specified agent.
    /// </summary>
    /// <returns>The evade target.</returns>
    /// <param name="agent">Agent.</param>
    public Vector3 Evade(Agent agent)
    {
        Vector3 agentFuturePosition = agent.position + agent.velocity;
        return Flee(agentFuturePosition);
    }

    /// <summary>
    /// Flock the specified group of agents.
    /// </summary>
    /// <returns>The flocking force.</returns>
    /// <param name="id">Identifier to determine what list of agents to use.</param>
    public Vector3 Flock(int id) {
        List<Agent> agents = world.GetAgents(id);
        Vector3 cohesionForce = Cohesion(agents);
        if (cohesionForce.magnitude > 0f)
        {
            Vector3 separationForce = Separation(agents);
            Vector3 alignmentForce = Alignment(agents);

            return cohesionForce + separationForce + alignmentForce;
        } else {
            return Vector3.zero;
        }
    }

    /// <summary>
    /// Align with the specified agents.
    /// </summary>
    /// <returns>The alignment force.</returns>
    /// <param name="agents">Agents to align with.</param>
    public Vector3 Alignment(List<Agent> agents)
    {
        Vector3 alignmentForce = Vector3.zero;

        foreach (Agent a in agents)
        {
            if (this.GetComponent<Agent>() != a)
            {
                alignmentForce += a.velocity * InverseForceWeight(a.position);
            }
        }
        return alignmentForce / agents.Count;
    }

    /// <summary>
    /// Separate from the specified agents.
    /// </summary>
    /// <returns>The separation force.</returns>
    /// <param name="agents">Agents to separate from.</param>
    public Vector3 Separation(List<Agent> agents)
    {
        Vector3 separationForce = Vector3.zero;
        foreach (Agent a in agents)
        {
            if (this.GetComponent<Agent>() != a)
            {
                if (Vector2.Distance(a.position, this.position) < radiusOfCaring)
                {
                    separationForce += AvoidObstacle(a.position);
                }
            }
        }
        return separationForce;
    }

    /// <summary>
    /// Seek the center of the specified agents.
    /// </summary>
    /// <returns>The cohesion force.</returns>
    /// <param name="agents">Agents to seek center of.</param>
    public Vector3 Cohesion(List<Agent> agents) {
        Vector3 center = Vector3.zero;
        int count = 0;

        foreach (Agent a in agents)
        {
            if (this.GetComponent<Agent>() != a/* && dotForward > 0*/)
            {
                center += a.position;
                count++;
            }

        }
        center = center / count;
        //Vector2 objCenter = center - this.position;
        //float dotForward = Vector2.Dot(rotation.forward, objCenter);

        if (true)
        {
            Vector3 cohesionForce = Seek(center, false);
            return cohesionForce;
        }
        else {
           return Vector3.zero;
        }
    }

    /// <summary>
    /// Avoids the given obstacle.
    /// </summary>
    /// <returns>Avoid Force.</returns>
    /// <param name="obstaclePosition">Obstacle position.</param>
    public Vector3 AvoidObstacle(Vector3 obstaclePosition) 
	{
		float distToObj = Vector2.Distance (this.transform.position, obstaclePosition);
		Vector2 objCenter = obstaclePosition - this.transform.position;


        float dotForward = Vector2.Dot (transform.forward, objCenter);

		//Is the object in front of us? If not, no reason to care.
		if (dotForward < 0)
			return Vector3.zero;

		//Vector3 objProjected = Vector3.Project (objCenter, this.transform.right);
        float dotRight = Vector2.Dot (transform.right, objCenter);

		//Is the object to our right? turn Left!
		if (dotRight > 0) {
            return (transform.right) * -1 * ForceWeight(obstaclePosition);// * obstacle.Weight;
		} 

		//Is the object to our left? turn Right!
		else if (dotRight < 0) {
            return transform.right * ForceWeight(obstaclePosition);// * obstacle.Weight;
		} 

		//It's...right in front of us? ... RIGHTTT
		else {
            return transform.right * ForceWeight(obstaclePosition);// * obstacle.Weight;
		}

	}

    /// <summary>
    /// Wanders around randomly
    /// </summary>
    /// <returns>The wander force.</returns>
    public Vector3 Wander()
    {
        // create a desired velocity vector
        Vector3 desiredVelocity = Vector3.zero;

        Vector3 forward = (this.velocity == Vector3.zero) ? transform.right : this.velocity;

        // get the center of the "circle"
        Vector3 circleCenter = forward.normalized + this.position;

        // negate the forward vector and scale it to radius
        Vector3 displacement = -forward;
        displacement = displacement.normalized * wanderRadius;

        // rotate that same vector randomly
        displacement = Quaternion.Euler(0, 0, Random.Range(-60f, 60f)) * displacement;

        // Find desired velocity by adding out velocity to the displacement
        desiredVelocity = velocity + displacement;
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;

        return desiredVelocity;
    }
    

    /// <summary>
    /// Weighting where closer target = weaker force
    /// </summary>
    /// <returns>The force weight.</returns>
    /// <param name="target">Target.</param>
    public float ForceWeight(Vector3 target) {
        return 1 / Mathf.Pow(Vector2.Distance(this.position, target), 2);
    }

    /// <summary>
    /// Weighting where closer target = stronger force
    /// </summary>
    /// <returns>The force weight.</returns>
    /// <param name="target">Target.</param>
    public float InverseForceWeight(Vector3 target)
    {
        return Vector2.Distance(this.position, target);
    }

	public abstract void CalcSteeringForces();
}
